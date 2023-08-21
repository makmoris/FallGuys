using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneycombProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    [SerializeField] HoneycombEliminatePanel EliminatePanel;

    public void SetNumberOfFallens(int numberOfFallens)
    {
        EliminatePanel.SetNumberOfFallens(numberOfFallens);
    }

    public void UpdateNamberOfFallens(int currentNumber)
    {
        EliminatePanel.UpdateNamberOfFallens(currentNumber);
    }
}
