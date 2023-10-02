using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class Shop : MonoBehaviour, IDetailedStoreListener
{
    IStoreController storeController;
    IExtensionProvider extensionProvider;

    //private System.Action OnPurchaseCompleted;

    private async void Awake()
    {
        var options = new InitializationOptions();

        await UnityServices.InitializeAsync(options);

        ResourceRequest operation = Resources.LoadAsync<TextAsset>("IAPProductCatalog");
        operation.completed += HandleIAPCatalogLoaded;
    }

    private void HandleIAPCatalogLoaded(AsyncOperation operation)
    {
        ResourceRequest request = operation as ResourceRequest;
        Debug.Log($"Loaded Asset: {request.asset}");

        ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
        Debug.Log($"Loaded catalog with: {catalog.allProducts.Count} items");

#if UNITY_ANDROID
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));
#elif UNITY_IOS
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore));
#endif

        foreach (ProductCatalogItem item in catalog.allProducts)
        {
            builder.AddProduct(item.id, item.type);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // кинуть этот скрипт на первую сцену загрузки и здесь событие которое будет отправляться лоадеру, что можно загружать дальше
        Debug.Log("IAP Initialized");
        storeController = controller;
        extensionProvider = extensions;
    }

    public void ConsumbleButtonPress(Product product)// вызывается кнопкой
    {
        storeController.InitiatePurchase(product);
        //this.OnPurchaseCompleted = OnPurchaseCompleted;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)// смотрим какая покупка была совершена
    {
        Debug.Log($"Purchase Complete = {purchaseEvent.purchasedProduct.definition.id}");

        var payout = purchaseEvent.purchasedProduct.definition.payout;

        switch (payout.type)
        {
            case PayoutType.Currency:

                if(payout.subtype == "Gold")
                {
                    CurrencyManager.Instance.AddGold((int)payout.quantity);
                }

                break;
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Error initializing IAP because of {error}." +
            $"\r\nShow a message to the player depending on the error");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //Debug.Log($"Failed to purchase {product.definition.id} because {failureReason}");
        //OnPurchaseCompleted?.Invoke();
        //OnPurchaseCompleted = null;
        
    }
}
