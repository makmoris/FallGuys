using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusControlInversion : AdditionalBulletBonus
{
    private float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }
    [SerializeField] private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }

    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusControlInversion;

    private void Awake()
    {
        Type = BonusType.ControlInversion;
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        Destroy(gameObject);
    }
}
