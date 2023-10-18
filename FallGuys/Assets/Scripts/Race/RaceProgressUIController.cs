using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceProgressUIController : LevelProgressUIController
{
    [Header("-----")]

    [Header("Finish")]
    [SerializeField] private RacePassedPanel winnersPanel;

    [Header("Timer")]
    [SerializeField] private GameTimer gameTimer;

    public override void ShowObserverUI()
    {
        base.ShowObserverUI();

        winnersPanel.gameObject.SetActive(false);
    }

    protected override void HideElementsBeforeStart()
    {
        base.HideElementsBeforeStart();

        if (winnersPanel.gameObject.activeSelf) winnersPanel.gameObject.SetActive(false);
        gameTimer.gameObject.SetActive(false);
    }

    public void SetNumberOfWinners(int numberOfWinners)
    {
        winnersPanel.SetNumberOfWinners(numberOfWinners);
    }

    public void UpdateNumberOfWinners(int currentNumber)
    {
        winnersPanel.UpdateNamberOfWinners(currentNumber);
    }

    protected override void TimerBeforeStartFinished()
    {
        base.TimerBeforeStartFinished();

        winnersPanel.gameObject.SetActive(true);
    }

    public void StartFinishTimer(float seconds, System.Action onTimerFinished)
    {
        gameTimer.gameObject.SetActive(true);

        gameTimer.StartTimer(seconds, onTimerFinished);
    }
}
