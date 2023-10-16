using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusControlInversion : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusControlInversion;

    private void Awake()
    {
        _bonusType = BonusType.ControlInversion;
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        Destroy(gameObject);
    }
}
