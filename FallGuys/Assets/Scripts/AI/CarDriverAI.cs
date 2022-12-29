using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class CarDriverAI : MonoBehaviour
{
    [Header("“очки »нтереса")]
    public List<Transform> targets;

    [Space]
    [SerializeField] private Transform targetPositionTransform;
    private Vector3 targetPosition;

    private bool targetReached;
    private bool isTargetReachedFirst;

    private WheelVehicle wheelVehicle;

    private bool obstacle;
    public bool Obstacle { get => obstacle;
        set => obstacle = value;
    }

    private void Awake()
    {
        wheelVehicle = GetComponent<WheelVehicle>();
    }

    private void Update()
    {
        if(!obstacle) Moving();
    }

    private void Moving()
    {
        if (targetPositionTransform != null) ChooseTargetPosition(targetPositionTransform.position);
        else CheackTargetListOnNullComponent();

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 4f;
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
                float reverseDistance = 0f;// если дальше чем на N, то разворачиваемс€, а не сдаем назад
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

                //print(gameObject.name + " догнал " + targetPositionTransform.name + "   - isTargetReachedFirst");
            }
        }

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

    private void ChooseTargetPosition(Vector3 targetPosition)
    {
        if (targetReached)// если цель достигнута, то выбираем новую
        {
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

    public void SetTargets(List<Transform> _targets)
    {
        targets = _targets.GetRange(0, _targets.Count);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].gameObject == gameObject) // исключаем из целей этого же бота, чтобы не ездил сам за собой
            {
                targets.RemoveAt(i);
            }
        }

        int rand = Random.Range(0, targets.Count);

        targetPositionTransform = targets[rand];
        isTargetReachedFirst = true;
    }

    public void SetNewPlayerTargetFromDetector(Transform transform)
    {
        targetPositionTransform = transform;
    }

    public void DetectorLostTarget()
    {
        targetReached = true;
        ChooseTargetPosition(targetPositionTransform.position);
    }

    private void RemoveFromTargetsList(GameObject removedObj)
    {
        if(removedObj.transform == targetPositionTransform)
        {
            targetReached = true;
            ChooseTargetPosition(targetPositionTransform.position);
        }

        targets.Remove(removedObj.transform);
    }

    private void CheackTargetListOnNullComponent()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
                targetReached = true;
                ChooseTargetPosition(Vector3.zero);
                break;
            }
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
            //print("«ащита");
            targetReached = true;
        }
    }

    private void OnEnable()
    {
        VisualIntermediary.PlayerWasDeadEvent += RemoveFromTargetsList;
    }

    private void OnDisable()
    {
        VisualIntermediary.PlayerWasDeadEvent -= RemoveFromTargetsList;
    }
}
