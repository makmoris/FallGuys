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

    private AsyncOperation asyncOperationNextLevelScene;
    private AsyncOperation asyncOperationToLobby;

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
    }

    //public void PrepareNextLevelScene(SceneField scene)// вызывается во время игры, чтобы подготовить следующую сцену
    //{
    //    asyncOperationNextLevelScene = SceneManager.LoadSceneAsync(scene);
    //    asyncOperationNextLevelScene.allowSceneActivation = false;
    //}

    public void PrepareLobbyScene()
    {
        asyncOperationToLobby = SceneManager.LoadSceneAsync(lobbyScene);
        asyncOperationToLobby.allowSceneActivation = false;
    }

    public void LoadLobbyScene()
    {
        Debug.Log("ToLobby");
        asyncOperationToLobby.allowSceneActivation = true; 
        //SceneManager.LoadScene(lobbyScene);
    }

    private void SceneWasChanged(Scene current, Scene next)
    {
        if (next.name != lobbyScene.SceneName)
        {
            if (mapSelectionCanvas.gameObject.activeSelf) mapSelectionCanvas.gameObject.SetActive(false);
        }
        else lobbyOpenEvent?.Invoke();
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneWasChanged;
    }
}
