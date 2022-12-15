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
    [SerializeField] internal string locationName; 
    [SerializeField] internal Image levelImage;
}

public class MapSelector : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Extensions.UI_InfiniteScroll infiniteScroll;
    [SerializeField] private List<MapSettings> maps;

    //[SerializeField]private bool isSceneLoaded;
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

    public void StartPlayGame()// вызываетс€ по кнопке Play
    {
        LoadMap();
    }

    public void GoToLevel()// вызываетс€ из UI_InfiniteScroll по завершению анимации
    {
        asyncOperation.allowSceneActivation = true;
    }

    private void LoadMap()
    {
        List<string> availableLocations = LeagueManager.Instance.GetAvailableLocations();
        int rand = Random.Range(0, availableLocations.Count);// получаем список доступных сцен в зависимости от лиги. ƒальше берем рандомную из них
        string randomLocationName = availableLocations[rand];

        int selectLocationIndex = GetLocationIndex(randomLocationName);// находим под каким индексом здесь хранитс€ сцена с таким же именем
        string sceneName = maps[selectLocationIndex].locationName;

        //int selectMapIndex = GetRandomMapIndex(availableLocations.Count);
        //string sceneName = maps[selectMapIndex].locationName;
        
        infiniteScroll.SetTargetIndex(maps[selectLocationIndex].levelImage.gameObject, this);

        StartCoroutine(LoadScene(sceneName));
        // параллельно проигрываем анимацию выбора карты

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
                //isSceneLoaded = true;
                if (!sendCanStop)
                {
                    infiniteScroll.CanStopScrolling();
                    sendCanStop = true;
                }
            }

            yield return null;
        }
    }

    private int GetLocationIndex(string randomName)
    {
        int val = 99999;
        int index = val;// заглушка, чтобы выдал ошибку, если карту с таким именем не найдет

        for (int i = 0; i < maps.Count; i++)
        {
            if (randomName == maps[i].locationName)
            {
                index = i;
                break;
            }
        }

        if (index == val) Debug.LogError($"Scene with name '{randomName}' not found");

        return index;
    }

    private int GetRandomMapIndex(int count)// далее здесь можно добавить проверки по количеству кубков - лигам, чтобы только доступные карты отдавать
    {
        int rand = Random.Range(0, count);

        return rand;
    }

    private void CreateClones()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int y = 0; y < maps.Count; y++)
            {
                Instantiate(maps[y].levelImage, maps[y].levelImage.transform.parent);
            }
        }
    }
}
