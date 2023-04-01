using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceCarDriverAI : MonoBehaviour
{
    [SerializeField] private int targetNumber;
    [Header("Точки Интереса")]
    public List<CheckpointGate> targets;


    [Space]
    [SerializeField] private Transform targetPositionTransform;
    [SerializeField] private Vector3 targetPosition;

    private bool targetReached;
    private bool isTargetReachedFirst;

    private WheelVehicle wheelVehicle;

    private bool obstacle;
    public bool Obstacle { get => obstacle;
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

    public float steering;

    private void Awake()
    {
        wheelVehicle = GetComponent<WheelVehicle>();
    }

    private void Start()
    {
        targets = RaceCheckpointManager.Instance.GetCheckpointsList();
        targetPositionTransform = targets[0].transform;
        targetPosition = targets[0].GetTargetPosition();
    }

    private void Update()
    {
        if (!obstacle && !side) Moving();
    }

    private void Moving()
    {

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 1f;
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
            //print(angleToDir);
            isTargetReachedFirst = true;
        }
        //else
        //{
        //    // reached target
        //    Debug.Log($"Доехал");

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

        // для движения
        wheelVehicle.Throttle = forwardAmount;

        // для поворотов
        wheelVehicle.Steering = turnAmount;

        // для нитро
        //wheelVehicle.boosting = true;

        // для прыжков / С прыжками проблема, т.к. он зависит от длительности нажатия кнопки. Сделать через корутину, чтобы подержать секунду true и 
        // отключить в false
        //wheelVehicle.jumping = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetPositionTransform)
        {
            //targetReached = true;
            targetNumber++;
            targetPositionTransform = targets[targetNumber].transform;
            targetPosition = targets[targetNumber].GetTargetPosition();
            Debug.Log($"Доехал");
        }
    }
}
