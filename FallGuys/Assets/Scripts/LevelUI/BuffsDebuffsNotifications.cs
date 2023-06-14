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

    private void Awake()
    {
        shieldBuff.gameObject.SetActive(false);

        slowDebuff.gameObject.SetActive(false);
        lightningDebuff.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Bumper.PlayerSlowedEvent += ShowSlowDebuff;
        Bumper.PlayerStoppedSlowingEvent += HideSlowDebuff;
    }

    public void ShowShieldBuff()
    {
        shieldBuff.gameObject.SetActive(true);

        if (slowDebuff.gameObject.activeSelf) HideSlowDebuff();
    }
    public void HideShieldBuff()
    {
        shieldBuff.gameObject.SetActive(false);

        // если щит отключился, но в этой время игрок был в луже, значит нужно показать дебафф лужи
        if (slowDebuffNeedToShow) ShowSlowDebuff();
    }

    private void ShowSlowDebuff()
    {
        slowDebuffNeedToShow = true;

        // если щит отключен, то показываем дебаф скорости
        if(!shieldBuff.gameObject.activeSelf) slowDebuff.gameObject.SetActive(true);
    }
    private void HideSlowDebuff()
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

    private void OnDisable()
    {
        Bumper.PlayerSlowedEvent -= ShowSlowDebuff;
        Bumper.PlayerStoppedSlowingEvent -= HideSlowDebuff;
    }
}
