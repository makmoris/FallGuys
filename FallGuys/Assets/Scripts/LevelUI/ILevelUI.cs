using UnityEngine;

public interface ILevelUI
{
    public UIEnemyPointers UIEnemyPointers { get; }

    public void AddEnemyPointer(EnemyPointer enemyPointer);

    public void UpdateCurrentPlayerHP(float hpValue);
    public void UpdateEnemyHP(float hpValue, EnemyPointer enemyPointer);

    public void ShowAttackPointer(Transform targetTransform);
    public void ShowingAttackPointer(Transform targetTransform);
    public void HideAttackPointer(Transform targetTransform);
    public void ObjectWithAttackPointerWasDestroyed();

    public void ShowEnemyPositionPointer(EnemyPointer enemyPointer);
    public void HideEnemyPositionPointer(EnemyPointer enemyPointer, bool remove);
}
