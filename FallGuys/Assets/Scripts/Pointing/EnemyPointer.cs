using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
{

    private void Start()
    {
        PointerManager.Instance.AddToList(this);
    }

    private void OnDestroy()
    {
        PointerManager.Instance.RemoveFromList(this);
    }
}
