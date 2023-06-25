using System.Collections;
using UnityEngine;

public class PlayerEffector
{
    private readonly IPlayer player;
    private readonly PlayerLimitsData limitsData;
    private Weapon _weapon;

    private LevelUI levelUI;

    private EnemyPointer enemyPointer;

    private LevelUINotifications levelUINotifications;

    private readonly VisualIntermediary _intermediary;

    private bool isShieldActive;// когда щит активен, игрок не может получать урон
    private Coroutine shieldCoroutine = null;

    private bool isAI;
    private bool isCurrentPlayer;

    private bool isImmortal;

    private float defaultHealth;// для вибрации

    private Coroutine disableWeaponCoroutine = null;

    public PlayerEffector(IPlayer _player, PlayerLimitsData _limitsData, LevelUINotifications _levelUINotifications, LevelUI _levelUI,
        bool _isImmortal = false)
    {
        isAI = false;
        isCurrentPlayer = true;

        isImmortal = _isImmortal;

        player = _player;
        limitsData = _limitsData;

        levelUI = _levelUI;

        levelUI.UpdatePlayerHP(player.Health);

        Weapon weapon = player.Weapon;
        _weapon = weapon;

        Bumper bumper = player.Vehicle.GetComponent<Bumper>();
        if(bumper != null)
        {
            bumper.OnBonusGot += ApplyBonus;
            bumper.OnBonusGotWithGameObject += ApplyBonus;
        }
        else Debug.LogError("Component Bumber not found");

        VisualIntermediary intermediary = player.Vehicle.GetComponent<VisualIntermediary>();
        if(intermediary != null)
        {
            _intermediary = intermediary;
        }
        else Debug.LogError("Component VisualIntermediary not found");


        bumper.SetIsCurrentPlayer(player.Vehicle);
        _intermediary.SetIsCurrentPlayer(player.Vehicle);

        levelUINotifications = _levelUINotifications;

        levelUI.UIEnemyPointers.SetCurrentPlayerTransform(player.Vehicle.transform);


        defaultHealth = player.Health;
    }

    public PlayerEffector(EnemyPointer _enemyPointer, IPlayerAI _playerAI, PlayerLimitsData _limitsData, LevelUI _levelUI,
        IPlayer _currentPlayer, bool _isImmortal = false) // enemyAI
    {
        enemyPointer = _enemyPointer;

        isAI = true;
        isCurrentPlayer = false;

        isImmortal = _isImmortal;

        player = _playerAI;
        limitsData = _limitsData;

        levelUI = _levelUI;

        levelUI.UpdateEnemyHP(player.Health, enemyPointer);

        Weapon weapon = player.Weapon;
        _weapon = weapon;

        Bumper bumper = player.Vehicle.GetComponent<Bumper>();
        if (bumper != null)
        {
            bumper.OnBonusGot += ApplyBonus;
            bumper.OnBonusGotWithGameObject += ApplyBonus;
        }
        else Debug.LogError("Component Bumber not found");

        VisualIntermediary intermediary = player.Vehicle.GetComponent<VisualIntermediary>();
        if (intermediary != null)
        {
            _intermediary = intermediary;
        }
        else Debug.LogError("Component VisualIntermediary not found");


        bumper.SetIsCurrentPlayer(_currentPlayer.Vehicle);
        _intermediary.SetIsCurrentPlayer(_currentPlayer.Vehicle);

        defaultHealth = player.Health;
    }

