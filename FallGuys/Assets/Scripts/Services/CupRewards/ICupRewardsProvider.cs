namespace PunchCars.CupRewards
{
    public interface ICupRewardsProvider
    {
        //public CustomCupReward[] CoinsRewards { get; }
        //public CustomCupReward[] PlayerItemRewards { get; }
        public CustomCupReward[] GetRewards { get; }
    }
}
