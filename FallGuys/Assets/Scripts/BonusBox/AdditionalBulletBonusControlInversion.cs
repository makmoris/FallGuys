using UnityEngine;

public class AdditionalBulletBonusControlInversion : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusControlInversion;

    [SerializeField] private float controlInversionTime = 4f;
    private ControlInversionBonus controlInversionBonus;


    public override void PlayEffect(Vector3 effectPosition)
    {
        Destroy(gameObject);
    }

    public override Bonus GetBonus()
    {
        controlInversionBonus = new ControlInversionBonus(controlInversionTime);
        return controlInversionBonus;
    }
}
