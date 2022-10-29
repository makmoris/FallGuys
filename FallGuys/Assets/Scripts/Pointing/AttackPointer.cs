using UnityEngine;
using System;

public class AttackPointer : MonoBehaviour // вешается на все объекты, в которые можно стрелять
{
    public delegate void attackPointerWasDeactivated(GameObject objWithAttackPointer);
    public static event attackPointerWasDeactivated attackPointerWasDeactivatedEvent;

    private void Awake()
    {
        if (gameObject.layer != 7) gameObject.layer = 7; // 7 - AttackPointer layer
    }

    private void OnDestroy() // или на любой другой, например, при SetActive(false)
    {
        Debug.Log("Destroy");
        attackPointerWasDeactivatedEvent?.Invoke(gameObject);
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        attackPointerWasDeactivatedEvent?.Invoke(gameObject);
    }
}
