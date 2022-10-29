using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualIntermediary : MonoBehaviour // висит на игроке и отвечает за визуальное представление. UI, эффект уничтожения и т.п.
{
    private EnemyPointer enemyPointer;
    private bool isPlayer = true;

    public static event Action<GameObject> PlayerWasDeadEvent; // кидаем событие всем заинтересованным

    private void Awake()
    {
        Transform enemyPointerTransform = transform.Find("EnemyPointer");
        if (enemyPointerTransform != null)
        {
            enemyPointer = enemyPointerTransform.GetComponent<EnemyPointer>();
            isPlayer = false;
        }
    }

    public void UpdateHealthInUI(float healthValue)
    {
        if (enemyPointer != null) PointerManager.Instance.UpdateHealthInUI(enemyPointer, healthValue);
        else PointerManager.Instance.UpdatePlayerHealthInUI(healthValue);
    }

    public void DestroyCar()
    {
        if (isPlayer) PointerManager.Instance.UpdatePlayerHealthInUI(-1000f);// как заглушка, чтобы счетчик хп показал 0
        else
        {

        }
        PlayerWasDeadEvent?.Invoke(this.gameObject);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
