using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RingsProgressUIController : LevelProgressUIController
{
    [Header("-----")]
    [SerializeField] private GameObject additioanlUpPanel;
    [SerializeField] private TextMeshProUGUI numberOfWinnersText;
    [Header("Colors")]
    [SerializeField] private Color winColor;
    [SerializeField] private Color failColor;
    [Header("Player Texts")]
    [SerializeField] private GameObject playerInfoGO;
    [SerializeField] private TextMeshProUGUI playerPlaceTextBig;
    [SerializeField] private TextMeshProUGUI playerPlaceTextSmall;
    [SerializeField] private TextMeshProUGUI playerPointsValueText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Image playerCirclePoints;
    [Header("Enemy Texts")]
    [SerializeField] private TextMeshProUGUI enemyPlaceTextSmall;
    [SerializeField] private TextMeshProUGUI enemyPointsValueText;
    [SerializeField] private TextMeshProUGUI enemyNameText;

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

    public void SetNumberOfWinners(int winners)
    {
        numberOfWinnersText.text = $"{winners} pass on";
    }

    public void UpdatePointsText(int currentPlayerPlace, int currentPlayerPoints, int nearestEnemyPlace, int nearestEnemyPoints,
        string playerName, string nearestEnemyName, bool isPlayerPass)
    {
        playerPlaceTextBig.text = OrdinalNumbers.ToOrdinal(currentPlayerPlace);
        playerPlaceTextSmall.text = OrdinalNumbers.ToOrdinal(currentPlayerPlace);
        playerPointsValueText.text = currentPlayerPoints.ToString();
        playerNameText.text = playerName;

        enemyPlaceTextSmall.text = OrdinalNumbers.ToOrdinal(nearestEnemyPlace);
        enemyPointsValueText.text = nearestEnemyPoints.ToString();
        enemyNameText.text = nearestEnemyName;

        if (isPlayerPass)
        {
            playerPlaceTextBig.color = winColor;
            playerPlaceTextSmall.color = winColor;
            playerPointsValueText.color = winColor;
            playerNameText.color = winColor;
            playerCirclePoints.color = winColor;
        }
        else
        {
            playerPlaceTextBig.color = failColor;
            playerPlaceTextSmall.color = failColor;
            playerPointsValueText.color = failColor;
            playerNameText.color = failColor;
            playerCirclePoints.color = failColor;
        }

        if (currentPlayerPlace == 1) playerInfoGO.transform.SetAsFirstSibling();
        else playerInfoGO.transform.SetAsLastSibling();
    }
}
