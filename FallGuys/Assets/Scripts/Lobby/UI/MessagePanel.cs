using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    private Image panelImage;
    private TextMeshProUGUI textMessage;

    private Coroutine fadingAnimation;

    private void Awake()
    {
        panelImage = GetComponent<Image>();
        textMessage = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if(fadingAnimation != null) StopAnimation();

        fadingAnimation = StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(1f);

        Color initialTextColor = textMessage.color;
        Color initialPanelColor = panelImage.color;

        Color targetTextColor = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0f);
        Color targetPanelColor = new Color(initialPanelColor.r, initialPanelColor.g, initialPanelColor.b, 0f);

        float elapsedTime = 0f;
        float fadeDuration = 10f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            textMessage.color = Color.Lerp(textMessage.color, targetTextColor, elapsedTime / fadeDuration);
            panelImage.color = Color.Lerp(panelImage.color, targetPanelColor, elapsedTime / fadeDuration);

            yield return null;
        }
    }

    private void StopAnimation()
    {
        StopCoroutine(fadingAnimation);

        Color color = panelImage.color;
        color.a = 1f;
        panelImage.color = color;

        Color colorText = textMessage.color;
        colorText.a = 1f;
        textMessage.color = colorText;
    }
}
