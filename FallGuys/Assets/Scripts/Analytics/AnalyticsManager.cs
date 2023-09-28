using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static string battle_id_key = "battle_id";

    private string current_player_car_id;// во время боя они не меняются
    private string current_player_gun_id;

    private bool initializeCompleted;
    
    public static event Action InitializeCompletedEvent;

    private List<IAnalytics> analytics = new List<IAnalytics>
    {
        new FacebookAnalytics(),
        new AmplitudeAnalytics(),
        new FirebaseAnalytics(),
        new GameAnalyticsAnalytics()
    };

    private int responseCount;// количество ответов, что аналитика инициализирована

    private IPlayer currentPlayer;

    public static AnalyticsManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            //if (!initializeCompleted) Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //private void Start()
    //{
    //    if (!initializeCompleted) Initialize();
    //}

    public void SetCurrentPlayer(IPlayer player)
    {
        currentPlayer = player;
    }

    public int GetCurrentPlayerHealth()
    {
        //return (int)currentPlayer.Health;
        return 0;
    }

    public void Initialize()
    {   // вызываем методы инициализации. Потом нужно дождаться ответов, что инициализация выполнена
        foreach (var item in analytics)
        {
            try
            {
                item.InitializationСompletedEvent += InitializationCompleted;

                item.Initialize(); // не означает, что инициализация завершена. Только запускаем ее

                //Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Initialized");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Initialize. {ex.Message}");
            }
        }

        initializeCompleted = true;
    }

    private void InitializationCompleted()
    {
        responseCount++;

        if(initializeCompleted && responseCount == analytics.Count)
        {
            // Посылаем событие, что инициализация выполнена
            InitializeCompletedEvent?.Invoke();


            foreach (var item in analytics)// инициализация выполняется один раз. Отписываемся от событий
            {
                item.InitializationСompletedEvent -= InitializationCompleted;
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Battle_Start Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Kill_Enemy Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Pick_MysteryBox Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Get_Shield Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Get_Gold Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Recover_HP Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Falls_Out_Map Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Destroyed Event. {ex.Message}");
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
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Battle_Finish Event. {ex.Message}");
            }
        }
    }

    #endregion
    
    #region GENERAL

    public void User(int battles_amount, float win_rate, int cups_amount, int league, int gold, string car_id, string gun_id, string control_type)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.User(battles_amount, win_rate, cups_amount, league, gold, car_id, gun_id, control_type);

                Debug.Log($"battles_amount = {battles_amount}; win_rate = {win_rate}; cups_amount = {cups_amount}; league = {league};" +
                    $"gold = {gold}; car_id = {car_id}; gun_id = {gun_id}; control_type = {control_type}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} User");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to User Event. {ex.Message}");
            }
        }
    }

    public void PlayerBuyCar(string new_car_id, int gold_spent, int gold)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerBuyCar(new_car_id, gold_spent, gold);

                Debug.Log($"new_car_id = {new_car_id}; gold_spent = {gold_spent}; gold = {gold}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Buy_Car");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Buy_Car Event. {ex.Message}");
            }
        }
    }

    public void PlayerChangedCar(string new_car_id, string old_car_id)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerChangedCar(new_car_id, old_car_id);

                Debug.Log($"new_car_id = {new_car_id}; old_car_id = {old_car_id}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Changed_Car");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Changed_Car Event. {ex.Message}");
            }
        }
    }

    public void PlayerBuyGun(string new_gun_id, int gold_spent, int gold)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerBuyGun(new_gun_id, gold_spent, gold);

                Debug.Log($"new_gun_id = {new_gun_id}; gold_spent = {gold_spent}; gold = {gold}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Buy_Gun");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Buy_Gun Event. {ex.Message}");
            }
        }
    }

    public void PlayerChangedGun(string new_gun_id, string old_gun_id)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerChangedGun(new_gun_id, old_gun_id);

                Debug.Log($"new_gun_id = {new_gun_id}; old_gun_id = {old_gun_id}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Changed_Gun");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Changed_Gun Event. {ex.Message}");
            }
        }
    }

    public void PlayerBuySkin(string new_skin_id, int gold_spent, int gold)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerBuySkin(new_skin_id, gold_spent, gold);

                Debug.Log($"new_skin_id = {new_skin_id}; gold_spent = {gold_spent}; gold = {gold}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Buy_Skin");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Buy_Skin Event. {ex.Message}");
            }
        }
    }

    public void PlayerChangedControls(string new_control_type, string old_control_type)
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerChangedControls(new_control_type, old_control_type);

                Debug.Log($"new_control_type = {new_control_type}; old_control_type = {old_control_type};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Changed_Controls");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Changed_Controls Event. {ex.Message}");
            }
        }
    }

    public void PlayerInternetConnectionRestore()
    {
        foreach (var item in analytics)
        {
            try
            {
                item.PlayerInternetConnectionRestore();

                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Internet_Connection_Restore");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Internet_Connection_Restore Event. {ex.Message}");
            }
        }
    }

    #endregion
}
