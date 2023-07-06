using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyWindowsController : MonoBehaviour
{
    public delegate void ShowLeagueProgressAnimation(int previousCupValue);
    public static event ShowLeagueProgressAnimation ShowLeagueProgressAnimationEvent;

    [SerializeField] private SceneField lobbyScene;
    private string currentScene;
    private string previousScene;

    private int previousCupValue;

    private string key = "IsFirstLobbyEnterFromLocation";

    private bool showLeagueWindow = false;

    public static LobbyWindowsController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        SceneManager.activeSceneChanged += SceneWasChanged;
        //Debug.Log("WINDOWCONTROLLER AWAKE");
    }

    private void SceneWasChanged(Scene s, Scene next)
    {
        if (next.name != lobbyScene.SceneName) previousCupValue = CurrencyManager.Instance.Cups;

        if (showLeagueWindow && !IsFirstLobbyEnterFromLocation())
        {
            showLeagueWindow = false;

            Canvas lobbyCanvas = FindObjectOfType<Canvas>();

            GameObject leagueWindow = lobbyCanvas.transform.Find("Lobby").transform.Find("LeagueWindow").gameObject;

            GameObject backToLobbyButton = leagueWindow.transform.Find("BackToLobbyButton").gameObject;
            GameObject homeButton = leagueWindow.transform.Find("HomeButton").gameObject;
            GameObject continueButton = leagueWindow.transform.Find("ContinueButton").gameObject;

            Debug.Log($"Лига окно = {leagueWindow.activeSelf}");
            leagueWindow.SetActive(true);
            Debug.Log($"Лига окно = {leagueWindow.activeSelf}");

            backToLobbyButton.SetActive(false);
            homeButton.SetActive(false);
            continueButton.SetActive(true);

            ShowLeagueProgressAnimationEvent?.Invoke(previousCupValue);
        }

        if (next.name == lobbyScene.SceneName) MusicManager.Instance.PlayLobbyMusic();
        if(next.name.EndsWith("Arena")) MusicManager.Instance.PlayArenaMusic();
        if (next.name.EndsWith("Race")) MusicManager.Instance.PlayArenaMusic();
    }

    public void ShowLeagueWindowOnLobby()// вызывают кнопки win lose окон
    {   // если это не первый вход в лобби из локации
        if (!IsFirstLobbyEnterFromLocation())
        {
            showLeagueWindow = true;
            Debug.Log("SHOW LEAGUE WINDOW TRUE");
        }
    }

    private bool IsFirstLobbyEnterFromLocation()
    {
        bool returnValue;
        int _value = PlayerPrefs.GetInt(key, 0);
        if (_value == 0) returnValue = true;
        else returnValue = false;

        PlayerPrefs.SetInt(key, 1);

        return returnValue;
    }

    private void OnDisable()
    {
        //Debug.Log("WINDOWCONTROLLER ONDISABLE");
        SceneManager.activeSceneChanged -= SceneWasChanged;
    }
}
