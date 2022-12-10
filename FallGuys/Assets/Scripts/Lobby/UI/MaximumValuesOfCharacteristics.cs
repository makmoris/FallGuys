using UnityEngine;

[CreateAssetMenu(fileName = "MaximumValuesOfCharacteristics")]
public class MaximumValuesOfCharacteristics : ScriptableObject
{
    [Header("Vehicle")]
    [SerializeField] private float _maxHP;
    [Header("Weapon")]
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _maxRechargeTime;
    [SerializeField] private float _maxAttackRange;

    public float MaxHP
    {
        get
        {
            return _maxHP;
        }
    }

    public float MaxDamage
    {
        get
        {
            return _maxDamage;
        }
    }
    public float MaxRechargeTime
    {
        get
        {
            return _maxRechargeTime;
        }
    }
    public float MaxAttackRange
    {
        get
        {
            return _maxAttackRange;
        }
    }
}
