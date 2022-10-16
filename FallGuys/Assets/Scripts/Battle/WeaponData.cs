using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public int id;
    public float damage;
    public float rechargeTime;
    public float attackRange;
}
