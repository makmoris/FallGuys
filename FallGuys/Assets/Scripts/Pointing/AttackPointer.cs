using UnityEngine;
using System;

public class AttackPointer : MonoBehaviour // �������� �� ��� �������, � ������� ����� ��������
{
    [SerializeField] private int priorityIndex;

    public delegate void attackPointerWasDeactivated(GameObject objWithAttackPointer);
    public static event attackPointerWasDeactivated attackPointerWasDeactivatedEvent;

    private void Awake()
    {
        if (gameObject.layer != 7) gameObject.layer = 7; // 7 - AttackPointer layer
    }

    public int GetPriorityIndex()
    {
        return priorityIndex;
    }

    private void OnDestroy() // ��� �� ����� ������, ��������, ��� SetActive(false)
    {
        //Debug.Log("Destroy");
        attackPointerWasDeactivatedEvent?.Invoke(gameObject);
    }

    private void OnDisable()
    {
        //Debug.Log("Disable");
        attackPointerWasDeactivatedEvent?.Invoke(gameObject);
    }
}
