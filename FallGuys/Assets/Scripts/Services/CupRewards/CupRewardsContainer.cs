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
        [OnValueChanged("UpdateAllRewardsPacks")]
        [SerializeField] private CupRewardSO[] _coinsRewardsPacks, _playerItemRewardsPacks;

        [Header("All Rewards (Sorted by cups increase)")]
        [ReadOnly]
        [InfoBox("Duplicate CUPS VALUE were found! This could cause errors!", InfoMessageType.Error,
            "CheckForDuplicate")]
        public CupRewardSO[] _allRewardsPacks;

        private CustomCupReward[] _coinsRewards;
        private CustomCupReward[] _playerItemRewards;

        private CustomCupReward[] _allRewards;

        //CustomCupReward[] ICupRewardsProvider.CoinsRewards
        //{
        //    get
        //    {
        //        return _coinsRewards ??= _coinsRewardsPacks.Select(r => r.GetCupReward()).ToArray();
        //    }
        //}

        //CustomCupReward[] ICupRewardsProvider.PlayerItemRewards
        //{
        //    get
        //    {
        //        return _playerItemRewards ??= _playerItemRewardsPacks.Select(r => r.GetCupReward()).ToArray();
        //    }
        //}

        CustomCupReward[] ICupRewardsProvider.GetRewards
        {
            get
            {
                return _allRewards ??= _allRewardsPacks.Select(r => r.GetCupReward()).ToArray();
            }
        }

        private void UpdateAllRewardsPacks()
        {
            Array.Clear(_allRewardsPacks, 0, _allRewardsPacks.Length);

            var allRewards = _coinsRewardsPacks.Take(_coinsRewardsPacks.Length)
                .Concat(_playerItemRewardsPacks.Take(_playerItemRewardsPacks.Length)).ToArray();

            _allRewardsPacks = allRewards.OrderBy(r => r.GetCupReward().CupsForReward).ToArray();
        }

        private bool CheckForDuplicate()
        {
            bool checkForDuplicate = _allRewardsPacks.GroupBy(v => v.GetCupReward().CupsForReward).Where(g => g.Count() > 1).
                Select(g => g.Key).ToArray().Length != 0;

            if (checkForDuplicate)
            {
                var duplicateElements = _allRewardsPacks.Select((el, idx) => (el, idx))
                .GroupBy(c => c.el.GetCupReward().CupsForReward)
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Select(c => c.idx).ToList())
                .ToList();

                for (int i = 0; i < duplicateElements.Count; i++)
                {
                    Debug.LogError($"{i+1}) Duplicate CUPS VALUE in CupRewardsContainer = {_allRewardsPacks[i]}");
                }
            }

            return checkForDuplicate;
        }
    }
}
