using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CupRewardProgressScale : MonoBehaviour
{
    [SerializeField] private RectTransform _progressScale;
    [SerializeField] private RectTransform _cupsHolder;
    [SerializeField] private TextMeshProUGUI _cupsText;

    private HorizontalLayoutGroup _parentHorizontalLayoutGroup;

    RectTransform lastRewardItemRect;
    RectTransform firstRewardItemRect;

    private void Awake()
    {
        _progressScale.anchorMin = new Vector2(0f, 0.5f);
        _progressScale.anchorMax = new Vector2(0f, 0.5f);
        _progressScale.pivot = new Vector2(0f, 0.5f);
    }

    public void Initialize(CupRewardItem firstRewardItem, CupRewardItem lastRewardItem)
    {
        _parentHorizontalLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();

        

        firstRewardItemRect = firstRewardItem.GetComponent<RectTransform>();
        lastRewardItemRect = lastRewardItem.GetComponent<RectTransform>();

        Debug.LogError($"{firstRewardItemRect.position}; {lastRewardItemRect.localPosition}; fff {_progressScale.localPosition}");
    }

    public void UpdateProgressScale(CupRewardItem lowCupRewardItem, CupRewardItem highCupRewardItem, int currentCupsValue)
    {
        firstRewardItemRect = lowCupRewardItem.GetComponent<RectTransform>();
        lastRewardItemRect = highCupRewardItem.GetComponent<RectTransform>();

        Debug.LogError($"min item pos = {lowCupRewardItem.GetComponent<RectTransform>().localRotation};" +
            $" max item pos = {highCupRewardItem.GetComponent<RectTransform>().localPosition}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError($"{firstRewardItemRect.position}; {lastRewardItemRect.localPosition}; fff {_progressScale.localPosition}");
        }
    }
}
