using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    //[SerializeField] private Canvas gameCanvas;
    //[Space]
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI fragText;

    [Header("Additional UI To Hide When Observe")]
    [SerializeField] private List<GameObject> additionalUIList;

    public override void ShowObserverUI()
    {
        base.ShowObserverUI();

        foreach (var elem in additionalUIList)
        {
            elem.SetActive(false);
        }
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
