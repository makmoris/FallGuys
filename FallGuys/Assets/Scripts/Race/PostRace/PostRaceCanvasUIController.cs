using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PostRaceCanvasUIController : PostLevelResultVisualization
{
    [SerializeField] private TextMeshProUGUI textWithNumberOfWinners;

    [Header("Win Window")]

    [Header("Lose Window")]
    [SerializeField] private Button backToLobbyButton;

    private void Awake()
    {
        if (backToLobbyButton.gameObject.activeSelf) backToLobbyButton.gameObject.SetActive(false);
    }

    public void SetNumberOfWinnersText(int numberOfWinners)
    {
        textWithNumberOfWinners.text = numberOfWinners.ToString();
    }

    public override void ShowPlayerWinWindow(List<GameObject> losers)
    {
        throw new System.NotImplementedException();
    }

    public override void ShowPlayerLoseWindow()
    {
        backToLobbyButton.gameObject.SetActive(true);
    }
}
