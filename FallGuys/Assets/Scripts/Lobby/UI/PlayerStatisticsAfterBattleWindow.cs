using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PunchCars.UserInterface.Views;
using Zenject;
using PunchCars.Advertisment;

public class PlayerStatisticsAfterBattleWindow : MonoBehaviour
{
    [SerializeField] private Canvas rewardButtonCanvas;
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera playerStatisticsCamera;

    [Header("Awards Manager")]
    [SerializeField] private AwardsManager awardsManager;

    [Header("Cup Rewards Window")]
    [SerializeField] private CupRewardsWindow cupRewardsWindow;

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
    [SerializeField] private TextMeshProUGUI adNotReceivedGoldText;
    [SerializeField] private TextMeshProUGUI adNotReceivedGoldCoefficientText;
    [Space]
    [SerializeField] private TextMeshProUGUI adReceivedGoldText;
    [SerializeField] private TextMeshProUGUI adReceivedGoldCoefficientText;
    [Space]
    [SerializeField] private GameObject coinsGO;
    [SerializeField] private GameObject coinsNotReceivedGO;
    [SerializeField] private GameObject coinsReceivedGO;
    [Space]
    [SerializeField] private TextMeshProUGUI cupsText;
    [SerializeField] private GameObject cupsGO;
    [SerializeField] private GameObject borderBeforeCupsGO;
    [Space]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button rewardButton;

    private int goldAwardValue;
    private int cupsAwardValue;

    private string key = "IsNotFirstLobbyEnterFromLocation";

    private GameObject playerVehicleGO;
    private GameObject playerWeaponGO;

    private IAdvertisementService _advertisementService;

    [Inject]
    public void Constructor( IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
    }

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

        goldAwardValue = awardsManager.GetGoldsAward(_playerPlace);
        cupsAwardValue = awardsManager.GetCupsAward(_playerPlace);

        goldText.text = goldAwardValue.ToString();
        adNotReceivedGoldText.text = ((int)(goldAwardValue * 1.5f)).ToString();
        adNotReceivedGoldCoefficientText.text = "x1,5";
        coinsGO.SetActive(true);
        coinsNotReceivedGO.SetActive(true);
        coinsReceivedGO.SetActive(false);

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

        //CurrencyManager.Instance.AddGold(goldAwardValue);
        //CurrencyManager.Instance.AddCup(cupsAwardValue);

        continueButton.interactable = false;
        SetOnClickEventOnContinueButton();
        continueButton.interactable = true;

        rewardButton.interactable = false;
        SetOnClickEventOnRewardButton();
        rewardButton.interactable = true;


        #region Analytics
        AnalyticsManager.Instance.PlayerGetBattleReward(goldAwardValue, cupsAwardValue);
        #endregion
    }

    private void ShowCupsProgressInLeagueWindow()
    {
        CurrencyManager.Instance.AddGold(goldAwardValue);
        CurrencyManager.Instance.AddCup(cupsAwardValue);

        UpdateTransformsLayersToDefault();

        playerStatisticsCamera.gameObject.SetActive(false);
        rewardButtonCanvas.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);

        //if (IsNotFirstLobbyEnterFromLocation())
        //{
        //    leagueWindow.gameObject.SetActive(true);
        //    leagueWindow.ShowCupsProgressAnimation(previousCupValue);
        //}
        //leagueWindow.gameObject.SetActive(true);
        //leagueWindow.ShowCupsProgressAnimation(previousCupValue);
        cupRewardsWindow.GetComponent<CupRewardsWindow>().OnCupRewardWindowShowAfterBattle();

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

    private void SetOnClickEventOnRewardButton()
    {
        rewardButton.onClick.RemoveAllListeners();
        rewardButton.onClick.AddListener(ShowReward);
    }

    private void ShowReward()
    {
        if (_advertisementService.IsRewardedAvailable())
        {
            _advertisementService.ShowRewarded(() =>
            {
                // если просмотрел рекламу - даем награду
                var rewardGold = goldAwardValue * 1.5f;
                goldAwardValue = (int)rewardGold;

                rewardButton.interactable = false;

                adReceivedGoldCoefficientText.text = "x1,5";
                adReceivedGoldText.text = goldAwardValue.ToString();

                coinsGO.SetActive(false);
                coinsNotReceivedGO.SetActive(false);
                coinsReceivedGO.SetActive(true);

                rewardButtonCanvas.gameObject.SetActive(false);
            });
        }
        else Debug.LogError("ADS not available");
    }

    private void HideUIElements()
    {
        mainCanvas.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);

        playerStatisticsCamera.gameObject.SetActive(true);
        rewardButtonCanvas.gameObject.SetActive(true);
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
