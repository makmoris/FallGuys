using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField]private GameObject currentTargetObject;// текущая цель. Если есть, то есть. Если нету, то null
    [SerializeField] private Weapon weapon;// ссылка на пушку, внутри которой находится детектор

    private bool isAI;
    private CarDriverAI driverAI;

    private void Start()
    {
        driverAI = transform.GetComponentInParent<CarDriverAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // проверяем, есть ли сейчас активная(текущая) цель
        if (currentTargetObject == null)
        {// проверяем, у этого объекта есть нужный скрипт?
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
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// если замеченный объект совпадает с выбранным объектом целью, то можем атаковать
        {
             if (!isAI) PointerManager.Instance.ShowAttackPointer(currentTargetObject.transform);// отображаем иконку прицела на объекте
            else driverAI.SetNewPlayerTargetFromDetector(currentTargetObject.transform);

            // разрешаем атаковать
            weapon.SetObjectForAttack(currentTargetObject.transform);

        }// если триггер охватывает больше одного объекта и текущая цель уходит, то нужно переключится на другую цель в триггере
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
