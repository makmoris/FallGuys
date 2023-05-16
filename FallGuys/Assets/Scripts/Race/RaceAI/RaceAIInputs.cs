using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaceAIInputs : MonoBehaviour
{
    [SerializeField] private float currentSteer;
    [SerializeField] private float currentAccel;
    
    private RaceDriverAI carAIReference;

    [SerializeField] internal bool obstacle = false;
    public bool Obstacle
    {
        get => obstacle;
        set => obstacle = value;
    }

    [SerializeField] internal float obstacleSteer;
    public float ObstacleSteer
    {
        get => obstacleSteer;
        set => obstacleSteer = value;
    }

    [Space]
    [SerializeField] private bool isPathMovement;
    public bool IsPathMovement
    {
        get => isPathMovement;
        set => isPathMovement = value;
    }

    #region UTILITY

    private float randomValue;
    private float avoidOtherCarTime;
    private float avoidOtherCarSlowdown;
    private float avoidPathOffset;

    private float targetAngle;

    #endregion

    [Header("Targets")]
    [SerializeField] private List<Transform> targetsList = new List<Transform>();
    [SerializeField] private Transform currentTarget;
    private int currentTargetIndex;

    private void Awake()
    {
        #region REFERENCES

        carAIReference = GetComponent<RaceDriverAI>();


        #endregion

        randomValue = Random.value * 100;
    }


    private void FixedUpdate()
    {
        if (isPathMovement)
        {
            PathMovement();
        }
        else
        {
            MovementToTarget();
        }
    }

    public void SetTargets(List<Transform> targets)
    {
        targetsList = targets;
        currentTargetIndex = 0;
    }

    private void MovementToTarget()
    {
        currentTarget = targetsList[currentTargetIndex];

        Vector3 targetPosition = currentTarget.position;

        float forwardAmount;
        float turnAmount;

        float reachedTargetDistance = 6f;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget > reachedTargetDistance)
        {
            // still too far, keep going

            //определяем, цель перед машиной или сзади
            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            if (dot > 0)
            {
                // target in front
                forwardAmount = 1f;
            }
            else
            {
                // target in behind
                float reverseDistance = 1f;// если дальше чем на N, то разворачиваемся, а не сдаем назад
                if (distanceToTarget > reverseDistance)// 0 - без заднего хода. Всегда в разворот (Мог застрять, сдавая назад и утыкаясь в препятствие)
                {
                    // too far to reverse
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -1f;
                }
            }

            // определяем сторону поворота 
            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir == 0 || (angleToDir > -15f && angleToDir < 15f))
            {
                turnAmount = 0;
            }
            else
            {
                if (angleToDir > 0)
                {
                    turnAmount = 1f;
                }
                else
                {
                    turnAmount = -1f;
                }
            }
            //print(angleToDir);
            //isTargetReachedFirst = true;
        }
        else
        {
            // reached target

            if (currentTargetIndex + 1 < targetsList.Count)
            {
                currentTargetIndex++;
            }

            if (carAIReference.CurrentSpeed > 1f)// если скорость больше 1, то тормозим 
            {
                forwardAmount = -1f;
            }
            else
            {
                forwardAmount = 0f;
            }
            turnAmount = 0f;

            //if (isTargetReachedFirst)
            //{
            //    targetReached = true;
            //    isTargetReachedFirst = false;

            //    //print(gameObject.name + " догнал " + targetPositionTransform.name + "   - isTargetReachedFirst");
            //}
        }

        currentAccel = forwardAmount;
        currentSteer = turnAmount;

        if (!obstacle) carAIReference.Move(turnAmount, forwardAmount);
        else carAIReference.Move(obstacleSteer, forwardAmount);
    }

    private void PathMovement()
    {
        #region MAX SPEED

        Vector3 fwd = transform.forward;
        if (carAIReference.rb.velocity.magnitude > carAIReference.maxSpeed * 0.1f)
        {
            fwd = carAIReference.rb.velocity;
        }

        float desiredSpeed = carAIReference.maxSpeed;

        #endregion

        #region BRAKE FOR PATH

        switch (carAIReference.brakeCondition)
        {
            case BrakeCondition.TargetDirectionDifference:
                {
                    float approachingCornerAngle = Vector3.Angle(carAIReference.carAItarget.forward, fwd);
                    //Debug.Log($"approachingCornerAngle (приближающийся угловой угол) = {approachingCornerAngle}");
                    float spinningAngle = carAIReference.rb.angularVelocity.magnitude * carAIReference.cautiousAngularVelocityFactor;
                    //Debug.Log($"spinningAngle = {spinningAngle}");
                    float cautiousnessRequired = Mathf.InverseLerp(0, carAIReference.cautiousAngle, Mathf.Max(spinningAngle, approachingCornerAngle));
                    //Debug.Log($"cautiousnessRequired (требуется осторожность) = {cautiousnessRequired}");
                    desiredSpeed = Mathf.Lerp(carAIReference.maxSpeed, carAIReference.maxSpeed * carAIReference.cautiousSpeedFactor, cautiousnessRequired);
                    //Debug.Log($"desiredSpeed (желаемая скорость) = {desiredSpeed}");
                    //Debug.Log("----------");
                    break;
                }

            case BrakeCondition.TargetDistance:
                {
                    Vector3 delta = carAIReference.carAItarget.position - transform.position;
                    float distanceCautiousFactor = Mathf.InverseLerp(carAIReference.cautiousDistance, 0, delta.magnitude);
                    float spinningAngle = carAIReference.rb.angularVelocity.magnitude * carAIReference.cautiousAngularVelocityFactor;
                    float cautiousnessRequired = Mathf.Max(Mathf.InverseLerp(0, carAIReference.cautiousAngle, spinningAngle), distanceCautiousFactor);
                    desiredSpeed = Mathf.Lerp(carAIReference.maxSpeed, carAIReference.maxSpeed * carAIReference.cautiousSpeedFactor, cautiousnessRequired);
                    //Debug.Log(desiredSpeed);
                    break;
                }

            case BrakeCondition.NeverBrake:
                break;
        }

        #endregion

        #region EVASIVE ACTION

        Vector3 offsetTargetPos = carAIReference.carAItarget.position;

        if (Time.time < avoidOtherCarTime)
        {
            desiredSpeed *= avoidOtherCarSlowdown;
            offsetTargetPos += carAIReference.carAItarget.right * avoidPathOffset;
        }
        else
        {
            offsetTargetPos += carAIReference.carAItarget.right * (Mathf.PerlinNoise(Time.time * carAIReference.lateralWanderSpeed, randomValue) * 2 - 1) * carAIReference.lateralWander;
        }

        #endregion

        #region SENSITIVITY

        float accelBrakeSensitivity = (desiredSpeed < carAIReference.CurrentSpeed)
                                          ? carAIReference.brakeSensitivity
                                          : carAIReference.accelSensitivity;



        float accel = Mathf.Clamp((desiredSpeed - carAIReference.CurrentSpeed) * accelBrakeSensitivity, -1, 1);
        #endregion

        #region STEER
        accel *= carAIReference.wanderAmount + (Mathf.PerlinNoise(Time.time * carAIReference.accelWanderSpeed, randomValue) * carAIReference.wanderAmount);

        currentAccel = accel;
        Vector3 localTarget;

        localTarget = transform.InverseTransformPoint(offsetTargetPos);

        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        float steer = Mathf.Clamp(targetAngle * carAIReference.steerSensitivity, -1, 1) * Mathf.Sign(carAIReference.CurrentSpeed);
        currentSteer = steer;
        #endregion

        #region MOVE CAR

        if (!obstacle) carAIReference.Move(steer, accel);
        else carAIReference.Move(obstacleSteer, accel);

        #endregion
    }

    #region COLLISION WITH OTHER CARS

    private void OnCollisionStay(Collision col)
    {
        if (col.rigidbody != null)
        {
            var otherAI = col.rigidbody.GetComponent<CarAIInputs>();
            if (otherAI != null)
            {
                avoidOtherCarTime = Time.time + 1;

                if (Vector3.Angle(transform.forward, otherAI.transform.position - transform.position) < 90)
                {
                    avoidOtherCarSlowdown = 0.5f;
                }
                else
                {
                    avoidOtherCarSlowdown = 1;
                }

                var otherCarLocalDelta = transform.InverseTransformPoint(otherAI.transform.position);
                float otherCarAngle = Mathf.Atan2(otherCarLocalDelta.x, otherCarLocalDelta.z);
                avoidPathOffset = carAIReference.lateralWander * -Mathf.Sign(otherCarAngle);
            }
        }
    }

    #endregion
}
