using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PostLevelResultVisualization : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager { set => gameManager = value; }

    public abstract void ShowPlayerWinWindow(List<GameObject> losers);

    public abstract void ShowPlayerLoseWindow();
}
