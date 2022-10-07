using System.Collections;
using UnityEngine;
using VehicleBehaviour;

public class CarDriverAI : MonoBehaviour
{
    [Header("“очки »нтереса")]
    public Transform[] pointsOnMapArray;
    //public Transform[] rivalsArray;

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

    private void Start()
    {
        int rand = Random.Range(0, pointsOnMapArray.Length);

        targetPositionTransform = pointsOnMapArray[rand];
        isTargetReachedFirst = true;
    }

    private void Update()
    {
        if(!obstacle) Moving();
    }

    private void Moving()
    {
        ChooseTargetPosition(targetPositionTransform.position);

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 7f;
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
                float reverseDistance = 10f;// если дальше чем на N, то разворачиваемс€, а не сдаем назад
                if (distanceToTarget > reverseDistance)
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
            int rand = Random.Range(0, pointsOnMapArray.Length);

            if (targetPositionTransform != pointsOnMapArray[rand])
            {
                targetPositionTransform = pointsOnMapArray[rand];
            }
            else
            {
                if (rand == 0)
                {
                    targetPositionTransform = pointsOnMapArray[rand + 1];
                }
                else if (rand == pointsOnMapArray.Length)
                {
                    targetPositionTransform = pointsOnMapArray[rand - 1];
                }
            }

            //чтобы почаще выбирали игрока
            int randPlayer = Random.Range(1, 5);
            if (randPlayer == 3)
            {
                targetPositionTransform = pointsOnMapArray[pointsOnMapArray.Length - 1];
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
            //print("«ащита");
            targetReached = true;
        }
    }
}
