using System;
using UnityEngine;

public class Bumper : MonoBehaviour /* ��������������������������� */ // �� ������� ������ ������ ����� ������ ���� ������
{
    public event Action<Bonus> OnBonusGot;

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
                    Debug.Log("��������� ���� � ������");
                }
            }
            else
            {
                Debug.Log("Event GetBonus" + other.name);
                OnBonusGot?.Invoke(bonus);
                bonus.Got();
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
