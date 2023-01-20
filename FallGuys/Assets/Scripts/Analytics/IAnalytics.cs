public interface IAnalytics
{
    public void Initialize();

    public string GetName();

    #region BATTLE
    public void BattleStart(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount);

    public void PlayerKillEnemy(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left, string killed_enemy_car_id, string killed_enemy_gun_id);

    public void PlayerPickMysteryBox(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left);

    public void PlayerGetShield(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left);

    public void PlayerGetGold(int gold_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left);

    public void PlayerRecoverHP(int hp_amount, int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left);

    public void PlayerFallsOutMap(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left);

    public void PlayerDestroyed(int battle_id, string player_car_id, string player_gun_id, int player_hp_left, int player_kills_amount,
        int player_gold_earn, int enemies_left);

    public void BattleFinish(int battle_id, string player_car_id, string player_gun_id, int league_id, string level_id, int enemies_amount,
        int player_took_place, int player_kills_amount, int player_gold_earn, int player_cups_earned);
    #endregion

    #region GENERAL
    public void User(int battles_amount, float win_rate, int cups_amount, int league, int gold, string car_id, string gun_id, string control_type);

    public void PlayerBuyCar(string new_car_id, int gold_spent, int gold);

    public void PlayerChangedCar(string new_car_id, string old_car_id);

    public void PlayerBuyGun(string new_gun_id, int gold_spent, int gold);

    public void PlayerChangedGun(string new_gun_id, string old_gun_id);

    public void PlayerBuySkin(string new_skin_id, int gold_spent, int gold);

    public void PlayerChangedControls(string new_control_type, string old_control_type);
    #endregion
}
