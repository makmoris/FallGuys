using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollowingOnOtherPlayers : MonoBehaviour
{
    [SerializeField] private UIEnemyPointers uIEnemyPointers;

    private CinemachineVirtualCamera camCinema;
    private AudioListener _audioListener;

    [SerializeField]private List<GameObject> drivers = new List<GameObject>();
    [SerializeField]private GameObject currentDriver;
    private int targetIndex = 0;

    private bool isMobile;

    [SerializeField]private bool canFollowOnOtherPlayers;

    private string key = "LastObservableName";

    public event System.Action<GameObject> StartFollowingANewPlayerEvent;

    private void Awake()
    {
        camCinema = GetComponent<CinemachineVirtualCamera>();
        _audioListener = GetComponent<AudioListener>();

        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (canFollowOnOtherPlayers)
        {
            if (isMobile)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        ChangeTarget();
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    ChangeTarget();
                }
            }
        }
    }

    public void EnableObserverMode()
    {
        canFollowOnOtherPlayers = true;

        SetTarget();
    }

    public void EnablePlayerMode()
    {
        canFollowOnOtherPlayers = false;
    }

    public void AddDriver(GameObject driver, bool isCurrentPlayer)
    {
        drivers.Add(driver);

        PlayerAudioListener playerAudioListener = driver.GetComponent<PlayerAudioListener>();
        playerAudioListener.DisableAudioListener();

        if (isCurrentPlayer)
        {
            currentDriver = driver;

            camCinema.m_Follow = currentDriver.transform;
            camCinema.m_LookAt = currentDriver.transform;

            playerAudioListener.EnableAudioListener();
            _audioListener.enabled = false;

            uIEnemyPointers.ChangeCurrentTransform(currentDriver.transform);
        }
    }

    public void RemoveDriver(GameObject driver)
    {
        if(driver == currentDriver) ChangeTarget();

        drivers.Remove(driver);
    }

    public GameObject GetCurrentDriver()
    {
        return currentDriver;
    }

    private void ChangeTarget()
    {
        if(drivers.Count > 0)
        {
            int nextIndex = targetIndex + 1;
            if (nextIndex < drivers.Count) targetIndex = nextIndex;
            else targetIndex = 0;
            
            camCinema.m_Follow = drivers[targetIndex].transform;
            camCinema.m_LookAt = drivers[targetIndex].transform;

            PlayerAudioListener playerAudioListener;
            if (currentDriver == null) playerAudioListener = drivers[targetIndex].GetComponent<PlayerAudioListener>();
            else playerAudioListener = currentDriver.GetComponent<PlayerAudioListener>();

            playerAudioListener.DisableAudioListener();
            currentDriver = drivers[targetIndex];
            currentDriver.GetComponent<PlayerAudioListener>().EnableAudioListener();

            uIEnemyPointers.ChangeCurrentTransform(currentDriver.transform);

            PlayerName currentDriverName = currentDriver.GetComponent<PlayerName>();
            if(currentDriverName != null)
            {
                string currentName = currentDriverName.Name;
                PlayerPrefs.SetString(key, currentName);
            }

            StartFollowingANewPlayerEvent?.Invoke(currentDriver);
        }
    }

    private void SetTarget()
    {
        _audioListener.enabled = false;

        string lastObservableName = PlayerPrefs.GetString(key, "default");

        bool continueInObserverMode = false;

        int currentDriverIndex = 0;
        foreach (var driver in drivers)
        {
            PlayerName playerName = driver.GetComponent<PlayerName>();
            if(playerName != null)
            {
                if(lastObservableName == playerName.Name)
                {
                    currentDriver = driver;
                    continueInObserverMode = true;

                    camCinema.m_Follow = currentDriver.transform;
                    camCinema.m_LookAt = currentDriver.transform;

                    uIEnemyPointers.ChangeCurrentTransform(currentDriver.transform);

                    currentDriver.GetComponent<PlayerAudioListener>().EnableAudioListener();

                    targetIndex = currentDriverIndex;

                    break;
                }
            }
            currentDriverIndex++;
        }

        if (!continueInObserverMode) ChangeTarget();
    }
}
