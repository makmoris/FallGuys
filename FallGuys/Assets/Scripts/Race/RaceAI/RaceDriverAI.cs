using PunchCars.DifficultyAILevels;
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

public class RaceDriverAI : DriverAI
{
    private RaceAIInputs raceAIInputs;
    private RaceAIWaipointTracker raceAIWaypointTracker;
    private RaceObstacleDetectionAI raceObstacleDetectionAI;

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

    [SerializeField]private bool brake;
    public bool Brake
    {
        get => brake;
        set => brake = value;
    }

    private bool isPathMovement;

    private GameObject currentPlayer;
    private float speedCoefficient = 1f;

    private bool isInit;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        if(_coroutine == null && isInit) _coroutine = StartCoroutine(CheckForPlayerFellBehindInRace());
    }

    public override void Initialize(GameObject aiPlayerGO, GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel)
    {
        wheelVehicle = aiPlayerGO.GetComponent<WheelVehicle>();
        rb = aiPlayerGO.GetComponent<Rigidbody>();

        currentPlayer = currentPlayerGO;

        maxSpeed = wheelVehicle.MaxSpeed;
        maximumSteerAngle = wheelVehicle.SteerAngle;

        #region FEATURES SCRIPTS

        raceAIInputs = gameObject.AddComponent<RaceAIInputs>();
        raceAIWaypointTracker = gameObject.AddComponent<RaceAIWaipointTracker>();

        //raceObstacleDetectionAI = GetComponentInParent<RaceObstacleDetectionAI>();
        //raceObstacleDetectionAI.Initialize(this);

        #endregion

        #region AI REFERENCES

        carAItargetObj = new GameObject("WaypointsTarget");
        //carAItargetObj.transform.parent = this.transform.GetChild(1);
        carAItarget = carAItargetObj.transform;

        #endregion

        isGround = true;

        ChoseDifficultyLevel(difficultyAILevel);

        isInit = true;
    }

    #region Difficulty AI Levels
    public override void SetLowDifficultyAILevel()
    {
        if (_coroutine == null) _coroutine = StartCoroutine(CheckForPlayerFellBehindInRace());
    }

    public override void SetNormalDifficultyAILevel()
    {
        
    }

    public override void SetHighDifficultyAILevel()
    {
        
    }
    #endregion

    private void FixedUpdate()
    {
        if (isPathMovement)
        {
            raceAIInputs.PathMovement();
        }
        else
        {
            raceAIInputs.MovementToTarget();
        }


        currentSpeed = wheelVehicle.Speed;

        if (moveForward)
        {
            MoveForward();
        }
    }

    public void StartMoveForward()
    {
        moveForward = true;
    }

    public void StopMoveForward()
    {
        moveForward = false;
    }

    private void MoveForward()
    {
        wheelVehicle.Steering = 0f;
        wheelVehicle.Throttle = 1f * speedCoefficient;
    }

    public void Move(float steering, float accel)
    {
        if (!obstacle)
        {
            if (!moveForward)
            {
                if (!brake && isGround)
                {
                    wheelVehicle.Steering = steering;
                    wheelVehicle.Throttle = accel * speedCoefficient;
                    wheelVehicle.Handbrake = false;
                }
                else
                {
                    wheelVehicle.Steering = 0f;

                    if (currentSpeed > 1f)
                    {
                        wheelVehicle.Handbrake = true;
                    }
                    else wheelVehicle.Handbrake = false;
                }
            }
        }
        else
        {
            wheelVehicle.Steering = obstacleSteer;
            wheelVehicle.Throttle = accel * speedCoefficient;
        }
    }

    public void SlowDownToDesiredSpeed(float desiredSpeed)
    {
        StartCoroutine(SlowDown(desiredSpeed));
    }

    public void SetNewWaypointsPath(RaceWaypointsPath newPath)
    {
        isPathMovement = true;
        raceWaypointsPath = newPath;
        raceAIWaypointTracker.SetNewWaypointsPath(newPath);
        moveForward = false;
    }

    public void WasRespawn()
    {
        if (isPathMovement) ResetWaypointsPath();
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
        isPathMovement = false;
        moveForward = false;
    }

    private IEnumerator SlowDown(float desiredSpeed)
    {
        while (currentSpeed > desiredSpeed)
        {
            brake = true;
            wheelVehicle.Throttle = -1f * speedCoefficient;
            yield return null;
        }

        brake = false;
        wheelVehicle.Throttle = 0f * speedCoefficient;
    }

    IEnumerator CheckForPlayerFellBehindInRace()
    {
        while (true)
        {
            if (IsPlayerBehind())
            {
                speedCoefficient = 0.5f;
            }
            else speedCoefficient = 1f;

            yield return new WaitForSeconds(0.25f);
        }
    }

    private bool IsPlayerBehind()
    {
        Vector3 forward = Vector3.forward;
        Vector3 toOther = currentPlayer.transform.position - transform.position;

        if (Vector3.Dot(forward, toOther) < 0)
        {
            return true;
        }
        else return false;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
