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
        amplitude.init("c93c41a93063b32bed422c3a91e96264");
    }

    public string GetName()
    {
        return "AmplitudeAnalytics";
    }
}
