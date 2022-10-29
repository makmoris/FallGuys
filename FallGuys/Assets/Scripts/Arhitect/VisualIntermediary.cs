using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualIntermediary : MonoBehaviour // ����� �� ������ � �������� �� ���������� �������������. UI, ������ ����������� � �.�.
{
    private EnemyPointer enemyPointer;
    private bool isPlayer = true;

    public static event Action<GameObject> PlayerWasDeadEvent; // ������ ������� ���� ����������������

    private void Awake()
    {
        Transform enemyPointerTransform = transform.Find("EnemyPointer");
        if (enemyPointerTransform != null)
        {
            enemyPointer = enemyPointerTransform.GetComponent<EnemyPointer>();
            isPlayer = false;
        }
    }

    public void UpdateHealthInUI(float healthValue)
    {
        if (enemyPointer != null) PointerManager.Instance.UpdateHealthInUI(enemyPointer, healthValue);
        else PointerManager.Instance.UpdatePlayerHealthInUI(healthValue);
    }

    public void DestroyCar()
    {
        if (isPlayer) PointerManager.Instance.UpdatePlayerHealthInUI(-1000f);// ��� ��������, ����� ������� �� ������� 0
        else
        {

        }
        PlayerWasDeadEvent?.Invoke(this.gameObject);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
