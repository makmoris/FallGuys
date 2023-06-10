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
        Bumper.PlayerSlowedEvent += ShowSlowDebuff;
        Bumper.PlayerStoppedSlowingEvent += HideSlowDebuff;
    }

    public void SetCurrentPlayer(GameObject gameObj)
    {
        currentPlayer = gameObj;
    }

    public void ShowShieldBuff()
    {
        shieldBuff.gameObject.SetActive(true);

        if (slowDebuff.gameObject.activeSelf) HideSlowDebuff();
    }
    public void HideShieldBuff()
    {
        shieldBuff.gameObject.SetActive(false);

        // ���� ��� ����������, �� � ���� ����� ����� ��� � ����, ������ ����� �������� ������ ����
        if (slowDebuffNeedToShow) ShowSlowDebuff();
    }

    private void ShowSlowDebuff()
    {
        slowDebuffNeedToShow = true;

        // ���� ��� ��������, �� ���������� ����� ��������
        if(!shieldBuff.gameObject.activeSelf) slowDebuff.gameObject.SetActive(true);
    }
    private void HideSlowDebuff()
    {
        slowDebuffNeedToShow = false;

        slowDebuff.gameObject.SetActive(false);
    }

    public void ShowLightningDebuff(GameObject gameObject)
    {
        if (currentPlayer == gameObject) lightningDebuff.gameObject.SetActive(true);
    }
    public void HideLightningDebuff(GameObject gameObject)
    {
        if (currentPlayer == gameObject) lightningDebuff.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Bumper.PlayerSlowedEvent -= ShowSlowDebuff;
        Bumper.PlayerStoppedSlowingEvent -= HideSlowDebuff;
    }
}
