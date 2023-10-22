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

    private Coroutine fillingBanTimeCoroutine = null;

    private void Awake()
    {
        banImage.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(true);
    }

    public void ShowBanImage(float banTime)
    {
        attackButton.gameObject.SetActive(false);
        banImage.gameObject.SetActive(true);

        if (fillingBanTimeCoroutine != null)
        {
            CoroutineRunner.Stop(fillingBanTimeCoroutine);
        }
        fillingBanTimeCoroutine = CoroutineRunner.Run(FillingBanScale(banTime));
    }

    public void HideBanImage()
    {
        banImage.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(true);
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
}
