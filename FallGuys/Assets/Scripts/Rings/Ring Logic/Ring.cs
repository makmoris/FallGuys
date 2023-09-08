using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private EnemyPointer enemyPointer;

    private void Awake()
    {
        enemyPointer = GetComponentInChildren<EnemyPointer>();
    }

    public void Initialize(LevelUI levelUI)
    {
        enemyPointer.Initialize(levelUI);
    }
}
