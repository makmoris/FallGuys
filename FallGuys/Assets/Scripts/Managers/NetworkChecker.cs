using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkChecker : MonoBehaviour
{
    [SerializeField] private GameObject canvasInternetWindow;
    [Space]
    [SerializeField] private SceneField lobbyScene;

    [SerializeField] private bool internetAvailable;

    private bool isFirstGameLaunch;

    NetworkCheck networking;

    public System.Action InternetConnectionLostEvent;
    public System.Action InternetConnectionAppearedEvent;

    private static NetworkChecker Instance { get; set; }
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
    }

    public void Initialize()
    {
        networking = new NetworkCheck();

        isFirstGameLaunch = true;
        canvasInternetWindow.SetActive(true);
    }

    void Update()
    {
        string message = "Offline";
        Color newColor = new Color(1, 0, 0);

        //Call 'CheckOnline' to check if there is an internet connection
        if (networking.CheckOnline)
        {
            // если интернета не было, а сейчас появился
            if (internetAvailable == false)
            {
                // событие - подключение восстановлено
                InternetConnectionAppearedEvent?.Invoke();

                if (canvasInternetWindow.activeSelf) canvasInternetWindow.SetActive(false);

                internetAvailable = true;

                Time.timeScale = 1f;

                if (!isFirstGameLaunch)
                {
                    // СОБЫТИЕ ЧТО ИНТЕРНЕТ ВОССТАНОВЛЕН
                    SendPlayerInternetConnectionRestoreEvent();
                }

                isFirstGameLaunch = false;
            }
            

            message = "";
            //Call 'CheckWifiState' to see if there is a WiFi Connection
            if (networking.CheckWifiState)
            {
                message = "Online Wifi ";
                newColor = new Color(0, 1, 0);
            }

            //Call 'CheckMobileState' to see if there is a Mobile Data Connection
            if (networking.CheckMobileState)
            {
                message += "Online Mobile";
                newColor = new Color(0, newColor.g, 1);
            }
        }
        else
        {
            // если интернет был, а сейчас отключился
            if(internetAvailable == true)
            {
                // событие - потеря интернет соединения
                InternetConnectionLostEvent?.Invoke();

                canvasInternetWindow.SetActive(true);

                internetAvailable = false;

                if(SceneManager.GetActiveScene().name != lobbyScene.SceneName)
                {
                    Time.timeScale = 0f;// чтобы не стопить скролл поиска карты
                }
            }

            if (isFirstGameLaunch)
            {
                isFirstGameLaunch = false;
            }
        }
    }

    public bool GetNetworkStatus()
    {
        return internetAvailable;
    }

    private void SendPlayerInternetConnectionRestoreEvent()
    {
        AnalyticsManager.Instance.PlayerInternetConnectionRestore();
    }
}
