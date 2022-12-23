using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceAwards
{
    [SerializeField] private string place;
    [SerializeField] internal int goldsValue;
    [SerializeField] internal int cupsValue;
}

[System.Serializable]
public class RewardLevel
{
    [SerializeField] private string leagueLevel;
    [SerializeField] internal int neededCups;
    [Header("Available Locations")]
    [SerializeField] internal List<string> locationNames;
    [Header("Reward For Frag")]
    [SerializeField] internal int goldRewardForFrag;
    [SerializeField] internal int cupRewardForFrag;
    [Header("Win Reward")]
    [SerializeField] internal List<PlaceAwards> winPlaceAwards;
    [Header("Lose Reward")]
    [SerializeField] internal List<PlaceAwards> losePlaceAwards;
}

public class LeagueManager : MonoBehaviour
{
    public static event System.Action<int> LeagueLevelUpdateEvent;

    [SerializeField] private int currentLeagueLevel;

    [SerializeField] private List<RewardLevel> leagueLevels;


    public static LeagueManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UpdateLeagueLevel(CurrencyManager.Instance.Cups);

        CurrencyManager.Instance.SetMaxCups(leagueLevels[leagueLevels.Count - 1].neededCups);
    }
    
    public List<string> GetAvailableLocations()// вызывается MapSelector-ом
    {
        int leagueLevel = currentLeagueLevel - 1;

        return leagueLevels[leagueLevel].locationNames;
    }

    public int ReceiveGoldsAsWinReward(int place)
    {
        int goldAward;

        int leagueLevel = currentLeagueLevel - 1;
        // если такое место забито в наградах, то даем нужную. Если это место не предусмотрено, то даем награду за последнее место
        // прим. объявлено 5 мест, но 6 игроков. Про 6 забыли, ему дадут награду как за 5ое место
        if (place <= leagueLevels[leagueLevel].winPlaceAwards.Count) goldAward = leagueLevels[leagueLevel].winPlaceAwards[place - 1].goldsValue;
        else goldAward = leagueLevels[leagueLevel].winPlaceAwards[leagueLevels[leagueLevel].winPlaceAwards.Count - 1].goldsValue;

        return goldAward;
    }
    public int ReceiveCupsAsWinReward(int place)
    {
        int cupAward;

        int leagueLevel = currentLeagueLevel - 1;

        if (place <= leagueLevels[leagueLevel].winPlaceAwards.Count) cupAward = leagueLevels[leagueLevel].winPlaceAwards[place - 1].cupsValue;
        else cupAward = leagueLevels[leagueLevel].winPlaceAwards[leagueLevels[leagueLevel].winPlaceAwards.Count - 1].cupsValue;

        return cupAward;
    }

    public int ReceiveGoldsAsLoseReward(int place)
    {
        int goldAward;

        int leagueLevel = currentLeagueLevel - 1;
        // если такое место забито в наградах, то даем нужную. Если это место не предусмотрено, то даем награду за последнее место
        // прим. объявлено 5 мест, но 6 игроков. Про 6 забыли, ему дадут награду как за 5ое место
        if (place <= leagueLevels[leagueLevel].losePlaceAwards.Count) goldAward = leagueLevels[leagueLevel].losePlaceAwards[place - 1].goldsValue;
        else goldAward = leagueLevels[leagueLevel].losePlaceAwards[leagueLevels[leagueLevel].losePlaceAwards.Count - 1].goldsValue;

        return goldAward;
    }
    public int ReceiveCupsAsLoseReward(int place)
    {
        int cupAward;

        int leagueLevel = currentLeagueLevel - 1;

        if (place <= leagueLevels[leagueLevel].losePlaceAwards.Count) cupAward = leagueLevels[leagueLevel].losePlaceAwards[place - 1].cupsValue;
        else cupAward = leagueLevels[leagueLevel].losePlaceAwards[leagueLevels[leagueLevel].losePlaceAwards.Count - 1].cupsValue;

        return cupAward;
    }

    public int GetGoldRewardForFrag()
    {
        int leagueLevel = currentLeagueLevel - 1;

        return leagueLevels[leagueLevel].goldRewardForFrag;
    }
    public int GetCupRewardForFrag()
    {
        int leagueLevel = currentLeagueLevel - 1;

        return leagueLevels[leagueLevel].cupRewardForFrag;
    }

    private void UpdateLeagueLevel(int cupsValue)
    {
        int _leagueLevel = 0;

        for (int i = 0; i < leagueLevels.Count; i++)
        {
            if (cupsValue >= leagueLevels[i].neededCups) _leagueLevel++;
            else break;
        }

        currentLeagueLevel = _leagueLevel;

        LeagueLevelUpdateEvent?.Invoke(currentLeagueLevel);
    }

    public int GetNeededCupsForLeagueLevel(int leagueLevelIndex)// вызывается LeagueWindowProgressVisualizer
    {
        if(leagueLevelIndex < leagueLevels.Count)
        {
            return leagueLevels[leagueLevelIndex].neededCups;
        }
        else
        {
            Debug.LogError("The number of leagues in the list of LEAGUE WINDOW does not match the number of leagues in the list of LEAGUE MANAGER");
            return 0;
        }
    }

    public int GetCurrentLeagueLevel()
    {
        return currentLeagueLevel;
    }

    private void OnEnable()
    {
        CurrencyManager.CupsUpdateEvent += UpdateLeagueLevel;
    }
    private void OnDisable()
    {
        CurrencyManager.CupsUpdateEvent -= UpdateLeagueLevel;
    }
}
