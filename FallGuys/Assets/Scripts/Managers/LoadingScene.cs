using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Space]
    [SerializeField] private Image loadingImage;
    [SerializeField] private TextMeshProUGUI progressText;

    [Header("Scene Names")]
    [SerializeField] private SceneField lobbyScene;
    [Space]
    [SerializeField] private GameModeScenes gameModeScenesAfterTutorial;

    private string key = "IsFirstGameLaunch"; 
    private bool isFirstGameLaunch;

    [Space]
    [SerializeField] private GameObject gameTutorialWindow;
    private string gameTutorialKey = "TutorialWindowWasShown";
    private bool tutorialWindowWasShown;

    private bool analyticsEventWasSended;

    [Space]
    [SerializeField] private bool internetConnectionIsActive;
    [SerializeField] private bool analyticsInitializeCompleted;

    private AsyncOperation asyncOperation;

    private void OnEnable()
    {
        NetworkChecker.InternetConnectionAppearedEvent += SetInternetConnectionIsActive;
        NetworkChecker.InternetConnectionLostEvent += SetInternetConnectionIsInactive;

        AnalyticsManager.InitializeCompletedEvent += AnalyticsInitializeCompleted;
    }

    private void Start()
    {
        CheckIsFirstGameLaunch();

        if (!tutorialWindowWasShown) GetParameters();// analytics первый раз, если без захода в лобби
        //if (!tutorialWindowWasShown) SendUserAnalyticEvent();// analytics

        if (isFirstGameLaunch)
        {
            gameManager.StartGameStageFromTutorialWindow();

            string sceneName = gameModeScenesAfterTutorial.GetRandomScene();

            StartCoroutine(LoadScene(sceneName));
        }
        else
        {
            StartCoroutine(LoadScene(lobbyScene.SceneName));
        }
    }

    private void AnalyticsInitializeCompleted()
    {
        analyticsInitializeCompleted = true;
    }

    private void SetInternetConnectionIsActive()
    {
        internetConnectionIsActive = true;

        if(tutorialWindowWasShown && analyticsInitializeCompleted) asyncOperation.allowSceneActivation = true;
    }
    private void SetInternetConnectionIsInactive()
    {
        internetConnectionIsActive = false;

        asyncOperation.allowSceneActivation = false;
    }

    public void GoToLevel()// вызывается окном gameTutorialWindow
    {
        asyncOperation.allowSceneActivation = true;
    }

    private void CheckIsFirstGameLaunch()
    {
        int value = PlayerPrefs.GetInt(key, 0);
        if (value == 0) isFirstGameLaunch = true;
        else isFirstGameLaunch = false;

        PlayerPrefs.SetInt(key, 1);// можно потом сделать, чтобы значение изменялось на самой сцене загруженного уровня в скрипте 
                                   // отвечающим за выбор подуровня в зависимости от лиги. Если первый запуск, то грузим нужный подуровень для
                                   // первого запуска

        int value2 = PlayerPrefs.GetInt(gameTutorialKey, 0);
        if (value2 == 0) tutorialWindowWasShown = false;
        else tutorialWindowWasShown = true;
        PlayerPrefs.SetInt(gameTutorialKey, 1);
    }

    IEnumerator LoadScene(string mapName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(mapName);

        //if(!tutorialWindowWasShown) asyncOperation.allowSceneActivation = false;// для показа окна тутора
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100f);

            if (asyncOperation.progress >= 0.9f)
            {
                //if (!tutorialWindowWasShown) gameTutorialWindow.SetActive(true);// для показа окна тутора
                if (!tutorialWindowWasShown && !analyticsEventWasSended && internetConnectionIsActive && analyticsInitializeCompleted)
                {
                    analyticsEventWasSended = true;
                    SendUserAnalyticEvent();// analytics
                }
                else if (tutorialWindowWasShown && internetConnectionIsActive && analyticsInitializeCompleted)
                {
                    Debug.Log("[TEST] Tutorial has been shown, Internet is active, Analytics has been initialized. Can go to the lobby");
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    // for analytics
    private int battles_amount;
    private float win_rate;
    private int cups_amount;
    private int league;
    private int gold;
    private string car_id;
    private string gun_id;
    private string control_type;

    private void GetParameters()
    {
        battles_amount = BattleStatisticsManager.Instance.BattlesAmount;

        int playerFirstPlaceValue = BattleStatisticsManager.Instance.NumberOfFirstPlaces;

        if (battles_amount != 0 && playerFirstPlaceValue != 0)
        {
            float firstPlacesPercent = (playerFirstPlaceValue / (float)battles_amount) * 100f;
            win_rate = firstPlacesPercent;
        }
        else win_rate = 0f;


        cups_amount = CurrencyManager.Instance.Cups;

        league = LeagueManager.Instance.GetCurrentLeagueLevel();

        gold = CurrencyManager.Instance.Gold;

        car_id = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;
        gun_id = CharacterManager.Instance.GetPlayerWeapon().GetComponent<WeaponId>().WeaponID;

        control_type = GetCurrentLayout();
    }

    private void SendUserAnalyticEvent()
    {
        //StartCoroutine(WaitAndSendEvent());

        AnalyticsManager.Instance.User(battles_amount, win_rate, cups_amount, league, gold, car_id, gun_id, control_type);

        if (!tutorialWindowWasShown) gameTutorialWindow.SetActive(true);// для показа окна тутора
    }

    IEnumerator WaitAndSendEvent()
    {
        yield return new WaitForSeconds(1f);
        AnalyticsManager.Instance.User(battles_amount, win_rate, cups_amount, league, gold, car_id, gun_id, control_type);

        if (!tutorialWindowWasShown) gameTutorialWindow.SetActive(true);// для показа окна тутора
    }

    private string GetCurrentLayout()
    {
        string key = "Layout";
        int layoutIndex = PlayerPrefs.GetInt(key, 0);

        string layoutName = "";

        switch (layoutIndex)
        {
            case 0:// arrows
                layoutName = "Arrows";
                break;

            case 1:// leftRught
                layoutName = "LeftRight_Joystick";
                break;

            case 2:// allJoystick
                layoutName = "Full_Joystick";
                break;
        }

        return layoutName;
    }

    private void OnDisable()
    {
        NetworkChecker.InternetConnectionAppearedEvent -= SetInternetConnectionIsActive;
        NetworkChecker.InternetConnectionLostEvent -= SetInternetConnectionIsInactive;

        AnalyticsManager.InitializeCompletedEvent -= AnalyticsInitializeCompleted;
    }
}
