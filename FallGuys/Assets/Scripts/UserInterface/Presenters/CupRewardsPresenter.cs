using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.UserInterface.Views;
using PunchCars.Models;
using Zenject;
using PunchCars.CupRewards;

namespace PunchCars.UserInterface.Presenters
{
    public class CupRewardsPresenter : BasePresenter<CupRewardsWindow>
    {
        private readonly CupRewardsModel _cupRewardsModel;
        private readonly CupRewardsWindow _cupRewardsView;

        private readonly ICupRewardsProvider _cupRewardsProvider;

        private System.Action _progressScaleFillingCompletedCallback;

        [Inject]
        public CupRewardsPresenter(CupRewardsModel cupRewardsModel, IUiSystem uiSystem, ICupRewardsProvider cupRewardsProvider)
        {
            _cupRewardsModel = cupRewardsModel;
            _cupRewardsView = uiSystem.GetView<CupRewardsWindow>();
            _cupRewardsProvider = cupRewardsProvider;
        }

        public void OnCupRewardsWindowShowed()
        {
            FillCupRewardsSection(_cupRewardsModel.GetCurrentCupsValue());

            _cupRewardsView.GetCupRewardProgressScale().SetProgressScalePosition(_cupRewardsModel.GetCurrentCupsValue());
        }

        public void OnCupRewardsWindowShowedAfterBattle()
        {
            FillCupRewardsSection(_cupRewardsModel.GetPreviousCupsValue());

            _progressScaleFillingCompletedCallback += CheckNeedToGiveReward;

            _cupRewardsView.GetCupRewardProgressScale().SetNewProgressScalePosition(
                _cupRewardsModel.GetPreviousCupsValue(), _cupRewardsModel.GetCurrentCupsValue(), _progressScaleFillingCompletedCallback);
        }

        public void OnCupRewardWindowClosed()
        {
            _cupRewardsModel.ClearReceivedCupRewardsData();
        }

        private void FillCupRewardsSection(int currentCupsValue)
        {
            if (_cupRewardsProvider.GetRewards.Length <= 0)
                return;

            // ¬озможно здесь еще понадобитс€ провер€ть акутальна ли награда (PlayerItem), т.к. ее могли купить в магазине
            // не дожида€сь получени€ в качестве награды. Ќужно помечать как полученную

            List<CupRewardItem> cupRewardItems = _cupRewardsView.CreateCupRewardItems(_cupRewardsProvider.GetRewards.Length);
            CupRewardProgressScale cupRewardProgressScale = _cupRewardsView.CreateCupRewardProgressScale();

            for (int i = 0; i < cupRewardItems.Count; i++)
            {
                CustomCupReward reward = _cupRewardsProvider.GetRewards[i];
                CupRewardItem rewardItem = cupRewardItems[i];

                switch (reward.RewardType)
                {
                    case CupRewardType.Coins:

                        rewardItem.SetCoinsIcon(reward.RewardIcon);
                        rewardItem.SetCoinsText(reward.CoinsRewardValue.ToString());
                        
                        rewardItem.ShowCoinsReward();

                        break;

                    case CupRewardType.PlayerItem:

                        rewardItem.SetPlayerItemIcon(reward.RewardIcon);
                        rewardItem.SetPlayerItemTierIcon(reward.PlayerItemData.GetPlayerItem().TierIcon);

                        rewardItem.ShowPlayerItemReward();

                        break;
                }

                if (reward.UseLeagueSettings)
                {
                    rewardItem.SetLeagueShieldIcon(reward.LeagueData.GetLeague().ShieldIcon);
                    rewardItem.SetLeagueLevelIcon(reward.LeagueData.GetLeague().LevelIcon);
                    rewardItem.SetLeagueNameText(reward.LeagueData.GetLeague().LeagueName, reward.LeagueData.GetLeague().LeagueID);

                    rewardItem.ShowLeagueSettings();
                }
                else rewardItem.HideLeagueSettings();

                rewardItem.SetCupValueForReward(reward.CupsForReward);

                if (currentCupsValue >= reward.CupsForReward)
                {
                    rewardItem.ShowThisRewardWasReceived();
                    if (reward.RewardType == CupRewardType.PlayerItem) reward.PlayerItemData.SetIsAvailable(true);

                    _cupRewardsModel.AddReceivedCupReward(reward);
                }
                else
                {
                    // if this reward has already been purchased in the store or is available by default from data file
                    if (reward.RewardType == CupRewardType.PlayerItem)
                    {
                        if(_cupRewardsModel.CheckPlayerItemAvailability(reward.PlayerItemData.GetPlayerItem().ID))
                        {
                            rewardItem.ShowThisRewardWasReceived();
                            _cupRewardsModel.AddReceivedCupReward(reward);
                        }
                        else rewardItem.ShowThisRewardWasNotReceived();

                        //if (reward.PlayerItemData.GetPlayerItem().IsAvailable)
                        //{
                        //    rewardItem.ShowThisRewardWasReceived();
                        //    _cupRewardsModel.AddReceivedCupReward(reward);
                        //}
                        //else rewardItem.ShowThisRewardWasNotReceived();
                    }
                    else rewardItem.ShowThisRewardWasNotReceived();
                }


                bool isFirstRewardItem = false;
                bool isLastRewardItem = false;

                if (i == 0) isFirstRewardItem = true;
                else if (i == cupRewardItems.Count - 1) isLastRewardItem = true;

                cupRewardProgressScale.AddCupReward(rewardItem, reward.CupsForReward, isFirstRewardItem, isLastRewardItem);
            }
        }

        private void CheckNeedToGiveReward()
        {
            _progressScaleFillingCompletedCallback -= CheckNeedToGiveReward;

            List<CupRewardItem> cupRewardItems = _cupRewardsView.GetCupRewardItems();
            for (int i = 0; i < cupRewardItems.Count; i++)
            {
                CustomCupReward reward = _cupRewardsProvider.GetRewards[i];
                CupRewardItem rewardItem = cupRewardItems[i];

                if (_cupRewardsModel.GetCurrentCupsValue() >= reward.CupsForReward)
                {
                    if (_cupRewardsModel.AddReceivedCupReward(reward))
                    {
                        rewardItem.ShowThisRewardWasReceived();

                        switch (reward.RewardType)
                        {
                            case CupRewardType.PlayerItem:

                                reward.PlayerItemData.SetIsAvailable(true);
                                _cupRewardsView.ShowPlayerItemRewardCongratulations(reward.RewardIcon);

                                Debug.LogError($"car available = {reward.PlayerItemData.GetPlayerItem().IsAvailable}");

                                break;
                            case CupRewardType.Coins:

                                _cupRewardsModel.AddCoinsFromReward(reward.CoinsRewardValue);
                                _cupRewardsView.ShowCoinsRewardCongratulations(reward.RewardIcon, reward.CoinsRewardValue.ToString());

                                break;
                        }

                        Debug.LogError($"Ќаграды {reward.RewardType} (номер {i + 1}) не была получена. ¬ыдаем ее игроку");
                    }
                }
                else break;
            }
        }
    }
}
