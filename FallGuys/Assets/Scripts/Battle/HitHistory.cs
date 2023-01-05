using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHistory : MonoBehaviour
{
    private GameObject lastShooter;// ��������� ���������� � ������ 
    [SerializeField] private float resetLastShooterTime = 2f;

    private Coroutine resetLastShooter = null;

    public void SetLastShooter(GameObject _shooter) // ����� � �������� ��� ����, ����� ���������, ����� �� ����� � ����� ��� ��� ��� ��������� 
    {           // ���������. ���� �� ������ ��������� � DeadZone � �������� ���� lastShooter, �� ���� Shooter ��� � �������� ����� ���������.
        lastShooter = _shooter;

        if (resetLastShooter != null) StopCoroutine(resetLastShooter);
        resetLastShooter = StartCoroutine(ResetLastShooter());
    }

    public GameObject GetLastShooter()// �������� VisualIntermediary, ����� ���������, ������ �� ������ ����
    {
        return lastShooter;
    }

    IEnumerator ResetLastShooter()
    {
        yield return new WaitForSeconds(resetLastShooterTime); // ����� ������ �������. ���� ���� ����� 1 �������, ������ ��� �� ������� ��� �������

        lastShooter = null;
    }
}
