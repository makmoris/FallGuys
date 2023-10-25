using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
        #region BATTLE ID

    private const string battle_id_key = "battle_id";

    public void UpdateBattleId()
    {
        int _battle_id = GetBattleId();
        _battle_id++;
        PlayerPrefs.SetInt(battle_id_key, _battle_id);
    }

    public int GetBattleId()
    {
        return PlayerPrefs.GetInt(battle_id_key, 0);
    }

        #endregion


        #region BATTLES AMOUNT

    private const string battles_amount_key = "battles_amount";

    public void UpdateBattlesAmount()
    {
        int _battles_amount = GetBattlesAmount();
        _battles_amount++;
        PlayerPrefs.SetInt(battles_amount_key, _battles_amount);
    }
    private int GetBattlesAmount()
    {
        return PlayerPrefs.GetInt(battles_amount_key, 0);
    }

        #endregion


        #region NUMBER OF FIRST PLACE

    private const string number_of_first_place_key = "number_of_first_place";

    public void UpdateNumberOfFirstPlace()
    {
        int _number_of_first_place = GetNumberOfFirstPlace();
        _number_of_first_place++;
        PlayerPrefs.SetInt(number_of_first_place_key, _number_of_first_place);
    }
    public int GetNumberOfFirstPlace()
    {
        return PlayerPrefs.GetInt(number_of_first_place_key, 0);
    }

    #endregion


        #region WIN RATE

    private float GetWinRate()
    {
        int battlesAmount = GetBattlesAmount();
        int numberOfFirstPlace = GetNumberOfFirstPlace();

        float winRate = (numberOfFirstPlace / (float)battlesAmount) * 100f;

        return winRate;

    }

    #endregion

        #region CAR ID
    
    private const string car_id_key = "car_id";
    private string _old_car_id;
   
    private void UpdateCarID(string newCarID)
    {
        _old_car_id = PlayerPrefs.GetString(car_id_key, "NULL");

        PlayerPrefs.SetString(car_id_key, newCarID);
    }
    private string GetCarID()
    {
        string carID = PlayerPrefs.GetString(car_id_key, "NULL");

        if (carID == "NULL")
        {
            carID = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;
            _old_car_id = carID;

            PlayerPrefs.SetString(car_id_key, carID);
        }

        return carID;
    }

    #endregion

        #region GUN ID

    private const string gun_id_key = "gun_id";
    private string _old_gun_id;

    private void UpdateGunID(string newGunID)
    {
        _old_gun_id = PlayerPrefs.GetString(gun_id_key, "NULL");

        PlayerPrefs.SetString(gun_id_key, newGunID);
    }
    private string GetGunID()
    {
        string gunID = PlayerPrefs.GetString(gun_id_key, "NULL");

        if (gunID == "NULL")
        {
            gunID = CharacterManager.Instance.GetPlayerWeapon().GetComponent<WeaponId>().WeaponID;
            _old_gun_id = gunID;

            PlayerPrefs.SetString(gun_id_key, gunID);
        }

        return gunID;
    }

    #endregion

        #region LEAGUE LEVEL

    private int GetLeagueLevel()
    {
        return LeagueManager.Instance.GetCurrentLeagueLevel();
    }

    #endregion

    #region CONFIG LEVELS ID

    private int _config_levels_id;

    public void UpdateConfigLevelsID(int currentConfigLevelsID)
    {
        _config_levels_id = currentConfigLevelsID;
    }

    private int GetConfigLevelsID()
    {
        return _config_levels_id;
    }

    #endregion

    #region GENRE ID

    private string _genre_id;

    public void UpdateGenreID(string genreID)
    {
        _genre_id = genreID;
    }

    private string GetGenreID() => _genre_id;

    #endregion

    #region MAP ID

    private string _map_id;

    public void UpdateMapID(string mapID)
    {
        _map_id = mapID;
    }

    private string GetMapID() => _map_id;

    #endregion

    #region PLAYERS AMOUNT 

    private int _players_amount;

    public void UpdatePlayersAmount(int playersAmount)
    {
        _players_amount = playersAmount;
    }

    private int GetPlayersAmount() => _players_amount;

    #endregion

    #region USER PLAY

    private bool _user_play;

    public void UpdateUserPlay(bool isUserPlay)
    {
        _user_play = isUserPlay;
    }
    private bool GetUserPlay() => _user_play;

    #endregion

    #region SKIN ID

    private const string skin_id_key = "skin_id";

    public void CheckCurrentSkinIDValue(string skinID)
    {
        string currentSkinIDValue = PlayerPrefs.GetString(skin_id_key, "NULL");

        if(currentSkinIDValue == "NULL")
        {
            PlayerPrefs.SetString(skin_id_key, skinID);
        }
    }

    private string GetSkinID()
    {
        return PlayerPrefs.GetString(skin_id_key, "NULL");
    }

    private void UpdateSkinID(string newSkinID)
    {
        PlayerPrefs.SetString(skin_id_key, newSkinID);
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>

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

    public void Initialize()
    {   // вызываем методы инициализации. Потом нужно дождаться ответов, что инициализация выполнена
        foreach (var item in analytics)
        {
            try
            {
                item.InitializationСompletedEvent += InitializationCompleted;

                item.Initialize(); // не означает, что инициализация завершена. Только запускаем ее
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
            Debug.Log("[A] InitializationCompleted");

            foreach (var item in analytics)// инициализация выполняется один раз. Отписываемся от событий
            {
                item.InitializationСompletedEvent -= InitializationCompleted;
            }
        }
    }

    #region BATTLE

    public void GameStart()
    {
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();
        string car_id = GetCarID();
        string gun_id = GetGunID();
        int config_levels_id = GetConfigLevelsID();

        foreach (var item in analytics)
        {
            try
            {
                item.GameStart(league_id, battle_id, car_id, gun_id, config_levels_id);

                Debug.Log($"league_id = {league_id}; battle_id = {battle_id}; car_id = {car_id}; gun_id = {gun_id}; " +
                    $"config_levels_id = {config_levels_id}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Game_Start");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Game_Start Event. {ex.Message}");
            }
        }
    }

    public void LevelStart()
    {
        string genre_id = GetGenreID();
        string map_id = GetMapID();
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();
        int players_amount = GetPlayersAmount();
        bool user_play = GetUserPlay();

        foreach (var item in analytics)
        {
            try
            {
                item.LevelStart(genre_id, map_id, league_id, battle_id, players_amount, user_play);

                Debug.Log($"genre_id = {genre_id}; map_id = {map_id}; league_id = {league_id}; battle_id = {battle_id}; " +
                    $"players_amount = {players_amount}; user_play = {user_play}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Level_Start");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Level_Start Event. {ex.Message}");
            }
        }
    }

    public void PlayerCompleteLevel()
    {
        string genre_id = GetGenreID();
        string map_id = GetMapID();
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerCompleteLevel(genre_id, map_id, league_id, battle_id);

                Debug.Log($"genre_id = {genre_id}; map_id = {map_id}; league_id = {league_id}; battle_id = {battle_id};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Complete_Level");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Complete_Level Event. {ex.Message}");
            }
        }
    }

    public void PlayerFailLevel()
    {
        string genre_id = GetGenreID();
        string map_id = GetMapID();
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerFailLevel(genre_id, map_id, league_id, battle_id);

                Debug.Log($"genre_id = {genre_id}; map_id = {map_id}; league_id = {league_id}; battle_id = {battle_id};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Fail_Level");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Fail_Level Event. {ex.Message}");
            }
        }
    }

    public void LevelFinish(int currentPlayerTookPlace)// from LevelControler
    {
        string genre_id = GetGenreID();
        string map_id = GetMapID();
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();
        int players_amount = GetPlayersAmount();
        int player_took_place = currentPlayerTookPlace;
        bool user_play = GetUserPlay();

        foreach (var item in analytics)
        {
            try
            {
                item.LevelFinish(genre_id, map_id, league_id, battle_id, players_amount, player_took_place, user_play);

                Debug.Log($"genre_id = {genre_id}; map_id = {map_id}; league_id = {league_id}; battle_id = {battle_id}; " +
                    $"players_amount = {players_amount}; player_place = {player_took_place}; user_play = {user_play};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Level_Finish");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Level_Finish Event. {ex.Message}");
            }
        }
    }

    public void PlayerLeaveGame()
    {
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerLeaveGame(league_id, battle_id);

                Debug.Log($"league_id = {league_id}; battle_id = {battle_id};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Leave_Game");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Leave_Game Event. {ex.Message}");
            }
        }
    }

    public void GameFinish(int playerPlace)
    {
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();
        int player_place = playerPlace;
        int config_levels_id = GetConfigLevelsID();

        foreach (var item in analytics)
        {
            try
            {
                item.GameFinish(league_id, battle_id, player_place, config_levels_id);

                Debug.Log($"league_id = {league_id}; battle_id = {battle_id}; player_place = {player_place}; config_level_id = {config_levels_id}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Game_Finish");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Game_Finish Event. {ex.Message}");
            }
        }
    }

    public void PlayerGetBattleReward(int playerGoldEarn, int playerCupsEarn)
    {
        int league_id = GetLeagueLevel();
        int battle_id = GetBattleId();
        int player_gold_earn = playerGoldEarn;
        int player_cups_earn = playerCupsEarn;

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerGetBattleReward(league_id, battle_id, player_gold_earn, player_cups_earn);

                Debug.Log($"league_id = {league_id}; battle_id = {battle_id}; player_gold_earn = {player_gold_earn};" +
                    $"player_cups_earn = {player_cups_earn}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Get_Battle_Reward");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Get_Battle_Reward Event. {ex.Message}");
            }
        }
    }

    #endregion

    #region GENERAL

    public void User(int cups_amount, int gold, string control_type)
    {
        int _battles_amount = GetBattlesAmount();
        float _win_rate = GetWinRate();
        string _car_id = GetCarID();
        string _gun_id = GetGunID();
        int _league = GetLeagueLevel();

        foreach (var item in analytics)
        {
            try
            {
                item.User(_battles_amount, _win_rate, cups_amount, _league, gold, _car_id, _gun_id, control_type);

                Debug.Log($"battles_amount = {_battles_amount}; win_rate = {_win_rate}; cups_amount = {cups_amount}; league = {_league};" +
                    $"gold = {gold}; car_id = {_car_id}; gun_id = {_gun_id}; control_type = {control_type}");
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

    public void PlayerChangedCar(string new_car_id)
    {
        UpdateCarID(new_car_id);

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerChangedCar(new_car_id, _old_car_id);

                Debug.Log($"new_car_id = {new_car_id}; old_car_id = {_old_car_id}");
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

    public void PlayerChangedGun(string new_gun_id)
    {
        UpdateGunID(new_gun_id);

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerChangedGun(new_gun_id, _old_gun_id);

                Debug.Log($"new_gun_id = {new_gun_id}; old_gun_id = {_old_gun_id}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Changed_Gun");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Changed_Gun Event. {ex.Message}");
            }
        }
    }

    public void PlayerBuySkin(string skin_id, int gold_spent, int gold)
    {
        string car_id = GetCarID();

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerBuySkin(car_id, skin_id, gold_spent, gold);

                Debug.Log($"car_id = {car_id}; skin_id = {skin_id}; gold_spent = {gold_spent}; gold = {gold}");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Buy_Skin");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Buy_Skin Event. {ex.Message}");
            }
        }
    }

    public void PlayerChangedSkin(string newSkinID)
    {
        string car_id = GetCarID();
        string old_skin_id = GetSkinID();
        string new_skin_id = newSkinID;

        UpdateSkinID(new_skin_id);

        foreach (var item in analytics)
        {
            try
            {
                item.PlayerChangedSkin(car_id, new_skin_id, old_skin_id);

                Debug.Log($"car_id = {car_id}; new_skin_id = {new_skin_id}; old_skin_id = {old_skin_id};");
                Debug.Log($"<color=green>[Analytics]</color> {item.GetName()} Player_Changed_Skin");
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=red>[Analytics]</color> {item.GetName()} - Failed to Player_Changed_Skin Event. {ex.Message}");
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
