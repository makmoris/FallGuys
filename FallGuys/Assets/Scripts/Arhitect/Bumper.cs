using System;
using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour /* ��������������������������� */ // �� ������� ������ ������ ����� ������ ���� ������
{
    private bool isPlayer;

    public event Action<Bonus> OnBonusGot;
    public event Action<Bonus, GameObject> OnBonusGotWithGameObject;

    //public static event Action PlayerSlowedEvent;
    //public static event Action PlayerStoppedSlowingEvent;
    private Coroutine oilPuddleCoroutine = null;
    private int oilPuddleCoroutineCounter;

    private Rigidbody _rb;
    private float _defaultDragValue;

    [SerializeField]private int slowdownsCounter;

    //public static event Action<int> BonusBoxGiveGoldEvent;// ��� �������������� ����������� �� ������ � ������� ������
    //public static event Action<int> BonusBoxGiveHPEvent;

    private LevelUINotifications levelUINotifications;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultDragValue = _rb.drag;
    }

    private void Start()
    {
        // ��� ������ enabled = true/false
    }

    public void Init(GameObject playerObj, LevelUINotifications levelUINotifications)
    {
        if (playerObj == gameObject)
        {
            isPlayer = true;

            this.levelUINotifications = levelUINotifications;
        }
        else isPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var bonus = other.GetComponent<Bonus>();
        if (bonus != null)
        {
            Bullet bullet = bonus.GetComponent<Bullet>();
            if (bullet != null)// ���� "�����" �������� �����
            {
                GameObject bulletParent = bullet.GetParent();// �������, ��� ��� ���� ��������. ���� "�������" ��������� � �������� �������
                if (bulletParent != this.gameObject)                 // ������ ��� ���� � ��� �� �����. �� ���� �� ������ ����.
                {
                    OnBonusGot?.Invoke(bonus);
                    //bonus.Got();
                    Debug.Log($"��������� ���� � ������");
                }
            }
            else
            {
                Explosion explosion = bonus.GetComponent<Explosion>();
                if (explosion == null)// ����� ���������� ����� public GetBonus. ����� �������� ����� ������ �����. + ����� �� ����� ����� ������
                {
                    if(other.GetComponent<DeadZone>() != null && enabled) // �������, �.�. ���� �� ��������� �������
                    {
                        if (isPlayer)
                        {
                            //SendPlayerFallsOutMapAnalyticEvent();
                        }

                        OnBonusGot?.Invoke(bonus);
                        //bonus.Got();

                        enabled = false; // enabled = true ������� DeadZone �� �����
                    }

                    Debug.Log($"{name} Event GetBonus {other.name}");
                    if (other.name == "MysteryBox" && isPlayer)
                    {
                        //SendPlayerPickMysteryBoxAnalyticEvent();
                        VibrationManager.Instance.BonusBoxVibration();

                        if (bonus.Type == BonusType.AddGold)// �����, �.�. �����, ��� ��� ����� �������� ����
                        {
                            //BonusBoxGiveGoldEvent?.Invoke((int)bonus.Value);
                            levelUINotifications.GameUINotifications.ShowGoldNotification((int)bonus.Value);
                        }
                        if (bonus.Type == BonusType.AddHealth)
                        {
                            //BonusBoxGiveHPEvent?.Invoke((int)bonus.Value);
                            levelUINotifications.GameUINotifications.ShowHealthNotification((int)bonus.Value);
                        }
                    }

                    if (enabled)
                    {
                        OnBonusGot?.Invoke(bonus);
                        //bonus.Got();
                        Debug.Log($"TEST {bonus.Type}");
                    }
                }
            }
        }
    }

    public void GetBonus(Bonus bonus)// ���������� ������� ��� ����������
    {
        Debug.Log($"Public GetBonus - {bonus.Type}; {gameObject.name}");
        OnBonusGot?.Invoke(bonus);
        //bonus.Got();
    }

    public void GetBonusWithGameObject(Bonus bonus, GameObject _gameObject)// ���������� ������� ��� ����������. ��� ������
    {
        Debug.Log($"Public GetBonus - {bonus.Type}; {gameObject.name}");
        OnBonusGotWithGameObject?.Invoke(bonus, _gameObject);
        //bonus.Got();
    }

    public void ShowAdditionalBulletBonusNotification(AdditionalBulletBonus additionalBulletBonus)
    {
        if (isPlayer) levelUINotifications.BuffsDebuffsNotifications.ShowAdditionalBulletBonusNotification(additionalBulletBonus);
    }

    public void HideAdditionalBulletBonusNotification()
    {
        if (isPlayer) levelUINotifications.BuffsDebuffsNotifications.HideAdditionalBulletBonusNotification();
    }

    #region OilPuddle
    public void GetBonusFromOilPuddle(Bonus bonus, float damageInterval)// ���������� ��� ������ � ���� �� OilPuddle
    {
        OnBonusGot?.Invoke(bonus);
        //bonus.Got();

        oilPuddleCoroutineCounter++;

        if (oilPuddleCoroutine == null)
        {
            oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, damageInterval));
        }

        if (isPlayer)
        {
            // �����������
            //PlayerSlowedEvent?.Invoke();
            levelUINotifications.BuffsDebuffsNotifications.ShowSlowdownDebuff();
        }
    }

    IEnumerator WaitAndGetBonusFromOilPuddle(Bonus bonus , float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log($"OIL COROUTINE");
        OnBonusGot?.Invoke(bonus);
        //bonus.Got();

        oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, time));
    }

    public void StopOilPuddleCoroutine()// ���������� ��� ������ �� ���� �� OilPuddle
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
                    // ����������� ����
                    //PlayerStoppedSlowingEvent?.Invoke();
                    levelUINotifications.BuffsDebuffsNotifications.HideSlowdownDebuff();
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
            // �����������
            //PlayerSlowedEvent?.Invoke();
            levelUINotifications.BuffsDebuffsNotifications.ShowSlowdownDebuff();
        }
    }

    public void StopSlowDown()
    {
        slowdownsCounter--;

        if (slowdownsCounter <= 0)
        {
            _rb.drag = _defaultDragValue;

            if (isPlayer)
            {
                // ����������� ����
                //PlayerStoppedSlowingEvent?.Invoke();
                levelUINotifications.BuffsDebuffsNotifications.HideSlowdownDebuff();
            }
        }
        else if (slowdownsCounter < 0) slowdownsCounter = 0;
    }
    #endregion

    private void OnDisable()
    {
        StopSlowDown();
    }

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
}
