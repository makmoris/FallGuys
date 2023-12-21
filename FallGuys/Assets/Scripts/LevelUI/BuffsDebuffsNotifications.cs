using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsDebuffsNotifications : MonoBehaviour
{
    [SerializeField] private Image banImage;

    [Header("Buffs")]
    [SerializeField] private Image shieldBuff;

    [Header("Debuffs")]
    [SerializeField] private Image slowDebuff;
    private bool slowDebuffNeedToShow;
    [SerializeField] private Image healthDamageDebuff;
    private bool healthDamageDebuffNeedToShow;
    [SerializeField] private Image lightningDebuff;
    [SerializeField] private Image controlInversion;

    [Header("Rings Bullet Buffs")]
    [SerializeField] private GameObject additionalBulletBonusLightning;
    [SerializeField] private GameObject additionalBulletBonusSlowdownOilPuddle;
    [SerializeField] private GameObject additionalBulletBonusExplosion;
    [Space]
    [SerializeField] private Color banColorForAdditionalBulletBonus;
    private List<Image> additionalBulletBonusImages = new List<Image>();

    private void Awake()
    {
        shieldBuff.gameObject.SetActive(false);

        slowDebuff.gameObject.SetActive(false);
        healthDamageDebuff.gameObject.SetActive(false);
        lightningDebuff.gameObject.SetActive(false);
        controlInversion.gameObject.SetActive(false);

        additionalBulletBonusLightning.SetActive(false);
        additionalBulletBonusSlowdownOilPuddle.SetActive(false);
        additionalBulletBonusExplosion.SetActive(false);

        additionalBulletBonusImages.Add(additionalBulletBonusLightning.GetComponent<Image>());
        additionalBulletBonusImages.Add(additionalBulletBonusSlowdownOilPuddle.GetComponent<Image>());
        additionalBulletBonusImages.Add(additionalBulletBonusExplosion.GetComponent<Image>());
    }

    public void ShowShieldBuff()
    {
        shieldBuff.gameObject.SetActive(true);

        if (slowDebuff.gameObject.activeSelf)
        {
            HideSlowdownDebuff();
            HideHealthDamageDebuff();
        }
    }
    public void HideShieldBuff()
    {
        shieldBuff.gameObject.SetActive(false);

        // если щит отключился, но в этой время игрок был в луже, значит нужно показать дебафф лужи
        if (slowDebuffNeedToShow) ShowSlowdownDebuff();
        if (healthDamageDebuffNeedToShow) ShowHealthDamageDebuff();
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

    public void ShowHealthDamageDebuff()
    {
        healthDamageDebuffNeedToShow = true;

        // если щит отключен, то показываем дебаф скорости
        if (!shieldBuff.gameObject.activeSelf) healthDamageDebuff.gameObject.SetActive(true);
    }
    public void HideHealthDamageDebuff()
    {
        healthDamageDebuffNeedToShow = false;

        healthDamageDebuff.gameObject.SetActive(false);
    }

    public void ShowLightningDebuff()
    {
        lightningDebuff.gameObject.SetActive(true);
    }
    public void HideLightningDebuff()
    {
        lightningDebuff.gameObject.SetActive(false);
    }

    public void ShowControlInversion()
    {
        controlInversion.gameObject.SetActive(true);
    }

    public void HideControlInversion()
    {
        controlInversion.gameObject.SetActive(false);
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

        banImage.enabled = false;
    }

    public void HideAdditionalBulletBonusNotification()
    {
        if (additionalBulletBonusLightning.activeSelf) additionalBulletBonusLightning.SetActive(false);
        if (additionalBulletBonusSlowdownOilPuddle.activeSelf) additionalBulletBonusSlowdownOilPuddle.SetActive(false);
        if (additionalBulletBonusExplosion.activeSelf) additionalBulletBonusExplosion.SetActive(false);

        banImage.enabled = true;
    }

    public void ShowAdditionalBulletBonusBan()
    {
        foreach (var image in additionalBulletBonusImages)
        {
            image.color = banColorForAdditionalBulletBonus;
        }
    }

    public void HideAdditionalBulletBonusBan()
    {
        foreach (var image in additionalBulletBonusImages)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        }
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
}
