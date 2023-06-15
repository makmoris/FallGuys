using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollowingOnOtherPlayers : MonoBehaviour
{
    private CinemachineVirtualCamera camCinema;

    [SerializeField]private List<GameObject> drivers = new List<GameObject>();
    private GameObject currentDriver;
    [SerializeField]private int targetIndex = 0;

    private bool isMobile;

    [SerializeField]private bool canFollowOnOtherPlayers;
    public bool CanFollowOnOtherPlayers
    {
        get => canFollowOnOtherPlayers;
        set => canFollowOnOtherPlayers = value;
    }

    private void Awake()
    {
        camCinema = GetComponent<CinemachineVirtualCamera>();

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

    public void AddDriver(GameObject driver)
    {
        drivers.Add(driver);
    }

    public void RemoveDriver(GameObject driver)
    {
        if(driver == currentDriver) ChangeTarget();

        drivers.Remove(driver);
    }

    public void ChangeTarget()
    {
        if(drivers.Count > 0)
        {
            int nextIndex = targetIndex + 1;
            if (nextIndex < drivers.Count) targetIndex = nextIndex;
            else targetIndex = 0;

            camCinema.m_Follow = drivers[targetIndex].transform;
            camCinema.m_LookAt = drivers[targetIndex].transform;

            currentDriver = drivers[targetIndex];

            //int nextIndex = targetIndex + 1;
            //if (nextIndex < drivers.Count) targetIndex = nextIndex;
            //else targetIndex = 0;
        }
    }
}
