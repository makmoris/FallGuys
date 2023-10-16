using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusBlankShot : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType =>  AdditionalBulletBonusTypeEnum.AdditionalBulletBonusBlankShot;

    public override void PlayEffect(Vector3 effectPosition)
    {
        Destroy(gameObject);
    }
}
