using PunchCars.Leagues;
using PunchCars.PlayerItems;
using UnityEngine;

namespace PunchCars.CupRewards
{
    public class CustomCupReward
    {
        public Sprite RewardIcon { get; private set; }
        public CupRewardType RewardType { get; private set; }
        public bool UseLeagueSettings { get; private set; }
        public LeagueSO LeagueData { get; private set; }
        public int CupsForReward { get; private set; }
        public int CoinsRewardValue { get; private set; }
        public PlayerItemSO PlayerItemData { get; private set; }

        public CustomCupReward(Sprite rewardIcon, CupRewardType rewardType, bool useLeagueSettings, LeagueSO leagueData,
            int cupsForReward, int coinsRewardValue, PlayerItemSO playerItemData)
        {
            RewardIcon = rewardIcon;
            RewardType = rewardType;
            UseLeagueSettings = useLeagueSettings;
            LeagueData = leagueData;
            CupsForReward = cupsForReward;
            CoinsRewardValue = coinsRewardValue;
            PlayerItemData = playerItemData;
        }
    }
}
