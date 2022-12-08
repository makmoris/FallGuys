using System;
using UnityEngine;

public class Bumper : MonoBehaviour /* ��������������������������� */ // �� ������� ������ ������ ����� ������ ���� ������
{
    public event Action<Bonus> OnBonusGot;

    private void Start()
    {
        // ��� ������ enabled = true/false
    }

    private void OnTriggerEnter(Collider other)
    {
        var bonus = other.GetComponent<Bonus>();
        if (bonus != null)
        {
            Bullet bullet = bonus.GetComponent<Bullet>();
            if (bullet != null)// ���� "�����" �������� �����
            {
                GameObject bulletParent = bullet.GetParent();// �������, ��� ��� ���� ��������. ���� "�������" ��������� � �������� �������
                if (bulletParent != this.gameObject)                 // ������ ��� ���� � ��� �� �����. �� ���� �� ������ ����.
                {
                    OnBonusGot?.Invoke(bonus);
                    bonus.Got();
                    Debug.Log($"��������� ���� � ������");
                }
            }
            else
            {
                Explosion explosion = bonus.GetComponent<Explosion>();
                if (explosion == null)// ����� ���������� ����� public GetBonus. ����� �������� ����� ������ �����. + ����� �� ����� ����� ������
                {
                    if(other.GetComponent<DeadZone>() != null && enabled) // �������, �.�. ���� �� ��������� �������
                    {
                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();

                        enabled = false; // enabled = true ������� DeadZone �� �����
                    }

                    if (enabled)
                    {
                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();
                    }

                    Debug.Log($"{name} Event GetBonus {other.name}");
                }
            }
        }
    }

    public void GetBonus(Bonus bonus)// ���������� ������� ��� ����������
    {
        Debug.Log("Public GetBonus");
        OnBonusGot?.Invoke(bonus);
        bonus.Got();
    }
}
