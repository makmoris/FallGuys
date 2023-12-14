using ArcadeVP;
using System.Collections;
using UnityEngine;
using VehicleBehaviour;

public class PlayerEffector
{
    private readonly IPlayerData player;
    //private readonly PlayerLimitsData limitsData;
    private Weapon _weapon;

    private LevelUI levelUI;

    private GameObject thisPlayerGO;
    private Bumper thisBumper;

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
    private Coroutine slowdownCoroutine = null;
    private Coroutine controlInversionCoroutine = null;

    public PlayerEffector(Player _player, GameObject _playerGO, LevelUINotifications _levelUINotifications, LevelUI _levelUI, Weapon _playerWeapon,
        bool _isImmortal = false)
    {
        isAI = false;
        isCurrentPlayer = true;

        isImmortal = _isImmortal;

        player = _player;

        levelUI = _levelUI;

        levelUI.UpdateCurrentPlayerHP(player.Health);

        //Weapon weapon = _player.Weapon;
        _weapon = _playerWeapon;

        thisPlayerGO = _playerGO;

        Bumper bumper = _playerGO.GetComponent<Bumper>();
        if (bumper != null)
        {
            bumper.OnBonusGot += ApplyBonus;
            bumper.OnBonusGotWithGameObject += ApplyBonus;
        }
        else Debug.LogError("Component Bumper not found");
        thisBumper = bumper;

        VisualIntermediary intermediary = _playerGO.GetComponent<VisualIntermediary>();
        if (intermediary != null)
        {
            _intermediary = intermediary;
        }
        else Debug.LogError("Component VisualIntermediary not found");

        bumper.Init(_playerGO, _levelUINotifications);
        _intermediary.SetIsCurrentPlayer(_playerGO);

        levelUINotifications = _levelUINotifications;

        defaultHealth = player.Health;
    }

    public PlayerEffector(EnemyPointer _enemyPointer, PlayerAI _playerAI, GameObject _playerAIGO, LevelUI _levelUI, GameObject _currentPlayerGO,
        Weapon _aiWeapon,
        bool _isImmortal = false) // enemyAI
    {
        enemyPointer = _enemyPointer;

        isAI = true;
        isCurrentPlayer = false;

        isImmortal = _isImmortal;

        player = _playerAI;

        levelUI = _levelUI;

        levelUI.UpdateEnemyHP(player.Health, enemyPointer);

        //Weapon weapon = _playerAI.Weapon;
        _weapon = _aiWeapon;

        thisPlayerGO = _playerAIGO;

        Bumper bumper = _playerAIGO.GetComponent<Bumper>();
        if (bumper != null)
        {
            bumper.OnBonusGot += ApplyBonus;
            bumper.OnBonusGotWithGameObject += ApplyBonus;
        }
        else Debug.LogError("Component Bumper not found");
        thisBumper = bumper;

        VisualIntermediary intermediary = _playerAIGO.GetComponent<VisualIntermediary>();
        if (intermediary != null)
        {
            _intermediary = intermediary;
        }
        else Debug.LogError("Component VisualIntermediary not found");

        bumper.Init(_currentPlayerGO, null);
        _intermediary.SetIsCurrentPlayer(_currentPlayerGO);

        defaultHealth = player.Health;
    }

    private void ApplyBonus(Bonus bonus)
    {
        switch (bonus.BonusType)
        {
            case BonusType.AddHealth:

                if (bonus.BonusValue < 0 && isShieldActive)// последнее - чтобы получать урон в щите, если выпал со сцены
                {

                }
                else if(!isImmortal)
                {
                    var resultHealth = player.Health + bonus.BonusValue;
                    Debug.Log(resultHealth + " = " + player.Health + " + " + bonus.BonusValue);

                    if (resultHealth <= 0)
                    {
                        resultHealth = 0;
                    }


                    player.SetHealth(resultHealth);

                    if (isCurrentPlayer) levelUI.UpdateCurrentPlayerHP(player.Health);
                    else levelUI.UpdateEnemyHP(player.Health, enemyPointer);

                    if (player.Health == 0)
                    {   
                        _intermediary.DestroyCar();
                    }


                    if (bonus.BonusValue < 0 && isCurrentPlayer)
                    {
                        float percent = ((bonus.BonusValue * -1f) / defaultHealth) * 100f;

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
                    }
                }

                break;

            case BonusType.AddShield:
                                                // сейчас мерцает в зависмости от времени действия. 5 раз в течении 1/3 от времени действия
                                                // можно переделать, чтобы независимо от времени действия, всегда последние 5 секунд, например
                if (shieldCoroutine != null)
                {
                    CoroutineRunner.Stop(shieldCoroutine);
                    _intermediary.HideShield();
                }
                shieldCoroutine = CoroutineRunner.Run(ShieldActive(bonus.BonusValue));

                if (isCurrentPlayer) { }//SendPlayerGetShieldAnalyticEvent();

                break;

            case BonusType.AddGold:

                if (isCurrentPlayer)
                {
                    //ArenaProgressController.Instance.AddGold((int)bonus.Value);
                    //SendPlayerGetGoldAnalyticEvent((int)bonus.Value);
                }

                break;

            case BonusType.DisableWeaponFromLightning:

                if (!isShieldActive)
                {
                    if (disableWeaponCoroutine != null)
                    {
                        CoroutineRunner.Stop(disableWeaponCoroutine);
                    }
                    disableWeaponCoroutine = CoroutineRunner.Run(WaitAndEnableWeapon(bonus.BonusValue, thisPlayerGO));

                    _weapon.DisableWeapon(thisPlayerGO);

                    if (isCurrentPlayer)
                    {
                        levelUINotifications.BuffsDebuffsNotifications.ShowLightningDebuff();
                        levelUINotifications.AttackBan.ShowBanImage(bonus.BonusValue);
                    }
                }

                break;

            case BonusType.Slowdown:

                if (!isShieldActive)
                {
                    SlowdownBonus slowdownBonus = bonus as SlowdownBonus;

                    if (slowdownCoroutine != null)
                    {
                        CoroutineRunner.Stop(slowdownCoroutine);
                    }
                    thisBumper.StartSlowdown(slowdownBonus.BonusValue);
                    slowdownCoroutine = CoroutineRunner.Run(WaitAndDeactivateSlowdown(slowdownBonus.SlowdownTime));

                    if (isCurrentPlayer)
                    {
                        levelUINotifications.BuffsDebuffsNotifications.ShowSlowdownDebuff();
                    }
                }

                break;

            case BonusType.ControlInversion:

                if (!isShieldActive)
                {
                    if (controlInversionCoroutine != null)
                    {
                        CoroutineRunner.Stop(controlInversionCoroutine);
                    }
                    
                    thisPlayerGO.GetComponent<ArcadeVehicleController>().ChangeTheTurnControllerToInverse();

                    controlInversionCoroutine = CoroutineRunner.Run(WaitAndDeactivateControlInversion(bonus.BonusValue));

                    if (isCurrentPlayer)
                    {
                        levelUINotifications.BuffsDebuffsNotifications.ShowControlInversion();
                    }
                }

                break;
        }
    }

