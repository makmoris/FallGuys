using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceUI : LevelUI
{



    #region Attack Pointer
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
    #endregion


    #region Enemy Position Pointer
    public override void ShowEnemyPositionPointer(EnemyPointer enemyPointer) { }

    public override void HideEnemyPositionPointer(EnemyPointer enemyPointer, bool remove) { }
    #endregion


    #region HP
    public override void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer) { }

    public override void UpdateCurrentPlayerHP(float hpValue) { }
    #endregion
}
