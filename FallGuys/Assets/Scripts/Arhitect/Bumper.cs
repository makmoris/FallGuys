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
            OnBonusGot?.Invoke(bonus);
            bonus.Got();
        }
    }
}
