using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
{
    private bool onStart = true;

    private void Awake()
    {
        ArenaPointerManager.Instance.AddToPositionList(this);
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
            ArenaPointerManager.Instance.ShowPositionPointer(this);
        }
    }

    private void OnDisable()
    {
        //PointerManager.Instance.RemoveFromPositionList(this);
        ArenaPointerManager.Instance.HidePositionPointer(this);
        onStart = false;
    }

    private void OnDestroy()
    {
        ArenaPointerManager.Instance.RemoveFromPositionList(this);
    }
}
