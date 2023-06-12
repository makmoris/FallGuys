using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    private GameObject currentTargetObject;// текущая цель. Если есть, то есть. Если нету, то null
    [SerializeField] private Weapon weapon;// ссылка на пушку, внутри которой находится детектор

    private bool isAI;
    private ArenaCarDriverAI arenaDriverAI;

    [SerializeField]private bool isRace;
    [SerializeField]private bool isArena;

    private void Awake()
    {
        arenaDriverAI = transform.GetComponentInParent<ArenaCarDriverAI>();
    }
    private void Start()
    {
        //driverAI = transform.GetComponentInParent<CarDriverAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // проверяем, есть ли сейчас активная(текущая) цель
        if (currentTargetObject == null)
        {// проверяем, у этого объекта есть нужный скрипт?
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (isArena)
                {
                    if (!isAI) ArenaUIPointers.Instance.StartShowingAttackPointer(currentTargetObject.transform);
                    else arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
                }
                else if (isRace)
                {
                    if (!isAI) RacePointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// если замеченный объект совпадает с выбранным объектом целью, то можем атаковать
        {
            if (isArena)
            {
                if (!isAI) ArenaUIPointers.Instance.ShowAttackPointer(currentTargetObject.transform);// отображаем иконку прицела на объекте
                else arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
            }
            else if (isRace)
            {
                Debug.Log("!!!!!!!!!!!");
                if (!isAI)
                {
                    Debug.Log("8889238894823849");
                    RacePointerManager.Instance.ShowAttackPointer(currentTargetObject.transform);// отображаем иконку прицела на объекте
                }
            }

            // разрешаем атаковать
            weapon.SetObjectForAttack(currentTargetObject.transform);

        }// если триггер охватывает больше одного объекта и текущая цель уходит, то нужно переключится на другую цель в триггере
        else if (currentTargetObject == null)
        {
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;

                if (isArena)
                {
                    if (!isAI) ArenaUIPointers.Instance.StartShowingAttackPointer(currentTargetObject.transform);
                    else arenaDriverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform.parent.transform);
                }
                else if (isRace)
                {
                    if (!isAI) RacePointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTargetObject)
        {
            if (isArena)
            {
                if (!isAI) ArenaUIPointers.Instance.FinishShowingAttackPointer(currentTargetObject.transform);
                else arenaDriverAI.DetectorLostTarget();
            }
            else if (isRace)
            {
                if (!isAI) RacePointerManager.Instance.FinishShowingAttackPointer(currentTargetObject.transform);
            }

            currentTargetObject = null;
            weapon.ReturnWeaponToPosition();
        }
    }

    private void CurrentTargetObjectWasDeactivated(GameObject objWithAttackPointer)
    {
        if (objWithAttackPointer == currentTargetObject)
        {
            if (isArena)
            {
                if (!isAI) ArenaUIPointers.Instance.ObjectWithAttackPointerWasDestroyed();
                else arenaDriverAI.DetectorLostTarget();
            }
            else if (isRace)
            {
                if (!isAI) RacePointerManager.Instance.ObjectWithAttackPointerWasDestroyed();
            }

            weapon.ReturnWeaponToPosition();
            currentTargetObject = null;
        }
    }

    public void IsAI(bool value)
    {
        isAI = value;
    }

    public void IsRace(bool value)
    {
        isRace = value;
    }

    public void IsArena(bool value)
    {
        isArena = value;
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
