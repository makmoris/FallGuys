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
    [Header("Reward For Frag")]
    [SerializeField] internal int goldRewardForFrag;
    [SerializeField] internal int cupRewardForFrag;
    [Header("Reward")]
    [SerializeField] internal List<PlaceAwards> placeAwards;
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
    }

    public int ReceiveGoldsAsReward(int place)
    {
        int goldAward;

        int leagueLevel = currentLeagueLevel - 1;
        // ���� ����� ����� ������ � ��������, �� ���� ������. ���� ��� ����� �� �������������, �� ���� ������� �� ��������� �����
        // ����. ��������� 5 ����, �� 6 �������. ��� 6 ������, ��� ����� ������� ��� �� 5�� �����
        if (place <= leagueLevels[leagueLevel].placeAwards.Count) goldAward = leagueLevels[leagueLevel].placeAwards[place - 1].goldsValue;
        else goldAward = leagueLevels[leagueLevel].placeAwards[leagueLevels[leagueLevel].placeAwards.Count - 1].goldsValue;

        return goldAward;
    }
    public int ReceiveCupsAsReward(int place)
    {
        int cupAward;

        int leagueLevel = currentLeagueLevel - 1;

        if (place <= leagueLevels[leagueLevel].placeAwards.Count) cupAward = leagueLevels[leagueLevel].placeAwards[place - 1].cupsValue;
        else cupAward = leagueLevels[leagueLevel].placeAwards[leagueLevels[leagueLevel].placeAwards.Count - 1].cupsValue;

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


    private void OnEnable()
    {
        CurrencyManager.CupsUpdateEvent += UpdateLeagueLevel;
    }
    private void OnDisable()
    {
        CurrencyManager.CupsUpdateEvent -= UpdateLeagueLevel;
    }
}
