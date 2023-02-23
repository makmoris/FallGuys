using System;
using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour /* ��������������������������� */ // �� ������� ������ ������ ����� ������ ���� ������
{
    private bool isPlayer;
    private bool playerWasSetted;

    private AudioListener playerAudioListener;

    public event Action<Bonus> OnBonusGot;
    public event Action<Bonus, GameObject> OnBonusGotWithGameObject;

    private Coroutine oilPuddleCoroutine = null;

    public static Action<int> BonusBoxGiveGoldEvent;// ��� �������������� ����������� �� ������ � ������� ������
    public static Action<int> BonusBoxGiveHPEvent;

    private void Start()
    {
        // ��� ������ enabled = true/false
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
                    bonus.Got();
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
                            SendPlayerFallsOutMapAnalyticEvent();
                        }

                        OnBonusGot?.Invoke(bonus);
                        bonus.Got();

                        enabled = false; // enabled = true ������� DeadZone �� �����
                    }

                    Debug.Log($"{name} Event GetBonus {other.name}");
                    if (other.name == "MysteryBox" && isPlayer)
                    {
                        SendPlayerPickMysteryBoxAnalyticEvent();
                        VibrationManager.Instance.BonusBoxVibration();

                        if (bonus.Type == BonusType.AddGold)
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

    public void GetBonus(Bonus bonus)// ���������� ������� ��� ����������
    {
        Debug.Log($"Public GetBonus - {bonus.Type}; {gameObject.name}");
        OnBonusGot?.Invoke(bonus);
        bonus.Got();
    }

    public void GetBonus(Bonus bonus, GameObject _gameObject)// ���������� ������� ��� ����������
    {
        Debug.Log($"Public GetBonus - {bonus.Type}; {gameObject.name}");
        OnBonusGotWithGameObject?.Invoke(bonus, _gameObject);
        bonus.Got();
    }

    #region OilPuddle
    public void GetBonusFromOilPuddle(Bonus bonus, float damageInterval)// ���������� ��� ������ � ���� �� OilPuddle
    {
        OnBonusGot?.Invoke(bonus);
        bonus.Got();

        oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, damageInterval));
    }

    IEnumerator WaitAndGetBonusFromOilPuddle(Bonus bonus , float time)
    {
        yield return new WaitForSeconds(time);

        OnBonusGot?.Invoke(bonus);
        bonus.Got();

        oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, time));
    }

    public void StopOilPuddleCoroutine()// ���������� ��� ������ �� ���� �� OilPuddle
    {
        if (oilPuddleCoroutine != null)
        {
            StopCoroutine(oilPuddleCoroutine);
        }
    }
    #endregion

    private void SendPlayerPickMysteryBoxAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = PointerManager.Instance.GetPlayerHealth();

        int _player_kills_amount = LevelProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = LevelProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = LevelProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerPickMysteryBox(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    private void SendPlayerFallsOutMapAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = PointerManager.Instance.GetPlayerHealth();

        int _player_kills_amount = LevelProgressController.Instance.GetCurrentNumberOfFrags();

        int _player_gold_earn = LevelProgressController.Instance.GetCurrentAmountOfGoldReward();

        int _enemies_left = LevelProgressController.Instance.GetCurrentEnemiesLeft();

        AnalyticsManager.Instance.PlayerFallsOutMap(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left);
    }

    private void SetIsPlayer(GameObject playerObj)
    {
        if (playerObj == gameObject) isPlayer = true;
        else isPlayer = false;

        if (isPlayer)
        {
            playerAudioListener = GetComponent<AudioListener>();
        }

        playerWasSetted = true;
    }

    private void OnEnable()
    {
        Installer.IsCurrentPlayer += SetIsPlayer;

        if (playerWasSetted)
        {
            //�������� �������� �� �����, ��������� �� ������
            if (isPlayer)
            {
                GameCameraAudioListenerController.Instance.DeactivateAudioListener();
                playerAudioListener.enabled = true;
            }
        }
    }
    private void OnDisable()
    {
        Installer.IsCurrentPlayer -= SetIsPlayer;

        if (playerWasSetted)
        {
            //��������� �������� �� �����, �������� �� ������
            if (isPlayer)
            {
                GameCameraAudioListenerController.Instance.ActivateAudioListener();
                playerAudioListener.enabled = false;
            }
        }
    }
}
