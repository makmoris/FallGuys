using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class MapSettings
{
    [SerializeField] private string name;
    [SerializeField] internal string mapScene; 
    [SerializeField] internal Image mapImage;
}

public class MapSelector : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Extensions.UI_InfiniteScroll infiniteScroll;
    [SerializeField] private List<MapSettings> maps;

    [SerializeField]private bool isSceneLoaded;
    private AsyncOperation asyncOperation;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartPlayGame();
        }
    }

    public void StartPlayGame()// ���������� �� ������ Play
    {
        LoadMap();
    }

    public void GoToLevel()// ���������� �� UI_InfiniteScroll �� ���������� ��������
    {
        asyncOperation.allowSceneActivation = true;
    }

    private void LoadMap()
    {
        int selectMapIndex = GetRandomMapIndex();
        string sceneName = maps[selectMapIndex].mapScene;
        
        infiniteScroll.SetTargetIndex(maps[selectMapIndex].mapImage.gameObject, this);

        StartCoroutine(LoadScene(sceneName));
        // ����������� ����������� �������� ������ �����

    }

    IEnumerator LoadScene(string mapName)
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(mapName);
        asyncOperation.allowSceneActivation = false;

        bool sendCanStop = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                isSceneLoaded = true;
                if (!sendCanStop)
                {
                    infiniteScroll.CanStopScrolling();
                    sendCanStop = true;
                }
            }

            yield return null;
        }
    }

    private int GetRandomMapIndex()// ����� ����� ����� �������� �������� �� ���������� ������ - �����, ����� ������ ��������� ����� ��������
    {
        int rand = Random.Range(0, maps.Count);

        return rand;
    }

    private void CreateClones()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int y = 0; y < maps.Count; y++)
            {
                Instantiate(maps[y].mapImage, maps[y].mapImage.transform.parent);
            }
        }
    }
}
