using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeVP
{
    public class ArcadeVehicleController : MonoBehaviour
    {
        public enum groundCheck { rayCast, sphereCaste };
        public enum MovementMode { Velocity, AngularVelocity };
        public MovementMode movementMode;
        public groundCheck GroundCheck;
        public LayerMask drivableSurface;

        public float Gravity = 7f;
        public float Downforce = 5f;

        public bool AirControl = false;
        public Rigidbody rb, carBody;

        private float defaultMaxSpeed;

        [HideInInspector]
        public RaycastHit hit;
        public AnimationCurve frictionCurve;
        public AnimationCurve turnCurve;
        public PhysicMaterial frictionMaterial;
        [Header("Visuals")]
        public Transform BodyMesh;
        public Transform[] FrontWheels = new Transform[2];
        public Transform[] RearWheels = new Transform[2];
        [HideInInspector]
        public Vector3 carVelocity;

        [Range(0, 10)]
        public float BodyTilt;
        [Header("Audio settings")]
        public AudioSource engineSound;
        [Range(0, 1)]
        public float minPitch;
        [Range(1, 3)]
        public float MaxPitch;
        public AudioSource SkidSound;

        [HideInInspector]
        public float skidWidth;


        private float radius, horizontalInput, verticalInput;
        private Vector3 origin;

        //

        [SerializeField]bool isPlayer = true;
        public bool IsPlayer
        {
            get => isPlayer;
            set => isPlayer = value;
        }

        [Header("Stucking")]
        [SerializeField] private float stuckTimeThreshold = 2f;
        [Space]
        [SerializeField] private float stuckTime_RolledOntoRoofOfSide;
        [SerializeField] private float stuckTime_PlayerPressesOnGasButDoesntMove;
        [SerializeField] private float stuckTime_StuckInSomething;

        [SerializeField] private float stuckTime_BotAIPressesOnGasButDoesntMove;
        [Space]
        [SerializeField] private float idleTime = 2f;
        private float playerIdleTime;

        private bool isShowingRestartButton;

        float steering;
        public float Steering
        {
            get => steering;
            set => steering = Mathf.Clamp(value, -1f, 1f);
        }
        
        float throttle;
        public float Throttle
        {
            get => throttle;
            set => throttle = Mathf.Clamp(value, -1f, 1f);
        }

        [SerializeField]bool handbrake;
        public bool Handbrake
        {
            get => handbrake;
            set => handbrake = value;
        }

        float speed = 0.0f;
        public float Speed => speed;

        private bool isInverseTurnController;

        [Header("Arcade Race AI")]
        [SerializeField] private bool isArcadeRaceAI;
        [Space]
        public Transform target;
        public Transform Target => target;

        //Ai stuff
        [HideInInspector]
        public float TurnAI = 1f;
        [HideInInspector]
        public float SpeedAI = 1f;
        [HideInInspector]
        public float brakeAI = 0f;
        public float brakeAngle = 30f;

        private float desiredTurning;

        private bool useSpeedControl;
        private float speedPercent;
        private CarPlayerWaipointTracker carPlayerWaipointTracker;

        private WaypointProgressTracker waypointProgressTracker;

        //
        private bool isObstacle;
        public bool IsObstacle
        {
            get => isObstacle;
            set => isObstacle = value;
        }

        private float obstacleSteer;
        public float ObstacleSteer
        {
            get => obstacleSteer;
            set => obstacleSteer = value;
        }

        // events
        public static event System.Action<ArcadeVehicleController, bool> GetRespanwPositionForArcadeVehicleControllerEvent;// проверить
        public static event System.Action HideRespawnButtonEvent;// проверить

        [Header("DEBUG")]
        public float MaxSpeed;
        public float Accelaration;
        public float Turn;

        private void Start()
        {
            radius = rb.GetComponent<SphereCollider>().radius;
            if (movementMode == MovementMode.AngularVelocity)
            {
                Physics.defaultMaxAngularSpeed = 100;
            }

            defaultMaxSpeed = MaxSpeed;
        }
        private void Update()
        {
            if (isPlayer)
            {
                horizontalInput = SimpleInput.GetAxis("CarHorizontal"); //turning input
                verticalInput = SimpleInput.GetAxis("CarVertical");     //accelaration input
            }
            else
            {
                if (isArcadeRaceAI)
                {
                    CalculatingRaceAITurnValue();
                }
                else
                {
                    horizontalInput = steering;
                    verticalInput = throttle;
                }

                if (useSpeedControl)
                {
                    if (waypointProgressTracker.ProgressDistance > carPlayerWaipointTracker.ProgressDistance)
                    {
                        MaxSpeed = defaultMaxSpeed * speedPercent;
                    }
                    else
                    {
                        MaxSpeed = defaultMaxSpeed;
                    }
                }
            }
            
            Visuals();
            AudioManager();

        }
        public void AudioManager()
        {
            engineSound.pitch = Mathf.Lerp(minPitch, MaxPitch, Mathf.Abs(carVelocity.z) / MaxSpeed);
            if (Mathf.Abs(carVelocity.x) > 10 && grounded())
            {
                SkidSound.mute = false;
            }
            else
            {
                SkidSound.mute = true;
            }
        }


        void FixedUpdate()
        {
            speed = carBody.velocity.magnitude;

            if (isInverseTurnController)
            {
                horizontalInput *= -1f;
                verticalInput *= -1f;
            }

            if (isArcadeRaceAI)
            {
                RaceAIMovement();
            }
            else
            {
                NotRaceAIMovement();
            }
        }

        private void NotRaceAIMovement()
        {
            carVelocity = carBody.transform.InverseTransformDirection(carBody.velocity);

            if (Mathf.Abs(carVelocity.x) > 0)
            {
                //changes friction according to sideways speed of car
                frictionMaterial.dynamicFriction = frictionCurve.Evaluate(Mathf.Abs(carVelocity.x / 100));
            }


            if (grounded())
            {
                #region Stuck Check
                if (isPlayer)
                {
                    if (isShowingRestartButton && Mathf.Abs(Mathf.RoundToInt(speed)) >= 5f)
                    {
                        HideRespawnButtonEvent?.Invoke();
                    }

                    if (verticalInput != 0 && (Mathf.Abs(Mathf.RoundToInt(speed)) <= 2f) && Vector3.Dot(Vector3.up, transform.up) <= 0.98f)
                    {
                        //Debug.Log("Жмем на газ. Застрял");

                        stuckTime_PlayerPressesOnGasButDoesntMove += Time.deltaTime;

                        if (stuckTime_PlayerPressesOnGasButDoesntMove >= stuckTimeThreshold)
                        {
                            stuckTime_RolledOntoRoofOfSide = 0f;
                            stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                            stuckTime_StuckInSomething = 0f;
                            RespanwAfterStuck();
                        }
                    }
                    else if (Mathf.Abs(Mathf.RoundToInt(speed)) <= 2f && !handbrake)
                    {
                        playerIdleTime += Time.deltaTime;

                        if (playerIdleTime >= idleTime)
                        {
                            playerIdleTime = 0;
                            RespanwAfterStuck();
                        }
                    }
                    else
                    {
                        //Debug.Log("Жмем кнопку");
                        stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                        playerIdleTime = 0f;
                    }
                }
                else
                {
                    if ((verticalInput != 0 || isArcadeRaceAI) && Mathf.RoundToInt(speed) == 0 && !handbrake)
                    {
                        //Debug.Log("Газует бот, но застрял");

                        stuckTime_BotAIPressesOnGasButDoesntMove += Time.deltaTime;

                        if (stuckTime_BotAIPressesOnGasButDoesntMove >= stuckTimeThreshold)
                        {
                            stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
                            RespanwAfterStuck();
                        }
                    }
                    else stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
                }

                // Проверка, что игрок не упал на бок или крышу
                if (Vector3.Dot(Vector3.up, transform.up) < 0.15f)
                {
                    //Debug.Log("Перевернулся на крышу или на бок");

                    stuckTime_RolledOntoRoofOfSide += Time.deltaTime;

                    if (stuckTime_RolledOntoRoofOfSide >= stuckTimeThreshold)
                    {
                        stuckTime_RolledOntoRoofOfSide = 0f;
                        stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                        stuckTime_StuckInSomething = 0f;
                        RespanwAfterStuck();
                    }

                }
                else
                {
                    stuckTime_RolledOntoRoofOfSide = 0f;
                }

                if (Mathf.Abs(Mathf.RoundToInt(speed)) <= 2f && Vector3.Dot(Vector3.up, transform.up) <= 0.98f)
                {
                    //Debug.Log("Просто застрял");

                    stuckTime_StuckInSomething += Time.deltaTime;

                    if (stuckTime_StuckInSomething >= stuckTimeThreshold)
                    {
                        stuckTime_RolledOntoRoofOfSide = 0f;
                        stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                        stuckTime_StuckInSomething = 0f;
                        RespanwAfterStuck();
                    }
                }
                else
                {
                    stuckTime_StuckInSomething = 0f;
                }
                #endregion

                //turnlogic
                float sign = Mathf.Sign(carVelocity.z);
                float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);
                if (verticalInput > 0.1f || carVelocity.z > 1)
                {
                    carBody.AddTorque(Vector3.up * horizontalInput * sign * Turn * 100 * TurnMultiplyer);
                }
                else if (verticalInput < -0.1f || carVelocity.z < -1)
                {
                    carBody.AddTorque(Vector3.up * horizontalInput * sign * Turn * 100 * TurnMultiplyer);
                }

                //brakelogic
                if (Input.GetAxis("Jump") > 0.1f)
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotationX;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.None;
                }

                //accelaration logic

                if (movementMode == MovementMode.AngularVelocity)
                {
                    if (Mathf.Abs(verticalInput) > 0.1f)
                    {
                        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, carBody.transform.right * verticalInput * MaxSpeed / radius, Accelaration * Time.deltaTime);
                    }
                }
                else if (movementMode == MovementMode.Velocity)
                {
                    if (Mathf.Abs(verticalInput) > 0.1f && Input.GetAxis("Jump") < 0.1f)
                    {
                        rb.velocity = Vector3.Lerp(rb.velocity, carBody.transform.forward * verticalInput * MaxSpeed, Accelaration / 10 * Time.deltaTime);
                    }
                }

                // down froce
                rb.AddForce(-transform.up * Downforce * rb.mass);

                //body tilt
                carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, hit.normal) * carBody.transform.rotation, 0.12f));

                if (handbrake)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                if (AirControl)
                {
                    //turnlogic
                    float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);

                    carBody.AddTorque(Vector3.up * horizontalInput * Turn * 100 * TurnMultiplyer);
                }

                carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, Vector3.up) * carBody.transform.rotation, 0.02f));
                rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity + Vector3.down * Gravity, Time.deltaTime * Gravity);
            }
        }

        private void RaceAIMovement()
        {
            carVelocity = carBody.transform.InverseTransformDirection(carBody.velocity);

            if (Mathf.Abs(carVelocity.x) > 0)
            {
                //changes friction according to sideways speed of car
                frictionMaterial.dynamicFriction = frictionCurve.Evaluate(Mathf.Abs(carVelocity.x / 100));
            }


            if (grounded())
            {
                #region Stuck Check
                if ((verticalInput != 0 || isArcadeRaceAI) && Mathf.RoundToInt(speed) == 0 && !handbrake)
                {
                    //Debug.Log("Газует бот, но застрял");

                    stuckTime_BotAIPressesOnGasButDoesntMove += Time.deltaTime;

                    if (stuckTime_BotAIPressesOnGasButDoesntMove >= stuckTimeThreshold)
                    {
                        stuckTime_BotAIPressesOnGasButDoesntMove = 0f;
                        RespanwAfterStuck();
                    }
                }
                else stuckTime_BotAIPressesOnGasButDoesntMove = 0f;

                // Проверка, что игрок не упал на бок или крышу
                if (Vector3.Dot(Vector3.up, transform.up) < 0.15f)
                {
                    //Debug.Log("Перевернулся на крышу или на бок");

                    stuckTime_RolledOntoRoofOfSide += Time.deltaTime;

                    if (stuckTime_RolledOntoRoofOfSide >= stuckTimeThreshold)
                    {
                        stuckTime_RolledOntoRoofOfSide = 0f;
                        stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                        stuckTime_StuckInSomething = 0f;
                        RespanwAfterStuck();
                    }

                }
                else
                {
                    stuckTime_RolledOntoRoofOfSide = 0f;
                }

                if (Mathf.Abs(Mathf.RoundToInt(speed)) <= 2f && Vector3.Dot(Vector3.up, transform.up) <= 0.98f)
                {
                    //Debug.Log("Просто застрял");

                    stuckTime_StuckInSomething += Time.deltaTime;

                    if (stuckTime_StuckInSomething >= stuckTimeThreshold)
                    {
                        stuckTime_RolledOntoRoofOfSide = 0f;
                        stuckTime_PlayerPressesOnGasButDoesntMove = 0f;
                        stuckTime_StuckInSomething = 0f;
                        RespanwAfterStuck();
                    }
                }
                else
                {
                    stuckTime_StuckInSomething = 0f;
                }
                #endregion

                //turnlogic
                float sign = Mathf.Sign(carVelocity.z);
                float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);
                if (SpeedAI > 0.1f || carVelocity.z > 1)
                {
                    carBody.AddTorque(Vector3.up * TurnAI * sign * Turn * 100 * TurnMultiplyer);
                }
                else if (SpeedAI < -0.1f || carVelocity.z < -1)
                {
                    carBody.AddTorque(Vector3.up * TurnAI * sign * Turn * 100 * TurnMultiplyer);
                }

                //brakelogic
                if (brakeAI > 0.1f)
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotationX;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.None;
                }

                //accelaration logic

                if (movementMode == MovementMode.AngularVelocity)
                {
                    if (Mathf.Abs(SpeedAI) > 0.1f)
                    {
                        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, carBody.transform.right * SpeedAI * MaxSpeed / radius, Accelaration * Time.deltaTime);
                    }
                }
                else if (movementMode == MovementMode.Velocity)
                {
                    if (Mathf.Abs(SpeedAI) > 0.1f && brakeAI < 0.1f)
                    {
                        rb.velocity = Vector3.Lerp(rb.velocity, carBody.transform.forward * SpeedAI * MaxSpeed, Accelaration / 10 * Time.deltaTime);
                    }
                }

                //body tilt
                carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, hit.normal) * carBody.transform.rotation, 0.12f));

                if (handbrake)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, Vector3.up) * carBody.transform.rotation, 0.02f));
            }
        }

        public void Visuals()
        {
            //tires
            foreach (Transform FW in FrontWheels)
            {
                FW.localRotation = Quaternion.Slerp(FW.localRotation, Quaternion.Euler(FW.localRotation.eulerAngles.x,
                                   30 * horizontalInput, FW.localRotation.eulerAngles.z), 0.1f);
                FW.GetChild(0).localRotation = rb.transform.localRotation;
            }
            RearWheels[0].localRotation = rb.transform.localRotation;
            RearWheels[1].localRotation = rb.transform.localRotation;

            //Body
            if (carVelocity.z > 1)
            {
                BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(Mathf.Lerp(0, -5, carVelocity.z / MaxSpeed),
                                   BodyMesh.localRotation.eulerAngles.y, BodyTilt * horizontalInput), 0.05f);
            }
            else
            {
                BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(0, 0, 0), 0.05f);
            }


        }

        public bool grounded() //checks for if vehicle is grounded or not
        {
            origin = rb.position + rb.GetComponent<SphereCollider>().radius * Vector3.up;
            var direction = -transform.up;
            var maxdistance = rb.GetComponent<SphereCollider>().radius + 0.2f;

            if (GroundCheck == groundCheck.rayCast)
            {
                if (Physics.Raycast(rb.position, Vector3.down, out hit, maxdistance, drivableSurface))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (GroundCheck == groundCheck.sphereCaste)
            {
                if (Physics.SphereCast(origin, radius + 0.1f, direction, out hit, maxdistance, drivableSurface))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }

        private void OnDrawGizmos()
        {
            //debug gizmos
            radius = rb.GetComponent<SphereCollider>().radius;
            float width = 0.02f;
            if (!Application.isPlaying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(rb.transform.position + ((radius + width) * Vector3.down), new Vector3(2 * radius, 2 * width, 4 * radius));
                if (GetComponent<BoxCollider>())
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
                    
                }

            }

        }

        public void SetIsRaceAI()
        {
            isArcadeRaceAI = true;
        }

        public void PlayerStuckUnder()// from UderPlayerCar. Only on Player car
        {
            RespanwAfterStuck();
        }

        private void RespanwAfterStuck()
        {
            if (isPlayer)
            {
                // показ кнопки
                GetRespanwPositionForArcadeVehicleControllerEvent?.Invoke(this, true);

                isShowingRestartButton = true;
            }
            else
            {
                GetRespanwPositionForArcadeVehicleControllerEvent?.Invoke(this, false);
            }
        }

        public void RespawnPlayerAfterStuckFromButton()
        {
            GetRespanwPositionForArcadeVehicleControllerEvent?.Invoke(this, false);

            isShowingRestartButton = false;
        }

        public void GetRespawnTransform(Transform respTransform)
        {
            transform.rotation = respTransform.rotation;
            transform.position = new Vector3(respTransform.position.x, 5f, respTransform.position.z);
        }


        #region Invers Turn
        public void ChangeTheTurnControllerToInverse()
        {
            isInverseTurnController = true;
        }
        public void ChangeTheTurnControllerToNormal()
        {
            isInverseTurnController = false;
        }
        #endregion

        #region AI

        private void CalculatingRaceAITurnValue()
        {
            // the new method of calculating turn value
            Vector3 aimedPoint = target.position;
            aimedPoint.y = transform.position.y;
            Vector3 aimedDir = (aimedPoint - transform.position).normalized;
            Vector3 myDir = transform.forward;
            myDir.Normalize();
            desiredTurning = Mathf.Abs(Vector3.Angle(myDir, Vector3.ProjectOnPlane(aimedDir, transform.up)));
            //

            float reachedTargetDistance = 1f;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            Vector3 dirToMovePosition = (target.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);
            float angleToMove = Vector3.Angle(transform.forward, dirToMovePosition);
            if (angleToMove > brakeAngle)
            {
                if (carVelocity.z > 15)
                {
                    brakeAI = 1;
                }
                else
                {
                    brakeAI = 0;
                }

            }
            else { brakeAI = 0; }

            if (distanceToTarget > reachedTargetDistance)
            {

                if (dot > 0)
                {
                    SpeedAI = 1f;

                    float stoppingDistance = 5f;
                    if (distanceToTarget < stoppingDistance)
                    {
                        //brakeAI = 1f;
                    }
                    else
                    {
                        brakeAI = 0f;
                    }
                }
                else
                {
                    float reverseDistance = 5f;
                    if (distanceToTarget > reverseDistance)
                    {
                        SpeedAI = 1f;
                    }
                    else
                    {
                        brakeAI = -1f;
                    }
                }

                float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

                if (angleToDir > 0)
                {
                    TurnAI = 1f * turnCurve.Evaluate(desiredTurning / 90);
                }
                else
                {
                    TurnAI = -1f * turnCurve.Evaluate(desiredTurning / 90);
                }

            }
            else
            {
                if (carVelocity.z > 1f)
                {
                    brakeAI = -1f;
                }
                else
                {
                    brakeAI = 0f;
                }
                TurnAI = 0f;
            }

            if (isObstacle) TurnAI = obstacleSteer;
        }

        public void ActivateSpeedControlWithPlayer(CarPlayerWaipointTracker carPlayerWaipointTracker, WaypointProgressTracker waypointProgressTracker,
            float speedPercent)
        {
            useSpeedControl = true;
            this.carPlayerWaipointTracker = carPlayerWaipointTracker;
            this.waypointProgressTracker = waypointProgressTracker;
            this.speedPercent = speedPercent;
        }

        #endregion
    }
}
