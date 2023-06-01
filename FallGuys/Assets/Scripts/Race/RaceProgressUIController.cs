using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceProgressUIController : MonoBehaviour
{
    [Header("Cut Scene")]
    [SerializeField] private ShowTrackCutScene trackCutScene;

    [Header("Start")]
    [SerializeField] private GameObject startRaceUIPanel;
    [SerializeField] private TimerBeforeStart timerBeforeStart;
    [SerializeField] private int time = 3;

    [Header("Passed")]
    [SerializeField] private GameObject passedPanel;

    public event System.Action RaceCanStartEvent;

    private void OnEnable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent += StartShowingGoalAndTimer;
        timerBeforeStart.TimerBeforeStartFinishedEvent += TimerBeforeStartFinished;
    }

    private void Awake()
    {
        if (!startRaceUIPanel.activeSelf) startRaceUIPanel.SetActive(true);
        if (passedPanel.activeSelf) passedPanel.SetActive(false);
    }

    private void StartShowingGoalAndTimer()
    {
        timerBeforeStart.StartTimer(time);
    }

    private void TimerBeforeStartFinished()
    {
        startRaceUIPanel.SetActive(false);
        passedPanel.SetActive(true);

        RaceCanStartEvent?.Invoke();
    }

    private void OnDisable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent -= StartShowingGoalAndTimer;
        timerBeforeStart.TimerBeforeStartFinishedEvent -= TimerBeforeStartFinished;
    }
}
