using UnityEngine;

namespace PunchCars.InAppPurchasing
{
    public class CustomProduct
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public BuyMethode BuyMethode { get; private set; }
        public Sprite Icon { get; private set; }
        public float Price { get; private set; }
        public float Amount { get; private set; }
        public bool MostPopular { get; private set; }
        public bool BestValue { get; private set; }

        public CustomProduct(string id, string name, string description,
            BuyMethode buyMethode, Sprite icon, float price, float amount, bool mostPopular, bool bestValue)
        {
            ID = id;
            Name = name;
            Description = description;
            BuyMethode = buyMethode;
            Icon = icon;
            Price = price;
            Amount = amount;
            MostPopular = mostPopular;
            BestValue = bestValue;
        }

        public void SetMostPopular(bool mostPopular) => MostPopular = mostPopular;
        public void SetBestValue(bool bestValue) => BestValue = bestValue;

        public CustomProduct Copy()
        {
            return new CustomProduct(ID, Name, Description, BuyMethode, Icon, Price, Amount, MostPopular, BestValue);
        }
    }
}
