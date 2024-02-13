namespace PunchCars.Advertisment
{
    public interface IAdvertisementService : IAdsProvider
    {
        public bool AdsRemoved { get; }
        public void RemoveAds();
    }
}
