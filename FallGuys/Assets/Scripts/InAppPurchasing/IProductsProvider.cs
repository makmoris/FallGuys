namespace PunchCars.InAppPurchasing
{
    public interface IProductsProvider
    {
        public CustomProduct[] CoinsPacks { get; }
        public CustomProduct[] DailyOfferDays { get; }
        public CustomProduct[] GetAllProducts();
    }
}