    private void ApplyBonus(Bonus bonus, GameObject _gameObject)
    {
        switch (bonus.BonusType)
        {
            case BonusType.AddHealth:

                DeadZone deadZone = _gameObject.GetComponent<DeadZone>();

                if (bonus.BonusValue < 0 && isShieldActive && deadZone == null)// последнее - чтобы получать урон в щите, если выпал со сцены
                {

                }
                else if (!isImmortal)
                {
                    var resultHealth = player.Health + bonus.BonusValue;
                    Debug.Log(resultHealth + " = " + player.Health + " + " + bonus.BonusValue);
                    
                    if (resultHealth <= 0)
                    {
                        resultHealth = 0;
                    }

                    player.SetHealth(resultHealth);

                    if (isCurrentPlayer) levelUI.UpdateCurrentPlayerHP(player.Health);
                    else levelUI.UpdateEnemyHP(player.Health, enemyPointer);

                    if (bonus.BonusValue < 0 && isCurrentPlayer)
                    {
                        float percent = ((bonus.BonusValue * -1f) / defaultHealth) * 100f;

                        if (percent < 10f)
                        {
                            VibrationManager.Instance.LowDamageVibration();
                        }
                        else if (percent >= 10f && percent <= 30f)
                        {
                            VibrationManager.Instance.MediumDamageVibration();
                        }
                        else
                        {
                            VibrationManager.Instance.LargeDamageVibration();
                        }
                    }

                    if (player.Health == 0)
                    {
                        Bullet bullet = _gameObject.GetComponent<Bullet>();

                        if (bullet != null)
                        {
                            _intermediary.DestroyCar(bullet.GetParent());
                        }
                        else
                        {
                            _intermediary.DestroyCar();
                        }

                        return;
                    }
                }

                if (deadZone != null) deadZone.RespawnCar(thisBumper);

                break;

            case BonusType.DisableWeapon:

                if (!isShieldActive)
                {
                    if (disableWeaponCoroutine != null)
                    {
                        CoroutineRunner.Stop(disableWeaponCoroutine);
                    }
                    disableWeaponCoroutine = CoroutineRunner.Run(WaitAndEnableWeapon(bonus.BonusValue, _gameObject));

                    //Debug.Log($"DisableWeapon = {_gameObject}");
                    _weapon.DisableWeapon(_gameObject);

                    if (isCurrentPlayer)
                    {
                        levelUINotifications.AttackBan.ShowBanImage(bonus.BonusValue);
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
                    disableWeaponCoroutine = CoroutineRunner.Run(WaitAndEnableWeapon(bonus.BonusValue, _gameObject));
                    
                    _weapon.DisableWeapon(_gameObject);
                    
                    if (isCurrentPlayer)
                    {
                        levelUINotifications.BuffsDebuffsNotifications.ShowLightningDebuff();
                        levelUINotifications.AttackBan.ShowBanImage(bonus.BonusValue);
                    }
                }

                break;
        }
    }

    IEnumerator WaitAndEnableWeapon(float time, GameObject _gameObject)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log($"Enable = {_gameObject}");
        _weapon.EnableWeapon(_gameObject);

        if (isCurrentPlayer)
        {
            levelUINotifications.BuffsDebuffsNotifications.HideLightningDebuff();
            levelUINotifications.AttackBan.HideBanImage();
        }
    }

    IEnumerator WaitAndDeactivateSlowdown(float time)
    {
        yield return new WaitForSeconds(time);
        thisBumper.StopSlowDown();
        
        if (isCurrentPlayer)
        {
            levelUINotifications.BuffsDebuffsNotifications.HideSlowdownDebuff();
        }
    }

    IEnumerator WaitAndDeactivateControlInversion(float time)
    {
        yield return new WaitForSeconds(time);
        thisPlayerGO.GetComponent<ArcadeVehicleController>().ChangeTheTurnControllerToNormal();

        if (isCurrentPlayer)
        {
            levelUINotifications.BuffsDebuffsNotifications.HideControlInversion();
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
        //Debug.Log("Щит активирован");
        yield return new WaitForSeconds(time);
        isShieldActive = false;
        _intermediary.HideShield();
        //Debug.Log("Щит отключен");

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
}
