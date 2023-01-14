using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private List<IAnalytics> analytics = new List<IAnalytics>
    {
        new FacebookAnalytics(),
        new AmplitudeAnalytics(),
        new FirebaseAnalytics()
    };

    public static AnalyticsManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Initialize();
    }

    private void Initialize()
    {
        foreach (var item in analytics)
        {
            try
            {
                item.Initialize();
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Initialized");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Initialize");
            }
        }
    }
}
