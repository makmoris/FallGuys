using System.Collections.Generic;

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

        amplitude.logEvent("Battle_Start", param);
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

        amplitude.logEvent("Player_Kill_Enemy", param);
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

        amplitude.logEvent("Player_Pick_MysteryBox", param);
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

        amplitude.logEvent("Player_Get_Shield", param);
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

        amplitude.logEvent("Player_Get_Gold", param);
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

        amplitude.logEvent("Player_Recover_HP", param);
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

        amplitude.logEvent("Player_Falls_Out_Map", param);
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

        amplitude.logEvent("Player_Destroyed", param);
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

        amplitude.logEvent("Battle_Finish", param);
    }
    #endregion
}
