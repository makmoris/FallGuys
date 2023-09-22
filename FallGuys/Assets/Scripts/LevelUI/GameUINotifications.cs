using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUINotifications : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthNotificationText;
    [SerializeField] private TextMeshProUGUI goldNotificationText;

    [Space]
    [SerializeField] private float timeToHide;

    private Coroutine hideCoroutine;

    public void ShowHealthNotification(int value)
    {
        healthNotificationText.text = $"+{value} HP";
        healthNotificationText.transform.localScale = Vector3.one;
        healthNotificationText.gameObject.SetActive(true);

        HideHealthNotification();
    }

    public void ShowGoldNotification(int value)
    {
        goldNotificationText.text = $"+{value}";
        goldNotificationText.transform.localScale = Vector3.one;
        goldNotificationText.gameObject.SetActive(true);

        HideGoldNotification();
    }

    private void HideHealthNotification()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideProcess(healthNotificationText.gameObject));
    }

    private void HideGoldNotification()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideProcess(goldNotificationText.gameObject));
    }

    IEnumerator HideProcess(GameObject obj)
    {
        yield return new WaitForSeconds(timeToHide);

        for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
        {
            obj.transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }
        obj.SetActive(false);
    }


    //private void OnEnable()
    //{
    //    Bumper.BonusBoxGiveHPEvent += ShowHealthNotification;
    //    Bumper.BonusBoxGiveGoldEvent += ShowGoldNotification;
    //}

    //private void OnDisable()
    //{
    //    Bumper.BonusBoxGiveHPEvent -= ShowHealthNotification;
    //    Bumper.BonusBoxGiveGoldEvent -= ShowGoldNotification;
    //}
}
