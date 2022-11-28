using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCharacteristicsData")]
public class WeaponCharacteristicsData : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float attackRange;

    public float Damage
    {
        get
        {
            return damage;
        }
    }
    public float RechargeTime
    {
        get
        {
            return rechargeTime;
        }
    }
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }
}
