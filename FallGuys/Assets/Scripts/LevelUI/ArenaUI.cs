using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaUI : LevelUI
{
    [Header("Player HP")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

    public override void ShowAttackPointer(Transform targetTransform)
    {
        uiEnemyPointers.ShowAttackPointer(targetTransform);
    }
    public override void ShowingAttackPointer(Transform targetTransform)
    {
        uiEnemyPointers.ShowingAttackPointer(targetTransform);
    }
    public override void HideAttackPointer(Transform targetTransform)
    {
        uiEnemyPointers.HideAttackPointer(targetTransform);
    }
    public override void ObjectWithAttackPointerWasDestroyed()
    {
        uiEnemyPointers.ObjectWithAttackPointerWasDestroyed();
    }


    public override void ShowEnemyPositionPointer(EnemyPointer enemyPointer)
    {
        uiEnemyPointers.ShowEnemyPositionPointer(enemyPointer);
    }

    public override void HideEnemyPositionPointer(EnemyPointer enemyPointer, bool remove)
    {
        uiEnemyPointers.HideEnemyPositionPointer(enemyPointer, remove);
    }


    public override void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer)
    {
        uiEnemyPointers.UpdateEnemyHP(hpValue, enemyPointer);
    }

    public override void UpdatePlayerHP(float hpValue)
    {
        playerHealthUI.UpdatePlayerHealthUI(hpValue);
    }
}
