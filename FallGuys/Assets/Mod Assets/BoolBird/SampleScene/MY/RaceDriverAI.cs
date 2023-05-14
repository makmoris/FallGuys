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

    #region PERSUIT AI

    public bool persuitAiOn;
    public GameObject persuitTarget;
    public float persuitDistance;

    #endregion

   // public RaceAIInputs raceAIInputs;
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

    private float currentSpeed;
    internal float CurrentSpeed
    {
        get => currentSpeed;
    }

    private bool isGround;
    internal bool IsGround
    {
        get => isGround;
        set => isGround = value;
    }

    private bool handbrake;
    internal bool Handbrake
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

        //raceAIInputs = gameObject.AddComponent<RaceAIInputs>();
        raceAIWaypointTracker = gameObject.AddComponent<RaceAIWaipointTracker>();

        #endregion

        #region AI REFERENCES

        carAItargetObj = new GameObject("WaypointsTarget");
        //carAItargetObj.transform.parent = this.transform.GetChild(1);
        carAItarget = carAItargetObj.transform;

        #endregion

        isGround = true;

        moveForward = true;
    }

    private void Update()
    {
        currentSpeed = rb.velocity.magnitude * 3.6f;

        if (moveForward)
        {
            MoveForward();
        }
    }

    internal void MoveForward()
    {
        wheelVehicle.Steering = 0f;
        wheelVehicle.Throttle = 1f;
    }

    public void Move(float steering, float accel, float footbrake)
    {
        if (!moveForward)
        {
            if (!handbrake)
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

    internal void SlowDownToDesiredSpeed(float desiredSpeed)
    {
        StartCoroutine(SlowDown(desiredSpeed));
    }

    private IEnumerator SlowDown(float desiredSpeed)
    {
        while (currentSpeed > desiredSpeed)
        {
            handbrake = true;
            yield return null;
        }

        handbrake = false;

        //yield return new WaitForSeconds(1.5f);
        //wheelVehicle.Handbrake = false;
        //handbrake = false;
    }

    public void SetNewWaypointsPath(RaceWaypointsPath newPath)
    {
        raceAIWaypointTracker.SetNewWaypointsPath(newPath);
        moveForward = false;
    }
}
