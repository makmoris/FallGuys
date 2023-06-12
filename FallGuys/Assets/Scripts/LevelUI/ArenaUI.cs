using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaUI : LevelUI
{
    [Header("Player HP")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

    [Header("UI Pointers")]
    [SerializeField] private ArenaUIPointers arenaUIPointers;// переименовать в ArenaUIPointers

    public override void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer)
    {
        arenaUIPointers.UpdateEnemyHP(hpValue, enemyPointer);
    }

    public override void UpdatePlayerHP(float hpValue)
    {
        playerHealthUI.UpdatePlayerHealthUI(hpValue);
    }
}
