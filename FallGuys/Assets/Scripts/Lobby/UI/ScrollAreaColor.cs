using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAreaColor : MonoBehaviour
{
    [SerializeField] private List<ColorContent> colorContents;

    [SerializeField] private GameObject activeVehicle;
    [SerializeField]private GameObject previousActiveVehicle;

    private ScrollRect scrollRect;

    private bool notFirstActive;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        //activeVehicle = lobbyManager.GetActiveVehicle();

        //CheckInactivity(colorContents);

        //ShowContentForActiveVehicle();

        if (notFirstActive)
        {
            //if (activeVehicle == null) SetActiveVehicle();

            ShowContentForActiveVehicle();
        }
    }

    private void Start()
    {
        //if (activeVehicle == null) SetActiveVehicle();

        if (!notFirstActive)
        {
            ShowContentForActiveVehicle();
            notFirstActive = true;
        }
        //ShowContentForActiveVehicle();
        //notFirstActive = true;
    }

    private void ShowContentForActiveVehicle()
    {
        SetActiveVehicle();

        foreach (var cont in colorContents)
        {
            if (activeVehicle == cont.GetContentVehicle())
            {
                cont.gameObject.SetActive(true);

                scrollRect.content = cont.GetComponent<RectTransform>();

                //break;
            }
            else
            {
                cont.gameObject.SetActive(false);
            }
        }
    }

    private void SetActiveVehicle()
    {
        activeVehicle = LobbyManager.Instance.GetActiveLobbyVehicle();
    }

    //private void CheckInactivity(List<ColorContent> list)
    //{
    //    foreach (var value in list)
    //    {
    //        if (value.gameObject.activeSelf) value.gameObject.SetActive(false);
    //    }
    //}

    private void OnDisable()
    {
        foreach (var value in colorContents)
        {
            value.gameObject.SetActive(false);
        }
    }
}
