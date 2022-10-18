using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField]private GameObject currentTargetObject;// ������� ����. ���� ����, �� ����. ���� ����, �� null
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
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// ���� ���������� ������ ��������� � ��������� �������� �����, �� ����� ���������
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

    private void CurrentTargetObjectWasDestroyed(GameObject objWithAttackPointer)
    {
        if (objWithAttackPointer == currentTargetObject)
        {
            PointerManager.Instance.ObjectWithAttackPointerWasDestroyed();
            cannon.ReturnWeaponToPosition();
        }
    }

    private void OnEnable()
    {
        AttackPointer.attackPointerWasDestroyedEvent += CurrentTargetObjectWasDestroyed;
    }

    private void OnDisable()
    {
        AttackPointer.attackPointerWasDestroyedEvent -= CurrentTargetObjectWasDestroyed;
    }
}
