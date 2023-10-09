using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
{
    private bool onStart = true;

    private LevelUI levelUI;
    //public LevelUI LevelUI
    //{
    //    set => levelUI = value;
    //}

    public void Initialize(LevelUI levelUI)
    {
        this.levelUI = levelUI;
        levelUI.AddEnemyPointer(this);
    }

    private void OnEnable()
    {
        if (!onStart)
        {
            if (levelUI != null) levelUI.ShowEnemyPositionPointer(this);
        }
    }

    private void OnDisable()
    {
        if (levelUI != null) levelUI.HideEnemyPositionPointer(this, false);
        onStart = false;
    }

    private void OnDestroy()
    {
        if (levelUI != null) levelUI.HideEnemyPositionPointer(this, true);
    }
}
