using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    private GameObject currentTargetObject;// ������� ����. ���� ����, �� ����. ���� ����, �� null
    [SerializeField] private Cannon cannon;// ������ �� �����, ������ ������� ��������� ��������


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
            // ��������� ���������
            cannon.SetObjectForAttack(currentTargetObject.transform);

        }// ���� ������� ���������� ������ ������ ������� � ������� ���� ������, �� ����� ������������ �� ������ ���� � ��������
        else if (currentTargetObject == null)
        {
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;
                PointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
            }
            else
            {
                //Debug.Log(other.name);
                //Debug.Log("Destoyed");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTargetObject)
        {
            PointerManager.Instance.FinishShowingAttackPointer(currentTargetObject.transform);
            currentTargetObject = null;
            cannon.ReturnWeaponToPosition();
        }
    }
}
