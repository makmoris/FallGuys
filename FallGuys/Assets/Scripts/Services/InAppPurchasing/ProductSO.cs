using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Linq;

namespace PunchCars.InAppPurchasing
{
    [CreateAssetMenu(fileName = "New Product", menuName = "PunchCars/IAP/Product", order = 0)]
    public class ProductSO : ScriptableObject, IProductProvider
    {
        [SerializeField] private BuyMethode _buyMethode;
        [SerializeField, ValueDropdown("GetProductIdentifiers"), LabelText("IAP Id")]
        [ShowIf("_buyMethode", BuyMethode.RealMoney)]
        private string _iapId;
        [HideIf("_buyMethode", BuyMethode.RealMoney)]
        [SerializeField] private string _customId;
        [SerializeField] private string _name;
        [ShowIf("_buyMethode", BuyMethode.Coins)]
        [SerializeField] private float _price;
        [SerializeField] private float _amount;
        [SerializeField] private bool _mostPopular;
        [SerializeField] private bool _bestValue;
        [SerializeField, PreviewField(75)] private Sprite _icon;
        [SerializeField, TextArea(10, 10)] private string _description;

        public CustomProduct GetProduct()
        {
            string id = _buyMethode == BuyMethode.RealMoney ? _iapId : _customId;
            return new CustomProduct(id, _name, _description, _buyMethode, _icon, _price, _amount, _mostPopular, _bestValue);
        }

        private List<string> GetProductIdentifiers()
        {
            ProductCatalog catalog = ProductCatalog.LoadDefaultCatalog();

            return catalog.allProducts.Select(x => x.id).ToList();
        }
    }
}

