using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Collections.Generic;

public class FirebaseAnalytics : IAnalytics
{
    public event Action Initialization—ompletedEvent;

    public void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                InitializeFirebase();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    private void InitializeFirebase()
    {
        Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        Firebase.Analytics.FirebaseAnalytics.SetUserProperty(Firebase.Analytics.FirebaseAnalytics.UserPropertySignUpMethod, "Google");

        UnityEngine.Debug.Log("[A] Firebase Initialization —ompleted");

        Initialization—ompletedEvent?.Invoke();

        //Firebase.Analytics.FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
    }

    public string GetName()
    {
        return "FirebaseAnalytics";
    }

    #region BATTLE
    public void GameStart(int league_id, int battle_id, string car_id, string gun_id, int config_levels_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id),
            new Parameter("car_id", car_id),
            new Parameter("gun_id", gun_id),
            new Parameter("config_levels_id", config_levels_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Game_Start", param.ToArray());
    }

    public void LevelStart(string genre_id, string map_id, int league_id, int battle_id, int players_amount, bool user_play)
    {
        string isUserPlay = "false";
        if (user_play) isUserPlay = "true";

        var param = new List<Parameter>
        {
            new Parameter("genre_id", genre_id),
            new Parameter("map_id", map_id),
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id),
            new Parameter("players_amount", players_amount),
            new Parameter("user_play", isUserPlay)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_Start", param.ToArray());
    }

    public void PlayerCompleteLevel(string genre_id, string map_id, int league_id, int battle_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("genre_id", genre_id),
            new Parameter("map_id", map_id),
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Complete_Level", param.ToArray());
    }

    public void PlayerFailLevel(string genre_id, string map_id, int league_id, int battle_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("genre_id", genre_id),
            new Parameter("map_id", map_id),
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Fail_Level", param.ToArray());
    }

    public void LevelFinish(string genre_id, string map_id, int league_id, int battle_id, int players_amount, int player_place, bool user_play)
    {
        string isUserPlay = "false";
        if (user_play) isUserPlay = "true";

        var param = new List<Parameter>
        {
            new Parameter("genre_id", genre_id),
            new Parameter("map_id", map_id),
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id),
            new Parameter("players_amount", players_amount),
            new Parameter("player_place", player_place),
            new Parameter("user_play", isUserPlay)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_Finish", param.ToArray());
    }

    public void PlayerLeaveGame(int league_id, int battle_id, string leave_type)
    {
        var param = new List<Parameter>
        {
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id),
            new Parameter("leave_type", leave_type)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Leave_Game", param.ToArray());
    }

    public void GameFinish(int league_id, int battle_id, int player_place, int config_levels_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id),
            new Parameter("player_place", player_place),
            new Parameter("config_levels_id", config_levels_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Game_Finish", param.ToArray());
    }

    public void PlayerGetBattleReward(int league_id, int battle_id, int player_gold_earn, int player_cups_earn)
    {
        var param = new List<Parameter>
        {
            new Parameter("league_id", league_id),
            new Parameter("battle_id", battle_id),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("player_cups_earn", player_cups_earn)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Get_Battle_Reward", param.ToArray());
    }

    #endregion

    #region GENERAL
    public void User(int battles_amount, float win_rate, int cups_amount, int league, int gold, string car_id, string gun_id, string control_type)
    {
        var param = new List<Parameter>
        {
            new Parameter("battles_amount", battles_amount),
            new Parameter("win_rate", win_rate),
            new Parameter("cups_amount", cups_amount),
            new Parameter("league", league),
            new Parameter("gold", gold),
            new Parameter("car_id", car_id),
            new Parameter("gun_id", gun_id),
            new Parameter("control_type", control_type)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("User", param.ToArray());
    }

    public void PlayerBuyCar(string new_car_id, int gold_spent, int gold)
    {
        var param = new List<Parameter>
        {
            new Parameter("new_car_id", new_car_id),
            new Parameter("gold_spent", gold_spent),
            new Parameter("gold", gold)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Buy_Car", param.ToArray());
    }

    public void PlayerChangedCar(string new_car_id, string old_car_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("new_car_id", new_car_id),
            new Parameter("old_car_id", old_car_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Changed_Car", param.ToArray());
    }

    public void PlayerBuyGun(string new_gun_id, int gold_spent, int gold)
    {
        var param = new List<Parameter>
        {
            new Parameter("new_gun_id", new_gun_id),
            new Parameter("gold_spent", gold_spent),
            new Parameter("gold", gold)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Buy_Gun", param.ToArray());
    }

    public void PlayerChangedGun(string new_gun_id, string old_gun_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("new_gun_id", new_gun_id),
            new Parameter("old_gun_id", old_gun_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Changed_Gun", param.ToArray());
    }

    public void PlayerBuySkin(string car_id, string skin_id, int gold_spent, int gold)
    {
        var param = new List<Parameter>
        {
            new Parameter("car_id", car_id),
            new Parameter("skin_id", skin_id),
            new Parameter("gold_spent", gold_spent),
            new Parameter("gold", gold)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Buy_Skin", param.ToArray());
    }

    public void PlayerChangedSkin(string car_id, string new_skin_id, string old_skin_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("car_id", car_id),
            new Parameter("new_skin_id", new_skin_id),
            new Parameter("old_skin_id", old_skin_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Changed_Skin", param.ToArray());
    }

    public void PlayerChangedControls(string new_control_type, string old_control_type)
    {
        var param = new List<Parameter>
        {
            new Parameter("new_control_type", new_control_type),
            new Parameter("old_control_type", old_control_type)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Changed_Controls", param.ToArray());
    }

    public void PlayerInternetConnectionRestore()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Internet_Connection_Restore");
    }
    #endregion
}
