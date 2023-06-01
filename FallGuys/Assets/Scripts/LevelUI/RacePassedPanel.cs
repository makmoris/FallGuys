using UnityEngine;
using TMPro;

public class RacePassedPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfWinnersText;
    [SerializeField] private TextMeshProUGUI currentNumberOfWinnersText;

    public void SetNumberOfWinners(int numberOfWinners)
    {
        numberOfWinnersText.text = $"{numberOfWinners}";
        currentNumberOfWinnersText.text = "0";
    }

    public void UpdateNamberOfWinners(int currentNumber)
    {
        currentNumberOfWinnersText.text = $"{currentNumber}";
    }
}
