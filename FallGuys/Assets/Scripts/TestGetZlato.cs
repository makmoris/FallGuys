using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGetZlato : MonoBehaviour
{
    public int cups;
    public void GetZlato()
    {
        CurrencyManager.Instance.AddGold(20);
        CurrencyManager.Instance.AddCup(cups);
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
