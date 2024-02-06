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

            _cupRewardsView.GetCupRewardProgressScale()
                .SetNewProgressScalePosition(_cupRewardsModel.GetPreviousCupsValue(), _cupRewardsModel.GetCurrentCupsValue());
        }

        private void FillCupRewardsSection(int currentCupsValue)
        {
            if (_cupRewardsProvider.GetRewards.Length <= 0)
                return;

            // Возможно здесь еще понадобится проверять акутальна ли награда (PlayerItem), т.к. ее могли купить в магазине
            // не дожидаясь получения в качестве награды. Нужно помечать как полученную

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
                        rewardItem.SetCoinsText(reward.CupsForReward.ToString());

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

                if(currentCupsValue > reward.CupsForReward) rewardItem.ShowThisRewardWasReceived();
                else rewardItem.ShowThisRewardWasNotReceived();


                bool isFirstRewardItem = false;
                bool isLastRewardItem = false;

                if (i == 0) isFirstRewardItem = true;
                else if (i == cupRewardItems.Count - 1) isLastRewardItem = true;

                cupRewardProgressScale.AddCupReward(rewardItem, reward.CupsForReward, isFirstRewardItem, isLastRewardItem);
            }
        }
    }
}
