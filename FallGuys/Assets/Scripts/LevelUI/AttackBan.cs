using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AttackBan : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [Space]
    [SerializeField] private Image banImage;
    [SerializeField] private Image banScaleImage;
    [SerializeField] private TextMeshProUGUI banText;

    private GameObject currentPlayer;

    private Coroutine fillingBunTimeCoroutine = null;

    private void Awake()
    {
        banImage.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(true);
    }

    private void SetCurrentPlayer(GameObject gameObj)
    {
        currentPlayer = gameObj;
    }

    private void ShowBanImage(GameObject gameObj, float banTime)
    {
        if (currentPlayer == gameObj)
        {
            attackButton.gameObject.SetActive(false);
            banImage.gameObject.SetActive(true);

            if(fillingBunTimeCoroutine != null)
            {
                StopCoroutine(fillingBunTimeCoroutine);
            }
            fillingBunTimeCoroutine = StartCoroutine(FillingBanScale(banTime));
        }
    }

    private void HideBanImage(GameObject gameObj)
    {
        if (currentPlayer == gameObj)
        {
            banImage.gameObject.SetActive(false);
            attackButton.gameObject.SetActive(true);
        }
    }

    IEnumerator FillingBanScale(float fillingTime)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / fillingTime;

            if (t > 1) t = 1;

            banScaleImage.fillAmount = Mathf.Lerp(0f, 1f, t);

            float time = Mathf.Lerp(fillingTime, 0.1f, t);
            banText.text = $"{Mathf.CeilToInt(time)}";

            yield return null;
        }
    }

    private void OnEnable()
    {
        Installer.IsCurrentPlayer += SetCurrentPlayer;
        PlayerEffector.EnableWeaponEvent += HideBanImage;
        PlayerEffector.DisableWeaponEvent += ShowBanImage;
    }
    private void OnDisable()
    {
        Installer.IsCurrentPlayer -= SetCurrentPlayer;
        PlayerEffector.EnableWeaponEvent -= HideBanImage;
        PlayerEffector.DisableWeaponEvent -= ShowBanImage;
    }
}
