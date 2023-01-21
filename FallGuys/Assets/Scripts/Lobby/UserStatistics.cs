using System.Collections;
using UnityEngine;

public class UserStatistics : MonoBehaviour
{
    private int battles_amount;
    private float win_rate;
    private int cups_amount;
    private int league;
    private int gold;
    private string car_id;
    private string gun_id;
    private string control_type;

    void Start()
    {
        GetParameters();
        SendUserAnalyticEvent();
    }

    private void GetParameters()
    {
        battles_amount = BattleStatisticsManager.Instance.BattlesAmount;

        int playerFirstPlaceValue = BattleStatisticsManager.Instance.NumberOfFirstPlaces;

        if (battles_amount != 0 && playerFirstPlaceValue != 0)
        {
            float firstPlacesPercent = (playerFirstPlaceValue / (float)battles_amount) * 100f;
            win_rate = firstPlacesPercent;
        }
        else win_rate = 0f;
        

        cups_amount = CurrencyManager.Instance.Cups;

        league = LeagueManager.Instance.GetCurrentLeagueLevel();

        gold = CurrencyManager.Instance.Gold;

        LobbyManager.Instance.SetCharacter();
        car_id = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;
        gun_id = CharacterManager.Instance.GetPlayerWeapon().GetComponent<WeaponId>().WeaponID;

        control_type = GetCurrentLayout();
    }

    private void SendUserAnalyticEvent()
    {
        StartCoroutine(WaitAndSendEvent());
        //AnalyticsManager.Instance.User(battles_amount, win_rate, cups_amount, league, gold, car_id, gun_id, control_type);
    }

    IEnumerator WaitAndSendEvent()
    {
        yield return new WaitForSeconds(2f);
        AnalyticsManager.Instance.User(battles_amount, win_rate, cups_amount, league, gold, car_id, gun_id, control_type);
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
