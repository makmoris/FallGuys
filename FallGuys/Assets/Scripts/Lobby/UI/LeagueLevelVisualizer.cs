using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeagueLevelVisualizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> leagueIcons;

    private int leaguLevel;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void UpdateLeagueLevelValue(int value)
    {
        leaguLevel = value;

        if (leaguLevel <= leagueIcons.Count) _image.sprite = leagueIcons[leaguLevel - 1];
        else Debug.LogError("League level higher than shield icons");
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
