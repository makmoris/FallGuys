using UnityEngine;

public class EnemyPointer : MonoBehaviour // кидаем на врагов
{
    private bool onStart = true;

    private void Start()
    {
        PointerManager.Instance.AddToList(this);
    }

    //private void Awake()
    //{
    //    PointerManager.Instance.AddToList(this);
    //    addedToList = true;
    //}

    //private void OnDestroy()
    //{
    //    PointerManager.Instance.RemoveFromList(this);
    //}

    private void OnEnable()
    {
        if (!onStart)
        {
            PointerManager.Instance.AddToList(this);
        }
    }

    private void OnDisable()
    {
        PointerManager.Instance.RemoveFromList(this);
        onStart = false;
    }
}
