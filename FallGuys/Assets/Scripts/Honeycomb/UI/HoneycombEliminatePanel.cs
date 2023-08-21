using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoneycombEliminatePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NeededEliminatedText;
    [SerializeField] private TextMeshProUGUI CurrentEliminatedPlayersText;

    public void SetNumberOfFallens(int numberOfFallens)
    {
        NeededEliminatedText.text = $"{numberOfFallens}";
        CurrentEliminatedPlayersText.text = "0";
    }

    public void UpdateNamberOfFallens(int currentNumber)
    {
        CurrentEliminatedPlayersText.text = $"{currentNumber}";
    }
}
