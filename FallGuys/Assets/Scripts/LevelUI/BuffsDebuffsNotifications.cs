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

    private GameObject currentPlayer;

    private void Awake()
    {
        shieldBuff.gameObject.SetActive(false);

        slowDebuff.gameObject.SetActive(false);
        lightningDebuff.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Installer.IsCurrentPlayer += SetCurrentPlayer;

        PlayerEffector.EnableShieldEvent += ShowShieldBuff;
        PlayerEffector.DisableShieldEvent += HideShieldBuff;

        Bumper.PlayerSlowedEvent += ShowSlowDebuff;
        Bumper.PlayerStoppedSlowingEvent += HideSlowDebuff;

        PlayerEffector.DisableWeaponEvent += ShowLightningDebuff;
        PlayerEffector.EnableWeaponEvent += HideLightningDebuff;
    }

    private void SetCurrentPlayer(GameObject gameObj)
    {
        currentPlayer = gameObj;
    }

    private void ShowShieldBuff()
    {
        shieldBuff.gameObject.SetActive(true);

        if (slowDebuff.gameObject.activeSelf) HideSlowDebuff();
    }
    private void HideShieldBuff()
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

    private void ShowLightningDebuff(GameObject gameObject, float value)
    {
        if (currentPlayer == gameObject) lightningDebuff.gameObject.SetActive(true);
    }
    private void HideLightningDebuff(GameObject gameObject)
    {
        if (currentPlayer == gameObject) lightningDebuff.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Installer.IsCurrentPlayer -= SetCurrentPlayer;

        PlayerEffector.EnableShieldEvent -= ShowShieldBuff;
        PlayerEffector.DisableShieldEvent -= HideShieldBuff;

        Bumper.PlayerSlowedEvent -= ShowSlowDebuff;
        Bumper.PlayerStoppedSlowingEvent -= HideSlowDebuff;

        PlayerEffector.DisableWeaponEvent -= ShowLightningDebuff;
        PlayerEffector.EnableWeaponEvent -= HideLightningDebuff;
    }
}
