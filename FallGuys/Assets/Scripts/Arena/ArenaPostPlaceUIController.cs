using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaPostPlaceUIController : PostLevelResultVisualization
{
    [Header("Win window")]
    [SerializeField] private GameObject winWindow;
    [SerializeField] private TextMeshProUGUI fragWWText;
    [SerializeField] private TextMeshProUGUI goldWWText;
    [SerializeField] private TextMeshProUGUI cupsWWText;

    [Header("Lose window")]
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private TextMeshProUGUI fragLWText;
    [SerializeField] private TextMeshProUGUI goldLWText;
    [SerializeField] private TextMeshProUGUI cupsLWText;

    public void ShowPlayerWinWindow(int numberOfFrags, int amountOfGoldReward, int amountOfCupReward)
    {
        if (!winWindow.activeSelf) winWindow.SetActive(true);
        if (loseWindow.activeSelf) loseWindow.SetActive(false);

        fragWWText.text = $"{numberOfFrags}";
        goldWWText.text = $"{amountOfGoldReward}";
        cupsWWText.text = $"{amountOfCupReward}";

        gameManager.StartGameStage();
    }

    public void ShowPlayerLoseWindow(int numberOfFrags, int amountOfGoldReward, int amountOfCupReward)
    {
        if (!loseWindow.activeSelf) loseWindow.SetActive(true);
        if (winWindow.activeSelf) winWindow.SetActive(false);

        fragLWText.text = $"{numberOfFrags}";
        goldLWText.text = $"{amountOfGoldReward}";
        cupsLWText.text = $"{amountOfCupReward}";

        gameManager.StartGameStage();
    }

    public override void ShowPlayerWinWindow(List<string> losersNames, float timeBeforeLoadNextLevel)
    {
        throw new System.NotImplementedException();
    }

    public override void ShowPlayerLoseWindow()
    {
        throw new System.NotImplementedException();
    }
}
