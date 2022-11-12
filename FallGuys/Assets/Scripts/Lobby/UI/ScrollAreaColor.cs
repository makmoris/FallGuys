using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAreaColor : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyManager;

    [SerializeField] private List<ColorContent> colorContents;

    private GameObject activeVehicle;

    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        activeVehicle = lobbyManager.GetActiveVehicle();

        //CheckInactivity(colorContents);

        ShowContentForActiveVehicle();
    }

    private void ShowContentForActiveVehicle()
    {
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

    //private void CheckInactivity(List<ColorContent> list)
    //{
    //    foreach (var value in list)
    //    {
    //        if (value.gameObject.activeSelf) value.gameObject.SetActive(false);
    //    }
    //}
}
