using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceAIWithFollower : MonoBehaviour
{
    [SerializeField] private SplineFollower splineFollower;
    [Space]
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

    

    private void Awake()
    {
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



        if (distanceToTarget > reachedTargetDistance)
        {
            // still too far, keep going

            //����������, ���� ����� ������� ��� �����
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
                float reverseDistance = 1f;// ���� ������ ��� �� N, �� ���������������, � �� ����� �����
                if (distanceToTarget > reverseDistance)// 0 - ��� ������� ����. ������ � �������� (��� ��������, ������ ����� � �������� � �����������)
                {
                    // too far to reverse
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -1f;
                }
            }

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
