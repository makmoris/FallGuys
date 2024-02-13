using PunchCars.UserInterface.Presenters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PunchCars.UserInterface.Views
{
    public class AfterBattleStatisticsWindow : BaseUiView<AfterBattleStatisticsPresenter>
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Canvas _mainCanvas;
        [Space]
        [SerializeField] private Camera _afterBattleStatisticsCamera;
        [Space]
        [SerializeField] private AwardsManager _awardsManager;
        [SerializeField] private LobbyManager _lobbyManager;
        [Space]
        [SerializeField] private CupRewardsWindow _cupRewardsWindow;
        [Header("Battle Statistics Elements")]
        [SerializeField] private GameObject _winBackgroundGO;
        [SerializeField] private GameObject _loseBackgroundGO;
        [Space]
        [SerializeField] private TextMeshProUGUI _fragsText;
        [SerializeField] private GameObject _fragsGO;
        [Space]
        [SerializeField] private TextMeshProUGUI _currentCoinsText;
        [SerializeField] private TextMeshProUGUI _coinsAfterAdRewardText;
        [SerializeField] private TextMeshProUGUI _adRewardCoinsFactorText;
        [Space]
        [SerializeField] private TextMeshProUGUI _cupsText;
        [SerializeField] private GameObject _cupsGO;
        [Space]
        [SerializeField] private GameObject _bordeGO;
        [Space]
        [SerializeField] private Button _continueButton;

        public void OnAfterBattleStatisticsShow(int playerPlace, int numberOfFrags)
        {
            if (!gameObject.activeSelf)
            {
                Presenter.OnAfterBattleStatisticsShow(playerPlace);
                ShowWindow();
            }
        }

        public void SetCurrentCoinsText(string currentCoins) => _currentCoinsText.text = currentCoins;
        public void SetCoinsAfterAdRewardText(string coinsAfterReward) => _coinsAfterAdRewardText.text = coinsAfterReward;
        public void SetAdRewardCoinsFactorText(string adRewardCoinsFactor) => _adRewardCoinsFactorText.text = adRewardCoinsFactor;

        public void ShowWinBackground()
        {
            _winBackgroundGO.SetActive(true);
            _loseBackgroundGO.SetActive(false);
        }

        public void ShowLoseBackground()
        {
            _loseBackgroundGO.SetActive(true);
            _winBackgroundGO.SetActive(false);
        }

        private void ShowWindow()
        {
            _mainCanvas.gameObject.SetActive(false);
            _mainCamera.gameObject.SetActive(false);

            _afterBattleStatisticsCamera.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
    }
}
