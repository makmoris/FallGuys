using Firebase;
using Firebase.Analytics;

public class FirebaseAnalytics : IAnalytics
{
    public void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }

    public string GetName()
    {
        return "FirebaseAnalytics";
    }
}
