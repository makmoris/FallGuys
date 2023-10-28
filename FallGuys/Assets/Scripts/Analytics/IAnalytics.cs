public interface IAnalytics
{
    public event System.Action Initialization—ompletedEvent;
    public void Initialize();

    public string GetName();

    #region BATTLE
    public void GameStart(int league_id, int battle_id, string car_id, string gun_id, int config_levels_id);
    public void LevelStart(string genre_id, string map_id, int league_id, int battle_id, int players_amount, bool user_play);
    public void PlayerCompleteLevel(string genre_id, string map_id, int league_id, int battle_id);
    public void PlayerFailLevel(string genre_id, string map_id, int league_id, int battle_id);
    public void LevelFinish(string genre_id, string map_id, int league_id, int battle_id, int players_amount, int player_took_place, bool user_play);
    public void PlayerLeaveGame(int league_id, int battle_id, string leave_type);
    public void GameFinish(int league_id, int battle_id, int player_place, int config_levels_id);
    public void PlayerGetBattleReward(int league_id, int battle_id, int player_gold_earn, int player_cups_earn);
    #endregion

    #region GENERAL
    public void User(int battles_amount, float win_rate, int cups_amount, int league_id, int gold, string car_id, string gun_id, string control_type);

    public void PlayerBuyCar(string new_car_id, int gold_spent, int gold);

    public void PlayerChangedCar(string new_car_id, string old_car_id);

    public void PlayerBuyGun(string new_gun_id, int gold_spent, int gold);

    public void PlayerChangedGun(string new_gun_id, string old_gun_id);

    public void PlayerBuySkin(string car_id, string skin_id, int gold_spent, int gold);
    public void PlayerChangedSkin(string car_id, string new_skin_id, string old_skin_id);

    public void PlayerChangedControls(string new_control_type, string old_control_type);

    public void PlayerInternetConnectionRestore();
    #endregion
}
