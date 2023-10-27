using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsUI : LevelUI
{
    public override void AddEnemyPointer(EnemyPointer enemyPointer) { }

    #region Attack Pointer
    public override void ShowAttackPointer(Transform targetTransform)
    {
        uiEnemyPointers.ShowAttackPointer(targetTransform);
    }
    public override void ShowingAttackPointer(Transform targetTransform)
    {
        uiEnemyPointers.ShowingAttackPointer(targetTransform);
    }
    public override void HideAttackPointerWithTarget(Transform targetTransform)
    {
        uiEnemyPointers.HideAttackPointerWithTarget(targetTransform);
    }
    public override void ObjectWithAttackPointerWasDestroyed()
    {
        uiEnemyPointers.ObjectWithAttackPointerWasDestroyed();
    }
    public override void HideAttackPointer()
    {
        uiEnemyPointers.HideAttackPointer();
    }
    #endregion


    #region Enemy Position Pointer
    public override void ShowEnemyPositionPointer(EnemyPointer enemyPointer) 
    {
        uiEnemyPointers.ShowEnemyPositionPointer(enemyPointer);
    }

    public override void HideEnemyPositionPointer(EnemyPointer enemyPointer, bool remove)
    { 
        uiEnemyPointers.HideEnemyPositionPointer(enemyPointer, remove); 
    }
    #endregion


    #region HP
    public override void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer) { }

    public override void UpdateCurrentPlayerHP(float hpValue) { }
    #endregion
}
