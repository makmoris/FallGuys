using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceProgressUIController : LevelProgressUIController
{
    [Header("-----")]

    [Header("Finish")]
    [SerializeField] private RacePassedPanel winnersPanel;

    public override void ShowObserverUI()
    {
        base.ShowObserverUI();

        winnersPanel.gameObject.SetActive(false);
    }

    protected override void HideElementsBeforeStart()
    {
        base.HideElementsBeforeStart();

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

    protected override void TimerBeforeStartFinished()
    {
        base.TimerBeforeStartFinished();

        winnersPanel.gameObject.SetActive(true);
    }
}
