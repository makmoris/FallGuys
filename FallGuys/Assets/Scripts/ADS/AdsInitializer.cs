using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] bool _testMode = true;
    [Space]
    [SerializeField] string _androidGameId = "5429243";
    [SerializeField] string _iOSGameId = "5429242";
    private string _gameId;

    public System.Action ADSInitializationCompleteEvent;

    public void Initialize()
    {
//#if UNITY_IOS
//            _gameId = _iOSGameId;
//#elif UNITY_ANDROID
//        _gameId = _androidGameId;
//#elif UNITY_EDITOR
//            _gameId = _androidGameId; //Only for testing the functionality in the Editor
//#endif
//        if (!Advertisement.isInitialized && Advertisement.isSupported)
//        {
//            Advertisement.Initialize(_gameId, _testMode, this);
//        }

    }


    public void OnInitializationComplete()
    {
        ADSInitializationCompleteEvent?.Invoke();
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
