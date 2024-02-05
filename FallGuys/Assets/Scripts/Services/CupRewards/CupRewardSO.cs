using Sirenix.OdinInspector;
using UnityEngine;
using PunchCars.Leagues;
using PunchCars.PlayerItems;

namespace PunchCars.CupRewards
{
    [CreateAssetMenu(fileName = "New Cup Reward", menuName = "PunchCars/CupRewards/CupReward", order = 0)]
    public class CupRewardSO : ScriptableObject, ICupRewardProvider
    {
        [Header("REWARD:")]
        [SerializeField] private CupRewardType _rewardType;

        [ShowIf("_rewardType", CupRewardType.Coins)]
        [Min(0)][SerializeField] private int _coins;
        [ShowIf("_rewardType", CupRewardType.Coins)]
        [SerializeField, PreviewField(75)] private Sprite _coinsIcon;

        [ShowIf("_rewardType", CupRewardType.PlayerItem)][OnValueChanged("ShowCurrentPlayerItemSettings")]
        [SerializeField] private PlayerItemSO _playerItemData;
        [ReadOnly, ShowIf("_rewardType", CupRewardType.PlayerItem)]
        [PreviewField(75)] public Sprite _playerItemIcon;

        [Header("CUPS VALUE FOR REWARD:")]
        [SerializeField] private bool _useLeagueSettings;
        [HideIf("_useLeagueSettings", true)]
        [Min(0)] [SerializeField] private int _cupsForReward;
        [ShowIf("_useLeagueSettings", true)][OnValueChanged("ShowCurrentLeagueSettings")]
        [SerializeField] private LeagueSO _leagueData;

        [ReadOnly, ShowIf("_useLeagueSettings", true)]
        public string _leagueName;
        [ReadOnly, ShowIf("_useLeagueSettings", true)]
        [PreviewField(75)] public Sprite _leagueShieldIcon;
        [ReadOnly, ShowIf("_useLeagueSettings", true)]
        [PreviewField(75)] public Sprite _leagueLevelIcon;
        [ReadOnly, ShowIf("_useLeagueSettings", true)]
        public int _leagueCupsForReward;

        private Sprite _rewardIcon;

        public CustomCupReward GetCupReward()
        {
            switch (_rewardType)
            {
                case CupRewardType.Coins:
                    _rewardIcon = _coinsIcon;
                    break;

                case CupRewardType.PlayerItem:
                    _rewardIcon = _playerItemData.GetPlayerItem().Icon;
                    break;
            }

            if (_useLeagueSettings)
            {
                CustomLeague leagueData = _leagueData.GetLeague();
                _leagueName = leagueData.LeagueName;
                _leagueShieldIcon = leagueData.ShieldIcon;
                _leagueLevelIcon = leagueData.LevelIcon;
                _cupsForReward = leagueData.CupsForAvailable;
            }

            return new CustomCupReward(_rewardIcon, _rewardType, _useLeagueSettings, _leagueData,
                _cupsForReward, _coins, _playerItemData);
        }

        private void ShowCurrentLeagueSettings()
        {
            CustomLeague leagueData = _leagueData.GetLeague();
            _leagueName = leagueData.LeagueName;
            _leagueShieldIcon = leagueData.ShieldIcon;
            _leagueLevelIcon = leagueData.LevelIcon;
            _leagueCupsForReward = leagueData.CupsForAvailable;

            _cupsForReward = leagueData.CupsForAvailable;
        }

        private void ShowCurrentPlayerItemSettings()
        {
            CustomPlayerItem customPlayerItem = _playerItemData.GetPlayerItem();
            _playerItemIcon = customPlayerItem.Icon;
        }
    }
}
