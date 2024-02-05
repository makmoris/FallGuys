using PunchCars.InAppPurchasing;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using PunchCars.UserInterface.Views;
using PunchCars.Models;

namespace PunchCars.UserInterface.Presenters
{
    public class ShopPresenter : BasePresenter<ShopWindow>
    {
        private readonly ShopModel _shopModel;
        private readonly ShopWindow _shopView;

        private readonly IProductsProvider _productsProvider;

        [Inject]
        public ShopPresenter(IUiSystem uiSystem, IProductsProvider productsProvider, ShopModel shopModel)
        {
            _shopView = uiSystem.GetView<ShopWindow>();
            _productsProvider = productsProvider;
            _shopModel = shopModel;
        }

        public void OnShopViewShowed()
        {
            // окно магазина открыто. «аполн€ем данными

            FillCoinsProductsSection();
        }

        public void OnCoinsSectionShowBtnClick()
        {

        }

        private void FillCoinsProductsSection()
        {
            if (_productsProvider.CoinsPacks.Length <= 0)
                return;

            List<CoinsShopItem> coinsItems = _shopView.CreateCoinsItems(_productsProvider.CoinsPacks.Length);

            for (var i = 0; i < coinsItems.Count; i++)
            {
                CustomProduct product = _productsProvider.CoinsPacks[i];
                CoinsShopItem shopItem = coinsItems[i];
                shopItem.SetPrice(product.BuyMethode, _shopModel.GetProductRealMoneyPrice(product));
                shopItem.SetAmount((int)product.Amount);
                shopItem.SetIcon(product.Icon);
                shopItem.SetBestValueLabel(product.BestValue);
                shopItem.SetMostPopularLabel(product.MostPopular);
                shopItem.SetBuyClickCallback(() => _shopModel.BuyProduct(product));
            }
        }
    }
}
