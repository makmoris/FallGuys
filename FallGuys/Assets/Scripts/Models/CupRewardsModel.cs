using PunchCars.CupRewards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PunchCars.Models
{
    public class CupRewardsModel
    {
        private ICupRewardsProvider _cupRewardsProvider;

        private List<CustomCupReward> _receivedCupRewards = new List<CustomCupReward>();

        [Inject]
        public CupRewardsModel(ICupRewardsProvider cupRewardsProvider)
        {
            _cupRewardsProvider = cupRewardsProvider;
        }

        public int GetCurrentCupsValue()
        {
            return CurrencyManager.Instance.Cups;
        }

        public int GetPreviousCupsValue()
        {
            return CurrencyManager.Instance.PreviousCups;
        }

        public bool AddReceivedCupReward(CustomCupReward receivedCupReward)
        {
            if (_receivedCupRewards.Contains(receivedCupReward)) return false;
            else
            {
                _receivedCupRewards.Add(receivedCupReward);
                return true;
            }
        }

        public void ClearReceivedCupRewardsData()
        {
            _receivedCupRewards.Clear();
        }

        public void AddCoinsFromReward(int coinsRewardValue)
        {
            CurrencyManager.Instance.AddGold(coinsRewardValue);
        }

        public bool CheckPlayerItemAvailability(string playerItemID)
        {
            // здесь надо пробить по ID объекта, доступен ли он. Для этого нужна какая-то база объектов

            return ElementsAvailableData.Instance.GetAvailableStatus(playerItemID);// костыль
        }
    }
}
