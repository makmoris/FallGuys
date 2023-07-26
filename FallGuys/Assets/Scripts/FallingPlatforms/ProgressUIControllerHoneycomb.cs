using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUIControllerHoneycomb : MonoBehaviour
{
    [SerializeField] HoneycombFellPanel fellPanel;

    public void SetNumberOfFallens(int numberOfFallens)
    {
        fellPanel.SetNumberOfFallens(numberOfFallens);
    }

    public void UpdateNamberOfFallens(int currentNumber)
    {
        fellPanel.UpdateNamberOfFallens(currentNumber);
    }
}
