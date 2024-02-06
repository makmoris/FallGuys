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
    }
}
