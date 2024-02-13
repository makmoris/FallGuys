using System;
using UnityEngine;
using Zenject;

namespace PunchCars.Advertisment
{
    public class AdvertisementService : IAdvertisementService, IInitializable
    {
        private bool _loaded;
        private float _progress;
        private bool _adsRemoved;
        private Action _onInterstitialWatched;
        private Action _onInterstitialSkipped;
        private Action _onRewardedWatched;
        private Action _onRewardedSkipped;

        //bool ILoadableService.Loaded => _loaded;
        bool IAdvertisementService.AdsRemoved => Advertisements.Instance.CanShowAds();
       // float ILoadableService.Progress => _progress;

        [Inject]
        public AdvertisementService()
        {
        }

        //async Task ILoadableService.Load()
        //{
        //    Advertisements.Instance.Initialize();

        //    while (!Advertisements.initialized)
        //        await Task.Delay(100);

        //    _loaded = true;
        //    _progress = 1;
        //}

        public void Initialize()
        {
            Advertisements.Instance.Initialize();
        }

        void IAdsRightsReceiver.SetUserConsent(bool accept) => Advertisements.Instance.SetUserConsent(accept);

        void IAdsRightsReceiver.SetCCPAConsent(bool accept) => Advertisements.Instance.SetCCPAConsent(accept);

        bool IAdsProvider.IsInterstitialAvailable() => Advertisements.Instance.IsInterstitialAvailable();

        bool IAdsProvider.IsRewardedAvailable() => Advertisements.Instance.IsRewardVideoAvailable();

        bool IAdsProvider.IsBannerAvailable() => Advertisements.Instance.IsBannerAvailable();

        void IAdsProvider.ShowInterstitial(Action onWatched, Action onSkipped, Action onClicked)
        {
            _onInterstitialWatched = onWatched;
            _onInterstitialSkipped = onSkipped;

            if (onClicked != null)
                Debug.LogWarning("On AD clicked not working in this implementation!");

            Advertisements.Instance.ShowInterstitial(OnInterstitialClosed);
        }

        void IAdsProvider.ShowRewarded(Action onWatched, Action onSkipped, Action onClicked)
        {
            _onRewardedWatched = onWatched;
            _onRewardedSkipped = onSkipped;

            if (onClicked != null)
                Debug.LogWarning("On AD clicked not working in this implementation!");

            Advertisements.Instance.ShowRewardedVideo(OnRewardedCompleted);
        }

        void IAdsProvider.ShowBanner(BannerPosition bannerPosition, Action onClicked)
        {
            if (onClicked != null)
                Debug.LogWarning("On AD clicked not working in this implementation!");

            Advertisements.Instance.ShowBanner(bannerPosition == BannerPosition.Top ?
                global::BannerPosition.TOP : global::BannerPosition.BOTTOM);
        }

        void IAdsProvider.HideBanner()
        {
            Advertisements.Instance.HideBanner();
        }

        void IAdvertisementService.RemoveAds()
        {
            Advertisements.Instance.RemoveAds(true);
        }

        private void OnInterstitialClosed()
        {
            _onInterstitialSkipped?.Invoke();
            _onInterstitialWatched?.Invoke();
        }

        private void OnRewardedCompleted(bool watched)
        {
            if (watched)
                _onRewardedWatched?.Invoke();
            else
                _onRewardedSkipped?.Invoke();
        }
    }
}
