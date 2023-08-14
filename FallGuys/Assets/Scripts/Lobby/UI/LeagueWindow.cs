using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeagueWindow : MonoBehaviour
{
    [SerializeField] private LeagueWindowProgressVisualizer leagueWindowProgressVisualizer;

    [Header("UI")]
    [SerializeField] private Button backToLobbyButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button continueButton;

    public void ShowCupsProgressAnimation(int _previousCupValue)
    {

        backToLobbyButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);

        leagueWindowProgressVisualizer.ShowProgressAnimationAfterPlay(_previousCupValue);
    }
}
