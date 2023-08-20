using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatisticsAfterBattleWindow : MonoBehaviour
{
    [Header("Awards Manager")]
    [SerializeField] private AwardsManager awardsManager;

    [Header("League Window")]
    [SerializeField] private LeagueWindow leagueWindow;

    [Header("UI To Disable")]
    [SerializeField] private GameObject LobbyGO;
    [SerializeField] private GameObject coinsButtonGO;

    [Header("Statistics UI")]
    [SerializeField] private TextMeshProUGUI placeText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI cupsText;
    [Space]
    [SerializeField] private Button continueButton;

    private int goldAwardValue;
    private int cupsAwardValue;

    private string key = "IsNotFirstLobbyEnterFromLocation";
    private int previousCupValue;

    public void ShowStatistics(int playerPlaceIndex)
    {
        HideUIElements();

        previousCupValue = CurrencyManager.Instance.Cups;

        int playerPlace = playerPlaceIndex + 1;

        goldAwardValue = awardsManager.GetGoldsAward(playerPlaceIndex);
        cupsAwardValue = awardsManager.GetCupsAward(playerPlaceIndex);

        placeText.text = playerPlace.ToString();
        goldText.text = goldAwardValue.ToString();
        cupsText.text = cupsAwardValue.ToString();

        CurrencyManager.Instance.AddGold(goldAwardValue);
        CurrencyManager.Instance.AddCup(cupsAwardValue);

        continueButton.interactable = false;
        SetOnClickEventOnContinueButton();
        continueButton.interactable = true;
    }

    private void ShowCupsProgressInLeagueWindow()
    {
        LobbyGO.SetActive(true);
        coinsButtonGO.SetActive(true);

        if (IsNotFirstLobbyEnterFromLocation())
        {
            leagueWindow.gameObject.SetActive(true);
            leagueWindow.ShowCupsProgressAnimation(previousCupValue);
        }

        gameObject.SetActive(false);
    }

    private void SetOnClickEventOnContinueButton()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(ShowCupsProgressInLeagueWindow);
        continueButton.onClick.AddListener(DeactivateContinueButton);
    }

    private void DeactivateContinueButton()
    {
        continueButton.interactable = false;
    }

    private void HideUIElements()
    {
        LobbyGO.SetActive(false);
        coinsButtonGO.SetActive(false);
    }

    private bool IsNotFirstLobbyEnterFromLocation()
    {
        bool returnValue;
        int _value = PlayerPrefs.GetInt(key, 0);
        if (_value == 1) returnValue = true;
        else returnValue = false;

        PlayerPrefs.SetInt(key, 1);

        return returnValue;
    }
}
