using System;
using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour /* ЧувакКоторогоНельзяНазывать */ // на обьекте нашего игрока будет висеть этот скрипт
{
    [SerializeField]private bool isPlayer;
    private bool playerWasSetted;

    private AudioListener playerAudioListener;

    public event Action<Bonus> OnBonusGot;
    public event Action<Bonus, GameObject> OnBonusGotWithGameObject;

    public static event Action PlayerSlowedEvent;
    public static event Action PlayerStoppedSlowingEvent;
    private Coroutine oilPuddleCoroutine = null;
    private int oilPuddleCoroutineCounter;

    private Rigidbody _rb;
    private float _defaultDragValue;

    private int slowdownsCounter;

    public static event Action<int> BonusBoxGiveGoldEvent;// для дополнительной нотификации на экране о подборе бонуса
    public static event Action<int> BonusBoxGiveHPEvent;


    private void OnEnable()
    {
        if (playerWasSetted)
        {
            //включаем листенер на тачке, отключаем на камере
            if (isPlayer)
            {
                GameCameraAudioListenerController.Instance.DeactivateAudioListener();
                playerAudioListener.enabled = true;
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultDragValue = _rb.drag;
    }

    private void Start()
    {
        // для работы enabled = true/false
    }

    public void SetIsCurrentPlayer(GameObject playerObj)
    {
        if (playerObj == gameObject) isPlayer = true;
        else isPlayer = false;

        if (isPlayer)
        {
            playerAudioListener = GetComponent<AudioListener>();
        }

        playerWasSetted = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var bonus = other.GetComponent<Bonus>();
        if (bonus != null)
        {
            Bullet bullet = bonus.GetComponent<Bullet>();
            if (bullet != null)// если "бонус" оказался пулей
            {
                GameObject bulletParent = bullet.GetParent();// смотрим, кто эту пулю выпустил. Если "стрелок" совпадает с хозяином бампера
                if (bulletParent != this.gameObject)                 // значит это один и тот же игрок. Не бьем по самому себе.
                {
                    OnBonusGot?.Invoke(bonus);
                    bonus.Got();
                    Debug.Log($"Прилетела пуля в бампер");
                }
            }
            else
            {
                Explosion explosion = bonus.GetComponent<Explosion>();
                if (explosion == null)// взрыв вызывается через public GetBonus. Самим бампером ловим только баффы. + Взрыв не сразу после наезда
                {
                    if(other.GetComponent<DeadZone>() != null && enabled) // сделано, т.к. было по несколько вызовов
                    {
                        if (isPlayer)
                        {
                            SendPlayerFallsOutMapAnalyticEvent();
                        }

                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();

                        enabled = false; // enabled = true сделает DeadZone на респе
                    }

                    Debug.Log($"{name} Event GetBonus {other.name}");
                    if (other.name == "MysteryBox" && isPlayer)
                    {
                        SendPlayerPickMysteryBoxAnalyticEvent();
                        VibrationManager.Instance.BonusBoxVibration();

                        if (bonus.Type == BonusType.AddGold)// здесь, т.к. знаем, что это игрок подобрал бокс
                        {
                            BonusBoxGiveGoldEvent?.Invoke((int)bonus.Value);
                        }
                        if (bonus.Type == BonusType.AddHealth)
                        {
                            BonusBoxGiveHPEvent?.Invoke((int)bonus.Value);
                        }
                    }

                    if (enabled)
                    {
                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();
                        Debug.Log($"TEST {bonus.Type}");
                    }
                }
            }
        }
    }

    public void GetBonus(Bonus bonus)// вызывается взрывом без коллайдера
    {
        Debug.Log($"Public GetBonus - {bonus.Type}; {gameObject.name}");
        OnBonusGot?.Invoke(bonus);
        bonus.Got();
    }

    public void GetBonusWithGameObject(Bonus bonus, GameObject _gameObject)// вызывается взрывом без коллайдера. Для молнии
    {
        Debug.Log($"Public GetBonus - {bonus.Type}; {gameObject.name}");
        OnBonusGotWithGameObject?.Invoke(bonus, _gameObject);
        bonus.Got();
    }

    #region OilPuddle
    public void GetBonusFromOilPuddle(Bonus bonus, float damageInterval)// вызывается при въезде в лужу из OilPuddle
    {
        OnBonusGot?.Invoke(bonus);
        bonus.Got();

        oilPuddleCoroutineCounter++;

        if (oilPuddleCoroutine == null)
        {
            oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, damageInterval));
        }

        if (isPlayer)
        {
            // нотификация
            PlayerSlowedEvent?.Invoke();
        }
    }

    IEnumerator WaitAndGetBonusFromOilPuddle(Bonus bonus , float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log($"OIL COROUTINE");
        OnBonusGot?.Invoke(bonus);
        bonus.Got();

        oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, time));
    }

    public void StopOilPuddleCoroutine()// вызывается при выходе из лужи из OilPuddle
    {
        if (oilPuddleCoroutine != null)
        {
            oilPuddleCoroutineCounter--;

            if (oilPuddleCoroutineCounter == 0)
            {
                StopCoroutine(oilPuddleCoroutine);
                oilPuddleCoroutine = null;
                Debug.Log($"STOP OIL COROUTINE");

                _rb.drag = _defaultDragValue;

                if (isPlayer)
                {
                    // нотификация стоп
                    PlayerStoppedSlowingEvent?.Invoke();
                }
            }
            else if (oilPuddleCoroutineCounter < 0) oilPuddleCoroutineCounter = 0;
        }
    }
    #endregion

    #region Slowdown
    public void StartSlowdown(float slowdownValue)
    {
        _rb.drag = slowdownValue;

        slowdownsCounter++;

        if (isPlayer)
        {
            // нотификация
            PlayerSlowedEvent?.Invoke();
        }
    }

    public void StopSlowDown()
    {
        slowdownsCounter--;

        if (slowdownsCounter == 0)
        {
            _rb.drag = _defaultDragValue;

            if (isPlayer)
            {
                // нотификация стоп
                PlayerStoppedSlowingEvent?.Invoke();
            }
        }
        else if (slowdownsCounter < 0) slowdownsCounter = 0;
    }
    #endregion

    private void SendPlayerPickMysteryBoxAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = AnalyticsManager.Instance.GetCurrentPlayerHealth();

        int _player_kills_amount = ArenaProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = ArenaProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = ArenaProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerPickMysteryBox(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    private void SendPlayerFallsOutMapAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = AnalyticsManager.Instance.GetCurrentPlayerHealth();

        int _player_kills_amount = ArenaProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = ArenaProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = ArenaProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerFallsOutMap(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    
    private void OnDisable()
    {
        if (playerWasSetted)
        {
            //отключаем листенер на тачке, включаем на камере, чтобы проиграть взрыв игрока
            if (isPlayer)
            {
                GameCameraAudioListenerController.Instance.ActivateAudioListener();
                playerAudioListener.enabled = false;
            }
        }
    }
}
