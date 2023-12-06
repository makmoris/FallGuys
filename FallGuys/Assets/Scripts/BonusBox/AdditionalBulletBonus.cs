using UnityEngine;

public enum AdditionalBulletBonusTypeEnum
{
    AdditionalBulletBonusLightning = 0,
    AdditionalBulletBonusSlowdownOilPuddle = 1,
    AdditionalBulletBonusExplosion = 2,
    AdditionalBulletBonusControlInversion = 3,
    AdditionalBulletBonusBlankShot = 4
}

public abstract class AdditionalBulletBonus : MonoBehaviour, IBonusForBonusBox
{
    public abstract AdditionalBulletBonusTypeEnum AdditionalBulletBonusType { get; }
    public abstract void PlayEffect(Vector3 effectPosition, GameObject parentGO);
    public abstract Bonus GetBonus();
}
