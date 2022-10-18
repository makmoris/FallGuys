using UnityEngine;
using System;

public class AttackPointer : MonoBehaviour // �������� �� ��� �������, � ������� ����� ��������
{
    public delegate void attackPointerWasDeactivated(GameObject objWithAttackPointer);
    public static event attackPointerWasDeactivated attackPointerWasDeactivatedEvent;

    private void Awake()
    {
        if (gameObject.layer != 7) gameObject.layer = 7; // 7 - AttackPointer layer
    }

    //private void OnDestroy() // ��� �� ����� ������, ��������, ��� SetActive(false)
    //{
    //    attackPointerWasDestroyedEvent?.Invoke(gameObject);
    //}

    private void OnDisable()
    {
        attackPointerWasDeactivatedEvent?.Invoke(gameObject);
    }
}
