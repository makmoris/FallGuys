using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneycombProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    [SerializeField] HoneycombEliminatePanel eliminatePanel;


    public void SetNumberOfFallens(int numberOfFallens)
    {
        eliminatePanel.SetNumberOfFallens(numberOfFallens);
    }

    public void UpdateNamberOfFallens(int currentNumber)
    {
        eliminatePanel.UpdateNamberOfFallens(currentNumber);
    }

    protected override void HideElementsBeforeStart()
    {
        base.HideElementsBeforeStart();

        if (eliminatePanel.gameObject.activeSelf) eliminatePanel.gameObject.SetActive(false);
    }

    protected override void TimerBeforeStartFinished()
    {
        base.TimerBeforeStartFinished();

        eliminatePanel.gameObject.SetActive(true);
    }
}
