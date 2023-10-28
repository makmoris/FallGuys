using GameAnalyticsSDK;
using System;
using System.Collections.Generic;

public class GameAnalyticsAnalytics : IAnalytics
{
    public event Action Initialization—ompletedEvent;

    public void Initialize()
    {
        GameAnalytics.Initialize();

        Initialization—ompletedEvent?.Invoke();
        UnityEngine.Debug.Log("[A] GameAnalytics Initialization —ompleted");
    }

    public string GetName()
    {
        return "GameAnalytics";
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

        GameAnalytics.NewDesignEvent("Game_Start", param);
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

        GameAnalytics.NewDesignEvent("Level_Start", param);
    }

    public void PlayerCompleteLevel(string genre_id, string map_id, int league_id, int battle_id)
    {
        var param = new Dictionary<string, object>();
        param["genre_id"] = genre_id;
        param["map_id"] = map_id;
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;

        GameAnalytics.NewDesignEvent("Player_Complete_Level", param);
    }

    public void PlayerFailLevel(string genre_id, string map_id, int league_id, int battle_id)
    {
        var param = new Dictionary<string, object>();
        param["genre_id"] = genre_id;
        param["map_id"] = map_id;
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;

        GameAnalytics.NewDesignEvent("Player_Fail_Level", param);
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

        GameAnalytics.NewDesignEvent("Level_Finish", param);
    }

    public void PlayerLeaveGame(int league_id, int battle_id, string leave_type)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["leave_type"] = leave_type;

        GameAnalytics.NewDesignEvent("Player_Leave_Game", param);
    }

    public void GameFinish(int league_id, int battle_id, int player_place, int config_levels_id)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["player_place"] = player_place;
        param["config_levels_id"] = config_levels_id;

        GameAnalytics.NewDesignEvent("Game_Finish", param);
    }

    public void PlayerGetBattleReward(int league_id, int battle_id, int player_gold_earn, int player_cups_earn)
    {
        var param = new Dictionary<string, object>();
        param["league_id"] = league_id;
        param["battle_id"] = battle_id;
        param["player_gold_earn"] = player_gold_earn;
        param["player_cups_earn"] = player_cups_earn;

        GameAnalytics.NewDesignEvent("Player_Get_Battle_Reward", param);
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

        GameAnalytics.NewDesignEvent("User", param);
    }

    public void PlayerBuyCar(string new_car_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["new_car_id"] = new_car_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        GameAnalytics.NewDesignEvent("Player_Buy_Car", param);
    }

    public void PlayerChangedCar(string new_car_id, string old_car_id)
    {
        var param = new Dictionary<string, object>();
        param["new_car_id"] = new_car_id;
        param["old_car_id"] = old_car_id;

        GameAnalytics.NewDesignEvent("Player_Changed_Car", param);
    }

    public void PlayerBuyGun(string new_gun_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["new_gun_id"] = new_gun_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        GameAnalytics.NewDesignEvent("Player_Buy_Gun", param);
    }

    public void PlayerChangedGun(string new_gun_id, string old_gun_id)
    {
        var param = new Dictionary<string, object>();
        param["new_gun_id"] = new_gun_id;
        param["old_gun_id"] = old_gun_id;

        GameAnalytics.NewDesignEvent("Player_Changed_Gun", param);
    }

    public void PlayerBuySkin(string car_id, string skin_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["car_id"] = car_id;
        param["skin_id"] = skin_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        GameAnalytics.NewDesignEvent("Player_Buy_Skin", param);
    }

    public void PlayerChangedSkin(string car_id, string new_skin_id, string old_skin_id)
    {
        var param = new Dictionary<string, object>();
        param["car_id"] = car_id;
        param["new_skin_id"] = new_skin_id;
        param["old_skin_id"] = old_skin_id;

        GameAnalytics.NewDesignEvent("Player_Changed_Skin", param);
    }

    public void PlayerChangedControls(string new_control_type, string old_control_type)
    {
        var param = new Dictionary<string, object>();
        param["new_control_type"] = new_control_type;
        param["old_control_type"] = old_control_type;

        GameAnalytics.NewDesignEvent("Player_Changed_Controls", param);
    }

    public void PlayerInternetConnectionRestore()
    {
        GameAnalytics.NewDesignEvent("Player_Internet_Connection_Restore");
    }
    #endregion
}
