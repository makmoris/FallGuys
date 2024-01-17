using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartGoalUIPanel : MonoBehaviour
{
    [Header("Goal Panel")]
    [SerializeField] private RectTransform goalPanel;
    [Space]
    [SerializeField] private float duration = 1f;
    [SerializeField] private float delay;
    [SerializeField] private Vector3 goalPanelStartPosition;
    [SerializeField] private Vector3 goalPanelEndPosition;
    [Header("Timer")]
    [SerializeField] private float time;
    [SerializeField] private TimerBeforeStart timerBeforeStart;

    public delegate void TimerCallback();

    private TimerCallback timerCallback;

    private Tween goalMovingTween;

    private void OnEnable()
    {
        if(timerBeforeStart.gameObject.activeSelf) timerBeforeStart.gameObject.SetActive(false);

        if (goalPanel.localPosition != goalPanelStartPosition) goalPanel.localPosition = goalPanelStartPosition;

        if (!goalPanel.gameObject.activeSelf) goalPanel.gameObject.SetActive(true);
    }

    public void ShowGoal(TimerCallback timerCallback)
    {
        this.timerCallback = timerCallback;

        ShowGoalAnimation();
    }

    private void ShowGoalAnimation()
    {
        goalMovingTween = goalPanel.transform.DOLocalMove(goalPanelEndPosition, duration).SetDelay(delay).SetEase(Ease.OutBack).
            OnComplete(StartTimer);
    }

    private void StartTimer()
    {
        goalPanel.localPosition = goalPanelEndPosition;
        KillTweens();
        timerBeforeStart.gameObject.SetActive(true);

        timerBeforeStart.TimerBeforeStartFinishedEvent += TimerFinished;
        timerBeforeStart.StartTimer(time);
    }

    private void TimerFinished()
    {
        timerBeforeStart.TimerBeforeStartFinishedEvent -= TimerFinished;
        timerBeforeStart.gameObject.SetActive(false);

        timerCallback();

        // выключаем панельку
        gameObject.SetActive(false);
    }

    private void KillTweens()
    {
        if (goalMovingTween != null && goalMovingTween.IsActive() && !goalMovingTween.IsComplete())
        {
            goalMovingTween.Kill();
            goalMovingTween = null;
        }
    }
}
