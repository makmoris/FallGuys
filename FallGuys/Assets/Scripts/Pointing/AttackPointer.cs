using UnityEngine;
using System;

public class AttackPointer : MonoBehaviour // вешается на все объекты, в которые можно стрелять
{
    public delegate void attackPointerWasDestroyed(GameObject objWithAttackPointer);
    public static event attackPointerWasDestroyed attackPointerWasDestroyedEvent;

    private void Awake()
    {
        if (gameObject.layer != 7) gameObject.layer = 7; // 7 - AttackPointer layer
    }

    private void OnDestroy() // или на любой другой, например, при SetActive(false)
    {
        attackPointerWasDestroyedEvent?.Invoke(gameObject);
    }
}
