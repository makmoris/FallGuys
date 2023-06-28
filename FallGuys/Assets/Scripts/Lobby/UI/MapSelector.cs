using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

[Serializable]
public class MapSettings
{
    [SerializeField] private string name;
    public SceneField scene; 
    public Image levelImage;
}

public class MapSelector : MonoBehaviour
{
    [SerializeField] private UI_InfiniteScroll infiniteScroll;

    [Space]
    [SerializeField] private List<MapSettings> maps;

    [SerializeField] private bool internetConnectionIsActive;

    private SceneLoader sceneLoader;

    private AsyncOperation asyncOperation;

    private void OnEnable()
    {
        NetworkChecker.InternetConnectionAppearedEvent += SetInternetConnectionIsActive;
        NetworkChecker.InternetConnectionLostEvent += SetInternetConnectionIsInactive;
    }
    private void SetInternetConnectionIsActive()
    {
        internetConnectionIsActive = true;
    }
    private void SetInternetConnectionIsInactive()
    {
        internetConnectionIsActive = false;

        asyncOperation.allowSceneActivation = false;
    }

    public void StartLoading(SceneField loadingScene, SceneLoader sceneLoader)// вызывается по кнопке Play
    {
        Debug.Log("MapSelector StartLoading");
        asyncOperation = null;

        if (this.sceneLoader == null) this.sceneLoader = sceneLoader;

        internetConnectionIsActive = NetworkChecker.Instance.GetNetworkStatus();

        LoadMap(loadingScene);

        MusicManager.Instance.PlayArenaMapSearchMusic();
    }

    public void GoToLevel()// вызывается из UI_InfiniteScroll по завершению анимации. Или окном gameTutorialWindow
    {
        sceneLoader.LoadAnimationFinished(asyncOperation);
    }

    private void LoadMap(SceneField loadingScene)
    {
        int selectLocationIndex = GetLocationIndex(loadingScene);// находим под каким индексом здесь хранится сцена с таким же именем

        infiniteScroll.SetTargetIndex(maps[selectLocationIndex].levelImage.gameObject, this);

        StartCoroutine(LoadScene(loadingScene));
        // параллельно проигрываем анимацию выбора карты
        
    }

    IEnumerator LoadScene(SceneField loadingScene)
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(loadingScene);
        asyncOperation.allowSceneActivation = false;
        //asyncOperation.allowSceneActivation = false;

        bool sendCanStop = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                //isSceneLoaded = true;
                if (!sendCanStop && internetConnectionIsActive)
                {
                    infiniteScroll.CanStopScrolling();
                    sendCanStop = true;
                }
            }

            yield return null;
        }
    }

    private int GetLocationIndex(SceneField loadingScene)
    {
        int val = 99999;
        int index = val;// заглушка, чтобы выдал ошибку, если карту с таким именем не найдет

        for (int i = 0; i < maps.Count; i++)
        {
            if (loadingScene.SceneName == maps[i].scene.SceneName)
            {
                index = i;
                break;
            }
        }

        if (index == val) Debug.LogError($"Scene with name '{loadingScene.SceneName}' not found");

        return index;
    }

    private void OnDisable()
    {
        NetworkChecker.InternetConnectionAppearedEvent -= SetInternetConnectionIsActive;
        NetworkChecker.InternetConnectionLostEvent -= SetInternetConnectionIsInactive;
    }
}
