using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LeagueWindowLeagues
{
    [SerializeField] private string name;
    [SerializeField] internal Image league;

    internal TextMeshProUGUI leagueNameText;
    internal Image leagueIconImage;
    internal Image availableIcon;
    internal TextMeshProUGUI cupsValueText;

    internal int neededCupsValue;
}

public class LeagueWindowProgressVisualizer : MonoBehaviour
{
    [Header("Scale")]
    [SerializeField] private RectTransform leagueProgressScale;
    [SerializeField] private TextMeshProUGUI cupsText;

    [SerializeField] private List<LeagueWindowLeagues> leagues;

    private int currentCupsValue;

    private HorizontalLayoutGroup horizontalLayoutGroup;
    
    private float elementWidth;

    private bool notFirstActive;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        horizontalLayoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();
        elementWidth = leagues[0].league.GetComponent<RectTransform>().rect.width;

        leagueProgressScale.anchorMin = new Vector2(0f, 0.5f);
        leagueProgressScale.anchorMax = new Vector2(0f, 0.5f);
        leagueProgressScale.pivot = new Vector2(0f, 0.5f);
        
        //startScalePos = (elementWidth / 2f) + horizontalLayoutGroup.padding.left;
        //endScalePos = ((leagues.Count - 1) * elementWidth) + ((leagues.Count - 1) * horizontalLayoutGroup.spacing) +
            //horizontalLayoutGroup.padding.left + (elementWidth / 2f);

        //valueBetweenStartAndEndScalePos = endScalePos - startScalePos;

        //leagueProgressScale.sizeDelta = new Vector2(startScalePos, leagueProgressScale.sizeDelta.y);

        foreach (var _league in leagues)
        {
            _league.leagueNameText = _league.league.transform.Find("LeagueNameText").GetComponent<TextMeshProUGUI>();
            _league.leagueIconImage = _league.league.transform.Find("LeagueIconImage").GetComponent<Image>();
            _league.availableIcon = _league.league.transform.Find("AvailableIcon").GetComponent<Image>();
            _league.cupsValueText = _league.league.transform.Find("CupsValueText").GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        if (notFirstActive)
        {
            UpdateCupsValue(CurrencyManager.Instance.Cups);
        }
    }

    private void Start()
    {
        if (!notFirstActive)
        {
            GetLeagueNeededCupsValue();
            UpdateCupsValue(CurrencyManager.Instance.Cups);
            notFirstActive = true;
        }
    }

    private void UpdateScalePosition()
    {
        int currentLeagueLevel = LeagueManager.Instance.GetCurrentLeagueLevel();

        float maxTargetCupsValue;
        if (currentLeagueLevel != leagues.Count) maxTargetCupsValue = leagues[currentLeagueLevel].neededCupsValue;
        else maxTargetCupsValue = leagues[leagues.Count - 1].neededCupsValue;

        if (maxTargetCupsValue == 0) return;

        float startScalePos = ((currentLeagueLevel - 1) * elementWidth) + ((currentLeagueLevel - 1) * horizontalLayoutGroup.spacing) +
            horizontalLayoutGroup.padding.left + (elementWidth / 2f);

        float endScalePos = ((currentLeagueLevel) * elementWidth) + ((currentLeagueLevel) * horizontalLayoutGroup.spacing) +
            horizontalLayoutGroup.padding.left + (elementWidth / 2f);
        
        float valueBetweenStartAndEndScalePos = endScalePos - startScalePos;

        float newScalePos;

        if (currentLeagueLevel == 1)
        {
            float currentCupsInPercent = currentCupsValue / maxTargetCupsValue;
            newScalePos = (valueBetweenStartAndEndScalePos * currentCupsInPercent) + startScalePos;
        }
        else if(currentLeagueLevel != leagues.Count) // последнний
        {
            float previousMaxTargetCupsValue = leagues[currentLeagueLevel - 1].neededCupsValue;
            float currentCupsInPercent = (currentCupsValue - previousMaxTargetCupsValue) / (maxTargetCupsValue - previousMaxTargetCupsValue);
            newScalePos = (valueBetweenStartAndEndScalePos * currentCupsInPercent) + startScalePos;
        }
        else
        {
            newScalePos = startScalePos;
        }
        
        leagueProgressScale.sizeDelta = new Vector2(newScalePos, leagueProgressScale.sizeDelta.y);
    }

    private void GetLeagueNeededCupsValue()
    {
        for (int i = 0; i < leagues.Count; i++)
        {
            leagues[i].neededCupsValue = LeagueManager.Instance.GetNeededCupsForLeagueLevel(i);
            leagues[i].cupsValueText.text = $"{leagues[i].neededCupsValue}";
        }

        UpdateScalePosition();
    }

    private void UpdateCupsValue(int value)
    {
        currentCupsValue = value;
        cupsText.text = $"{currentCupsValue}";

        UpdateScalePosition();
        UpdateElementsVisual();
    }

    private void UpdateElementsVisual()
    {
        foreach (var league in leagues)
        {
            Image _leagueBack = league.league;
            Image _leagueIconImage = league.leagueIconImage;
            Image _availableIcon = league.availableIcon;
            TextMeshProUGUI _leagueNameText = league.leagueNameText;
            int _neededCupsValue = league.neededCupsValue;
            
            if(currentCupsValue >= _neededCupsValue)
            {
                _leagueBack.color = new Color(1f, 1f, 1f, 100f / 255f);
                _leagueIconImage.color = Color.white;
                _availableIcon.color = Color.black;

                Color textColor = _leagueNameText.color;
                textColor = new Color(textColor.r, textColor.g, textColor.b, 1f);
                _leagueNameText.color = textColor;
            }
            else
            {
                _leagueBack.color = new Color(1f, 1f, 1f, 0f);
                _leagueIconImage.color = new Color(1f, 1f, 1f, 30f / 255f);
                _availableIcon.color = Color.white;

                Color textColor = _leagueNameText.color;
                textColor = new Color(textColor.r, textColor.g, textColor.b, 30f / 255f);
                _leagueNameText.color = textColor;
            }
        }
    }
}   
