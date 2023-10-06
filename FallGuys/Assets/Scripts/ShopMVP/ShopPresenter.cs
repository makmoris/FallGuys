using PunchCars.InAppPurchasing;
using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

namespace PunchCars.ShopMVP
{
    public class ShopPresenter : IDisposable
    {
        private ShopModel _shopModel;
        private IShopView _shopView;

        private IProductsProvider _productsProvider;

        [Inject]
        public ShopPresenter(ShopModel model, IShopView view, IProductsProvider productsProvider)
        {
            _shopModel = model;
            _shopView = view;
            _productsProvider = productsProvider;

            _shopView.OnShow += OnViewShown;
            _shopView.OnHide += OnViewHide;
        }

        private void OnViewShown()
        {
            Debug.Log("SHOW");
            // обновляем кнопки нужной инфой. Берем инфу из Модели и передаем ее во Вью, чтобы он обновил данные (цена, доступность и т.д.)
            FillCoinsProductsSection();
        }

        private void OnViewHide()
        {
            Debug.Log("HIDE");
        }

        private void FillCoinsProductsSection()
        {
            if (_productsProvider.GoldPacks.Length <= 0) 
                return;

            List<CoinsShopItem> coinsItems = _shopView.CreateCoinsItems(_productsProvider.GoldPacks.Length);

            for (var i = 0; i < coinsItems.Count; i++)
            {
                CustomProduct product = _productsProvider.GoldPacks[i];
                CoinsShopItem shopItem = coinsItems[i];
                shopItem.SetPrice(product.BuyMethode, _shopModel.GetProductRealMoneyPrice(product));
                shopItem.SetAmount((int)product.Amount);
                shopItem.SetIcon(product.Icon);
                shopItem.SetBestValueLabel(product.BestValue);
                shopItem.SetMostPopularLabel(product.MostPopular);
                shopItem.SetBuyClickCallback(() => _shopModel.BuyProduct(product));
            }
        }

        public void Dispose()
        {
            _shopView.OnShow -= OnViewShown;
            _shopView.OnHide -= OnViewHide;
        }
    }
}
