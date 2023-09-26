using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusBlankShot : AdditionalBulletBonus
{
    private float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }
    private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }

    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType =>  AdditionalBulletBonusTypeEnum.AdditionalBulletBonusBlankShot;


    public override void PlayEffect(Vector3 effectPosition)
    {
        Destroy(gameObject);
    }
}
