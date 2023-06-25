using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PostLevelResultVisualization : MonoBehaviour
{
    protected GameManager gameManager;
    public GameManager GameManager { set => gameManager = value; }

    public abstract void ShowPlayerWinWindow(List<GameObject> losers, float timeBeforeLoadNextLevel);

    public abstract void ShowPlayerLoseWindow();
}
