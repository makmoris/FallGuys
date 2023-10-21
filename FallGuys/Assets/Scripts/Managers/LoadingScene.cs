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

    private NetworkChecker networkChecker;
    private AnalyticsManager analyticsManager;
    private AdsInitializer adsInitializer;

    [Space]
    [SerializeField] private bool internetConnectionIsActive;
    [SerializeField] private bool analyticsInitializeCompleted;
    [SerializeField] private bool adsInitializeCompleted;

    private AsyncOperation asyncOperation;

    public void Initialize(NetworkChecker networkChecker, AnalyticsManager analyticsManager, AdsInitializer adsInitializer)
    {
        this.networkChecker = networkChecker;
        this.analyticsManager = analyticsManager;
        this.adsInitializer = adsInitializer;
    }

    private void OnEnable()
    {
        networkChecker.InternetConnectionAppearedEvent += SetInternetConnectionIsActive;
        networkChecker.InternetConnectionLostEvent += SetInternetConnectionIsInactive;

        AnalyticsManager.InitializeCompletedEvent += AnalyticsInitializeCompleted;

        adsInitializer.ADSInitializationCompleteEvent += AdsInitializeCompleted;
    }

    private void Start()
    {
        CheckIsFirstGameLaunch();

        if (!tutorialWindowWasShown) GetParameters();// analytics ������ ���, ���� ��� ������ � �����
        //if (!tutorialWindowWasShown) SendUserAnalyticEvent();// analytics

        if (isFirstGameLaunch)
        {
            gameManager.StartGameStageFromTutorialWindow();

            string sceneName = gameModeScenesAfterTutorial.GetRandomScene();

            #region Analytics
            AnalyticsManager.Instance.UpdateGenreID(gameModeScenesAfterTutorial.GameMode.ToString());
            AnalyticsManager.Instance.UpdateMapID(sceneName);
            #endregion

            StartCoroutine(LoadScene(sceneName));

            #region Analytics
            AnalyticsManager.Instance.LevelStart();
            #endregion
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

    private void AdsInitializeCompleted()
    {
        adsInitializeCompleted = true;
    }

    public void GoToLevel()// ���������� ����� gameTutorialWindow
    {
        asyncOperation.allowSceneActivation = true;
    }

    private void CheckIsFirstGameLaunch()
    {
        int value = PlayerPrefs.GetInt(key, 0);
        if (value == 0) isFirstGameLaunch = true;
        else isFirstGameLaunch = false;

        PlayerPrefs.SetInt(key, 1);// ����� ����� �������, ����� �������� ���������� �� ����� ����� ������������ ������ � ������� 
                                   // ���������� �� ����� ��������� � ����������� �� ����. ���� ������ ������, �� ������ ������ ���������� ���
                                   // ������� �������

        int value2 = PlayerPrefs.GetInt(gameTutorialKey, 0);
        if (value2 == 0) tutorialWindowWasShown = false;
        else tutorialWindowWasShown = true;
        PlayerPrefs.SetInt(gameTutorialKey, 1);
    }

    IEnumerator LoadScene(string mapName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(mapName);

        //if(!tutorialWindowWasShown) asyncOperation.allowSceneActivation = false;// ��� ������ ���� ������
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100f);

            if (asyncOperation.progress >= 0.9f)
            {
                //if (!tutorialWindowWasShown) gameTutorialWindow.SetActive(true);// ��� ������ ���� ������
                if (!tutorialWindowWasShown && !analyticsEventWasSended && internetConnectionIsActive && analyticsInitializeCompleted && adsInitializeCompleted)
                {
                    analyticsEventWasSended = true;
                    SendUserAnalyticEvent();// analytics
                }
                else if (tutorialWindowWasShown && internetConnectionIsActive && analyticsInitializeCompleted && adsInitializeCompleted)
                {
                    Debug.Log("[TEST] Tutorial has been shown, Internet is active, Analytics has been initialized. Can go to the lobby");
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    // for analytics
    private int cups_amount;
    private int gold;
    private string control_type;

    private void GetParameters()
    {
        cups_amount = CurrencyManager.Instance.Cups;

        gold = CurrencyManager.Instance.Gold;

        control_type = GetCurrentLayout();
    }

    private void SendUserAnalyticEvent()
    {
        //StartCoroutine(WaitAndSendEvent());

        AnalyticsManager.Instance.User(cups_amount, gold, control_type);

        if (!tutorialWindowWasShown) gameTutorialWindow.SetActive(true);// ��� ������ ���� ������
    }

    IEnumerator WaitAndSendEvent()
    {
        yield return new WaitForSeconds(1f);
        AnalyticsManager.Instance.User(cups_amount, gold, control_type);

        if (!tutorialWindowWasShown) gameTutorialWindow.SetActive(true);// ��� ������ ���� ������
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
        networkChecker.InternetConnectionAppearedEvent -= SetInternetConnectionIsActive;
        networkChecker.InternetConnectionLostEvent -= SetInternetConnectionIsInactive;

        AnalyticsManager.InitializeCompletedEvent -= AnalyticsInitializeCompleted;

        adsInitializer.ADSInitializationCompleteEvent -= AdsInitializeCompleted;
    }
}
