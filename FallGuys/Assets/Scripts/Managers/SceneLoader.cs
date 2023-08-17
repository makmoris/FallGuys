using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Canvas mapSelectionCanvas;
    private MapSelector mapSelector;
    [Space]
    [SerializeField] private SceneField lobbyScene;

    public event System.Action lobbyOpenEvent;

    private static SceneLoader Instance { get; set; }
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

        if (mapSelectionCanvas.gameObject.activeSelf) mapSelectionCanvas.gameObject.SetActive(false);
        mapSelector = mapSelectionCanvas.transform.GetComponentInChildren<MapSelector>(true);
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneWasChanged;
    }

    public void LoadNextLevelSceneWithAnimation(SceneField nextScene)
    {
        mapSelectionCanvas.gameObject.SetActive(true);
        mapSelector.StartLoading(nextScene, this);
    }

    public void LoadAnimationFinished(AsyncOperation asyncOperation)
    {
        asyncOperation.allowSceneActivation = true;
        Debug.Log("Load Anim Finished");
    }

    public void LoadLobbyScene()
    {
        Debug.Log("ToLobby");
        SceneManager.LoadScene(lobbyScene);
    }


    private void SceneWasChanged(Scene current, Scene next)
    {
        if (next.name != lobbyScene.SceneName)
        {
            if (mapSelectionCanvas.gameObject.activeSelf) mapSelectionCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("LOBBY SCENE");

            lobbyOpenEvent?.Invoke();
        }

        if (next.name == lobbyScene.SceneName) MusicManager.Instance.PlayLobbyMusic();
        if (next.name.EndsWith("Arena")) MusicManager.Instance.PlayArenaMusic();
        if (next.name.EndsWith("Race")) MusicManager.Instance.PlayRaceMusic();
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneWasChanged;
    }
}
