using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyWindowsController : MonoBehaviour
{
    public delegate void ShowLeagueProgressAnimation(int previousCupValue);
    public static event ShowLeagueProgressAnimation ShowLeagueProgressAnimationEvent;

    [SerializeField] private string lobbySceneName;
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
        Debug.Log("WINDOWCONTROLLER AWAKE");
    }

    private void SceneWasChanged(Scene current, Scene next)
    {
        if (next.name != lobbySceneName) previousCupValue = CurrencyManager.Instance.Cups;

        if (showLeagueWindow && !IsFirstLobbyEnterFromLocation())
        {
            Canvas lobbyCanvas = FindObjectOfType<Canvas>();
            GameObject leagueWindow = lobbyCanvas.transform.Find("Lobby").transform.Find("LeagueWindow").gameObject;
            leagueWindow.SetActive(true);
            ShowLeagueProgressAnimationEvent?.Invoke(previousCupValue);
            showLeagueWindow = false;
        }

        if (next.name == lobbySceneName) MusicManager.Instance.PlayLobbyMusic();
        if(next.name.EndsWith("Arena")) MusicManager.Instance.PlayArenaMusic();
    }

    public void ShowLeagueWindowOnLobby()// вызывают кнопки win lose окон
    {   // если это не первый вход в лобби из локации
        if(!IsFirstLobbyEnterFromLocation()) showLeagueWindow = true;
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
        Debug.Log("WINDOWCONTROLLER ONDISABLE");
        SceneManager.activeSceneChanged -= SceneWasChanged;
    }
}
