using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private bool canAttack;
    [SerializeField] private Weapon _weapon;


    private void Update()
    {
        if (canAttack)
        {
            if (SimpleInput.GetButtonDown("Fire1"))
            {

            }
        }
    }

    public void SetWeapon(Weapon weaponPrefab)
    {
        
    }
}
