using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
{
    private bool onStart = true;

    private LevelUI levelUI;
    public LevelUI LevelUI
    {
        set => levelUI = value;
    }


    private void OnEnable()
    {
        if (!onStart)
        {
            levelUI.ShowEnemyPositionPointer(this);
        }
    }

    private void OnDisable()
    {
        levelUI.HideEnemyPositionPointer(this, false);
        onStart = false;
    }

    private void OnDestroy()
    {
        levelUI.HideEnemyPositionPointer(this, true);
    }
}
