using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
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
            //PointerManager.Instance.AddToPositionList(this);
            PointerManager.Instance.ShowPositionPointer(this);
        }
    }

    private void OnDisable()
    {
        //PointerManager.Instance.RemoveFromPositionList(this);
        PointerManager.Instance.HidePositionPointer(this);
        onStart = false;
    }

    private void OnDestroy()
    {
        PointerManager.Instance.RemoveFromPositionList(this);
    }
}
