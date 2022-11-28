using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetZlato : MonoBehaviour
{
    public void GetZlato()
    {
        CurrencyManager.Instance.AddGold(20);
        CurrencyManager.Instance.AddCup(5);
    }
}
