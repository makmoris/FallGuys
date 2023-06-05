using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostRaceCanvasUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textWithNumberOfWinners;

    public void SetNumberOfWinnersText(int numberOfWinners)
    {
        textWithNumberOfWinners.text = numberOfWinners.ToString();
    }
}
