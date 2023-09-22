using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusLightning : AdditionalBulletBonus
{
    [SerializeField] private float value;
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

    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusLightning;

    [SerializeField] private AdditionalBulletBonusLightningEffect lightningEffect;

    private void Awake()
    {
        lightningEffect.gameObject.SetActive(false);
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        Debug.LogError("Playu");
        transform.localPosition = effectPosition;
        lightningEffect.gameObject.SetActive(true);
    }
}
