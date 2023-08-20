using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    [Header("Cut Scene")]
    [SerializeField] private ShowTrackCutScene trackCutScene;

    [Header("Start")]
    [SerializeField] private GameObject startRaceUIPanel;
    [SerializeField] private TimerBeforeStart timerBeforeStart;
    [SerializeField] private int time = 3;

    [Header("Finish")]
    [SerializeField] private RacePassedPanel winnersPanel;

    public event System.Action RaceCanStartEvent;

    private void OnEnable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent += StartShowingGoalAndTimer;
        timerBeforeStart.TimerBeforeStartFinishedEvent += TimerBeforeStartFinished;
    }

    public override void ShowObserverUI()
    {
        base.ShowObserverUI();

        winnersPanel.gameObject.SetActive(false);
    }

    protected override void HideElementsBeforeStart()
    {
        base.HideElementsBeforeStart();

        if (!startRaceUIPanel.activeSelf) startRaceUIPanel.SetActive(true);
        if (winnersPanel.gameObject.activeSelf) winnersPanel.gameObject.SetActive(false);
    }

    public void SetNumberOfWinners(int numberOfWinners)
    {
        winnersPanel.SetNumberOfWinners(numberOfWinners);
    }

    public void UpdateNumberOfWinners(int currentNumber)
    {
        winnersPanel.UpdateNamberOfWinners(currentNumber);
    }

    private void StartShowingGoalAndTimer()
    {
        timerBeforeStart.StartTimer(time);
    }

    private void TimerBeforeStartFinished()
    {
        startRaceUIPanel.SetActive(false);
        winnersPanel.gameObject.SetActive(true);

        RaceCanStartEvent?.Invoke();
    }

    private void OnDisable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent -= StartShowingGoalAndTimer;
        timerBeforeStart.TimerBeforeStartFinishedEvent -= TimerBeforeStartFinished;
    }
}
