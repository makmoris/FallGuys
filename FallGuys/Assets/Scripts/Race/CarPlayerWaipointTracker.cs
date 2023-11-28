using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerWaipointTracker : MonoBehaviour
{
    private WaypointsPath circuit;
    private Transform target;

    [SerializeField] private float progressDistance;
    public float ProgressDistance => progressDistance;
    [SerializeField] private int progressNum;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private float speed;


    #region KEY POINTS

    //[HideInInspector]
    public Transform[] pathTransform;

    //[HideInInspector]
    public WaypointsPath.RoutePoint targetPoint { get; private set; }
    //[HideInInspector]
    public WaypointsPath.RoutePoint speedPoint { get; private set; }
    //[HideInInspector]
    public WaypointsPath.RoutePoint progressPoint { get; private set; }

    #endregion

    public void Initialize(WaypointsPath waypointsPath)
    {
        circuit = waypointsPath;

        var targetObj = new GameObject("WaypointsTarget");
        target = targetObj.transform;

        Reset();
    }

    public void Reset()
    {
        progressDistance = 0;
        progressNum = 0;
    }

    private void Update()
    {
        FollowPath();
    }

    public void FollowPath()
    {
        if (Time.deltaTime > 0)
        {
            speed = Mathf.Lerp(speed, (lastPosition - transform.position).magnitude / Time.deltaTime, Time.deltaTime);
        }
        target.position = circuit.GetRoutePoint(progressDistance * speed).position;
        target.rotation = Quaternion.LookRotation(circuit.GetRoutePoint(progressDistance * speed).direction);

        progressPoint = circuit.GetRoutePoint(progressDistance);
        Vector3 progressDelta = progressPoint.position - transform.position;
        if (Vector3.Dot(progressDelta, progressPoint.direction) < 0)
        {
            progressDistance += progressDelta.magnitude * 0.5f;
        }

        lastPosition = transform.position;
    }
}
