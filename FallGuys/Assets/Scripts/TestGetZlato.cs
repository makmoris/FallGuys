using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGetZlato : MonoBehaviour
{
    public void GetZlato()
    {
        CurrencyManager.Instance.AddGold(20);
        CurrencyManager.Instance.AddCup(5);
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
