using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PunchCars.CupRewards
{
    [CreateAssetMenu(fileName = "New Rewards Container", menuName = "PunchCars/CupRewards/CupRewardsContainer", order = 0)]
    public class CupRewardsContainer : ScriptableObject, ICupRewardsProvider
    {
        [SerializeField] private CupRewardSO[] _coinsRewardsPacks;
        [SerializeField] private CupRewardSO[] _playerItemRewardsPacks;

        private CustomCupReward[] _coinsRewards;
        private CustomCupReward[] _playerItemRewards;

        CustomCupReward[] ICupRewardsProvider.CoinsRewards
        {
            get
            {
                return _coinsRewards ??= _coinsRewardsPacks.Select(r => r.GetCupReward()).ToArray();
            }
        }

        CustomCupReward[] ICupRewardsProvider.PlayerItemRewards
        {
            get
            {
                return _playerItemRewards ??= _playerItemRewardsPacks.Select(r => r.GetCupReward()).ToArray();
            }
        }
    }
}
