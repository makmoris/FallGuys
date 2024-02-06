using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static event Action<int> GoldUpdateEvent;
    public static event Action<int> CupsUpdateEvent;

    public static CurrencyManager Instance { get; private set; }

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

    private const string saveKey = "currencySave";

    //

    #region Gold
    [SerializeField]private int _gold;
    public int Gold
    {
        get {   return _gold; }
        private set 
        { 
            _gold = value;

            GoldUpdateEvent?.Invoke(_gold);

            Save();
        }
    }

    public void AddGold(int value)
    {
        Gold = _gold + value;
    }

    public bool SpendGold(int value)// true - если можем потратить столько денег
    {
        int result = _gold - value;

        if (result >= 0)
        {
            Gold = result;
            return true;
        }
        else
        {
            Debug.Log("Недостаточно золота");
            return false;
        }
    }
    #endregion

    //

    #region Cups
    private int _maxCups;// должен равняться кубкам высшей лиги
    private bool _isMaxCupsValueSetted;

    [SerializeField] private int _cups;
    public int Cups
    {
        get 
        {
            return _cups; 
        }
        private set
        {
            _previousCups = _cups;

            _cups = value;

            if (_cups <= 0) _cups = 0;

            if (_cups > _maxCups && _isMaxCupsValueSetted) _cups = _maxCups;

            CupsUpdateEvent?.Invoke(_cups);

            Save();
        }
    }

    [SerializeField] private int _previousCups;
    public int PreviousCups => _previousCups;

    public void SetMaxCups(int maxValue)// leagueManager
    {
        _maxCups = maxValue;
        _isMaxCupsValueSetted = true;
    }

    public void AddCup(int value)
    {
        Cups = _cups + value;
    }
    #endregion

    //

    #region SaveLoad
    private void Load()
    {
        var data = PunchCars.SaveData.SaveManager.Load<PunchCars.SaveData.PlayerCurrency>(saveKey);

        _gold = data.gold;
        _cups = data.cups;
        _previousCups = data.previousCups;
    }

    private void Save()
    {
        PunchCars.SaveData.SaveManager.Save(saveKey, GetPlayerCurrency());
    }

    private PunchCars.SaveData.PlayerCurrency GetPlayerCurrency()
    {
        var data = new PunchCars.SaveData.PlayerCurrency()
        {
            gold = Gold,
            cups = Cups,
            previousCups = _previousCups
        };

        return data;
    }
    #endregion
}
