using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PunchCars.Leagues;

public class CupRewardItem : MonoBehaviour
{
    [Header("Reward Icon")]
    [SerializeField] private Image _coinsIcon;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [Space]
    [SerializeField] private Image _playerItemTierIcon;
    [SerializeField] private Image _playerItemIcon;
    [SerializeField] private Image _rewardReceivedIcon;
    [SerializeField] private Image _rewardReceivedCheckMarkIcon;
    [Header("League Icon")]
    [SerializeField] private RectTransform _leagueIconHolder;
    [Space]
    [SerializeField] private Image _leagueShieldIcon;
    [SerializeField] private Image _leagueLevelIcon;
    [Space]
    [SerializeField] private RectTransform _leagueNameTextHolder;
    [Space]
    [SerializeField] private TextMeshProUGUI _woodLeagueNameText;
    [SerializeField] private TextMeshProUGUI _steelLeagueNameText;
    [SerializeField] private TextMeshProUGUI _bronzeLeagueNameText;
    [SerializeField] private TextMeshProUGUI _silverLeagueNameText;
    [SerializeField] private TextMeshProUGUI _goldLeagueNameText;
    [SerializeField] private TextMeshProUGUI _diamondLeagueNameText;
    [Header("Available Color Icon")]
    [SerializeField] private Image _rewardReceivedColorIcon;
    [SerializeField] private Color _rewardReceivedColor;
    [SerializeField] private Color _rewardNotReceivedColor;
    [Header("Cup Value For Reward")]
    [SerializeField] private TextMeshProUGUI _cupValueForRewardText;

    private int _cupValueForReward;
    public int CupValueForReward => _cupValueForReward;

    public void SetCoinsIcon(Sprite coinsIcon) => _coinsIcon.sprite = coinsIcon;
    public void SetCoinsText(string coinsText) => _coinsText.text = coinsText;
    public void SetPlayerItemTierIcon(Sprite playerItemTierIcon) => _playerItemTierIcon.sprite = playerItemTierIcon;
    public void SetPlayerItemIcon(Sprite playerItemIcon) => _playerItemIcon.sprite = playerItemIcon;
    public void SetLeagueShieldIcon(Sprite leagueShieldIcon) => _leagueShieldIcon.sprite = leagueShieldIcon;
    public void SetLeagueLevelIcon(Sprite leagueLevelIcon) => _leagueLevelIcon.sprite = leagueLevelIcon;
    public void SetLeagueNameText(string leagueNameText, LeagueID leagueID) => ChooseAndActiveLeagueNameText(leagueNameText, leagueID);
    public void SetCupValueForReward(int cupValueForReward)
    {
        _cupValueForRewardText.text = cupValueForReward.ToString();
        _cupValueForReward = cupValueForReward;
    }

    public void ShowCoinsReward()
    {
        _coinsIcon.gameObject.SetActive(true);
        _coinsText.gameObject.SetActive(true);

        _playerItemTierIcon.gameObject.SetActive(false);
        _playerItemIcon.gameObject.SetActive(false);
    }

    public void ShowPlayerItemReward()
    {
        _playerItemTierIcon.gameObject.SetActive(true);
        _playerItemIcon.gameObject.SetActive(true);

        _coinsIcon.gameObject.SetActive(false);
        _coinsText.gameObject.SetActive(false);
    }

    public void ShowThisRewardWasReceived()
    {
        _rewardReceivedIcon.gameObject.SetActive(true);
        _rewardReceivedCheckMarkIcon.gameObject.SetActive(true);
        _rewardReceivedColorIcon.color = _rewardReceivedColor;
    }

    public void ShowThisRewardWasNotReceived()
    {
        _rewardReceivedIcon.gameObject.SetActive(false);
        _rewardReceivedCheckMarkIcon.gameObject.SetActive(false);
        _rewardReceivedColorIcon.color = _rewardNotReceivedColor;
    }

    public void ShowLeagueSettings()
    {
        _leagueIconHolder.gameObject.SetActive(true);
    }
    public void HideLeagueSettings()
    {
        _leagueIconHolder.gameObject.SetActive(false);
    }

    private void ChooseAndActiveLeagueNameText(string leagueNameText, LeagueID leagueID)
    {
        HideAllLeagueNameTexts();

        switch (leagueID)
        {
            case LeagueID.Wood_1:
            case LeagueID.Wood_2:
            case LeagueID.Wood_3:
            case LeagueID.Wood_4:

                _woodLeagueNameText.gameObject.SetActive(true);
                break;

            case LeagueID.Steel_1:
            case LeagueID.Steel_2:
            case LeagueID.Steel_3:
            case LeagueID.Steel_4:

                _steelLeagueNameText.gameObject.SetActive(true);
                break;

            case LeagueID.Bronze_1:
            case LeagueID.Bronze_2:
            case LeagueID.Bronze_3:
            case LeagueID.Bronze_4:

                _bronzeLeagueNameText.gameObject.SetActive(true);
                break;

            case LeagueID.Silver_1:
            case LeagueID.Silver_2:
            case LeagueID.Silver_3:
            case LeagueID.Silver_4:

                _silverLeagueNameText.gameObject.SetActive(true);
                break;

            case LeagueID.Gold_1:
            case LeagueID.Gold_2:
            case LeagueID.Gold_3:
            case LeagueID.Gold_4:

                _goldLeagueNameText.gameObject.SetActive(true);
                break;

            case LeagueID.Diamond_1:
            case LeagueID.Diamond_2:
            case LeagueID.Diamond_3:
            case LeagueID.Diamond_4:

                _diamondLeagueNameText.gameObject.SetActive(true);
                break;
        }
    }
    private void HideAllLeagueNameTexts()
    {
        for (int i = 0; i < _leagueNameTextHolder.transform.childCount; i++)
        {
            _leagueNameTextHolder.GetChild(i).gameObject.SetActive(false);
        }
    }
}
