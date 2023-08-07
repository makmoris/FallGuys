using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    [SerializeField] private Canvas gameCanvas;
    [Space]
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI fragText;

    public void HideGameCanvas()
    {
        gameCanvas.gameObject.SetActive(false);
    }

    public void UpdateLeftText(int numberOfPlayers)
    {
        leftText.text = $"LEFT: {numberOfPlayers}";
    }

    public void UpdateFragText(int numberOfFrags)
    {
        fragText.text = $"{numberOfFrags}";
    }
}
