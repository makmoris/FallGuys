using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceCarDriverAI : MonoBehaviour
{
    [SerializeField] private int pathNumber;

    [SerializeField] private int targetNumber;
    [Header("����� ��������")]
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

    [Header("Player Following Controller")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float dotPlayer;

    [SerializeField] private float maxDistanceInFrontOfPlayer;// ������������ ��������� ������ ����� �������
    [SerializeField] private float maxDistanceBehindOfPlayer;// ������������ ��������� ���������� ������ ������
    

    private void Awake()
    {
        wheelVehicle = GetComponent<WheelVehicle>();
    }

    private void Start()
    {
        targets = RaceCheckpointManager.Instance.GetCheckpointsList();
        targetPositionTransform = targets[0].transform;
        targetPosition = targets[0].GetTargetPosition(pathNumber);
    }

    private void Update()
    {
        if (!obstacle && !side) Moving();
        PlayerChecker();
    }

    private void PlayerChecker()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        Vector3 dirToMovePosition = (playerTransform.position - transform.position);
        dotPlayer = Vector3.Dot(transform.forward, dirToMovePosition);

        if (dotPlayer <= maxDistanceBehindOfPlayer) // ����� �������. ��� ���������� ��������
        {
            Debug.Log("����� �������. ��� ���������� ��������");
            Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.red);
        }
        // ��� � �������� �����. �� ������ ������� � �� ������ ������� �� ������. ��� ���� ��� ������
        if (dotPlayer > maxDistanceBehindOfPlayer && dotPlayer < maxDistanceInFrontOfPlayer)
        {
            Debug.Log("��� � �������� ���������� ��������� �� ������. ����������� ��������");
            Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.blue);
        }

        if (dotPlayer >= maxDistanceInFrontOfPlayer)// ����� ������� �������� ������. ��� �����������
        {
            Debug.Log("����� ������� �������� ������. ��� �����������");
            Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.yellow);
        }

        
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
            //print(angleToDir);
            isTargetReachedFirst = true;
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
        steering = turnAmount;
        // ��� �����
        //wheelVehicle.boosting = true;

        // ��� ������� / � �������� ��������, �.�. �� ������� �� ������������ ������� ������. ������� ����� ��������, ����� ��������� ������� true � 
        // ��������� � false
        //wheelVehicle.jumping = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetPositionTransform)
        {
            //targetReached = true;
            targetNumber++;
            targetPositionTransform = targets[targetNumber].transform;
            targetPosition = targets[targetNumber].GetTargetPosition(pathNumber);
            Debug.Log($"������");
        }
    }
}
