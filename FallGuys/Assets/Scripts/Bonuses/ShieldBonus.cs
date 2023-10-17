public class ShieldBonus : Bonus
{
    public ShieldBonus(float value)
    {
        _bonusType = BonusType.AddShield;
        _bonusValue = value;
    }
}
