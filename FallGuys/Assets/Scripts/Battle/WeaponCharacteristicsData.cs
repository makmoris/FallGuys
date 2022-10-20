using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCharacteristicsData")]
public class WeaponCharacteristicsData : ScriptableObject
{
    public float damage;
    public float rechargeTime;
    public float attackRange;
}