    private void ApplyBonus(Bonus bonus)
    {
        //Debug.Log($"PLAYER EFFECTOR {bonus.Type}");

        switch (bonus.Type)
        {
            case BonusType.AddHealth:

                if (bonus.Value < 0 && isShieldActive && bonus.GetComponent<DeadZone>() == null)// последнее - чтобы получать урон в щите, если выпал со сцены
                {

                }
                else if(!isImmortal)
                {
                    var resultHealth = player.Health + bonus.Value;
                    Debug.Log(resultHealth + " = " + player.Health + " + " + bonus.Value);
                    if (resultHealth > limitsData.MaxHP)
                    {
                        //resultHealth = _limitsData.MaxHP; // убрали лимит на хп
                    }
                    else if (resultHealth <= 0)
                    {
                        resultHealth = 0;
                    }


                    player.SetHealth(resultHealth);

                    if (isCurrentPlayer) levelUI.UpdatePlayerHP(player.Health);
                    else levelUI.UpdateEnemyHP(player.Health, enemyPointer);

                    if (player.Health == 0)
                    {
                        Debug.Log("DA 1");
                        Bullet bullet = bonus.GetComponent<Bullet>();

                        if (bullet != null)
                        {
                            Debug.Log("BULLET 1");
                            _intermediary.DestroyCar(bullet.GetParent());
                        }
                        else
                        {
                            _intermediary.DestroyCar();
                        }
                        //Debug.Log("Destroy in effector");

                        if (isCurrentPlayer) SendPlayerDestroyedAnalyticEvent();
                    }

                    if (bonus.Value > 0 && isCurrentPlayer) SendPlayerRecoverHPAnalyticEvent((int)bonus.Value);

                    if (bonus.Value < 0 && isCurrentPlayer)
                    {
                        float percent = ((bonus.Value * -1f) / defaultHealth) * 100f;

                        if(percent < 10f)
                        {
                            VibrationManager.Instance.LowDamageVibration();
                        }
                        else if(percent >= 10f && percent <= 30f)
                        {
                            VibrationManager.Instance.MediumDamageVibration();
                        }
                        else
                        {
                            VibrationManager.Instance.LargeDamageVibration();
                        }
                        Debug.Log($"Percent = {percent}");
                    }
                }

                break;

            //case BonusType.AddSpeed:

            //    break;

            //case BonusType.AddDamage:

            //    break;

            case BonusType.AddShield:
                                                // сейчас мерцает в зависмости от времени действия. 5 раз в течении 1/3 от времени действия
                                                // можно переделать, чтобы независимо от времени действия, всегда последние 5 секунд, например
                if (shieldCoroutine != null)
                {
                    CoroutineRunner.Stop(shieldCoroutine);
                    _intermediary.HideShield();
                }
                shieldCoroutine = CoroutineRunner.Run(ShieldActive(bonus.Value));

                if(isCurrentPlayer) SendPlayerGetShieldAnalyticEvent();

                break;

            case BonusType.AddGold:

                if (isCurrentPlayer)
                {
                    ArenaProgressController.Instance.AddGold((int)bonus.Value);
                    SendPlayerGetGoldAnalyticEvent((int)bonus.Value);
                }

                break;
        }
    }

    private void ApplyBonus(Bonus bonus, GameObject _gameObject)
    {
        switch (bonus.Type)
        {
            case BonusType.DisableWeapon:

                if (!isShieldActive)
                {
                    if (disableWeaponCoroutine != null)
                    {
                        CoroutineRunner.Stop(disableWeaponCoroutine);
                    }
                    disableWeaponCoroutine = CoroutineRunner.Run(WaitAndEnableWeapon(bonus.Value, _gameObject));

                    _weapon.DisableWeapon(_gameObject);

                    if (isCurrentPlayer)
                    {
                        levelUINotifications.AttackBan.ShowBanImage(bonus.Value);
                    }
                }

                break;

            case BonusType.DisableWeaponFromLightning:

                if (!isShieldActive)
                {
                    if (disableWeaponCoroutine != null)
                    {
                        CoroutineRunner.Stop(disableWeaponCoroutine);
                    }
                    disableWeaponCoroutine = CoroutineRunner.Run(WaitAndEnableWeapon(bonus.Value, _gameObject));
                    
                    _weapon.DisableWeapon(_gameObject);
                    
                    if (isCurrentPlayer)
                    {
                        levelUINotifications.BuffsDebuffsNotifications.ShowLightningDebuff();
                        levelUINotifications.AttackBan.ShowBanImage(bonus.Value);
                    }
                }

                break;
        }
    }

    IEnumerator WaitAndEnableWeapon(float time, GameObject _gameObject)
    {
        yield return new WaitForSeconds(time);

        _weapon.EnableWeapon(_gameObject);

        if (isCurrentPlayer)
        {
            levelUINotifications.BuffsDebuffsNotifications.HideLightningDebuff();
            levelUINotifications.AttackBan.HideBanImage();
        }
    }

    IEnumerator ShieldActive(float time)
    {
        if (isCurrentPlayer)
        {
            // notification
            levelUINotifications.BuffsDebuffsNotifications.ShowShieldBuff();
        }

        isShieldActive = true;
        _intermediary.ShowShield(time);
        Debug.Log("Щит активирован");
        yield return new WaitForSeconds(time);
        isShieldActive = false;
        _intermediary.HideShield();
        Debug.Log("Щит отключен");

        if (isCurrentPlayer)
        {
            // stop notification
            levelUINotifications.BuffsDebuffsNotifications.HideShieldBuff();
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void SendPlayerGetShieldAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = (int)player.Health;

        int _player_kills_amount = ArenaProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = ArenaProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = ArenaProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerGetShield(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    private void SendPlayerGetGoldAnalyticEvent(int goldAmountValue)
    {
        int _gold_amount = goldAmountValue;

        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = (int)player.Health;

        int _player_kills_amount = ArenaProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = ArenaProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = ArenaProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerGetGold(_gold_amount, _battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    private void SendPlayerRecoverHPAnalyticEvent(int hpAmountValue)
    {
        int _hp_amount = hpAmountValue;

        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = (int)player.Health;

        int _player_kills_amount = ArenaProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = ArenaProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = ArenaProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerRecoverHP(_hp_amount, _battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    private void SendPlayerDestroyedAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = 0;

        int _player_kills_amount = ArenaProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = ArenaProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = ArenaProgressController.Instance.GetCurrentEnemiesLeft() + 1;

        AnalyticsManager.Instance.PlayerDestroyed(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }
}
