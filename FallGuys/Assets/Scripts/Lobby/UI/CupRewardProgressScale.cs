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

    [Header("Progress Filling Animation Settings")]
    [SerializeField] private float _timeBeforeStartFilling = 0.5f;
    [SerializeField] private float _fillingAnimationSpeed = 2f;

    private float _targetScalePositionX;
    private bool _isShowFillingAnimationOnEnable;

    private Dictionary<float, int> _cupRewardPositionXWithCupsValuePairs = new Dictionary<float, int>();

    private HorizontalLayoutGroup _horizontalLayoutGroup;
    private RectTransform _horizontalLayoutGroupRectTransform;

    private RectTransform _canvasRectTransform;

    private float _lastPositionX;
    private float _widthOfFirstReward;
    private float _widthOfLastReward;

    private bool isInit;

    private System.Action _progressFillingCompletedCallback;

    private void OnEnable()
    {
        if(_isShowFillingAnimationOnEnable) StartCoroutine(ShowProgressScaleAnimation(_targetScalePositionX));
    }

    private void Initialize()
    {
        _horizontalLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>(true);
        _horizontalLayoutGroupRectTransform = _horizontalLayoutGroup.GetComponent<RectTransform>();
        _canvasRectTransform = GetComponentInParent<Canvas>(true).GetComponent<RectTransform>();

        _progressScale.anchorMin = new Vector2(0f, 0.5f);
        _progressScale.anchorMax = new Vector2(0f, 0.5f);
        _progressScale.pivot = new Vector2(0f, 0.5f);

        isInit = true;
    }

    public void AddCupReward(CupRewardItem cupRewardItem, int cupsValueForReward, bool isFirstReward, bool isLastReward)
    {
        if (!isInit) Initialize();

        RectTransform cupRewardsRectTransform = cupRewardItem.GetComponent<RectTransform>();

        if (isFirstReward)
        {
            float firstPosX = _horizontalLayoutGroup.padding.left + cupRewardsRectTransform.rect.width / 2f;
            _cupRewardPositionXWithCupsValuePairs.Add(firstPosX, cupsValueForReward);

            _lastPositionX = firstPosX;
            _widthOfFirstReward = cupRewardsRectTransform.rect.width;

            return;
        }

        float posX = _horizontalLayoutGroup.spacing + cupRewardsRectTransform.rect.width + _lastPositionX;
        _cupRewardPositionXWithCupsValuePairs.Add(posX, cupsValueForReward);

        _lastPositionX = posX;

        if(isLastReward) _widthOfLastReward = cupRewardsRectTransform.rect.width;
    }

    public void SetProgressScalePosition(int currentCupsValue)
    {
        float currentScalePositionX = GetProgressScalePosition(currentCupsValue);

        _progressScale.sizeDelta = new Vector2(currentScalePositionX, _progressScale.sizeDelta.y);
        _cupsText.text = currentCupsValue.ToString();

        UpdateScrollPosition(currentScalePositionX);
    }

    public void SetNewProgressScalePosition(int previousCupsValue, int currentCupsValue, System.Action progressFillingCompletedCallback)
    {
        float previousScalePositionX = GetProgressScalePosition(previousCupsValue);
        _targetScalePositionX = GetProgressScalePosition(currentCupsValue);

        _progressScale.sizeDelta = new Vector2(previousScalePositionX, _progressScale.sizeDelta.y);
        _cupsHolder.gameObject.SetActive(false);
        _cupsText.text = currentCupsValue.ToString();

        _isShowFillingAnimationOnEnable = true;

        _progressFillingCompletedCallback = progressFillingCompletedCallback;
    }

    private void UpdateScrollPosition(float currentScalePositionX)
    {
        float parentWidth = 
            (_lastPositionX + _horizontalLayoutGroup.padding.right + _widthOfLastReward / 2f) - _canvasRectTransform.rect.width;

        float percentageOfCurrentScalePositionFromLastPosition =
            (currentScalePositionX - _horizontalLayoutGroup.padding.left - _widthOfFirstReward) / _lastPositionX;

        _horizontalLayoutGroupRectTransform.anchoredPosition = 
            new Vector2(parentWidth * percentageOfCurrentScalePositionFromLastPosition * -1f,
            _horizontalLayoutGroupRectTransform.anchoredPosition.y);
    }

    private float GetProgressScalePosition(int currentCupsValue)
    {
        float lowRewardPosX = 0f;
        int lowRewardCupsValue = 0;

        float highRewardPosX = 0f;
        int highRewardCupsValue = 0;

        foreach (var kvp in _cupRewardPositionXWithCupsValuePairs)
        {
            float rewardPosX = kvp.Key;
            int cups = kvp.Value;

            if (currentCupsValue >= cups)
            {
                lowRewardPosX = rewardPosX;
                lowRewardCupsValue = cups;
            }
            else
            {
                highRewardPosX = rewardPosX;
                highRewardCupsValue = cups;

                break;
            }
        }

        float lengthBetweenLowAndHighPosX = highRewardPosX - lowRewardPosX;
        int cupsValueBetweenLowAndHighCupsValues = highRewardCupsValue - lowRewardCupsValue;

        int currentCupsForLowValue = currentCupsValue - lowRewardCupsValue;

        float percentageOfCurrentCupsValueFromCupsValueBetweenLowAndHigh = (float)currentCupsForLowValue / cupsValueBetweenLowAndHighCupsValues;

        float progressScalePosX =
            lengthBetweenLowAndHighPosX * percentageOfCurrentCupsValueFromCupsValueBetweenLowAndHigh + lowRewardPosX;

        return progressScalePosX;
    }

    IEnumerator ShowProgressScaleAnimation(float targetProgressScalePosX)
    {
        MusicManager.Instance.PlayFillingLeagueScaleMusic();

        yield return new WaitForSeconds(_timeBeforeStartFilling);

        float scaleStep = _fillingAnimationSpeed;

        Vector2 target = new Vector2(targetProgressScalePosX, _progressScale.sizeDelta.y);

        while (Vector2.Distance(_progressScale.sizeDelta, target) > 0.001f)
        {
            _progressScale.sizeDelta = Vector2.MoveTowards(_progressScale.sizeDelta, target, scaleStep);

            UpdateScrollPosition(_progressScale.sizeDelta.x);

            yield return new WaitForFixedUpdate();
        }

        _cupsHolder.gameObject.SetActive(true);

        _isShowFillingAnimationOnEnable = false;

        _progressFillingCompletedCallback?.Invoke();

        MusicManager.Instance.PlayLobbyMusic();
    }
}
