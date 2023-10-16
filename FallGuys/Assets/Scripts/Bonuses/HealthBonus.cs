public class HealthBonus : Bonus
{
    public HealthBonus(float value)
    {
        _bonusType = BonusType.AddHealth;
        _bonusValue = value;
    }
}
