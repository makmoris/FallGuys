using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceAIDriver : MonoBehaviour
{
    [Space]
    PathFollower pathFollower;
    [SerializeField] private Transform targetPositionTransform;
    [SerializeField] private Vector3 targetPosition;

    private WheelVehicle wheelVehicle;

    private bool obstacle;
    public bool Obstacle
    {
        get => obstacle;
        set => obstacle = value;
    }

    private bool side;
    public bool Side
    {
        get => side;
        set => side = value;
    }

    private ArenaDifficultyLevelsAI arenaDifficultyLevelAI;
    // private RaceDifficultyLevelsAI raceDifficultyLevelAI;

    public float distanceToTarget;
    public float steering;

    [Header("Following distance")]
    [SerializeField] private float shortDistance;
    [SerializeField] private float longDistance;

    private void Awake()
    {
        pathFollower = targetPositionTransform.GetComponent<PathFollower>();
        wheelVehicle = GetComponent<WheelVehicle>();
    }

    private void Start()
    {
        targetPosition = targetPositionTransform.position;
    }

    private void Update()
    {
        if (!obstacle && !side) Moving();
    }

    private void Moving()
    {
        targetPosition = targetPositionTransform.position;

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 1f;
        distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if(distanceToTarget <= shortDistance)
        {
            forwardAmount = 0f;
            Debug.DrawRay(transform.position, targetPosition - transform.position, Color.red);
        }

        if(distanceToTarget > shortDistance && distanceToTarget < longDistance)
        {
            forwardAmount = 1f;
            pathFollower.ReturnNormalSpeed();
            Debug.DrawRay(transform.position, targetPosition - transform.position, Color.blue);
        }

        if (distanceToTarget >= longDistance)
        {
            forwardAmount = 1f;
            pathFollower.SlowDown();
            Debug.DrawRay(transform.position, targetPosition - transform.position, Color.yellow);
        }


        if (distanceToTarget > reachedTargetDistance)
        {
            // still too far, keep going

            //определ€ем, цель перед машиной или сзади
            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            //if (dot > 0)
            //{
            //    // target in front
            //    forwardAmount = 1f;
            //}
            //else
            //{
            //    // target in behind
            //    float reverseDistance = 1f;// если дальше чем на N, то разворачиваемс€, а не сдаем назад
            //    if (distanceToTarget > reverseDistance)// 0 - без заднего хода. ¬сегда в разворот (ћог застр€ть, сдава€ назад и утыка€сь в преп€тствие)
            //    {
            //        // too far to reverse
            //        forwardAmount = 1f;
            //    }
            //    else
            //    {
            //        forwardAmount = -1f;
            //    }
            //}

            // определ€ем сторону поворота 
            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
            //Debug.Log($"Angle to direction = {angleToDir}");
            if (angleToDir == 0 || (angleToDir > -10f && angleToDir < 10f))
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
        }
        //else
        //{
        //    // reached target
        //    Debug.Log($"ƒоехал");

        //    if (wheelVehicle.Speed > 1f)// если скорость больше 1, то тормозим 
        //    {
        //        forwardAmount = -1f;
        //    }
        //    else
        //    {
        //        forwardAmount = 0f;
        //    }
        //    turnAmount = 0f;

        //    //if (isTargetReachedFirst)
        //    //{
        //    //    targetReached = true;
        //    //    isTargetReachedFirst = false;

        //    //    //targetNumber++;

        //    //    print(gameObject.name + " приехал к " + targetPositionTransform.name + "   - isTargetReachedFirst");
        //    //}
        //}

        // дл€ движени€
        wheelVehicle.Throttle = forwardAmount;

        // дл€ поворотов
        wheelVehicle.Steering = turnAmount;

        // дл€ нитро
        //wheelVehicle.boosting = true;

        // дл€ прыжков / — прыжками проблема, т.к. он зависит от длительности нажати€ кнопки. —делать через корутину, чтобы подержать секунду true и 
        // отключить в false
        //wheelVehicle.jumping = true;
    }
}
