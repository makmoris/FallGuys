using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Network Connection")]
    [SerializeField] private NetworkChecker networkChecker;

    [Header("Analytics")]
    [SerializeField] private AnalyticsManager analyticsManager;

    [Header("ADS")]
    [SerializeField] private AdsInitializer adsInitializer;

    [Header("Scene Loader")]
    [SerializeField] private SceneLoader sceneLoader;

    [Header("Loader")]
    [SerializeField] private LoadingScene loadingScene;

    private void Awake()
    {
        networkChecker.Initialize();
        analyticsManager.Initialize();
        adsInitializer.Initialize();

        sceneLoader.Initialize(networkChecker);

        loadingScene.Initialize(networkChecker, analyticsManager, adsInitializer);
    }
}
