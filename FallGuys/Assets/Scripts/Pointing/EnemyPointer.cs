using UnityEngine;

public class EnemyPointer : MonoBehaviour // ������ �� ������
{
    private bool onStart = true;

    private void Awake()
    {
        PointerManager.Instance.AddToPositionList(this);
    }

    //private void Start()
    //{
    //    PointerManager.Instance.AddToPositionList(this);
    //}

    private void OnEnable()
    {
        if (!onStart)
        {
            PointerManager.Instance.AddToPositionList(this);
        }
    }

    private void OnDisable()
    {
        PointerManager.Instance.RemoveFromPositionList(this);
        onStart = false;
    }
}
