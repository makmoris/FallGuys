using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorContent : MonoBehaviour
{
    [SerializeField] private LobbyVehicle vehicleForContent;
    [SerializeField] private TextMeshProUGUI nameText;


    private List<ColorButton> colorButtons = new List<ColorButton>();

    private bool notFirstActive;

    private void OnEnable()
    {
        if (notFirstActive)
        {
            UpdateAllButtonInfo();
        }
    }

    private void Start()
    {
        UpdateAllButtonInfo();
        notFirstActive = true;
    }


    public GameObject GetContentVehicle()
    {
        return vehicleForContent.gameObject;
    }

    public VehicleCustomizer GetVehicleCustomizer()
    {
        return vehicleForContent.GetComponent<VehicleCustomizer>();
    }

    public TextMeshProUGUI GetNameText()
    {
        return nameText;
    }

    public void ShowColorWasChanged()
    {
        foreach (var but in colorButtons)
        {
            but.ChangeShowSelectColor();
        }
    }

    public void InfoWasUpdate()
    {
        UpdateAllButtonInfo();
    }

    private void UpdateAllButtonInfo()
    {
        if (colorButtons.Count == 0) SetColorButtonsList();

        foreach (var but in colorButtons)
        {
            but.UpdateInfo();
        }
    }

    private void SetColorButtonsList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            colorButtons.Add(transform.GetChild(i).gameObject.GetComponent<ColorButton>());
        }
    }
}
