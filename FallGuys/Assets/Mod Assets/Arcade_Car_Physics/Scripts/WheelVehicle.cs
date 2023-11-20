﻿/*
 * This code is part of Arcade Car Physics for Unity by Saarg (2018)
 * 
 * This is distributed under the MIT Licence (see LICENSE.md for details)
 */

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

#if MULTIOSCONTROLS
    using MOSC;
#endif

[assembly: InternalsVisibleTo("VehicleBehaviour.Dots")]
namespace VehicleBehaviour {
    [RequireComponent(typeof(Rigidbody))]
    public class WheelVehicle : MonoBehaviour {
        
        [Header("Inputs")]
    #if MULTIOSCONTROLS
        [SerializeField] PlayerNumber playerId;
    #endif

        // If isPlayer is false inputs are ignored
         bool isPlayer = true;
        public bool IsPlayer { get => isPlayer;
            set => isPlayer = value;
        } 

        // Input names to read using GetAxis
        [SerializeField] internal VehicleInputs m_Inputs;
        string throttleInput => m_Inputs.ThrottleInput;
        string brakeInput => m_Inputs.BrakeInput;
        string turnInput => m_Inputs.TurnInput;
        string jumpInput => m_Inputs.JumpInput;
        string driftInput => m_Inputs.DriftInput;
	    string boostInput => m_Inputs.BoostInput;
        
        /* 
         *  Turn input curve: x real input, y value used
         *  My advice (-1, -1) tangent x, (0, 0) tangent 0 and (1, 1) tangent x
         */
        [SerializeField] AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

        [Header("Wheels")]
        [SerializeField] WheelCollider[] driveWheel = new WheelCollider[0];
        public WheelCollider[] DriveWheel => driveWheel;
        [SerializeField] WheelCollider[] turnWheel = new WheelCollider[0];

        public WheelCollider[] TurnWheel => turnWheel;

        // This code checks if the car is grounded only when needed and the data is old enough
        bool isGrounded = false;
        int lastGroundCheck = 0;
        public bool IsGrounded { get {
            if (lastGroundCheck == Time.frameCount)
                return isGrounded;

            lastGroundCheck = Time.frameCount;
            isGrounded = true;
            foreach (WheelCollider wheel in wheels)
            {
                if (!wheel.gameObject.activeSelf || !wheel.isGrounded)
                    isGrounded = false;
            }
            return isGrounded;
        }}

