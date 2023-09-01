using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    private GameObject currentTargetObject;// ������� ����. ���� ����, �� ����. ���� ����, �� null
    [SerializeField] private Weapon weapon;// ������ �� �����, ������ ������� ��������� ��������

    private bool isAI;
    private ArenaCarDriverAI arenaDriverAI;

    private LevelUI levelUI;
    public LevelUI LevelUI
    {
        set => levelUI = value;
    }

    private void Awake()
    {
        arenaDriverAI = transform.root.GetComponentInChildren<ArenaCarDriverAI>();
    }
    private void Start()
    {
        //driverAI = transform.GetComponentInParent<CarDriverAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� �� ������ ��������(�������) ����
        if (currentTargetObject == null)
        {// ���������, � ����� ������� ���� ������ ������?
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (!isAI) levelUI.ShowAttackPointer(currentTargetObject.transform);
                else if(arenaDriverAI != null) arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// ���� ���������� ������ ��������� � ��������� �������� �����, �� ����� ���������
        {
            if (!isAI) levelUI.ShowingAttackPointer(currentTargetObject.transform);
            else if (arenaDriverAI != null) arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);

            // ��������� ���������
            weapon.SetObjectForAttack(currentTargetObject.transform);

        }// ���� ������� ���������� ������ ������ ������� � ������� ���� ������, �� ����� ������������ �� ������ ���� � ��������
        else if (currentTargetObject == null)
        {
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (!isAI) levelUI.ShowAttackPointer(currentTargetObject.transform);
                else if (arenaDriverAI != null) arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTargetObject)
        {
            if (!isAI) levelUI.HideAttackPointer(currentTargetObject.transform);
            else if (arenaDriverAI != null) arenaDriverAI.DetectorLostTarget();

            currentTargetObject = null;
            weapon.ReturnWeaponToPosition();
        }
    }

    private void CurrentTargetObjectWasDeactivated(GameObject objWithAttackPointer)
    {
        if (objWithAttackPointer == currentTargetObject)
        {
            if (!isAI) levelUI.ObjectWithAttackPointerWasDestroyed();
            else if (arenaDriverAI != null) arenaDriverAI.DetectorLostTarget();

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
