using System;

namespace PunchCars.Advertisment
{
    public interface IAdsProvider : IAdsRightsReceiver
    {
        public bool IsInterstitialAvailable();
        public bool IsRewardedAvailable();
        public bool IsBannerAvailable();
        public void ShowInterstitial(Action onWatched = null, Action onSkipped = null, Action onClicked = null);
        public void ShowRewarded(Action onWatched = null, Action onSkipped = null, Action onClicked = null);
        public void ShowBanner(BannerPosition bannerPosition, Action onClicked = null);
        public void HideBanner();
    }
}
