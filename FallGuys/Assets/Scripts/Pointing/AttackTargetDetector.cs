using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField] private GameObject currentTargetObject;// ������� ����. ���� ����, �� ����. ���� ����, �� null

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� �� ������ ��������(�������) ����
        if (currentTargetObject == null)
        {// ���������, � ����� ������� ���� ������ ������?
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;
                PointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == currentTargetObject)// ���� ���������� ������ ��������� � ��������� �������� �����, �� ����� ���������
        {
            PointerManager.Instance.ShowAttackPointer(currentTargetObject.transform);// ���������� ������ ������� �� �������
            // �������
        }// ���� ������� ���������� ������ ������ ������� � ������� ���� ������, �� ����� ������������ �� ������ ���� � ��������
        else if (currentTargetObject == null)
        {
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;
                PointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTargetObject)
        {
            PointerManager.Instance.FinishShowingAttackPointer(currentTargetObject.transform);
            currentTargetObject = null;
        }
    }
}
