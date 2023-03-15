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
    public void BattleStart(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["league_id"] = league_id;
        param["level_id"] = level_id;
        param["enemies_amount"] = enemies_amount;

        GameAnalytics.NewDesignEvent("Battle_Start", param);
    }

    public void PlayerKillEnemy(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left, string killed_enemy_car_id, string killed_enemy_gun_id)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;
        param["killed_enemy_car_id"] = killed_enemy_car_id;
        param["killed_enemy_gun_id"] = killed_enemy_gun_id;

        GameAnalytics.NewDesignEvent("Player_Kill_Enemy", param);
    }

    public void PlayerPickMysteryBox(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;

        GameAnalytics.NewDesignEvent("Player_Pick_MysteryBox", param);
    }

    public void PlayerGetShield(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;

        GameAnalytics.NewDesignEvent("Player_Get_Shield", param);
    }

    public void PlayerGetGold(int gold_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new Dictionary<string, object>();
        param["gold_amount"] = gold_amount;
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;

        GameAnalytics.NewDesignEvent("Player_Get_Gold", param);
    }

    public void PlayerRecoverHP(int hp_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new Dictionary<string, object>();
        param["hp_amount"] = hp_amount;
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;

        GameAnalytics.NewDesignEvent("Player_Recover_HP", param);
    }

    public void PlayerFallsOutMap(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;

        GameAnalytics.NewDesignEvent("Player_Falls_Out_Map", param);
    }

    public void PlayerDestroyed(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["player_hp_left"] = player_hp_left;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["enemies_left"] = enemies_left;

        GameAnalytics.NewDesignEvent("Player_Destroyed", param);
    }

    public void BattleFinish(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount,
        int player_took_place, int player_kills_amount, int player_gold_earn, int player_cups_earned)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["league_id"] = league_id;
        param["level_id"] = level_id;
        param["enemies_amount"] = enemies_amount;
        param["player_took_place"] = player_took_place;
        param["player_kills_amount"] = player_kills_amount;
        param["player_gold_earn"] = player_gold_earn;
        param["player_cups_earned"] = player_cups_earned;

        GameAnalytics.NewDesignEvent("Battle_Finish", param);
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

    public void PlayerBuySkin(string new_skin_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["new_skin_id"] = new_skin_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        GameAnalytics.NewDesignEvent("Player_Buy_Skin", param);
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
