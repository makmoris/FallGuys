using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PostRaceCanvasUIController : MonoBehaviour
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

    public void ShowPlayerWinWindow()
    {

    }

    public void ShowPlayerLoseWindow()
    {
        backToLobbyButton.gameObject.SetActive(true);
    }
}
