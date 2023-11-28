using PunchCars.DifficultyAILevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceDriverAI : DriverAI
{
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

    [SerializeField]private bool handbrake;
    public bool Handbrake
    {
        get => handbrake;
        set
        {
            handbrake = value;
            anyCarAI.Handbrake = handbrake;
            wheelVehicle.Handbrake = handbrake;
        }
    }

    private GameObject currentPlayer;

    private AnyCarAI anyCarAI;

    public override void Initialize(GameObject aiPlayerGO, GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel)
    {
        wheelVehicle = aiPlayerGO.GetComponent<WheelVehicle>();
        rb = aiPlayerGO.GetComponent<Rigidbody>();

        currentPlayer = currentPlayerGO;

        isGround = true;

        anyCarAI = GetComponent<AnyCarAI>();
        RaceProgressController raceProgressController = FindObjectOfType<RaceProgressController>();
        anyCarAI.Initialize(raceProgressController.GetWaypointsPath(), true);

        ChoseDifficultyLevel(difficultyAILevel);
    }

    #region Difficulty AI Levels
    public override void SetLowDifficultyAILevel()
    {
        CarPlayerWaipointTracker carPlayerWaipointTracker = currentPlayer.GetComponent<CarPlayerWaipointTracker>();
        anyCarAI.ActivateSpeedControlWithPlayer(carPlayerWaipointTracker);
    }

    public override void SetNormalDifficultyAILevel()
    {
        
    }

    public override void SetHighDifficultyAILevel()
    {
        
    }
    #endregion

    public void WasRespawn()
    {
        anyCarAI.WasRespawn();
    }

    public void SaveCheckpoint()
    {
        anyCarAI.SaveCheckpoint();
    }
}
