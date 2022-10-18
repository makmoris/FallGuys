using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField]private GameObject currentTargetObject;// ������� ����. ���� ����, �� ����. ���� ����, �� null
    [SerializeField] private Weapon weapon;// ������ �� �����, ������ ������� ��������� ��������

    private bool isAI;
    private CarDriverAI driverAI;

    private void Start()
    {
        driverAI = transform.GetComponentInParent<CarDriverAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� �� ������ ��������(�������) ����
        if (currentTargetObject == null)
        {// ���������, � ����� ������� ���� ������ ������?
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (!isAI) PointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
                else driverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// ���� ���������� ������ ��������� � ��������� �������� �����, �� ����� ���������
        {
             if (!isAI) PointerManager.Instance.ShowAttackPointer(currentTargetObject.transform);// ���������� ������ ������� �� �������
            else driverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform);

            // ��������� ���������
            weapon.SetObjectForAttack(currentTargetObject.transform);

        }// ���� ������� ���������� ������ ������ ������� � ������� ���� ������, �� ����� ������������ �� ������ ���� � ��������
        else if (currentTargetObject == null)
        {
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (!isAI) PointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
                else driverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTargetObject)
        {
            if (!isAI) PointerManager.Instance.FinishShowingAttackPointer(currentTargetObject.transform);
            else driverAI.DetectorLostTarget();

            currentTargetObject = null;
            weapon.ReturnWeaponToPosition();
        }
    }

    private void CurrentTargetObjectWasDeactivated(GameObject objWithAttackPointer)
    {
        if (objWithAttackPointer == currentTargetObject)
        {
            if (!isAI) PointerManager.Instance.ObjectWithAttackPointerWasDestroyed();
            else driverAI.DetectorLostTarget();

            weapon.ReturnWeaponToPosition();
            currentTargetObject = null;
        }
    }

    public void IsAI(bool value)
    {
        isAI = value;
    }

    private void OnEnable()
    {
        AttackPointer.attackPointerWasDeactivatedEvent += CurrentTargetObjectWasDeactivated;
    }

    private void OnDisable()
    {
        AttackPointer.attackPointerWasDeactivatedEvent -= CurrentTargetObjectWasDeactivated;
    }
}
