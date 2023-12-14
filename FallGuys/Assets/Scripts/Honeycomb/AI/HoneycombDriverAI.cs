using ArcadeVP;
using PunchCars.DifficultyAILevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class HoneycombDriverAI : DriverAI
{
    [Header("DEBUG")]
    public List<Transform> targets;

    [Space]
    [SerializeField] private Transform targetPositionTransform;
    private Vector3 targetPosition;

    private bool targetReached;
    private bool isTargetReachedFirst;

    private ArcadeVehicleController arcadeVehicleController;

    [SerializeField]private TargetsControllerHoneycomb targetsControllerHoneycomb;

    public override void Initialize(GameObject aiPlayerGO, GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel)
    {
        arcadeVehicleController = aiPlayerGO.GetComponent<ArcadeVehicleController>();
    }

    private void Update()
    {
        if (!obstacle) Moving();
        else
        {
            //targetReached = true;
            if(arcadeVehicleController.Speed > 30f)
            {
                arcadeVehicleController.Steering = obstacleSteer;
                arcadeVehicleController.Throttle = -1f;
            }
            else
            {
                arcadeVehicleController.Steering = obstacleSteer;
                arcadeVehicleController.Throttle = 1f;
            }
        }
        // если есть преп€тствие, то надо вз€ть другую цель

        // поставить ограничение по скорости, а то улетают сильно. ћб кинуть замедление при соприкосновении с преп€тсвием
    }

    public void SetTargetController(TargetsControllerHoneycomb fallingPlatformTargetsController)// from installer
    {
        this.targetsControllerHoneycomb = fallingPlatformTargetsController;

        this.targetsControllerHoneycomb.PlatformFellEvent += RemoveFromTargetsList;

        List<Transform> targetsFromTargetsController = targetsControllerHoneycomb.GetTargets();
        targets = targetsFromTargetsController.GetRange(0, targetsFromTargetsController.Count);

        targetReached = true;
        ChooseTargetPosition(targets[0].position);
    }

    public void ResetTargetController(TargetsControllerHoneycomb fallingPlatformTargetsController)
    {
        targetsControllerHoneycomb.PlatformFellEvent -= RemoveFromTargetsList;
        targets.Clear();

        SetTargetController(fallingPlatformTargetsController);
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

            //определ€ем, цель перед машиной или сзади
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
                float reverseDistance = 1f;// если дальше чем на N, то разворачиваемс€, а не сдаем назад
                if (distanceToTarget > reverseDistance)// 0 - без заднего хода. ¬сегда в разворот (ћог застр€ть, сдава€ назад и утыка€сь в преп€тствие)
                {
                    // too far to reverse
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -1f;
                }
            }

            // определ€ем сторону поворота 
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

            if (arcadeVehicleController.Speed > 1f)// если скорость больше 1, то тормозим 
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

                //print(gameObject.name + " догнал " + targetPositionTransform.name + "   - isTargetReachedFirst");
            }
        }

        // дл€ движени€
        arcadeVehicleController.Throttle = forwardAmount;

        // дл€ поворотов
        arcadeVehicleController.Steering = turnAmount;

        // дл€ нитро
        //wheelVehicle.boosting = true;

        // дл€ прыжков / — прыжками проблема, т.к. он зависит от длительности нажати€ кнопки. —делать через корутину, чтобы подержать секунду true и 
        // отключить в false
        //wheelVehicle.jumping = true;
    }

    private void ChooseTargetPosition(Vector3 targetPosition)
    {
        if (targetReached)// если цель достигнута, то выбираем новую
        {
            // вместо target[rand] обращаемс€ к TargetController и у него берем цель дл€ следовани€. “.к. цели удал€ютс€ 

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

    private void RemoveFromTargetsList(Transform removedTrn)
    {
        if (removedTrn == targetPositionTransform)
        {
            targetReached = true;
            ChooseTargetPosition(targetPositionTransform.position);
        }

        targets.Remove(removedTrn);
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
            //print("«ащита");
            targetReached = true;
        }
    }

    private void OnDisable()
    {
        targetsControllerHoneycomb.PlatformFellEvent -= RemoveFromTargetsList;
    }

    #region Difficulty AI Levels
    public override void SetLowDifficultyAILevel()
    {
        
    }

    public override void SetNormalDifficultyAILevel()
    {
        
    }

    public override void SetHighDifficultyAILevel()
    {
        
    }
    #endregion
}
