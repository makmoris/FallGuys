using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusBlankShot : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType =>  AdditionalBulletBonusTypeEnum.AdditionalBulletBonusBlankShot;

    private HealthBonus healthBonus;

    public override void PlayEffect(Vector3 effectPosition, GameObject ignorePlayer)
    {
        Destroy(gameObject);
    }

    public override Bonus GetBonus()
    {
        healthBonus = new HealthBonus(0);
        return healthBonus;
    }
}
