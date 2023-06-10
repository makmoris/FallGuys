using System.Collections;
using UnityEngine;

public class PlayerEffector
{
    private readonly IPlayer _player;
    private readonly PlayerLimitsData _limitsData;
    private Weapon _weapon;

    private GameUIManager _gameUIManager;

    private readonly VisualIntermediary _intermediary;

    private bool isShieldActive;// когда щит активен, игрок не может получать урон
    private Coroutine shieldCoroutine = null;

    private bool _isAI;
    private bool _isCurrentPlayer;

    private float defaultHealth;// для вибрации

    private Coroutine disableWeaponCoroutine = null;

    public PlayerEffector(bool isAI, IPlayer player, PlayerLimitsData limitsData, bool isCurrentPlayer, GameUIManager gameUIManager)
    {
        _isAI = isAI;
        _player = player;
        _limitsData = limitsData;

        Weapon weapon = _player.Weapon;
        _weapon = weapon;

        Bumper bumper = _player.Vehicle.GetComponent<Bumper>();
        if(bumper != null)
        {
            bumper.OnBonusGot += ApplyBonus;
            bumper.OnBonusGotWithGameObject += ApplyBonus;
        }
        else Debug.LogError("Component Bumber not found");

        VisualIntermediary intermediary = _player.Vehicle.GetComponent<VisualIntermediary>();
        if(intermediary != null)
        {
            _intermediary = intermediary;

            _intermediary.UpdateHealthInUI(_player.Health);
        }
        else Debug.LogError("Component VisualIntermediary not found");

        _isCurrentPlayer = isCurrentPlayer;

        if (_isCurrentPlayer)
        {
            _gameUIManager = gameUIManager;

            _gameUIManager.SetCurrentPlayerGO(_player.Vehicle);
            bumper.SetIsCurrentPlayer(_player.Vehicle);
            _intermediary.SetIsCurrentPlayer(_player.Vehicle);
        }

        defaultHealth = _player.Health;
    }

    private void ApplyBonus(Bonus bonus)
    {
        Debug.Log($"PLAYER EFFECTOR {bonus.Type}");

        switch (bonus.Type)
        {
            case BonusType.AddHealth:

                if (bonus.Value < 0 && isShieldActive && bonus.GetComponent<DeadZone>() == null)// последнее - чтобы получать урон в щите, если выпал со сцены
                {

                }
                else
                {
                    var resultHealth = _player.Health + bonus.Value;
                    Debug.Log(resultHealth + " = " + _player.Health + " + " + bonus.Value);
                    if (resultHealth > _limitsData.MaxHP)
                    {
                        //resultHealth = _limitsData.MaxHP; // убрали лимит на хп
                    }
                    else if (resultHealth <= 0)
                    {
                        resultHealth = 0;
                    }
                    
                    
                    _player.SetHealth(resultHealth);
                    _intermediary.UpdateHealthInUI(_player.Health);

                    if (_player.Health == 0)
                    {
                        Bullet bullet = bonus.GetComponent<Bullet>();

                        if (bullet != null)
                        {
                            _intermediary.DestroyCar(bullet.GetParent());
                        }
                        else
                        {
                            _intermediary.DestroyCar();
                        }
                        //Debug.Log("Destroy in effector");

                        if (_isCurrentPlayer) SendPlayerDestroyedAnalyticEvent();
                    }

                    if (bonus.Value > 0 && _isCurrentPlayer) SendPlayerRecoverHPAnalyticEvent((int)bonus.Value);

                    if (bonus.Value < 0 && _isCurrentPlayer)
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

                if(_isCurrentPlayer) SendPlayerGetShieldAnalyticEvent();

                break;

            case BonusType.AddGold:

                if (_isCurrentPlayer)
                {
                    LevelProgressController.Instance.AddGold((int)bonus.Value);
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

                    if (_isCurrentPlayer)
                    {
                        _gameUIManager.ActivateAttackBan(_gameObject, bonus.Value);
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
                    
                    if (_isCurrentPlayer)
                    {
                        _gameUIManager.ShowLightningDebuffNotification(_gameObject);
                        _gameUIManager.ActivateAttackBan(_gameObject, bonus.Value);
                    }
                }

                break;
        }
    }

    IEnumerator WaitAndEnableWeapon(float time, GameObject _gameObject)
    {
        yield return new WaitForSeconds(time);

        _weapon.EnableWeapon(_gameObject);

        if (_isCurrentPlayer)
        {
            _gameUIManager.HideLightningDebuffNotification(_gameObject);
            _gameUIManager.DeactivateAttackBan(_gameObject);
        }
    }

    IEnumerator ShieldActive(float time)
    {
        if (_isCurrentPlayer)
        {
            // notification
            _gameUIManager.ShowShieldBuffNotification();
        }

        isShieldActive = true;
        _intermediary.ShowShield(time);
        Debug.Log("Щит активирован");
        yield return new WaitForSeconds(time);
        isShieldActive = false;
        _intermediary.HideShield();
        Debug.Log("Щит отключен");

        if (_isCurrentPlayer)
        {
            // stop notification
            _gameUIManager.HideShieldBuffNotification();
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

        int _player_hp_left = ArenaPointerManager.Instance.GetPlayerHealth();

        int _player_kills_amount = LevelProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = LevelProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = LevelProgressController.Instance.GetCurrentEnemiesLeft();

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

        int _player_hp_left = ArenaPointerManager.Instance.GetPlayerHealth();

        int _player_kills_amount = LevelProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = LevelProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = LevelProgressController.Instance.GetCurrentEnemiesLeft();

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

        int _player_hp_left = ArenaPointerManager.Instance.GetPlayerHealth();

        int _player_kills_amount = LevelProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = LevelProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = LevelProgressController.Instance.GetCurrentEnemiesLeft();

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

        int _player_kills_amount = LevelProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = LevelProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = LevelProgressController.Instance.GetCurrentEnemiesLeft() + 1;

        AnalyticsManager.Instance.PlayerDestroyed(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }
}
