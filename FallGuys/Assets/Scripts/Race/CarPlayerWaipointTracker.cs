using ArcadeVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerWaipointTracker : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float progressDistance;
    public float ProgressDistance => progressDistance;
    [SerializeField] private int progressNum;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private float speed;

    private WaypointCircuit circuit;

    private ArcadeVehicleController arcadeVehicleController;

    [SerializeField] private float lookAheadForTargetOffset = 5;
    [SerializeField] private float lookAheadForTargetFactor = .1f;
    private float lookAheadForSpeedOffset = 50;
    private float lookAheadForSpeedFactor = .2f;

    public WaypointCircuit.RoutePoint progressPoint { get; private set; }

    public void Initialize()
    {
        circuit = FindObjectOfType<WaypointCircuit>();

        var targetObj = new GameObject("WaypointsTarget");
        target = targetObj.transform;

        arcadeVehicleController = transform.GetComponentInParent<ArcadeVehicleController>();

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
            speed = arcadeVehicleController.carVelocity.z;
        }
        target.position =
            circuit.GetRoutePoint(progressDistance + lookAheadForTargetOffset + lookAheadForTargetFactor * speed)
                   .position;
        target.rotation =
            Quaternion.LookRotation(
                circuit.GetRoutePoint(progressDistance + lookAheadForSpeedOffset + lookAheadForSpeedFactor * speed)
                       .direction);


        // get our current progress along the route
        progressPoint = circuit.GetRoutePoint(progressDistance);
        Vector3 progressDelta = progressPoint.position - transform.position;
        if (Vector3.Dot(progressDelta, progressPoint.direction) < 0)
        {
            progressDistance += progressDelta.magnitude * 0.5f;
        }

        lastPosition = transform.position;
    }
}
