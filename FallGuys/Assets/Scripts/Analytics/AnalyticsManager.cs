using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static string battle_id_key = "battle_id";

    private string current_player_car_id;// во врем€ бо€ они не мен€ютс€
    private string current_player_gun_id;

    private List<IAnalytics> analytics = new List<IAnalytics>
    {
        new FacebookAnalytics(),
        new AmplitudeAnalytics(),
        new FirebaseAnalytics(),
        new GameAnalyticsAnalytics()
    };

    public static AnalyticsManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        foreach (var item in analytics)
        {
            try
            {
                item.Initialize();
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Initialized");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Initialize");
            }
        }
    }

    #region BATTLE

    public void BattleStart(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount)
    {
        foreach (var item in analytics)
        {
            try
            {
                current_player_car_id = player_car_id;
                current_player_gun_id = player_gun_id;

                item.BattleStart(battle_id, player_car_id, player_gun_id, league_id, level_id, enemies_amount);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"league_id = {league_id}; level_id = {level_id}; enemies_amount = {enemies_amount};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Battle_Start");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Battle_Start Event");
            }
        }
    }

    public string GetCurrentPlayerCarId()
    {
        return current_player_car_id;
    }
    public string GetCurrentPlayerGunId()
    {
        return current_player_gun_id;
    }

    public void PlayerKillEnemy(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left, string killed_enemy_car_id, string killed_enemy_gun_id)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerKillEnemy(battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn,
                    enemies_left, killed_enemy_car_id, killed_enemy_gun_id);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left}; killed_enemy_car_id = {killed_enemy_car_id}; killed_enemy_gun_id = {killed_enemy_gun_id}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Kill_Enemy");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Kill_Enemy Event");
            }
        }
    }

    public void PlayerPickMysteryBox(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerPickMysteryBox(battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn, enemies_left);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Pick_MysteryBox");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Pick_MysteryBox Event");
            }
        }
    }

    public void PlayerGetShield(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerGetShield(battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn, enemies_left);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Get_Shield");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Get_Shield Event");
            }
        }
    }

    public void PlayerGetGold(int gold_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerGetGold(gold_amount, battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn, 
                    enemies_left);

                Debug.Log($"gold_amount = {gold_amount}; battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Get_Gold");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Get_Gold Event");
            }
        }
    }

    public void PlayerRecoverHP(int hp_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerRecoverHP(hp_amount, battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn, 
                    enemies_left);

                Debug.Log($"hp_amount = {hp_amount}; battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Recover_HP");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Recover_HP Event");
            }
        }
    }

    public void PlayerFallsOutMap(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerFallsOutMap(battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn, enemies_left);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Falls_Out_Map");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Falls_Out_Map Event");
            }
        }
    }

    public void PlayerDestroyed(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerDestroyed(battle_id, player_car_id, player_gun_id, player_hp_left, player_kills_amount, player_gold_earn, enemies_left);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"player_hp_left = {player_hp_left}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"enemies_left = {enemies_left};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Destroyed");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Destroyed Event");
            }
        }
    }

    public void BattleFinish(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount,
        int player_took_place, int player_kills_amount, int player_gold_earn, int player_cups_earned)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.BattleFinish(battle_id, player_car_id, player_gun_id, league_id, level_id, enemies_amount, player_took_place, player_kills_amount,
                    player_gold_earn, player_cups_earned);

                Debug.Log($"battle_id = {battle_id}; player_car_id = {player_car_id}; player_gun_id = {player_gun_id};" +
                    $"league_id = {league_id}; level_id = {level_id}; enemies_amount = {enemies_amount};" +
                    $"player_took_place = {player_took_place}; player_kills_amount = {player_kills_amount}; player_gold_earn = {player_gold_earn};" +
                    $"player_cups_earned = {player_cups_earned}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Battle_Finish");
            }
            catch
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Battle_Finish Event");
            }
        }
    }

    #endregion
}
