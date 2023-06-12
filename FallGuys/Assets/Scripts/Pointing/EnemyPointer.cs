using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
{
    private bool onStart = true;

    private void OnEnable()
    {
        if (!onStart)
        {
            ArenaUIPointers.Instance.ShowPositionPointer(this);
        }
    }

    private void OnDisable()
    {
        ArenaUIPointers.Instance.HidePositionPointer(this);
        onStart = false;
    }

    private void OnDestroy()
    {
        ArenaUIPointers.Instance.RemoveFromPositionList(this);
    }
}
