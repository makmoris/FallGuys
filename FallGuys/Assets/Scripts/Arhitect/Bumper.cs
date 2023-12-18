using System;
using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour /* ЧувакКоторогоНельзяНазывать */ // на обьекте нашего игрока будет висеть этот скрипт
{
    private bool isPlayer;

    public event Action<Bonus> OnBonusGot;
    public event Action<Bonus, GameObject> OnBonusGotWithGameObject;

    private Coroutine waitAndStopSlowDownCoroutine = null;
    private Coroutine oilPuddleCoroutine = null;
    private int oilPuddleCoroutineCounter;


    private Rigidbody _rb;
    private float _defaultDragValue;

    [SerializeField]private int slowdownsCounter;

    private LevelUINotifications levelUINotifications;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultDragValue = _rb.drag;
    }

    private void Start()
    {
        // для работы enabled = true/false
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

    public void GetBonus(Bonus bonus)// вызывается взрывом без коллайдера
    {
       //Debug.Log($"Public GetBonus - {bonus.BonusType}; {gameObject.name}");
        OnBonusGot?.Invoke(bonus);
    }

    public void GetBonus(Bonus bonus, GameObject _gameObject)// вызывается взрывом без коллайдера. Для молнии
    {
        //Debug.Log($"Public GetBonus - {bonus.BonusType}; {gameObject.name}");
        OnBonusGotWithGameObject?.Invoke(bonus, _gameObject);
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
    public void GetBonusFromOilPuddle(Bonus bonus, float damageInterval)// вызывается при въезде в лужу из OilPuddle
    {
        OnBonusGot?.Invoke(bonus);

        oilPuddleCoroutineCounter++;

        if (oilPuddleCoroutine == null)
        {
            oilPuddleCoroutine = StartCoroutine(WaitAndGetBonusFromOilPuddle(bonus, damageInterval));
        }

        if (isPlayer)
        {
            // нотификация
            levelUINotifications.BuffsDebuffsNotifications.ShowSlowdownDebuff();
            levelUINotifications.BuffsDebuffsNotifications.ShowHealthDamageDebuff();
        }
    }

    IEnumerator WaitAndGetBonusFromOilPuddle(Bonus bonus , float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log($"OIL COROUTINE");
        OnBonusGot?.Invoke(bonus);

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
                    levelUINotifications.BuffsDebuffsNotifications.HideSlowdownDebuff();
                    levelUINotifications.BuffsDebuffsNotifications.HideHealthDamageDebuff();
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
                // нотификация стоп
                levelUINotifications.BuffsDebuffsNotifications.HideSlowdownDebuff();
            }
        }
        else if (slowdownsCounter < 0) slowdownsCounter = 0;
    }

    public void WaitAndStopSlowDown(float waitTime)
    {
        if(waitAndStopSlowDownCoroutine == null)
        {
            waitAndStopSlowDownCoroutine = StartCoroutine(WaitAndStopSlowDownCoroutine(waitTime));
        }
        else
        {
            StopCoroutine(waitAndStopSlowDownCoroutine);
            StopSlowDown();

            waitAndStopSlowDownCoroutine = StartCoroutine(WaitAndStopSlowDownCoroutine(waitTime));
        }
    }

    IEnumerator WaitAndStopSlowDownCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        StopSlowDown();

        waitAndStopSlowDownCoroutine = null;
    }
    #endregion

    private void OnDisable()
    {
        StopSlowDown();
    }
}