        [Header("Behaviour")]
        /*
         *  Motor torque represent the torque sent to the wheels by the motor with x: speed in km/h and y: torque
         *  The curve should start at x=0 and y>0 and should end with x>topspeed and y<0
         *  The higher the torque the faster it accelerate
         *  the longer the curve the faster it gets
         */
        [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

        // Differential gearing ratio
        [Range(2, 16)]
        [SerializeField] float diffGearing = 4.0f;
        public float DiffGearing { get => diffGearing;
            set => diffGearing = value;
        }

        // Basicaly how hard it brakes
        [SerializeField] float brakeForce = 1500.0f;
        public float BrakeForce { get => brakeForce;
            set => brakeForce = value;
        }

        // Max steering hangle, usualy higher for drift car
        [Range(0f, 50.0f)]
        [SerializeField] float steerAngle = 30.0f;
        public float SteerAngle { get => steerAngle;
            set => steerAngle = Mathf.Clamp(value, 0.0f, 50.0f);
        }

        // The value used in the steering Lerp, 1 is instant (Strong power steering), and 0 is not turning at all
        [Range(0.001f, 1.0f)]
        [SerializeField] float steerSpeed = 0.2f;
        public float SteerSpeed { get => steerSpeed;
            set => steerSpeed = Mathf.Clamp(value, 0.001f, 1.0f);
        }

        // How hight do you want to jump?
        [Range(1f, 1.5f)]
        [SerializeField] float jumpVel = 1.3f;
        public float JumpVel { get => jumpVel;
            set => jumpVel = Mathf.Clamp(value, 1.0f, 1.5f);
        }

        // How hard do you want to drift?
        [Range(0.0f, 2f)]
        [SerializeField] float driftIntensity = 1f;
        public float DriftIntensity { get => driftIntensity;
            set => driftIntensity = Mathf.Clamp(value, 0.0f, 2.0f);
        }

        // Reset Values
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        /*
         *  The center of mass is set at the start and changes the car behavior A LOT
         *  I recomment having it between the center of the wheels and the bottom of the car's body
         *  Move it a bit to the from or bottom according to where the engine is
         */
        [SerializeField] Transform centerOfMass = null;

        // Force aplied downwards on the car, proportional to the car speed
        [Range(0.5f, 10f)]
        [SerializeField] float downforce = 1.0f;

        public float Downforce
        {
            get => downforce;
            set => downforce = Mathf.Clamp(value, 0, 5);
        }     

        // When IsPlayer is false you can use this to control the steering
        [SerializeField]float steering;
        public float Steering { get => steering;
            set => steering = Mathf.Clamp(value, -1f, 1f);
        } 

        // When IsPlayer is false you can use this to control the throttle
        [SerializeField]float throttle;
        public float Throttle { get => throttle;
            set => throttle = Mathf.Clamp(value, -1f, 1f);
        } 

        // Like your own car handbrake, if it's true the car will not move
        [SerializeField] bool handbrake;
        public bool Handbrake { get => handbrake;
            set => handbrake = value;
        }
        
        // Use this to disable drifting
        [HideInInspector] public bool allowDrift = true;
        bool drift;
        public bool Drift { get => drift;
            set => drift = value;
        }         

        // Use this to read the current car speed (you'll need this to make a speedometer)
        [SerializeField] float speed = 0.0f;
        public float Speed => speed;

        private float defaultMaxSpeed;
        [SerializeField] internal float maxSpeed = 200f;
        public float MaxSpeed
        {
            get => maxSpeed;
            set => maxSpeed = value;
        }

        [Header("Particles")]
        // Exhaust fumes
        [SerializeField] ParticleSystem[] gasParticles = new ParticleSystem[0];

        [Header("Boost")]
        // Disable boost
        [HideInInspector] public bool allowBoost = true;

        // Maximum boost available
        [SerializeField] float maxBoost = 10f;
        public float MaxBoost { get => maxBoost;
            set => maxBoost = value;
        }

        // Current boost available
        [SerializeField] float boost = 10f;
        public float Boost { get => boost;
            set => boost = Mathf.Clamp(value, 0f, maxBoost);
        }

        // Regen boostRegen per second until it's back to maxBoost
        [Range(0f, 1f)]
        [SerializeField] float boostRegen = 0.2f;
        public float BoostRegen { get => boostRegen;
            set => boostRegen = Mathf.Clamp01(value);
        }

        /*
         *  The force applied to the car when boosting
         *  NOTE: the boost does not care if the car is grounded or not
         */
        [SerializeField] float boostForce = 5000;
        public float BoostForce { get => boostForce;
            set => boostForce = value;
        }

        // Use this to boost when IsPlayer is set to false
        public bool boosting = false;
        // Use this to jump when IsPlayer is set to false
        public bool jumping = false;

        // Boost particles and sound
       ParticleSystem[] boostParticles = new ParticleSystem[0];
       AudioClip boostClip = default;
        AudioSource boostSource = default;
        
        // Private variables set at the start
        Rigidbody rb = default;
        internal WheelCollider[] wheels = new WheelCollider[0];

        // Init rigidbody, center of mass, wheels and more

        // Для проверки, что игрок не перевернулся
        [Header("Stucking")]
        [SerializeField] private float stuckTimeThreshold = 2f;
        [Space]
        [SerializeField] private float stuckTime_RolledOntoRoofOfSide;
        [SerializeField] private float stuckTime_PlayerPressesOnGasButDoesntMove;
        [SerializeField] private float stuckTime_StuckInSomething;

        [SerializeField] private float stuckTime_BotAIPressesOnGasButDoesntMove;
        //[SerializeField]private bool aiIsTriedToPutItBack;
        //[SerializeField]private bool aiGiveBack;

        //[SerializeField]private int immobilityValue;// сколько игрок провел в неподвижном состоянии
        //[SerializeField] private int immobilityValue2;
        //[SerializeField] private int immobilityValue3;
        //[SerializeField] private int immobilityValueOnlyForBot;

        private bool isInverseTurnController;

        public static event Action<WheelVehicle> NotifyGetRespanwPositionForWheelVehicleEvent;

        void Start() {
#if MULTIOSCONTROLS
            Debug.Log("[ACP] Using MultiOSControls");
#endif
            if (boostClip != null) {
                boostSource.clip = boostClip;
            }

		    boost = maxBoost;

            rb = GetComponent<Rigidbody>();
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            if (rb != null && centerOfMass != null)
            {
                rb.centerOfMass = centerOfMass.localPosition;
            }

            wheels = GetComponentsInChildren<WheelCollider>();

            // Set the motor torque to a non null value because 0 means the wheels won't turn no matter what
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0001f;
            }

            defaultMaxSpeed = maxSpeed;

            ////AI
            //if (isBot && isPlayer) isPlayer = false;
        }

        // Visual feedbacks and boost regen
        void Update()
        {
            foreach (ParticleSystem gasParticle in gasParticles)
            {
                gasParticle.Play();
                ParticleSystem.EmissionModule em = gasParticle.emission;
                em.rateOverTime = handbrake ? 0 : Mathf.Lerp(em.rateOverTime.constant, Mathf.Clamp(150.0f * throttle, 30.0f, 100.0f), 0.1f);
            }

            if (isPlayer && allowBoost) {
                boost += Time.deltaTime * boostRegen;
                if (boost > maxBoost) { boost = maxBoost; }
            }
        }
        
