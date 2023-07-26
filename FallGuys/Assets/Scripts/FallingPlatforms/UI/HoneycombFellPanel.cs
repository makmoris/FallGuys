using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoneycombFellPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfFallenText;
    [SerializeField] private TextMeshProUGUI currentNumberOfFallenText;

    public void SetNumberOfFallens(int numberOfFallens)
    {
        numberOfFallenText.text = $"{numberOfFallens}";
        currentNumberOfFallenText.text = "0";
    }

    public void UpdateNamberOfFallens(int currentNumber)
    {
        currentNumberOfFallenText.text = $"{currentNumber}";
    }
}
