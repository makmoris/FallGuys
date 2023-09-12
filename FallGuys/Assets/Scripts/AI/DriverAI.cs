using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DriverAI : MonoBehaviour
{
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

    public abstract void Initialize(GameObject aiPlayerGO , GameObject currentPlayerGO);
}
