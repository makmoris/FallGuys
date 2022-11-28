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
    [SerializeField] internal Object mapScene; 
    [SerializeField] internal Image mapImage;
}

public class MapSelector : MonoBehaviour
{
    [SerializeField] private List<MapSettings> maps;

    [SerializeField]private bool isSceneLoaded;
    private AsyncOperation asyncOperation;

    private void Awake()
    {
        CreateClones();
    }

    public void StartPlayGame()// вызывается по кнопке Play
    {
        LoadMap();
    }

    private void LoadMap()
    {
        int selectMapIndex = GetRandomMapIndex();
        string sceneName = maps[selectMapIndex].mapScene.name;

        StartCoroutine(LoadScene(sceneName));
        // параллельно проигрываем анимацию выбора карты

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

    private int GetRandomMapIndex()// далее здесь можно добавить проверки по количеству кубков - лигам, чтобы только доступные карты отдавать
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
