using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelUI : MonoBehaviour, ILevelUI
{
    [Header("UI Pointers")]
    [SerializeField] protected UIEnemyPointers uiEnemyPointers;
    public UIEnemyPointers UIEnemyPointers { get => uiEnemyPointers; }

    public abstract void UpdatePlayerHP(float hpValue);
    public abstract void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer);


    public abstract void ShowAttackPointer(Transform targetTransform);
    public abstract void ShowingAttackPointer(Transform targetTransform);
    public abstract void HideAttackPointer(Transform targetTransform);
    public abstract void ObjectWithAttackPointerWasDestroyed();


    public abstract void ShowEnemyPositionPointer(EnemyPointer enemyPointer);
    public abstract void HideEnemyPositionPointer(EnemyPointer enemyPointer, bool remove);
}
