using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.moon4xgames.punch_cars.10gold":
                Grant10Gold();
                break;
        }
    }

    private void Grant10Gold()
    {
        CurrencyManager.Instance.AddGold(10);
        Debug.Log("Add Gold");
    }
}
