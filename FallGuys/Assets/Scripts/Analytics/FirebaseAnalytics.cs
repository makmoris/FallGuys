using Firebase;
using Firebase.Analytics;
using System.Collections.Generic;

public class FirebaseAnalytics : IAnalytics
{
    public void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }

    public string GetName()
    {
        return "FirebaseAnalytics";
    }

    #region BATTLE
    public void BattleStart(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("league_id", league_id),
            new Parameter("level_id", level_id),
            new Parameter("enemies_amount", enemies_amount)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Battle_Start", param.ToArray());
    }

    public void PlayerKillEnemy(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left, string killed_enemy_car_id, string killed_enemy_gun_id)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left),
            new Parameter("killed_enemy_car_id", killed_enemy_car_id),
            new Parameter("killed_enemy_gun_id", killed_enemy_gun_id)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Kill_Enemy", param.ToArray());
    }

    public void PlayerPickMysteryBox(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Pick_MysteryBox", param.ToArray());
    }

    public void PlayerGetShield(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Get_Shield", param.ToArray());
    }

    public void PlayerGetGold(int gold_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new List<Parameter>
        {
            new Parameter("gold_amount", gold_amount),
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Get_Gold", param.ToArray());
    }

    public void PlayerRecoverHP(int hp_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new List<Parameter>
        {
            new Parameter("hp_amount", hp_amount),
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Recover_HP", param.ToArray());
    }

    public void PlayerFallsOutMap(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Falls_Out_Map", param.ToArray());
    }

    public void PlayerDestroyed(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("player_hp_left", player_hp_left),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("enemies_left", enemies_left)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Player_Destroyed", param.ToArray());
    }

    public void BattleFinish(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount,
        int player_took_place, int player_kills_amount, int player_gold_earn, int player_cups_earned)
    {
        var param = new List<Parameter>
        {
            new Parameter("battle_id", battle_id),
            new Parameter("player_car_id", player_car_id),
            new Parameter("player_gun_id", player_gun_id),
            new Parameter("league_id", league_id),
            new Parameter("level_id", level_id),
            new Parameter("enemies_amount", enemies_amount),
            new Parameter("player_took_place", player_took_place),
            new Parameter("player_kills_amount", player_kills_amount),
            new Parameter("player_gold_earn", player_gold_earn),
            new Parameter("player_cups_earned", player_cups_earned)
        };

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Battle_Finish", param.ToArray());
    }
    #endregion
}
