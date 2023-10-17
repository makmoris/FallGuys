using Sirenix.OdinInspector;
using UnityEngine;

public class AdditionalBulletBonusLightning : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusLightning;

    [MinValue(0)][SerializeField] private float disableTime = 5f;
    [SerializeField] private AdditionalBulletBonusLightningEffect lightningEffect;

    private DisableWeaponFromLightningBonus disableWeaponFromLightningBonus;

    private void Awake()
    {
        lightningEffect.gameObject.SetActive(false);
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        transform.localPosition = effectPosition;
        lightningEffect.gameObject.SetActive(true);
    }

    public override Bonus GetBonus()
    {
        disableWeaponFromLightningBonus = new DisableWeaponFromLightningBonus(disableTime);
        return disableWeaponFromLightningBonus;
    }
}
