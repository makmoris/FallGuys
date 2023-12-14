using ArcadeVP;
using PunchCars.DifficultyAILevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceDriverAI : DriverAI
{
    private ArcadeVehicleController arcadeVehicleController;
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
            arcadeVehicleController.Handbrake = handbrake;
        }
    }

    private GameObject currentPlayer;

    //private AnyCarAI anyCarAI;
    private WaypointProgressTracker waypointProgressTracker;

    public override void Initialize(GameObject aiPlayerGO, GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel)
    {
        arcadeVehicleController = aiPlayerGO.GetComponent<ArcadeVehicleController>();
        rb = aiPlayerGO.GetComponent<Rigidbody>();

        arcadeVehicleController.SetIsRaceAI();

        isGround = true;

        waypointProgressTracker = GetComponent<WaypointProgressTracker>();
        waypointProgressTracker.Initialize();

        if (currentPlayerGO != null)
        {
            currentPlayer = currentPlayerGO;

            ChoseDifficultyLevel(difficultyAILevel);
        }
    }

    #region Difficulty AI Levels
    public override void SetLowDifficultyAILevel()
    {
        CarPlayerWaipointTracker carPlayerWaipointTracker = currentPlayer.GetComponent<CarPlayerWaipointTracker>();
        arcadeVehicleController.ActivateSpeedControlWithPlayer(carPlayerWaipointTracker, waypointProgressTracker);
    }

    public override void SetNormalDifficultyAILevel()
    {
        CarPlayerWaipointTracker carPlayerWaipointTracker = currentPlayer.GetComponent<CarPlayerWaipointTracker>();
        arcadeVehicleController.ActivateSpeedControlWithPlayer(carPlayerWaipointTracker, waypointProgressTracker);
    }

    public override void SetHighDifficultyAILevel()
    {
        CarPlayerWaipointTracker carPlayerWaipointTracker = currentPlayer.GetComponent<CarPlayerWaipointTracker>();
        arcadeVehicleController.ActivateSpeedControlWithPlayer(carPlayerWaipointTracker, waypointProgressTracker);
    }
    #endregion

    public void WasRespawn()
    {
        waypointProgressTracker.WasRespawn();
    }

    public void SaveCheckpoint()
    {
        waypointProgressTracker.SaveCheckpoint();
    }
}
