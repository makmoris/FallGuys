using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsDebuffsNotifications : MonoBehaviour
{
    [Header("Buffs")]
    [SerializeField] private Image shieldBuff;

    [Header("Debuffs")]
    [SerializeField] private Image slowDebuff;
    private bool slowDebuffNeedToShow;
    [SerializeField] private Image lightningDebuff;

    [Header("Rings Bullet Buffs")]
    [SerializeField] private GameObject additionalBulletBonusLightning;
    [SerializeField] private GameObject additionalBulletBonusSlowdownOilPuddle;
    [SerializeField] private GameObject additionalBulletBonusExplosion;

    private void Awake()
    {
        shieldBuff.gameObject.SetActive(false);

        slowDebuff.gameObject.SetActive(false);
        lightningDebuff.gameObject.SetActive(false);

        additionalBulletBonusLightning.SetActive(false);
        additionalBulletBonusSlowdownOilPuddle.SetActive(false);
        additionalBulletBonusExplosion.SetActive(false);
    }

    //private void OnEnable()
    //{
    //    Bumper.PlayerSlowedEvent += ShowSlowdownDebuff;
    //    Bumper.PlayerStoppedSlowingEvent += HideSlowdownDebuff;
    //}

    public void ShowShieldBuff()
    {
        shieldBuff.gameObject.SetActive(true);

        if (slowDebuff.gameObject.activeSelf) HideSlowdownDebuff();
    }
    public void HideShieldBuff()
    {
        shieldBuff.gameObject.SetActive(false);

        // если щит отключился, но в этой время игрок был в луже, значит нужно показать дебафф лужи
        if (slowDebuffNeedToShow) ShowSlowdownDebuff();
    }

    public void ShowSlowdownDebuff()
    {
        slowDebuffNeedToShow = true;

        // если щит отключен, то показываем дебаф скорости
        if(!shieldBuff.gameObject.activeSelf) slowDebuff.gameObject.SetActive(true);
    }
    public void HideSlowdownDebuff()
    {
        slowDebuffNeedToShow = false;

        slowDebuff.gameObject.SetActive(false);
    }

    public void ShowLightningDebuff()
    {
        lightningDebuff.gameObject.SetActive(true);
    }
    public void HideLightningDebuff()
    {
        lightningDebuff.gameObject.SetActive(false);
    }

    #region Additional Bullet Bonuses

    public void ShowAdditionalBulletBonusNotification(AdditionalBulletBonus additionalBulletBonus)
    {
        HideAdditionalBulletBonusNotification();

        switch (additionalBulletBonus.AdditionalBulletBonusType)
        {
            case AdditionalBulletBonusTypeEnum.AdditionalBulletBonusLightning:

                ShowAdditionalBulletBonusLightning();

                break;

            case AdditionalBulletBonusTypeEnum.AdditionalBulletBonusSlowdownOilPuddle:

                ShowAdditionalBulletBonusSlowdownOilPuddle();

                break;

            case AdditionalBulletBonusTypeEnum.AdditionalBulletBonusExplosion:

                ShowAdditionalBulletBonusExplosion();

                break;

        }
    }

    public void HideAdditionalBulletBonusNotification()
    {
        if (additionalBulletBonusLightning.activeSelf) additionalBulletBonusLightning.SetActive(false);
        if (additionalBulletBonusSlowdownOilPuddle.activeSelf) additionalBulletBonusSlowdownOilPuddle.SetActive(false);
        if (additionalBulletBonusExplosion.activeSelf) additionalBulletBonusExplosion.SetActive(false);
    }

    private void ShowAdditionalBulletBonusLightning()
    {
        additionalBulletBonusLightning.SetActive(true);
    }

    private void ShowAdditionalBulletBonusSlowdownOilPuddle()
    {
        additionalBulletBonusSlowdownOilPuddle.SetActive(true);
    }

    private void ShowAdditionalBulletBonusExplosion()
    {
        additionalBulletBonusExplosion.SetActive(true);
    }

    #endregion

    //private void OnDisable()
    //{
    //    Bumper.PlayerSlowedEvent -= ShowSlowdownDebuff;
    //    Bumper.PlayerStoppedSlowingEvent -= HideSlowdownDebuff;
    //}
}
