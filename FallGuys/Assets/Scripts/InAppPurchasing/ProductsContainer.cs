using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PunchCars.InAppPurchasing
{
    [CreateAssetMenu(fileName = "New Products Container", menuName = "PuchCars/IAP/ProductsContainer", order = 0)]
    public class ProductsContainer : ScriptableObject, IProductsProvider
    {
        [InfoBox("Duplicate identifiers were found! This could cause errors!", InfoMessageType.Error,
            "HasDuplicatesIdentifiers")]
        [SerializeField] private ProductSO[] _moneyPacks;
        [SerializeField] private ProductSO[] _dailyOfferDays;

        private CustomProduct[] _moneyPackProducts;
        private CustomProduct[] _dailyOfferDaysProducts;

        CustomProduct[] IProductsProvider.MoneyPacks
        {
            get
            {
                return _moneyPackProducts ??= _moneyPacks.Select(p => p.GetProduct()).ToArray();
            }
        }

        CustomProduct[] IProductsProvider.DailyOfferDays
        {
            get
            {
                return _dailyOfferDaysProducts ??= _dailyOfferDays.Select(p => p.GetProduct()).ToArray();
            }
        }

        public CustomProduct[] GetAllProducts()
        {
            IProductsProvider provider = this;
            var allProducts = new List<CustomProduct>();
            allProducts.AddRange(provider.MoneyPacks);
            allProducts.AddRange(provider.DailyOfferDays);

            return allProducts.ToArray();
        }

        private bool HasDuplicatesIdentifiers()
        {
            var products = new List<CustomProduct>(_moneyPacks.Length);
            products.AddRange(_moneyPacks.Select(p => p.GetProduct()));
            var setOfUniqueIds = new HashSet<string>(products.Select(p => p.ID));

            return products.Count != setOfUniqueIds.Count;
        }
    }
}
