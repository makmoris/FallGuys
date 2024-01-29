using PunchCars.InAppPurchasing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Zenject;

namespace PunchCars.Models
{
    public class ShopModel
    {
        private IIAPService _iapService;
        private IProductsProvider _productsProvider;

        private Action _onProductBought;

        [Inject]
        public ShopModel(IIAPService iapService, IProductsProvider productsProvider)
        {
            _iapService = iapService;
            _productsProvider = productsProvider;
        }

        public string GetProductRealMoneyPrice(CustomProduct product)
        {
            if (product.BuyMethode == BuyMethode.RealMoney)
            {
                string priceString = _iapService.GetLocalizedPriceString(product.ID);

                return priceString;
            }

            return product.Price.ToString(CultureInfo.InvariantCulture);
        }

        public void BuyProduct(CustomProduct customProduct, float discount = 0, Action onBought = null)
        {
            _onProductBought = onBought;

            switch (customProduct.BuyMethode)
            {
                case BuyMethode.RealMoney:
                    BuyProductByRealMoney(customProduct.ID, discount);
                    break;
                    //case BuyMethode.Coins:
                    //    BuyProductByGold(customProduct.ID, discount);
                    //    break;
                    //case BuyMethode.RewardedAd:
                    //    BuyProductByRewardedAd(customProduct.ID);
                    //    break;
                    //default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BuyProductByRealMoney(string productID, float discount)
        {
            _iapService.BuyProduct(productID, OnPurchaseComplete, OnPurchaseFailed);
        }
        private void OnPurchaseComplete(string productID)
        {
            if (TryGetProductById(_productsProvider.CoinsPacks, productID, out CustomProduct moneyPack))
                HandleMoneyPackPurchase(moneyPack);

            _onProductBought?.Invoke();
        }
        private void HandleMoneyPackPurchase(CustomProduct moneyPack)
        {
            CurrencyManager.Instance.AddGold((int)moneyPack.Amount);
        }

        private void OnPurchaseFailed(string productID)
        {

        }

        private bool TryGetProductById(IEnumerable<CustomProduct> products, string productID, out CustomProduct product)
        {
            product = products.FirstOrDefault(p => p.ID.Equals(productID));

            return product != null;
        }
    }
}
