using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.Advertisment;
using Zenject;

namespace PunchCars.Models
{
    public class AfterBattleStatisticsModel
    {
        private IAdvertisementService _advertisementService;

        [Inject]
        public AfterBattleStatisticsModel(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public void BuyProductByRewardedAd()
        {
            if (_advertisementService.IsRewardedAvailable())
            {
                _advertisementService.ShowRewarded(() =>
                {
                    // если просмотрел рекламу - даем награду
                });
            }
        }

        public GameObject GetActiveLobbyCar() => LobbyManager.Instance.GetActiveSaveVehicle();
        public GameObject GetActiveLobbyWeapon() => LobbyManager.Instance.GetActiveLobbyWeapon();
        public int GetCurrentCoinsValue() => CurrencyManager.Instance.Gold;
        public int GetCurrentCupsValue() => CurrencyManager.Instance.Cups;
    }
}
