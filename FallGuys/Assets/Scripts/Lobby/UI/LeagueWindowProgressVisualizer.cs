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
    [SerializeField] private RectTransform leagueContent;
    [SerializeField] private RectTransform leagueProgressScale;
    [SerializeField] private TextMeshProUGUI cupsText;
    [SerializeField] private GameObject cupsPlace;
    [Space]
    [SerializeField] private float scaleFillingAnimationSpeed = 2f;

    [Header("Leagues")]
    [SerializeField] private List<LeagueWindowLeagues> leagues;

    [Space]
    [SerializeField] private NewLeagueWindow newLeagueWindow;

    private int currentCupsValue;
    private int previousCupsValue;

    private HorizontalLayoutGroup horizontalLayoutGroup;
    
    private float elementWidth;

    float valueBetweenStartAndEndScalePos;

    private bool notFirstActive;
    private bool isShowingProgressAnimationAfterPlay;// показываем анимацию шкалы прогресса после игры на уровне

    private string leagueIconsKey = "LeagueWindowEnabledLeagueIcons";

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
        
        // для позиции скролла (Content)
        float startScalePos = (elementWidth / 2f) + horizontalLayoutGroup.padding.left;
        float endScalePos = ((leagues.Count - 1) * elementWidth) + ((leagues.Count - 1) * horizontalLayoutGroup.spacing) +
            horizontalLayoutGroup.padding.left + (elementWidth / 2f);
        
        valueBetweenStartAndEndScalePos = endScalePos - startScalePos;

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
        //LobbyWindowsController.ShowLeagueProgressAnimationEvent += ShowProgressAnimationAfterPlay;

        if (notFirstActive)
        {
            UpdateCupsValue(CurrencyManager.Instance.Cups);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            float x = leagueContent.sizeDelta.x;
            Debug.Log($"Это Dwidth = {x}");
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

    public void ShowProgressAnimationAfterPlay(int _previousCupValue)
    {
        previousCupsValue = _previousCupValue;
        isShowingProgressAnimationAfterPlay = true;
    }

    private void UpdateScalePosition()
    {
        int currentLeagueLevel = LeagueManager.Instance.GetCurrentLeagueLevel();

        float maxTargetCupsValue;
        if (currentLeagueLevel != leagues.Count) maxTargetCupsValue = leagues[currentLeagueLevel].neededCupsValue;
        else maxTargetCupsValue = leagues[leagues.Count - 1].neededCupsValue;

        float newScalePos;

        if (maxTargetCupsValue == 0) return;

        // шкала прогресса
        if(currentCupsValue == previousCupsValue || !isShowingProgressAnimationAfterPlay)// без анимации
        {
            cupsText.text = $"{currentCupsValue}";
            newScalePos = GetNewScalePosX(currentLeagueLevel, currentCupsValue, maxTargetCupsValue);
            isShowingProgressAnimationAfterPlay = false;
            cupsPlace.SetActive(true);
        }
        else
        {
            cupsText.text = $"{previousCupsValue}";
            newScalePos = GetNewScalePosX(currentLeagueLevel, previousCupsValue, maxTargetCupsValue);
            leagueProgressScale.sizeDelta = new Vector2(newScalePos, leagueProgressScale.sizeDelta.y);

            newScalePos = GetNewScalePosX(currentLeagueLevel, currentCupsValue, maxTargetCupsValue);
        }

        // ставим сам скролл, чтобы не выходил за пределы экрана
        if (currentLeagueLevel == 1)
        {
            leagueContent.anchoredPosition = new Vector2(0, leagueContent.anchoredPosition.y);
        }
        else
        {
            float percentForScale = newScalePos / valueBetweenStartAndEndScalePos;
            leagueContent.anchoredPosition = new Vector2((leagueContent.sizeDelta.x * percentForScale) * -1f, leagueContent.anchoredPosition.y);
        }

        //Debug.Log($"Стартовая позиция шкалы = {leagueProgressScale.sizeDelta.x}; Финишная позиция шкалы = {newScalePos}; {maxTargetCupsValue}");

        if (isShowingProgressAnimationAfterPlay)
        {
            cupsPlace.SetActive(false);
            StartCoroutine(FillingAnimationWithoutText(newScalePos));

            //StartCoroutine(FillingAnimation(newScalePos));
            isShowingProgressAnimationAfterPlay = false;
        }
        else leagueProgressScale.sizeDelta = new Vector2(newScalePos, leagueProgressScale.sizeDelta.y);
    }

    private float GetNewScalePosX(int currentLeagueLevel, int cupsValue, float maxTargetCupsValue)
    {
        float startScalePos = ((currentLeagueLevel - 1) * elementWidth) + ((currentLeagueLevel - 1) * horizontalLayoutGroup.spacing) +
            horizontalLayoutGroup.padding.left + (elementWidth / 2f);

        float endScalePos = ((currentLeagueLevel) * elementWidth) + ((currentLeagueLevel) * horizontalLayoutGroup.spacing) +
            horizontalLayoutGroup.padding.left + (elementWidth / 2f);

        float valueBetweenStartAndEndScalePos = endScalePos - startScalePos;

        float newScalePos;

        if (currentLeagueLevel == 1)
        {
            float currentCupsInPercent = cupsValue / maxTargetCupsValue;
            newScalePos = (valueBetweenStartAndEndScalePos * currentCupsInPercent) + startScalePos;
        }
        else if (currentLeagueLevel != leagues.Count) // последнний
        {
            float previousMaxTargetCupsValue = leagues[currentLeagueLevel - 1].neededCupsValue;
            float currentCupsInPercent = (cupsValue - previousMaxTargetCupsValue) / (maxTargetCupsValue - previousMaxTargetCupsValue);
            newScalePos = (valueBetweenStartAndEndScalePos * currentCupsInPercent) + startScalePos;
        }
        else
        {
            newScalePos = startScalePos;
            isShowingProgressAnimationAfterPlay = false;
        }

        return newScalePos;
    }

    private void GetLeagueNeededCupsValue()
    {
        for (int i = 0; i < leagues.Count; i++)
        {
            leagues[i].neededCupsValue = LeagueManager.Instance.GetNeededCupsForLeagueLevel(i);
            leagues[i].cupsValueText.text = $"{leagues[i].neededCupsValue}";
        }

        //UpdateScalePosition();
    }


    private void UpdateCupsValue(int value)
    {
        currentCupsValue = value;

        if (!isShowingProgressAnimationAfterPlay) UpdateElementsVisual(currentCupsValue);
        else UpdateElementsVisual(previousCupsValue);

        UpdateScalePosition();
    }

    private void UpdateElementsVisual(int _currentCupsValue)
    {
        int enabledLeagueIcons = PlayerPrefs.GetInt(leagueIconsKey, 1);
        int enabledLeagueIconsCounter = 0;

        foreach (var league in leagues)
        {
            Image _leagueBack = league.league;
            Image _leagueIconImage = league.leagueIconImage;
            Image _availableIcon = league.availableIcon;
            TextMeshProUGUI _leagueNameText = league.leagueNameText;
            int _neededCupsValue = league.neededCupsValue;
            
            if(_currentCupsValue >= _neededCupsValue)
            {
                _leagueBack.color = new Color(1f, 1f, 1f, 100f / 255f);
                _leagueIconImage.color = Color.white;
                _availableIcon.color = Color.black;

                Color textColor = _leagueNameText.color;
                textColor = new Color(textColor.r, textColor.g, textColor.b, 1f);
                _leagueNameText.color = textColor;

                enabledLeagueIconsCounter++;
                if (enabledLeagueIconsCounter > enabledLeagueIcons && enabledLeagueIconsCounter != 1)// показываем окно поздравления с новой лигой
                {
                    Debug.Log($"Лига {league.leagueNameText.text} открыта впервые!");
                    newLeagueWindow.gameObject.SetActive(true);
                    newLeagueWindow.ShowLeagueIcon(enabledLeagueIconsCounter - 1);

                    PlayerPrefs.SetInt(leagueIconsKey, enabledLeagueIconsCounter);
                }
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

    IEnumerator FillingAnimation(float targetXPos)
    {
        yield return new WaitForSeconds(0.5f);

        //1) находим дистанцию, которую нужно пройти
        float distance = Mathf.Abs(targetXPos - leagueProgressScale.sizeDelta.x);
        //2) находим время, за которое будет пройдена эта дистанция с учетом, что ОДИН шаг проходим за 0.02
        float fixedFrameTime = Time.fixedDeltaTime;
        float time = distance * fixedFrameTime;
        //3) находим количество кубков, на которое изменится текущее значение кубков
        float cupsValue = Mathf.Abs(previousCupsValue - currentCupsValue);
        //4) находим скорость, с которой нужно изменять текст, чтобы он закончил изменение в момент остановки шкалы
        float textSpeed = cupsValue / time;
        float textStep = textSpeed * fixedFrameTime;
        //5) задаем шаг шкалы
        float scaleStep = scaleFillingAnimationSpeed;
        //6) домножаем значение текстового шага на значение шага шкалы. Получаем шаг, с которым нужно "двигать" текст
        textStep *= scaleStep;

        Vector2 target = new Vector2(targetXPos, leagueProgressScale.sizeDelta.y);

        float _cupsValue = previousCupsValue;

        while (Vector2.Distance(leagueProgressScale.sizeDelta, target) > 0.001f)
        {
            leagueProgressScale.sizeDelta = Vector2.MoveTowards(leagueProgressScale.sizeDelta, target, scaleStep);

            _cupsValue = Mathf.MoveTowards(_cupsValue, currentCupsValue, textStep);
            cupsText.text = $"{(int)_cupsValue}";

            //yield return null;
            yield return new WaitForFixedUpdate();
        }
        cupsText.text = $"{currentCupsValue}";// на всякий, если не успеет

        //cupsPlace.SetActive(true); - если без анимации подсчета кубков
    }

    IEnumerator FillingAnimationWithoutText(float targetXPos)
    {
        // старт музыка заполнения 
        MusicManager.Instance.PlayFillingLeagueScaleMusic();

        yield return new WaitForSeconds(0.5f);

        float scaleStep = scaleFillingAnimationSpeed;

        Vector2 target = new Vector2(targetXPos, leagueProgressScale.sizeDelta.y);

        while (Vector2.Distance(leagueProgressScale.sizeDelta, target) > 0.001f)
        {
            leagueProgressScale.sizeDelta = Vector2.MoveTowards(leagueProgressScale.sizeDelta, target, scaleStep);

            //yield return null;
            yield return new WaitForFixedUpdate();
        }
        cupsText.text = $"{currentCupsValue}";// на всякий, если не успеет

        cupsPlace.SetActive(true);

        UpdateElementsVisual(currentCupsValue);

        // стоп музыка заполнения 
        MusicManager.Instance.PlayLobbyMusic();
    }

    private void OnDisable()
    {
        //LobbyWindowsController.ShowLeagueProgressAnimationEvent -= ShowProgressAnimationAfterPlay;
    }
}   
