using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PostRaceCanvasUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textWithNumberOfWinners;

    [Header("Win Window")]
    [SerializeField] private MapSelector mapSelectionWindow;

    [Header("Lose Window")]
    [SerializeField] private Button backToLobbyButton;

    private void Awake()
    {
        if (mapSelectionWindow.gameObject.activeSelf) mapSelectionWindow.gameObject.SetActive(false);
        if (backToLobbyButton.gameObject.activeSelf) backToLobbyButton.gameObject.SetActive(false);
    }

    public void SetNumberOfWinnersText(int numberOfWinners)
    {
        textWithNumberOfWinners.text = numberOfWinners.ToString();
    }

    public void ShowPlayerWinWindow()
    {
        StartCoroutine(WaitAndStartMapSelection());
    }

    public void ShowPlayerLoseWindow()
    {
        backToLobbyButton.gameObject.SetActive(true);
    }

    IEnumerator WaitAndStartMapSelection()
    {
        yield return new WaitForSeconds(5f);

        mapSelectionWindow.gameObject.SetActive(true);
        mapSelectionWindow.StartPlayGame();
    }
}
