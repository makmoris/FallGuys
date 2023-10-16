using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    protected BonusType _bonusType;
    public BonusType BonusType { get => _bonusType; }

    protected float _bonusValue;
    public float BonusValue { get => _bonusValue; }
}
