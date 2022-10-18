using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField]private GameObject currentTargetObject;// текущая цель. Если есть, то есть. Если нету, то null
    [SerializeField] private Cannon cannon;// ссылка на пушку, внутри которой находится детектор


    private void OnTriggerEnter(Collider other)
    {
        // проверяем, есть ли сейчас активная(текущая) цель
        if (currentTargetObject == null)
        {// проверяем, у этого объекта есть нужный скрипт?
            if (other.GetComponent<AttackPointer>() != null)
            {
                currentTargetObject = other.gameObject;
                PointerManager.Instance.StartShowingAttackPointer(currentTargetObject.transform);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == currentTargetObject && currentTargetObject != null)// если замеченный объект совпадает с выбранным объектом целью, то можем атаковать
        {
            PointerManager.Instance.ShowAttackPointer(currentTargetObject.transform);// отображаем иконку прицела на объекте
            // разрешаем атаковать
            cannon.SetObjectForAttack(currentTargetObject.transform);

        }// если триггер охватывает больше одного объекта и текущая цель уходит, то нужно переключится на другую цель в триггере
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
