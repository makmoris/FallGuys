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

    private bool showLeagueWindow;

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
    }

    private void SceneWasChanged(Scene current, Scene next)
    {
        
        if(next.name != lobbySceneName) previousCupValue = CurrencyManager.Instance.Cups;

        if (showLeagueWindow)
        {
            Canvas lobbyCanvas = FindObjectOfType<Canvas>();
            GameObject leagueWindow = lobbyCanvas.transform.Find("Lobby").transform.Find("LeagueWindow").gameObject;
            leagueWindow.SetActive(true);
            ShowLeagueProgressAnimationEvent?.Invoke(previousCupValue);
            showLeagueWindow = false;
        }
    }

    public void ShowLeagueWindowOnLobby()// вызывают кнопки win lose окон
    {
        showLeagueWindow = true;
    }
}