        // Update everything
        void FixedUpdate () {
            // Mesure current speed
            speed = transform.InverseTransformDirection(rb.velocity).z * 3.6f;
            speed = Math.Clamp(speed, -maxSpeed, maxSpeed);

            // Get all the inputs!
            if (isPlayer)
            {
                // Accelerate & brake
                if (throttleInput != "" && throttleInput != null)
                {
                    throttle = GetInput(throttleInput) - GetInput(brakeInput);

                    //throttle = Mathf.Clamp(throttle, -0.7f, 0.7f);

                    if (throttle != 0 && Mathf.RoundToInt(speed) == 0 && Vector3.Dot(Vector3.up, transform.up) <= 0.98f)
                    {
                        //Debug.Log("Жмем на газ. Застрял");
                        //immobilityValue2++;
                        stuckTime_PlayerPressesOnGasButDoesntMove += Time.deltaTime;

                        if (stuckTime_PlayerPressesOnGasButDoesntMove >= stuckTimeThreshold)
                        {
                            stuckTime_RolledOntoRoofOfSide = 0f;
                            stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                            stuckTime_StuckInSomething = 0f;
                            RespanwAfterStuck();
                            //transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                            //transform.position = GetAppearPosition();
                        }
                    }
                    else if (throttle == 0)
                    {
                        //Debug.Log("Отпустили кнопку");
                    }
                    else
                    {
                        //Debug.Log("Жмем кнопку");
                        stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                    }
                }
                // Boost
                boosting = (GetInput(boostInput) > 0.5f);
                // Turn
                steering = turnInputCurve.Evaluate(GetInput(turnInput)) * steerAngle;
                if (isInverseTurnController) steering *= -1f;
                // Dirft
                drift = GetInput(driftInput) > 0 && rb.velocity.sqrMagnitude > 100;
                // Jump
                jumping = GetInput(jumpInput) != 0;
            }
            else
            {
                steering = turnInputCurve.Evaluate(steering) * steerAngle;

                if (throttle != 0 && Mathf.RoundToInt(speed) == 0 && !handbrake)
                {
                    //if (aiGiveBack) throttle = -1f;

                    //Debug.Log("Газует бот, но застрял");
                    //immobilityValueOnlyForBot++;
                    stuckTime_BotAIPressesOnGasButDoesntMove += Time.deltaTime;

                    if (stuckTime_BotAIPressesOnGasButDoesntMove >= stuckTimeThreshold)
                    {
                        stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
                        RespanwAfterStuck();

                        //if (aiIsTriedToPutItBack)
                        //{
                        //    aiIsTriedToPutItBack = false;

                        //    stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
                        //    RespanwAfterStuck();
                        //}
                        //else
                        //{
                        //    aiIsTriedToPutItBack = true;

                        //    stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
                        //    TryAIToGiveBack();
                        //}

                        //transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                        //transform.position = GetAppearPosition();
                    }
                }
                else stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
            }

            // Direction
            foreach (WheelCollider wheel in turnWheel)
            {
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, steerSpeed);
            }

            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0001f;
                wheel.brakeTorque = 0;
            }

            //if (aiGiveBack) throttle = -1f;

            // Handbrake
            if (handbrake)
            {
                foreach (WheelCollider wheel in wheels)
                {
                    // Don't zero out this value or the wheel completly lock up
                    wheel.motorTorque = 0.0001f;
                    wheel.brakeTorque = brakeForce;
                }
            }
            else if (throttle != 0 && (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(throttle)))
            {
                foreach (WheelCollider wheel in driveWheel)
                {
                    wheel.motorTorque = throttle * motorTorque.Evaluate(speed) * diffGearing / driveWheel.Length;
                }
            }
            else if (throttle != 0)
            {
                foreach (WheelCollider wheel in wheels)
                {
                    wheel.brakeTorque = Mathf.Abs(throttle) * brakeForce;
                }
            }

            // Jump
            if (jumping /*&& isPlayer*/) {
                if (!IsGrounded)
                    return;
                
                rb.velocity += transform.up * jumpVel;
            }

            // Boost
            if (boosting && allowBoost && boost > 0.1f) {

                int value;
                if (throttle > 0) value = 1;
                else value = -1;

                rb.AddForce(transform.forward * boostForce * value);

                //boost -= Time.fixedDeltaTime;
                //if (boost < 0f) { boost = 0f; }

                //if (boostParticles.Length > 0 && !boostParticles[0].isPlaying) {
                //    foreach (ParticleSystem boostParticle in boostParticles) {
                //        boostParticle.Play();
                //    }
                //}

                //if (boostSource != null && !boostSource.isPlaying) {
                //    boostSource.Play();
                //}
            } 
            //else {
            //    if (boostParticles.Length > 0 && boostParticles[0].isPlaying) {
            //        foreach (ParticleSystem boostParticle in boostParticles) {
            //            boostParticle.Stop();
            //        }
            //    }

