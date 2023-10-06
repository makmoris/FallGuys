using System;
using UnityEngine.Purchasing;

namespace PunchCars.InAppPurchasing
{
    public interface IIAPService
    {
        public void BuyProduct(string productID, Action<string> onComplete, Action<string> onFailed);
        public void RestorePurchases(Action onComplete, Action<string> onFailed);

        public string GetLocalizedTitle(string productID);
        public string GetLocalizedDescription(string productID);
        public string GetLocalizedPriceString(string productID);
        public bool ValidateProduct(string productID);
        string GetIsoCurrencyCode(string productID);
        public SubscriptionInfo GetSubscriptionInfo(string productID);
    }
}
