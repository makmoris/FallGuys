using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualIntermediary : MonoBehaviour // висит на игроке и отвечает за визуальное представление. UI, эффект уничтожения и т.п.
{
    public GameObject destroyEffect;
    public GameObject shield;

    private Material shieldMaterial;
    private float startShieldAlpha;
    private float downShieldAlpha;
    private Coroutine waitShieldCoroutine = null;
    private Coroutine shieldCoroutine = null;

    private EnemyPointer enemyPointer;

    private GameObject playerGO;
    private bool isPlayer;

    private HitHistory hitHistory;

    public static event Action<GameObject> PlayerWasDeadEvent; // кидаем событие всем заинтересованным


    private void Awake()
    {
        Transform enemyPointerTransform = transform.Find("EnemyPointer");
        if (enemyPointerTransform != null)
        {
            enemyPointer = enemyPointerTransform.GetComponent<EnemyPointer>();
        }

        if (shield.activeSelf) shield.SetActive(false);
        shieldMaterial = shield.GetComponent<MeshRenderer>().material;

        hitHistory = GetComponent<HitHistory>();
    }

    private void Start()
    {
        startShieldAlpha = shieldMaterial.color.a;
        downShieldAlpha = 20f / 255f;
    }

    public void SetIsCurrentPlayer(GameObject playerObj)
    {
        if (playerObj == gameObject) isPlayer = true;
        else isPlayer = false;

        playerGO = playerObj;
    }

    public void UpdateHealthInUI(float healthValue)
    {
        if (enemyPointer != null) ArenaPointerManager.Instance.UpdateHealthInUI(enemyPointer, healthValue);
        else ArenaPointerManager.Instance.UpdatePlayerHealthInUI(healthValue);
    }

    public void DestroyCar()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);

        if (isPlayer) ArenaPointerManager.Instance.UpdatePlayerHealthInUI(-1000f);// как заглушка, чтобы счетчик хп показал 0
        else { }

        if(playerGO != null)
        {
            if (hitHistory.GetLastShooter() == playerGO)
            {
                Debug.Log($"игрока {gameObject.name} вытолкнул и убил {playerGO.name}");
                LevelProgressController.Instance.AddFrag();
                LevelProgressController.Instance.SendPlayerKillEnemyAnalyticEvent(gameObject);
            }
        }

        PlayerWasDeadEvent?.Invoke(this.gameObject);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void DestroyCar(GameObject killer)// смотрим, если киллер игрок (есть нужный скрипт)
    {
        if (playerGO != null)
        {
            Debug.Log($"игрока {gameObject.name} убил {killer.name}");
            if (killer == playerGO)
            {
                LevelProgressController.Instance.AddFrag();
                LevelProgressController.Instance.SendPlayerKillEnemyAnalyticEvent(gameObject);
            }
        }

        Instantiate(destroyEffect, transform.position, Quaternion.identity);

        if (isPlayer) ArenaPointerManager.Instance.UpdatePlayerHealthInUI(-1000f);// как заглушка, чтобы счетчик хп показал 0
        else { }

        PlayerWasDeadEvent?.Invoke(this.gameObject);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }


    #region Shield
    public void ShowShield(float time)
    {
        Color color = shieldMaterial.color;
        color.a = startShieldAlpha;
        shieldMaterial.color = color;

        shield.SetActive(true);
        float waitTime = (time / 3f) * 2f; // 15/3 = 5; 5*2 = 10. Через 10 секунд начинаем показывать, что скоро конец. Когда остается 1/3 времени
        float blinkingFrequency = (time / 3f) / 5f;// 5 мерцаний до деактивации щита

        waitShieldCoroutine = StartCoroutine(WaitAndHideShield(waitTime, blinkingFrequency));
    }

    public void HideShield()
    {
        if (waitShieldCoroutine != null) StopCoroutine(waitShieldCoroutine);
        if(shieldCoroutine != null) StopCoroutine(shieldCoroutine);

        shield.SetActive(false);
    }

    IEnumerator WaitAndHideShield(float waitTime, float blinkingFrequency)
    {
        yield return new WaitForSeconds(waitTime);
        shieldCoroutine = StartCoroutine(ShowShieldCoroutine(blinkingFrequency));
    }

    IEnumerator ShowShieldCoroutine(float blinkingFrequency)
    {
        for (; ; )
        {
            for (float t = 0; t <= blinkingFrequency; t += Time.deltaTime)// hide
            {
                Color color = shieldMaterial.color;
                color.a = Mathf.Lerp(startShieldAlpha, downShieldAlpha, t / blinkingFrequency);
                shieldMaterial.color = color;
                yield return null;
            }

            for (float t = 0; t <= blinkingFrequency; t += Time.deltaTime)// show
            {
                Color color = shieldMaterial.color;
                color.a = Mathf.Lerp(downShieldAlpha, startShieldAlpha, t / blinkingFrequency);
                shieldMaterial.color = color;
                yield return null;
            }
            yield return null;
        }
    }
    #endregion
}
