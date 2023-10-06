using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatisticsManager : MonoBehaviour
{
    public static BattleStatisticsManager Instance { get; private set; }

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

        Load();
    }

    private const string saveKey = "battleStatisticsSave";

    #region BattlesAmount

    [SerializeField]private int _battlesAmount;
    public int BattlesAmount
    {
        get { return _battlesAmount; }
        private set
        {
            _battlesAmount = value;

            Save();
        }
    }

    public void AddBattle()
    {
        BattlesAmount = _battlesAmount + 1;
    }
    #endregion

    #region NumberOfFirstPlaces

    [SerializeField] private int _numberOfFirstPlaces;
    public int NumberOfFirstPlaces
    {
        get { return _numberOfFirstPlaces; }
        private set
        {
            _numberOfFirstPlaces = value;

            Save();
        }
    }

    public void AddFirstPlace()
    {
        NumberOfFirstPlaces = _numberOfFirstPlaces + 1;
    }
    #endregion

    #region SaveLoad
    private void Load()
    {
        var data = PunchCars.SaveData.SaveManager.Load<PunchCars.SaveData.PlayerBattleStatistics>(saveKey);

        BattlesAmount = data.battlesAmount;
        NumberOfFirstPlaces = data.numberOfFirstPlaces;
    }

    private void Save()
    {
        PunchCars.SaveData.SaveManager.Save(saveKey, GetPlayerBattleStatistics());
    }

    private PunchCars.SaveData.PlayerBattleStatistics GetPlayerBattleStatistics()
    {
        var data = new PunchCars.SaveData.PlayerBattleStatistics()
        {
            battlesAmount = BattlesAmount,
            numberOfFirstPlaces = NumberOfFirstPlaces
        };

        return data;
    }
    #endregion
}
