using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RingsProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    [SerializeField] private GameObject additioanlUpPanel;
    [Header("Player Texts")]
    [SerializeField] private TextMeshProUGUI playerPlaceTextBig;
    [SerializeField] private TextMeshProUGUI playerPlaceTextSmall;
    [SerializeField] private TextMeshProUGUI playerPointsValueText;
    [Header("Enemy Texts")]
    [SerializeField] private TextMeshProUGUI enemyPlaceTextSmall;
    [SerializeField] private TextMeshProUGUI enemyPointsValueText;

    protected override void HideElementsBeforeStart()
    {
        base.HideElementsBeforeStart();

        if (additioanlUpPanel.activeSelf) additioanlUpPanel.SetActive(false);
    }
    protected override void TimerBeforeStartFinished()
    {
        additioanlUpPanel.SetActive(true);

        base.TimerBeforeStartFinished();
    }

    public void UpdatePointsText(int currentPlayerPlace, int currentPlayerPoints, int nearestEnemyPlace, int nearestEnemyPoints)
    {
        playerPlaceTextBig.text = currentPlayerPlace.ToString();
        playerPlaceTextSmall.text = currentPlayerPlace.ToString();
        playerPointsValueText.text = currentPlayerPoints.ToString();

        enemyPlaceTextSmall.text = nearestEnemyPlace.ToString();
        enemyPointsValueText.text = nearestEnemyPoints.ToString();
    }
}
