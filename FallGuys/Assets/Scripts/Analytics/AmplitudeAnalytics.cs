using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAnalytics : IAnalytics
{
    Amplitude amplitude = null;

    public void Initialize()
    {
        amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init("YOUR_API_KEY");
    }

    public string GetName()
    {
        return "AmplitudeAnalytics";
    }
}
