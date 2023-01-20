using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;

public class FacebookAnalytics : IAnalytics
{
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
    public void BattleStart(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount)
    {
        var param = new Dictionary<string, object>();
        param["battle_id"] = battle_id;
        param["player_car_id"] = player_car_id;
        param["player_gun_id"] = player_gun_id;
        param["league_id"] = league_id;
        param["level_id"] = level_id;
        param["enemies_amount"] = enemies_amount;

        FB.LogAppEvent("Battle_Start", parameters : param);
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

        FB.LogAppEvent("Player_Kill_Enemy", parameters: param);
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

        FB.LogAppEvent("Player_Pick_MysteryBox", parameters: param);
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

        FB.LogAppEvent("Player_Get_Shield", parameters: param);
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

        FB.LogAppEvent("Player_Get_Gold", parameters: param);
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

        FB.LogAppEvent("Player_Recover_HP", parameters: param);
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

        FB.LogAppEvent("Player_Falls_Out_Map", parameters: param);
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

        FB.LogAppEvent("Player_Destroyed", parameters: param);
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

        FB.LogAppEvent("Battle_Finish", parameters: param);
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

    public void PlayerBuySkin(string new_skin_id, int gold_spent, int gold)
    {
        var param = new Dictionary<string, object>();
        param["new_skin_id"] = new_skin_id;
        param["gold_spent"] = gold_spent;
        param["gold"] = gold;

        FB.LogAppEvent("Player_Buy_Skin", parameters: param);
    }

    public void PlayerChangedControls(string new_control_type, string old_control_type)
    {
        var param = new Dictionary<string, object>();
        param["new_control_type"] = new_control_type;
        param["old_control_type"] = old_control_type;

        FB.LogAppEvent("Player_Changed_Controls", parameters: param);
    }
    #endregion
}
