using PunchCars.DifficultyAILevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RingsDriverAI : DriverAI
{
    [Header("DEBUG")]
    public List<Transform> targets;

    [Space]
    [SerializeField] private Transform targetPositionTransform;
    private Vector3 targetPosition;

    private bool targetReached;
    private bool isTargetReachedFirst;

    private WheelVehicle wheelVehicle;

    private List<Ring> ringsList = new List<Ring>();

    public override void Initialize(GameObject aiPlayerGO, GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel)
    {
        wheelVehicle = aiPlayerGO.GetComponent<WheelVehicle>();
    }

    private void Update()
    {
        if (!obstacle) Moving();
        else
        {
            wheelVehicle.Steering = obstacleSteer;
            wheelVehicle.Throttle = 1f;
        }
    }

    public void SetTargets(List<Transform> _targets)
    {
        targets = _targets.GetRange(0, _targets.Count);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].gameObject == wheelVehicle.gameObject) // исключаем из целей этого же бота, чтобы не ездил сам за собой
            {
                targets.RemoveAt(i);
                break;
            }
        }

        int rand = Random.Range(0, targets.Count);

        targetPositionTransform = targets[rand];
        isTargetReachedFirst = true;
    }

    public void SelectRingToTarget()
    {
        List<Transform> activeRingTransformList = new List<Transform>();

        foreach (var ring in ringsList)
        {
            if (ring.IsActive) activeRingTransformList.Add(ring.transform);
        }

        if(activeRingTransformList.Count > 0)
        {
            int indexOfRingWithMinDistance = 0;
            float minDistance = Mathf.Infinity;

            for (int i = 0; i < activeRingTransformList.Count; i++)
            {
                float distance = (activeRingTransformList[i].position - wheelVehicle.transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    indexOfRingWithMinDistance = i;
                }
            }

            targetPositionTransform = activeRingTransformList[indexOfRingWithMinDistance];
        }
    }

    public void SetRings(List<Ring> ringsList)
    {
        this.ringsList = ringsList.GetRange(0, ringsList.Count);
    }

    private void Moving()
    {
        ChooseTargetPosition(targetPositionTransform.position);

        float forwardAmount = 0f;
        float turnAmount = 0f;

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
            isTargetReachedFirst = true;
        }
        else
        {
            // reached target

            if (wheelVehicle.Speed > 1f)// если скорость больше 1, то тормозим 
            {
                forwardAmount = -1f;
            }
            else
            {
                forwardAmount = 0f;
            }
            turnAmount = 0f;

            if (isTargetReachedFirst)
            {
                targetReached = true;
                isTargetReachedFirst = false;
            }
        }

        wheelVehicle.Throttle = forwardAmount;
        wheelVehicle.Steering = turnAmount;
    }

    private void ChooseTargetPosition(Vector3 targetPosition)
    {
        if (targetReached)// если цель достигнута, то выбираем новую
        {
            // вместо target[rand] обращаемся к TargetController и у него берем цель для следования. Т.к. цели удаляются 

            int rand = Random.Range(0, targets.Count);

            if (targetPositionTransform != targets[rand])
            {
                targetPositionTransform = targets[rand];
            }
            else
            {
                if (rand == 0)
                {
                    targetPositionTransform = targets[rand + 1];
                }
                else if (rand == targets.Count)
                {
                    targetPositionTransform = targets[rand - 1];
                }
            }

            targetReached = false;

            StartCoroutine(CheckWhatTargetChoose());
        }
        else
        {
            this.targetPosition = targetPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetPositionTransform)
        {
            targetReached = true;
        }
    }

    IEnumerator CheckWhatTargetChoose()
    {
        yield return new WaitForSeconds(1f);
        if (!targetReached && !isTargetReachedFirst)
        {
            //print("Защита");
            targetReached = true;
        }
    }

    #region Difficulty AI Levels
    public override void SetLowDifficultyAILevel()
    {
        throw new System.NotImplementedException();
    }

    public override void SetNormalDifficultyAILevel()
    {
        throw new System.NotImplementedException();
    }

    public override void SetHighDifficultyAILevel()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
