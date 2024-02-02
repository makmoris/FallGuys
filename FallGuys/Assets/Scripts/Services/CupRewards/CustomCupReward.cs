using UnityEngine;

namespace PunchCars.CupRewards
{
    public class CustomCupReward
    {
        public Sprite RewardIcon { get; private set; }
        public CupRewardType RewardType { get; private set; }
        public Sprite LeagueIcon { get; private set; }
        public string LeagueName { get; private set; }
        public int CupsForReward { get; private set; }

        public CustomCupReward(Sprite rewardIcon, CupRewardType rewardType, Sprite leagueIcon, string leagueName, int cupsForReward)
        {
            RewardIcon = rewardIcon;
            RewardType = rewardType;
            LeagueIcon = leagueIcon;
            LeagueName = leagueName;
            CupsForReward = cupsForReward;
        }
    }
}
