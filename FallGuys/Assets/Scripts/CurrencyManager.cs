using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
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
        get { return _gold; }
        private set 
        { 
            _gold = value;
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
            Debug.Log("Ќедостаточно золота");
            return false;
        }
    }
    #endregion

    //

    #region Cups
    [SerializeField] private int _cups;
    public int Cups
    {
        get { return _cups; }
        private set
        {
            _cups = value;
            Save();
        }
    }

    public void AddCup(int value)
    {
        Cups = _cups + value;
    }

    //public bool SpendCup(int value)// true - если можем потратить столько денег
    //{
    //    int result = _cups - value;

    //    if (result >= 0)
    //    {
    //        Cups = result;
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("Ќедостаточно кубков");
    //        return false;
    //    }
    //}
    #endregion

    //

    #region SaveLoad
    private void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerCurrency>(saveKey);

        Gold = data.gold;
        Cups = data.cups;
    }

    private void Save()
    {
        SaveManager.Save(saveKey, GetPlayerCurrency());
    }

    private SaveData.PlayerCurrency GetPlayerCurrency()
    {
        var data = new SaveData.PlayerCurrency()
        {
            gold = Gold,
            cups = Cups
        };

        return data;
    }
    #endregion
}
