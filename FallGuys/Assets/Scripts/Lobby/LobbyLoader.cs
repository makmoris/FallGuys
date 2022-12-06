using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyLoader : MonoBehaviour // �������� �� ������ ������. ���������� ����� ��� �������� ������
{
    [SerializeField] private Object lobbyScene;

    private bool isSceneLoaded;
    private AsyncOperation asyncOperation;

    private bool loadingStarted;

    private void Start()
    {
        if (!loadingStarted)
        {
            StartCoroutine(LoadScene(lobbyScene.name));
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
            // ����� ��� �� ������ �����������, �� �� ���������� �������� ����� ���������. �� �������� ������ �������� �� �������
            Debug.Log($"����� {lobbyScene.name} �� ������ �����������");
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
