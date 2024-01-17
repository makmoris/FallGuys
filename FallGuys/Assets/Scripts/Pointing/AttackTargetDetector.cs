using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    private GameObject currentTargetObject;// текущая цель. Если есть, то есть. Если нету, то null
    [SerializeField] private Weapon weapon;// ссылка на пушку, внутри которой находится детектор

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // проверяем, есть ли сейчас активная(текущая) цель
        if (currentTargetObject == null)
        {// проверяем, у этого объекта есть нужный скрипт?
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (!isAI) levelUI.ShowAttackPointer(currentTargetObject.transform);
                else if(arenaDriverAI != null) arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
            }
        }
        else
        {
            AttackPointer attackPointerCurrent = currentTargetObject.GetComponent<AttackPointer>();
            AttackPointer newAttackPointer = other.GetComponent<AttackPointer>();

            if(newAttackPointer != null)
            {
                if (newAttackPointer.GetPriorityIndex() > attackPointerCurrent.GetPriorityIndex())
                {
                    currentTargetObject = other.gameObject;

                    if (!isAI) levelUI.ShowAttackPointer(currentTargetObject.transform);
                    else if (arenaDriverAI != null) arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// если замеченный объект совпадает с выбранным объектом целью, то можем атаковать
        {
            if (!isAI) levelUI.ShowingAttackPointer(currentTargetObject.transform);
            else if (arenaDriverAI != null) arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);

            // разрешаем атаковать
            weapon.SetObjectForAttack(currentTargetObject.transform);

        }// если триггер охватывает больше одного объекта и текущая цель уходит, то нужно переключится на другую цель в триггере
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
            if (!isAI) levelUI.HideAttackPointerWithTarget(currentTargetObject.transform);
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

        if (!isAI) levelUI.HideAttackPointer();

        if (!isAI) levelUI.ObjectWithAttackPointerWasDestroyed();
        else if (arenaDriverAI != null) arenaDriverAI.DetectorLostTarget();

        weapon.ReturnWeaponToPosition();
        currentTargetObject = null;
    }
}
