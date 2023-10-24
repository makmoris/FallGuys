using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatisticsAfterBattleWindow : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera playerStatisticsCamera;

    [Header("Awards Manager")]
    [SerializeField] private AwardsManager awardsManager;

    [Header("League Window")]
    [SerializeField] private LeagueWindow leagueWindow;

    [Header("Lobby Manager")]
    [SerializeField] private LobbyManager lobbyManager;

    [Header("UI To Disable")]
    [SerializeField] private Canvas mainCanvas;

    [Header("Win/Lose Background")]
    [SerializeField] private GameObject winBackgroundGO;
    [SerializeField] private GameObject loseBackgroundGO;

    [Header("Statistics UI")]
    [SerializeField] private TextMeshProUGUI fragsText;
    [SerializeField] private GameObject fragsGO;
    [SerializeField] private GameObject borderAfterFrags;
    [Space]
    [SerializeField] private TextMeshProUGUI goldText;
    [Space]
    [SerializeField] private TextMeshProUGUI cupsText;
    [SerializeField] private GameObject cupsGO;
    [SerializeField] private GameObject borderBeforeCupsGO;
    [Space]
    [SerializeField] private Button continueButton;

    private int goldAwardValue;
    private int cupsAwardValue;

    private string key = "IsNotFirstLobbyEnterFromLocation";
    private int previousCupValue;

    private GameObject playerVehicleGO;
    private GameObject playerWeaponGO;

    public void ShowStatistics(int playerPlace, int numberOfFrags)
    {
        HideUIElements();

        playerVehicleGO = lobbyManager.GetActiveSaveVehicle();
        playerWeaponGO = lobbyManager.GetActiveLobbyWeapon();

        UpdateTransformsLayersToUI();

        int _playerPlace = playerPlace;

        if (_playerPlace == 1)
        {
            winBackgroundGO.SetActive(true);
            loseBackgroundGO.SetActive(false);
        }
        else
        {
            loseBackgroundGO.SetActive(true);
            winBackgroundGO.SetActive(false);
        }

        previousCupValue = CurrencyManager.Instance.Cups;

        goldAwardValue = awardsManager.GetGoldsAward(_playerPlace);
        cupsAwardValue = awardsManager.GetCupsAward(_playerPlace);

        goldText.text = goldAwardValue.ToString();

        if (cupsAwardValue > 0)
        {
            cupsGO.SetActive(true);
            borderBeforeCupsGO.SetActive(true);
            cupsText.text = cupsAwardValue.ToString();
        }
        else
        {
            cupsGO.SetActive(false);
            borderBeforeCupsGO.SetActive(false);
        }

        if(numberOfFrags >= 0)
        {
            fragsGO.SetActive(true);
            borderAfterFrags.SetActive(true);
            fragsText.text = numberOfFrags.ToString();
        }
        else
        {
            fragsGO.SetActive(false);
            borderAfterFrags.SetActive(false);
        }

        CurrencyManager.Instance.AddGold(goldAwardValue);
        CurrencyManager.Instance.AddCup(cupsAwardValue);

        continueButton.interactable = false;
        SetOnClickEventOnContinueButton();
        continueButton.interactable = true;

        #region Analytics
        AnalyticsManager.Instance.PlayerGetBattleReward(goldAwardValue, cupsAwardValue);
        #endregion
    }

    private void ShowCupsProgressInLeagueWindow()
    {
        UpdateTransformsLayersToDefault();

        playerStatisticsCamera.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);

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
        mainCanvas.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);

        playerStatisticsCamera.gameObject.SetActive(true);
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

    private void UpdateTransformsLayersToUI()
    {
        Transform[] allChieldVehicleTransforms = playerVehicleGO.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldVehicleTransforms)
        {
            trn.gameObject.layer = 31;
        }

        Transform[] allChieldWeaponTransforms = playerWeaponGO.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldWeaponTransforms)
        {
            trn.gameObject.layer = 31;
        }
    }
    private void UpdateTransformsLayersToDefault()
    {
        Transform[] allChieldTransforms = playerVehicleGO.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldTransforms)
        {
            trn.gameObject.layer = 0;
        }

        Transform[] allChieldWeaponTransforms = playerWeaponGO.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldWeaponTransforms)
        {
            trn.gameObject.layer = 0;
        }
    }
}
