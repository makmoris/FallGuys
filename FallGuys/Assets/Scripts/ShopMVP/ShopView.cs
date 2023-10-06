using System;
using System.Collections.Generic;
using UnityEngine;

namespace PunchCars.ShopMVP
{
    public class ShopView : MonoBehaviour, IShopView
    {
        [Header("Prefabs")]
        [SerializeField] private CoinsShopItem _coinsItemPrefab;

        [Header("Containers")]
        [SerializeField] private RectTransform _coinsSectionParent;

        private readonly List<CoinsShopItem> _coinsShopItems = new List<CoinsShopItem>();

        private ShopPresenter _shopPresenter;

        public event Action OnShow;
        public event Action OnHide;

        public void Construct(ShopPresenter presenter)
        {
            _shopPresenter = presenter;
        }

        private void OnEnable()
        {
            Show();
        }

        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            ClearCoinsItems();

            OnHide?.Invoke();
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
            Hide();
        }
    }
}