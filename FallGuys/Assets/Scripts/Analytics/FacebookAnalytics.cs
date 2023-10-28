using Facebook.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FacebookAnalytics : IAnalytics
{
    public event Action Initialization—ompletedEvent;

    #region Initialize
    public void Initialize() // Calling from AnalyticsManager
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            Initialization—ompletedEvent?.Invoke();
            UnityEngine.Debug.Log("[A] Facebook Initialization —ompleted");
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    #endregion

    public string GetName()
    {
        return "FacebookAnalytics";
    }

    #region BATTLE
    public void GameStart(int league_id, int battle_id, string car_id, string gun_id, int config_levels_id)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["car_id"] = car_id;
        param["gun_id"] = gun_id;
        param["config_levels_id"] = config_levels_id;

        FB.LogAppEvent("Game_Start", parameters: param);
    }

    public void LevelStart(string genre_id, string map_id, int league_id, int battle_id, int players_amount, bool user_play)
    {
        var param = new Dictionary<string, object>();
        param["genre_id"] = genre_id;
        param["map_id"] = map_id;
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["players_amount"] = players_amount;

        string boolToString_UserPlay = "false";
        if (user_play)
        {
            boolToString_UserPlay = "true";
        }
        param["user_play"] = boolToString_UserPlay;

        FB.LogAppEvent("Level_Start", parameters: param);
    }

    public void PlayerCompleteLevel(string genre_id, string map_id, int league_id, int battle_id)
    {
        var param = new Dictionary<string, object>();
        param["genre_id"] = genre_id;
        param["map_id"] = map_id;
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;

        FB.LogAppEvent("Player_Complete_Level", parameters: param);
    }

    public void PlayerFailLevel(string genre_id, string map_id, int league_id, int battle_id)
    {
        var param = new Dictionary<string, object>();
        param["genre_id"] = genre_id;
        param["map_id"] = map_id;
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;

        FB.LogAppEvent("Player_Fail_Level", parameters: param);
    }

    public void LevelFinish(string genre_id, string map_id, int league_id, int battle_id, int players_amount, int player_place, bool user_play)
    {
        var param = new Dictionary<string, object>();
        param["genre_id"] = genre_id;
        param["map_id"] = map_id;
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["players_amount"] = players_amount;
        param["player_place"] = player_place;

        string boolToString_UserPlay = "false";
        if (user_play)
        {
            boolToString_UserPlay = "true";
        }
        param["user_play"] = boolToString_UserPlay;

        FB.LogAppEvent("Level_Finish", parameters: param);
    }

    public void PlayerLeaveGame(int league_id, int battle_id, string leave_type)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["leave_type"] = leave_type;

        FB.LogAppEvent("Player_Leave_Game", parameters: param);
    }

    public void GameFinish(int league_id, int battle_id, int player_place, int config_levels_id)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["player_place"] = player_place;
        param["config_levels_id"] = config_levels_id;

        FB.LogAppEvent("Game_Finish", parameters: param);
    }

    public void PlayerGetBattleReward(int league_id, int battle_id, int player_gold_earn, int player_cups_earn)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["player_gold_earn"] = player_gold_earn;
        param["player_cups_earn"] = player_cups_earn;

        FB.LogAppEvent("Player_Get_Battle_Reward", parameters: param);
    }

    #endregion

    #region GENERAL
    public void User(int battles_amount, float win_rate, int cups_amount, int league, int gold, string car_id, string gun_id, string control_type)
    {
        var param = new Dictionary<string, object>();
        param["battles_amount"] = battles_amount;
        param["win_rate"] = win_rate;
        param["cups_amount"] = cups_amount;
        param["league"] = league;
        param["gold"] = gold;
        param["car_id"] = car_id;
        param["gun_id"] = gun_id;
        param["control_type"] = control_type;

        FB.LogAppEvent("User", parameters: param);
    }

    public void PlayerBuyCar(string new_car_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["new_car_id"] = new_car_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        FB.LogAppEvent("Player_Buy_Car", parameters: param);
    }

    public void PlayerChangedCar(string new_car_id, string old_car_id)
    {
        var param = new Dictionary<string, object>();
        param["new_car_id"] = new_car_id;
        param["old_car_id"] = old_car_id;

        FB.LogAppEvent("Player_Changed_Car", parameters: param);
    }

    public void PlayerBuyGun(string new_gun_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["new_gun_id"] = new_gun_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        FB.LogAppEvent("Player_Buy_Gun", parameters: param);
    }

    public void PlayerChangedGun(string new_gun_id, string old_gun_id)
    {
        var param = new Dictionary<string, object>();
        param["new_gun_id"] = new_gun_id;
        param["old_gun_id"] = old_gun_id;

        FB.LogAppEvent("Player_Changed_Gun", parameters: param);
    }

    public void PlayerBuySkin(string car_id, string skin_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["car_id"] = car_id;
        param["skin_id"] = skin_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        FB.LogAppEvent("Player_Buy_Skin", parameters: param);
    }

    public void PlayerChangedSkin(string car_id, string new_skin_id, string old_skin_id)
    {
        var param = new Dictionary<string, object>();
        param["car_id"] = car_id;
        param["new_skin_id"] = new_skin_id;
        param["old_skin_id"] = old_skin_id;

        FB.LogAppEvent("Player_Changed_Skin", parameters: param);
    }

    public void PlayerChangedControls(string new_control_type, string old_control_type)
    {
        var param = new Dictionary<string, object>();
        param["new_control_type"] = new_control_type;
        param["old_control_type"] = old_control_type;

        FB.LogAppEvent("Player_Changed_Controls", parameters: param);
    }

    public void PlayerInternetConnectionRestore()
    {
        FB.LogAppEvent("Player_Internet_Connection_Restore");
    }
    #endregion
}
