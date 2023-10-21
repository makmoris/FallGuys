using System.Collections;
using UnityEngine;

public class UserStatistics : MonoBehaviour // only on lobby
{
    private int cups_amount;
    private int gold;
    private string control_type;

    private void OnEnable()
    {
        StartCoroutine(WaitAndSend());
    }

    private void SendUserAnalyticEvent()
    {
        cups_amount = CurrencyManager.Instance.Cups;

        gold = CurrencyManager.Instance.Gold;

        control_type = GetCurrentLayout();

        AnalyticsManager.Instance.User(cups_amount, gold, control_type);
    }

    IEnumerator WaitAndSend()
    {
        yield return new WaitForSeconds(1f);
        SendUserAnalyticEvent();
    }

    private string GetCurrentLayout()
    {
        string key = "Layout";
        int layoutIndex = PlayerPrefs.GetInt(key, 0);

        string layoutName = "";

        switch (layoutIndex)
        {
            case 0:// arrows
                layoutName = "Arrows";
                break;

            case 1:// leftRught
                layoutName = "LeftRight_Joystick";
                break;

            case 2:// allJoystick
                layoutName = "Full_Joystick";
                break;
        }

        return layoutName;
    }
}
