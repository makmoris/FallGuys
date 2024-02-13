namespace PunchCars.Advertisment
{
    public interface IAdsRightsReceiver
    {
        public void SetUserConsent(bool accept);
        public void SetCCPAConsent(bool accept);
    }
}
