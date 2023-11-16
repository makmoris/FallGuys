using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceAIWaipointTracker : MonoBehaviour
{
    //[HideInInspector]
    public Transform target;
    //private WaypointsPath circuit;
    [SerializeField] private RaceWaypointsPath raceWaypointsPath;
    [SerializeField] private RaceDriverAI raceDriverAI;

    [SerializeField] private float lookAheadForTargetOffset = 5;
    [SerializeField] private float lookAheadForTargetFactor = .1f;
    [SerializeField] private float lookAheadForSpeedOffset = 10;
    [SerializeField] private float lookAheadForSpeedFactor = .2f;
    [SerializeField] private int progressStyle;
    [SerializeField] private float pointToPointThreshold = 4;

    [SerializeField] private float progressDistance;
    [SerializeField] private int progressNum;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private float speed;

    private int checkpointProgressNum;
    private float checkpointProgressDistance;


    #region KEY POINTS

    //[HideInInspector]
    public Transform[] pathTransform;

    public RaceWaypointsPath.RoutePoint targetPoint { get; private set; }
    public RaceWaypointsPath.RoutePoint speedPoint { get; private set; }
    public RaceWaypointsPath.RoutePoint progressPoint { get; private set; }

    #endregion

    private void Start()
    {

        raceDriverAI = this.GetComponent<RaceDriverAI>();
        raceWaypointsPath = raceDriverAI.raceWaypointsPath;
        lookAheadForTargetOffset = raceDriverAI.lookAheadForTarget;
        lookAheadForTargetFactor = raceDriverAI.lookAheadForTargetFactor;
        lookAheadForSpeedOffset = raceDriverAI.lookAheadForSpeedOffset;
        lookAheadForSpeedFactor = raceDriverAI.lookAheadForSpeedFactor;
        pointToPointThreshold = raceDriverAI.pointThreshold;
        progressStyle = (int)raceDriverAI.progressStyle;


        if (target == null)
        {
            target = raceDriverAI.carAItarget;
        }

        Reset();
    }

    public void Reset()
    {
        progressDistance = 0;
        progressNum = 0;

        if (progressStyle == 1 && raceWaypointsPath != null)
        {
            target.position = raceWaypointsPath.nodes[progressNum].position;
            target.rotation = raceWaypointsPath.nodes[progressNum].rotation;
        }
    }

    public void SetNewWaypointsPath(RaceWaypointsPath newPath)
    {
        raceWaypointsPath = newPath;
        Reset();
    }

    public void CarWasRespawn()
    {
        progressNum = checkpointProgressNum;
        progressDistance = checkpointProgressDistance;
    }

    public void SaveCheckpoint()
    {
        checkpointProgressNum = progressNum;
        checkpointProgressDistance = progressDistance;
    }

    private void Update()
    {
        if(raceWaypointsPath != null) FollowPath();
        //if (!this.transform.GetComponent<RaceDriverAI>().persuitAiOn)
        //{
        //    this.transform.GetComponent<RaceDriverAI>().persuitAiOn = false;
        //    FollowPath();
        //}
        //else
        //{
        //    if (raceDriverAI.persuitTarget != null)
        //    {
        //        Transform tempPersuitCollider = raceDriverAI.persuitTarget.GetComponentInChildren<MeshCollider>().transform;

        //        target = tempPersuitCollider;
        //    }
        //    else
        //    {
        //        FollowPath();
        //    }
        //}
    }

    #region DRAW DIRECTION

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && raceWaypointsPath != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(raceWaypointsPath.GetRoutePosition(progressDistance), 1);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(target.position, target.position + target.forward);
        }
    }

    #endregion

    #region FOLLOW PATH

    public void FollowPath()
    {
        if (raceDriverAI.progressStyle == 0)
        {
            if (Time.deltaTime > 0)
            {
                speed = Mathf.Lerp(speed, (lastPosition - transform.position).magnitude / Time.deltaTime, Time.deltaTime);
            }
            target.position = raceWaypointsPath.GetRoutePoint(progressDistance + lookAheadForTargetOffset + lookAheadForTargetFactor * speed).position;
            target.rotation = Quaternion.LookRotation(raceWaypointsPath.GetRoutePoint(progressDistance + lookAheadForSpeedOffset + lookAheadForSpeedFactor * speed).direction);

            progressPoint = raceWaypointsPath.GetRoutePoint(progressDistance);
            Vector3 progressDelta = progressPoint.position - transform.position;
            if (Vector3.Dot(progressDelta, progressPoint.direction) < 0)
            {
                progressDistance += progressDelta.magnitude * 0.5f;
            }

            lastPosition = transform.position;
        }
        else
        {
            Vector3 targetDelta = target.position - transform.position;
            if (targetDelta.magnitude < pointToPointThreshold)
            {
                progressNum = (progressNum + 1) % raceWaypointsPath.nodes.Count;
            }

            target.position = raceWaypointsPath.nodes[progressNum].position;
            target.rotation = raceWaypointsPath.nodes[progressNum].rotation;

            progressPoint = raceWaypointsPath.GetRoutePoint(progressDistance);
            Vector3 progressDelta = progressPoint.position - transform.position;
            if (Vector3.Dot(progressDelta, progressPoint.direction) < 0)
            {
                progressDistance += progressDelta.magnitude;
            }
            lastPosition = transform.position;
        }
    }

    #endregion
}
