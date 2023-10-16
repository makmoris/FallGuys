using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusLightning : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusLightning;

    [SerializeField] private AdditionalBulletBonusLightningEffect lightningEffect;

    private void Awake()
    {
        lightningEffect.gameObject.SetActive(false);
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        transform.localPosition = effectPosition;
        lightningEffect.gameObject.SetActive(true);
    }
}
