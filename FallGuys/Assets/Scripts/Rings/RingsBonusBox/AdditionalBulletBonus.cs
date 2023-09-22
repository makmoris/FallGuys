using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdditionalBulletBonusTypeEnum
{
    AdditionalBulletBonusLightning = 0,
    AdditionalBulletBonusSlowdownOilPuddle = 1,
    AdditionalBulletBonusExplosion = 2
}

public abstract class AdditionalBulletBonus : Bonus
{
    public abstract AdditionalBulletBonusTypeEnum AdditionalBulletBonusType { get; }

    public abstract void PlayEffect(Vector3 effectPosition);

    public override void Got()
    {
        // заглушка, чтобы не удалять пулю
    }
}
