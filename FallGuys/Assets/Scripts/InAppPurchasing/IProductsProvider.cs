namespace PunchCars.InAppPurchasing
{
    public interface IProductsProvider
    {
        public CustomProduct[] GoldPacks { get; }
        public CustomProduct[] DailyOfferDays { get; }
        public CustomProduct[] GetAllProducts();
    }
}
