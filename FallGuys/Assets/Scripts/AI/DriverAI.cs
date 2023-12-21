using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.DifficultyAILevels;
using ArcadeVP;

public abstract class DriverAI : MonoBehaviour, IDifficultyAILevels
{
    [Header("AI Car Settings")]
    [SerializeField] protected bool useAISettings;
    [SerializeField] protected float aiMaxSpeed;
    [SerializeField] protected float aiAccelaration;
    [SerializeField] protected float aiTurn;

    protected bool obstacle;
    public bool Obstacle
    {
        get => obstacle;
        set => obstacle = value;
    }

    protected float obstacleSteer;
    public float ObstacleSteer
    {
        get => obstacleSteer;
        set => obstacleSteer = value;
    }

    public abstract void Initialize(GameObject aiPlayerGO , GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel);

    public abstract void SetLowDifficultyAILevel();

    public abstract void SetNormalDifficultyAILevel();

    public abstract void SetHighDifficultyAILevel();

    public abstract void CheckOnUseAISettings(ArcadeVehicleController arcadeVehicleController);

    protected void ChoseDifficultyLevel(EnumDifficultyAILevels difficultyAILevel)
    {
        switch (difficultyAILevel)
        {
            case EnumDifficultyAILevels.Low:
                SetLowDifficultyAILevel();
                break;

            case EnumDifficultyAILevels.Normal:
                SetNormalDifficultyAILevel();
                break;

            case EnumDifficultyAILevels.High:
                SetHighDifficultyAILevel();
                break;
        }
    }
}
