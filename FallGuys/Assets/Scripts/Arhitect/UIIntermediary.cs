using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIIntermediary : MonoBehaviour
{
    private EnemyPointer enemyPointer;

    private void Awake()
    {
        Transform enemyPointerTransform = transform.Find("EnemyPointer");
        if (enemyPointerTransform != null) enemyPointer = enemyPointerTransform.GetComponent<EnemyPointer>();
    }

    public void UpdateHealthInUI(float healthValue)
    {
        if (enemyPointer != null) PointerManager.Instance.UpdateHealthInUI(enemyPointer, healthValue);
        else PointerManager.Instance.UpdatePlayerHealthInUI(healthValue);
    }
}
