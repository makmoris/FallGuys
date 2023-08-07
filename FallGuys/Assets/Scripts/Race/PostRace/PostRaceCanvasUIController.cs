using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PostRaceCanvasUIController : PostLevelUIController
{
    [SerializeField] private TextMeshProUGUI textWithNumberOfWinners;

    public void SetNumberOfWinnersText(int numberOfWinners)
    {
        textWithNumberOfWinners.text = numberOfWinners.ToString();
    }
}
