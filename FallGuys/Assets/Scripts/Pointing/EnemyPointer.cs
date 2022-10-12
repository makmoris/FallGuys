using UnityEngine;

public class EnemyPointer : MonoBehaviour // ������ �� ������
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
