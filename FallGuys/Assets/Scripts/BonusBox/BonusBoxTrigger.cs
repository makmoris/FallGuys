using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBoxTrigger : MonoBehaviour
{
    private BonusBox _bonusBox;

    public void Initialize(BonusBox bonusBox)
    {
        _bonusBox = bonusBox;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            _bonusBox.ApplyBonus(other.gameObject);
        }
    }
}
