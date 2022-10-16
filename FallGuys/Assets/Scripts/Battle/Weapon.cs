using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    protected float damage;
    protected float rechargeTime;
    protected float attackRange;

    public void Attack() { }
}
