using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace PunchCars.InAppPurchasing
{
    public class IAPService : IIAPService, IInitializable
    {
        private List<StoreProduct> _products;

        private Action<string> _onPurchaseComplete;
        private Action<string> _onPurchaseFailed;
        private Action _onRestoreComplete;
        private Action<string> _onRestoreFailed;

        [Inject]
        public IAPService()
        {
            
        }

        public void Initialize()
        {
            IAPManager.Instance.InitializeIAPManager(OnInitializationComplete);
        }

        public void BuyProduct(string productID, Action<string> onComplete, Action<string> onFailed)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);
            _onPurchaseComplete = onComplete;
            _onPurchaseFailed = onFailed;

            IAPManager.Instance.BuyProduct(productName, PurchaseCompleteHandler);
        }

        public void RestorePurchases(Action onComplete, Action<string> onFailed)
        {
            _onRestoreComplete = onComplete;
            _onRestoreFailed = onFailed;
            IAPManager.Instance.RestorePurchases(OnPurchasesRestorationHandler);
        }

        public string GetLocalizedTitle(string productID)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);

            return IAPManager.Instance.GetLocalizedTitle(productName);
        }

        public string GetLocalizedDescription(string productID)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);

            return IAPManager.Instance.GetLocalizedDescription(productName);
        }

        public string GetLocalizedPriceString(string productID)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);

            return IAPManager.Instance.GetLocalizedPriceString(productName);
        }

        public bool ValidateProduct(string productID)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);

            return IAPManager.Instance.IsActive(productName);
        }

        public string GetIsoCurrencyCode(string productID)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);

            return IAPManager.Instance.GetIsoCurrencyCode(productName);
        }

        public SubscriptionInfo GetSubscriptionInfo(string productID)
        {
            StoreProduct product = GetProductById(productID);
            ShopProductNames productName = IAPManager.Instance.ConvertNameToShopProduct(product.productName);

            return IAPManager.Instance.GetSubscriptionInfo(productName);
        }

        private void OnPurchasesRestorationHandler(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                _onRestoreComplete?.Invoke();
            }
            else
            {
                Debug.LogError(message);
                _onRestoreFailed?.Invoke(message);
            }
        }

        private void PurchaseCompleteHandler(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                _onPurchaseComplete?.Invoke(product.GetStoreID());
            }
            else
            {
                Debug.LogError(message);
                _onPurchaseFailed?.Invoke(message);
            }
        }

        private StoreProduct GetProductById(string productID)
        {
            StoreProduct product = _products.FirstOrDefault(p => productID.Equals(p.GetStoreID()));

            if (product == null)
                throw new Exception($"Product with id={productID} not found!");

            return product;
        }

        private void OnInitializationComplete(IAPOperationStatus status, string message, List<StoreProduct> products)
        {
            if (status != IAPOperationStatus.Success)
                return;

            _products = products;

            Debug.Log("IAP Service Completed Initialization");
        }
    }
}
