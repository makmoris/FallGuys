namespace PunchCars.InAppPurchasing
{
    public interface IProductsProvider
    {
        public CustomProduct[] MoneyPacks { get; }
        public CustomProduct[] DailyOfferDays { get; }
        public CustomProduct[] GetAllProducts();
    }
}
