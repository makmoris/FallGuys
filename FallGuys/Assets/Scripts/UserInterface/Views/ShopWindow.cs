using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunchCars.UserInterface.Presenters;

namespace PunchCars.UserInterface.Views
{
    public class ShopWindow : BaseUiView<ShopPresenter>
{
        [Header("Prefabs")]
        [SerializeField] private CoinsShopItem _coinsItemPrefab;

        [Header("Containers")]
        [SerializeField] private RectTransform _coinsSectionParent;

        private readonly List<CoinsShopItem> _coinsShopItems = new List<CoinsShopItem>();

        public void OnCoinsSectionShow()// по кнопочке из лобби
        {
            if (!gameObject.activeSelf)
            {
                Presenter.OnShopViewShowed();
                gameObject.SetActive(true);
            }

            Presenter.OnCoinsSectionShowBtnClick();

            #region Analytics
            AnalyticsManager.Instance.LogUserOpenShop();
            #endregion
        }

        public List<CoinsShopItem> CreateCoinsItems(int count)
        {
            for (var i = 0; i < count; i++)
                _coinsShopItems.Add(Instantiate(_coinsItemPrefab, _coinsSectionParent));

            return _coinsShopItems;
        }

        private void ClearCoinsItems()
        {
            foreach (CoinsShopItem coinsShopItem in _coinsShopItems)
                Destroy(coinsShopItem.gameObject);

            _coinsShopItems.Clear();
        }

        private void OnDisable()
        {
            ClearCoinsItems();
        }
    }
}

