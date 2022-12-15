using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyLoader : MonoBehaviour // вешается на канвас уровня. Подгружает лобби для быстрого выхода
{
    [SerializeField] private string lobbySceneName;

    private bool isSceneLoaded;
    private AsyncOperation asyncOperation;

    private bool loadingStarted;

    private void Start()
    {
        if (!loadingStarted)
        {
            StartCoroutine(LoadScene(lobbySceneName));
            loadingStarted = true;
        }
    }

    public void ExitToLobby()
    {
        if (isSceneLoaded)
        {
            asyncOperation.allowSceneActivation = true;
        }
        else
        {
            // сцена еще не успела загрузиться, но по завершению загрузки сразу переходим. Мб добавить кружок ожидания на будущее
            Debug.Log($"Сцена {lobbySceneName} не успела загрузиться");
            asyncOperation.allowSceneActivation = true;
        }
    }

    IEnumerator LoadScene(string mapName)
    {
        yield return null;

        asyncOperation = SceneManager.LoadSceneAsync(mapName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                isSceneLoaded = true;
            }

            yield return null;
        }
    }
}
