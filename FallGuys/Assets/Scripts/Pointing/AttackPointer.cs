using UnityEngine;

public class AttackPointer : MonoBehaviour // �������� �� ��� �������, � ������� ����� ��������
{

    private void Awake()
    {
        if (gameObject.layer != 7) gameObject.layer = 7; // 7 - AttackPointer layer
    }
}