            //    if (boostSource != null && boostSource.isPlaying) {
            //        boostSource.Stop();
            //    }
            //}

            // Drift
            if (drift && allowDrift) {
                Vector3 driftForce = -transform.right;
                driftForce.y = 0.0f;
                driftForce.Normalize();

                if (steering != 0)
                    driftForce *= rb.mass * speed/7f * throttle * steering/steerAngle;
                Vector3 driftTorque = transform.up * 0.1f * steering/steerAngle;


                rb.AddForce(driftForce * driftIntensity, ForceMode.Force);
                rb.AddTorque(driftTorque * driftIntensity, ForceMode.VelocityChange);             
            }
            
            // Downforce
            rb.AddForce(-transform.up * speed * downforce);

            // Проверка, что игрок не упал на бок или крышу
            if (Vector3.Dot(Vector3.up, transform.up) < 0.15f)
            {
                //immobilityValue++;
                //if (immobilityValue >= 100)
                //{
                //    immobilityValue = 0;
                //    immobilityValue2 = 0;
                //    immobilityValue3 = 0;
                //    RespanwAfterStuck();
                //    //transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                //    //transform.position = GetAppearPosition();
                //}

                //Debug.Log("Перевернулся на крышу или на бок");

                stuckTime_RolledOntoRoofOfSide += Time.deltaTime;

                if (stuckTime_RolledOntoRoofOfSide >= stuckTimeThreshold)
                {
                    stuckTime_RolledOntoRoofOfSide = 0f;
                    stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                    stuckTime_StuckInSomething = 0f;
                    RespanwAfterStuck();
                    //transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                    //transform.position = GetAppearPosition();
                }

            }
            else stuckTime_RolledOntoRoofOfSide = 0f;

            if (Mathf.RoundToInt(speed) == 0 && Vector3.Dot(Vector3.up, transform.up) <= 0.98f)
            {
                //immobilityValue3++;
                //Debug.Log("Просто застрял");

                stuckTime_StuckInSomething += Time.deltaTime;

                if (stuckTime_StuckInSomething >= stuckTimeThreshold)
                {
                    stuckTime_RolledOntoRoofOfSide = 0f;
                    stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                    stuckTime_StuckInSomething = 0f;
                    RespanwAfterStuck();
                    //transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                    //transform.position = GetAppearPosition();
                }
            }
            else stuckTime_StuckInSomething = 0f;

            //if (Mathf.RoundToInt(speed) == 0)
            //{
            //    Debug.Log("STOIT");
            //}
        }

        // Reposition the car to the start position
        public void ResetPos() {
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void ToogleHandbrake(bool h)
        {
            handbrake = h;
        }

        public void ChangeTheTurnControllerToInverse()
        {
            isInverseTurnController = true;
        }
        public void ChangeTheTurnControllerToNormal()
        {
            isInverseTurnController = false;
        }

        // MULTIOSCONTROLS is another package I'm working on ignore it I don't know if it will get a release.
#if MULTIOSCONTROLS
        private static MultiOSControls _controls;
#endif

        // Use this method if you want to use your own input manager
        private float GetInput(string input) {
#if MULTIOSCONTROLS
        return MultiOSControls.GetValue(input, playerId);
#else
            //return Input.GetAxis(input);
            return SimpleInput.GetAxis(input);
#endif
        }

        private Vector3 GetAppearPosition()
        {
            Vector3 currentPos = transform.position;
            int random = UnityEngine.Random.Range(-5, 6);
            if (random == 0) random = 2;
            Vector3 newPos = new Vector3(currentPos.x + random, currentPos.y + 7f, currentPos.z + random);

            return newPos;
        }

        //private void TryAIToGiveBack()
        //{
        //    aiGiveBack = true;
        //    StartCoroutine(AIGiveBack());
        //}

        //IEnumerator AIGiveBack()
        //{
        //    yield return new WaitForSeconds(2f);

        //    aiGiveBack = false;
        //}

        public void PlayerStuckUnder()// from UderPlayerCar. Only on Player car
        {
            RespanwAfterStuck();
        }

        private void RespanwAfterStuck()
        {
            NotifyGetRespanwPositionForWheelVehicleEvent?.Invoke(this);
        }

        public void GetRespawnTransform(Transform respTransform)
        {
            transform.rotation = respTransform.rotation;
            transform.position = new Vector3(respTransform.position.x, 5f, respTransform.position.z);
        }
    }
}
