using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public enum BrakeCondition
{
    NeverBrake,
    TargetDirectionDifference,
    TargetDistance,
}
public enum ProgressStyle
{
    SmoothAlongRoute,
    PointToPoint,
}

public class RaceDriverAI : MonoBehaviour
{
    private RaceAIInputs raceAIInputs;
    public RaceAIWaipointTracker raceAIWaypointTracker;

    #region INPUTS

    [SerializeField] public BrakeCondition brakeCondition = BrakeCondition.TargetDirectionDifference;

    [SerializeField] [Range(0, 1)] public float cautiousSpeedFactor = 0.05f;
    [SerializeField] [Range(0, 180)] public float cautiousAngle = 50f;
    [SerializeField] [Range(0, 200)] public float cautiousDistance = 100f;
    [SerializeField] public float cautiousAngularVelocityFactor = 30f;
    [SerializeField] [Range(0, 0.1f)] public float steerSensitivity = 0.05f;
    [SerializeField] [Range(0, 0.1f)] public float accelSensitivity = 0.04f;
    [SerializeField] [Range(0, 1)] public float brakeSensitivity = 1f;
    [SerializeField] [Range(0, 10)] public float lateralWander = 3f;
    [SerializeField] public float lateralWanderSpeed = 0.5f;
    [SerializeField] [Range(0, 1)] public float wanderAmount = 0.1f;
    [SerializeField] public float accelWanderSpeed = 0.1f;

    [SerializeField] public bool isDriving;
    [SerializeField] public Transform carAItarget;
    private GameObject carAItargetObj;
    [SerializeField] public bool stopWhenTargetReached;
    [SerializeField] public float reachTargetThreshold = 2;

    #endregion

    #region PROGRESS TRACKER

    [SerializeField] internal ProgressStyle progressStyle = ProgressStyle.SmoothAlongRoute;
    [SerializeField] public RaceWaypointsPath raceWaypointsPath;
    [SerializeField] [Range(5, 50)] public float lookAheadForTarget = 5;
    [SerializeField] public float lookAheadForTargetFactor = .1f;
    [SerializeField] public float lookAheadForSpeedOffset = 10;
    [SerializeField] public float lookAheadForSpeedFactor = .2f;
    [SerializeField] [Range(1, 10)] public float pointThreshold = 4;
    public Transform AItarget;

    #endregion

    private WheelVehicle wheelVehicle;
    internal Rigidbody rb;
    internal float maxSpeed;
    internal float maximumSteerAngle;

    [SerializeField]private bool moveForward;

    [SerializeField] private float currentSpeed;
    public float CurrentSpeed
    {
        get => currentSpeed;
    }

    private bool isGround;
    public bool IsGround
    {
        get => isGround;
        set => isGround = value;
    }

    [SerializeField]private bool handbrake;
    public bool Handbrake
    {
        get => handbrake;
        set => handbrake = value;
    }

    private void Start()
    {
        wheelVehicle = GetComponent<WheelVehicle>();
        rb = GetComponent<Rigidbody>();

        maxSpeed = wheelVehicle.MaxSpeed;
        maximumSteerAngle = wheelVehicle.SteerAngle;

        #region FEATURES SCRIPTS

        raceAIInputs = GetComponent<RaceAIInputs>();
        raceAIWaypointTracker = gameObject.AddComponent<RaceAIWaipointTracker>();

        #endregion

        #region AI REFERENCES

        carAItargetObj = new GameObject("WaypointsTarget");
        //carAItargetObj.transform.parent = this.transform.GetChild(1);
        carAItarget = carAItargetObj.transform;

        #endregion

        isGround = true;

        //moveForward = true;
    }

    private void Update()
    {
        currentSpeed = rb.velocity.magnitude * 3.6f;

        if (moveForward)
        {
            MoveForward();
        }
    }

    public void StartMoveForward()
    {
        moveForward = true;
        Debug.Log($"Авто {this.gameObject.name} начало движение MoveForward");
    }

    public void StopMoveForward()
    {
        moveForward = false;
    }

    private void MoveForward()
    {
        wheelVehicle.Steering = 0f;
        wheelVehicle.Throttle = 1f;
    }

    public void Move(float steering, float accel)
    {
        if (!moveForward)
        {
            if (!handbrake && isGround)
            {
                wheelVehicle.Steering = steering;
                wheelVehicle.Throttle = accel;
                wheelVehicle.Handbrake = false;
            }
            else
            {
                wheelVehicle.Steering = 0f;
                wheelVehicle.Handbrake = true;
            }
        }
    }

    public void SlowDownToDesiredSpeed(float desiredSpeed)
    {
        StartCoroutine(SlowDown(desiredSpeed));
    }

    public void SetNewWaypointsPath(RaceWaypointsPath newPath)
    {
        raceAIInputs.IsPathMovement = true;
        raceWaypointsPath = newPath;
        raceAIWaypointTracker.SetNewWaypointsPath(newPath);
        moveForward = false;
    }

    public void WasRespawn()
    {
        if (raceAIInputs.IsPathMovement) ResetWaypointsPath();
        else ResetMovementToTarget();
    }

    private void ResetWaypointsPath()
    {
        raceAIWaypointTracker.SetNewWaypointsPath(raceWaypointsPath);
        moveForward = false;
    }

    private void ResetMovementToTarget()
    {
        raceAIInputs.ResetTargets();
    }

    public void SetTargets(List<Transform> targets)// вызывается сектором с целями. Передает список платформ
    {
        raceAIInputs.SetTargets(targets);
        ResetMovementToTarget();
        raceAIInputs.IsPathMovement = false;
        moveForward = false;
    }

    private IEnumerator SlowDown(float desiredSpeed)
    {
        while (currentSpeed > desiredSpeed)
        {
            handbrake = true;
            yield return null;
        }

        handbrake = false;
    }
}
