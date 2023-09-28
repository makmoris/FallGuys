using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private List<RewardButton> rewardButtonsList;
    private RewardButton _showAdButton;
    string _androidAdUnitId = "Rewarded_Android";
    string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Disable the button until the ad is ready to show:
        InitializeButtons();

        LoadAd();
    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            foreach (var button in rewardButtonsList)
            {
                button.Button.onClick.AddListener(delegate { ShowAd(button); });
                button.Button.interactable = true;
            }
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd(RewardButton _rewardButton)
    {
        _showAdButton = _rewardButton;

        foreach (var button in rewardButtonsList) button.Button.interactable = false;

        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.

            switch (_showAdButton.RewardType)
            {
                case RewardType.Money:
                    CurrencyManager.Instance.AddGold(_showAdButton.RewardValue);
                    break;
                case RewardType.Cups:
                    CurrencyManager.Instance.AddCup(_showAdButton.RewardValue);
                    break;
            }
        }

        foreach (var button in rewardButtonsList) button.Button.onClick.RemoveAllListeners();
        LoadAd();
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void InitializeButtons()
    {
        foreach (var button in rewardButtonsList)
        {
            button.Initialize();
            button.Button.interactable = false;
        }
    }


    void OnDestroy()
    {
        // Clean up the button listeners:
        //_showAdButton.onClick.RemoveAllListeners();

        foreach (var button in rewardButtonsList) button.Button.onClick.RemoveAllListeners();
    }
}
