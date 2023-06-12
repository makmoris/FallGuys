using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelUI : MonoBehaviour, ILevelUI
{
    public abstract void UpdatePlayerHP(float hpValue);
    public abstract void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer);
}
