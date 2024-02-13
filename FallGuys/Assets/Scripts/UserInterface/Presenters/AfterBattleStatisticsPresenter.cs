using PunchCars.Models;
using PunchCars.UserInterface.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PunchCars.UserInterface.Presenters
{
    public class AfterBattleStatisticsPresenter : BasePresenter<AfterBattleStatisticsWindow>
    {
        private AfterBattleStatisticsModel _model;
        private AfterBattleStatisticsWindow _view;

        [Inject]
        public AfterBattleStatisticsPresenter(AfterBattleStatisticsModel model, IUiSystem uiSystem)
        {
            _model = model;
            _view = uiSystem.GetView<AfterBattleStatisticsWindow>();
        }

        public void OnAfterBattleStatisticsShow(int playerPlace)
        {
            UpdateCarAndWeaponLayersToUI();
            ChooseShowWinOrLose(playerPlace);
            ShowStatistics(playerPlace);
        }

        private void ShowStatistics(int playerPlace)
        {
            int coinsValue = _model.GetCurrentCoinsValue();
            int cupsValue = _model.GetCurrentCupsValue();

            _view.SetCurrentCoinsText(coinsValue.ToString());
            // здесь нужно взять инфу из кнопки рекламной сколько у нее увеличение

            
        }

        private void ChooseShowWinOrLose(int playerPlace)
        {
            if (playerPlace == 1) _view.ShowWinBackground();
            else _view.ShowLoseBackground();
        }

        private void UpdateCarAndWeaponLayersToUI()
        {
            GameObject carGO = _model.GetActiveLobbyCar();
            Transform[] allChieldVehicleTransforms = carGO.GetComponentsInChildren<Transform>();
            foreach (var trn in allChieldVehicleTransforms)
            {
                trn.gameObject.layer = 31;
            }

            GameObject weaponGO = _model.GetActiveLobbyWeapon();
            Transform[] allChieldWeaponTransforms = weaponGO.GetComponentsInChildren<Transform>();
            foreach (var trn in allChieldWeaponTransforms)
            {
                trn.gameObject.layer = 31;
            }
        }
    }
}
