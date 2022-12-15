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

        float stepText = 8f / 255f;
        float stepPanel = 8f / 255f;

        float textAlpha = textMessage.color.a;
        float panelAlpha = panelImage.color.a;

        while (panelAlpha >= 10f / 255f)
        {
            textAlpha = Mathf.MoveTowards(textMessage.color.a, targetTextColor.a, stepText);
            textMessage.color = new Color(textMessage.color.r, textMessage.color.g, textMessage.color.b, textAlpha);
            textAlpha = textMessage.color.a;

            panelAlpha = Mathf.MoveTowards(panelImage.color.a, targetPanelColor.a, stepPanel);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, panelAlpha);
            panelAlpha = panelImage.color.a;

            //yield return null;
            yield return new WaitForFixedUpdate();
        }

        gameObject.SetActive(false);
    }

    //IEnumerator Fading()
    //{
    //    yield return new WaitForSeconds(1f);

    //    Color initialTextColor = textMessage.color;
    //    Color initialPanelColor = panelImage.color;

    //    Color targetTextColor = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0f);
    //    Color targetPanelColor = new Color(initialPanelColor.r, initialPanelColor.g, initialPanelColor.b, 0f);

    //    float elapsedTime = 0f;
    //    float fadeDurationImage = 8f;
    //    float fadeDurationText = 3f;

    //    while (elapsedTime < fadeDurationImage)
    //    {
    //        elapsedTime += Time.deltaTime;

    //        textMessage.color = Color.Lerp(textMessage.color, targetTextColor, elapsedTime / fadeDurationText);
    //        panelImage.color = Color.Lerp(panelImage.color, targetPanelColor, elapsedTime / fadeDurationImage);

    //        //yield return null;
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

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
