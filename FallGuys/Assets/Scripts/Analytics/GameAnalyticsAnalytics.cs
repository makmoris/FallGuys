using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsAnalytics : IAnalytics
{
    public void Initialize()
    {
        GameAnalytics.Initialize();
    }

    public string GetName()
    {
        return "GameAnalytics";
    }
}
