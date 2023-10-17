public class DisableWeaponFromLightningBonus : Bonus
{
    public DisableWeaponFromLightningBonus(float disableTime)
    {
        _bonusType = BonusType.DisableWeaponFromLightning;
        _bonusValue = disableTime;
    }
}
