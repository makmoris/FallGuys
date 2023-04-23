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

            //����������, ���� ����� ������� ��� �����
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
            //    float reverseDistance = 1f;// ���� ������ ��� �� N, �� ���������������, � �� ����� �����
            //    if (distanceToTarget > reverseDistance)// 0 - ��� ������� ����. ������ � �������� (��� ��������, ������ ����� � �������� � �����������)
            //    {
            //        // too far to reverse
            //        forwardAmount = 1f;
            //    }
            //    else
            //    {
            //        forwardAmount = -1f;
            //    }
            //}

            // ���������� ������� �������� 
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
        //    Debug.Log($"������");

        //    if (wheelVehicle.Speed > 1f)// ���� �������� ������ 1, �� �������� 
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

        //    //    print(gameObject.name + " ������� � " + targetPositionTransform.name + "   - isTargetReachedFirst");
        //    //}
        //}

        // ��� ��������
        wheelVehicle.Throttle = forwardAmount;

        // ��� ���������
        wheelVehicle.Steering = turnAmount;

        // ��� �����
        //wheelVehicle.boosting = true;

        // ��� ������� / � �������� ��������, �.�. �� ������� �� ������������ ������� ������. ������� ����� ��������, ����� ��������� ������� true � 
        // ��������� � false
        //wheelVehicle.jumping = true;
    }
}
