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
            Debug.Log("Недостаточно золота");
            return false;
        }
    }

    
    private void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerCurrency>(saveKey);

        Gold = data.gold;
    }

    private void Save()
    {
        SaveManager.Save(saveKey, GetPlayerCurrency());
    }

    private SaveData.PlayerCurrency GetPlayerCurrency()
    {
        var data = new SaveData.PlayerCurrency()
        {
            gold = Gold
        };

        return data;
    }
}
