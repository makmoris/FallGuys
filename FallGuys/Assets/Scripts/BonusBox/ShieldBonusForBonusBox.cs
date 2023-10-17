using UnityEngine;

public class ShieldBonusForBonusBox : MonoBehaviour, IBonusForBonusBox
{
    [Header("Shield")]
    [Min(0)][SerializeField] private float minShieldTimeValue;
    [Min(0)][SerializeField] private float maxShieldTimeValue;

    private ShieldBonus shieldBonus;

    public Bonus GetBonus()
    {
        shieldBonus = new ShieldBonus(GetRandomTimeValue());

        return shieldBonus;
    }

    private float GetRandomTimeValue()
    {
        float r = Random.Range(minShieldTimeValue, maxShieldTimeValue);

        return r;
    }
}
