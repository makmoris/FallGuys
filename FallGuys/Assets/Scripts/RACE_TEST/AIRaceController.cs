using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRaceController : MonoBehaviour
{
    [Header("Follower")]
    [SerializeField] private SplineFollower target;
    [Header("AIDriver")]
    [SerializeField] private RaceAIWithFollower driver;

    [Space]
    [SerializeField] private float distanceToTarget;

    [Header("Following distance")]
    [SerializeField] private float shortDistance;
    [SerializeField] private float longDistance;

    private void Update()
    {
        CheckingDistanceBetweenTargetAndDriver();
    }

    private void CheckingDistanceBetweenTargetAndDriver()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 driverPosition = driver.transform.position;

        distanceToTarget = Vector3.Distance(driverPosition, targetPosition);

        if (distanceToTarget <= shortDistance)
        {
            target.follow = true;
            target.followSpeed++;
            Debug.DrawRay(driverPosition, targetPosition - driverPosition, Color.red);
        }

        if (distanceToTarget > shortDistance && distanceToTarget < longDistance)
        {
            target.follow = true;
            //pathFollower.ReturnNormalSpeed();
            Debug.DrawRay(driverPosition, targetPosition - driverPosition, Color.blue);
        }

        if (distanceToTarget >= longDistance)
        {
            target.follow = false;
            //pathFollower.SlowDown();
            Debug.DrawRay(driverPosition, targetPosition - driverPosition, Color.yellow);
        }
    }
}
