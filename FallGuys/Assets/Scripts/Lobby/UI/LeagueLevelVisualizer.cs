using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeagueLevelVisualizer : MonoBehaviour
{
    private TextMeshProUGUI leagueText;

    private void Awake()
    {
        leagueText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateLeagueLevelValue(int value)
    {
        leagueText.text = value.ToString();
    }


    private void OnEnable()
    {
        LeagueManager.LeagueLevelUpdateEvent += UpdateLeagueLevelValue;
    }

    private void OnDisable()
    {
        LeagueManager.LeagueLevelUpdateEvent -= UpdateLeagueLevelValue;
    }
}
