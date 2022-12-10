using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorContent : MonoBehaviour
{
    [SerializeField] private LobbyVehicle vehicleForContent;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI applyText;
    [SerializeField] private Image applyTextPanel;
    [Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI cupsText;
    [Header("Message Panel")]
    [SerializeField] private Image messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;
    [Header("Weapon Characteristic")]
    [SerializeField] private GameObject weaponCharacteristic;


    private List<ColorButton> colorButtons = new List<ColorButton>();

    private bool notFirstActive;

    private void OnEnable()
    {
        if (notFirstActive)
        {
            UpdateAllButtonInfo();

            weaponCharacteristic.SetActive(false);
        }
    }

    private void Start()
    {
        if (!notFirstActive)
        {
            UpdateAllButtonInfo();
            notFirstActive = true;

            weaponCharacteristic.SetActive(false);
        }
    }

    #region GET
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

    public Image GetMessagePanel()
    {
        return messagePanel;
    }
    public TextMeshProUGUI GetMessageText()
    {
        return messageText;
    }

    public Image GetApplyTextPanel()
    {
        return applyTextPanel;
    }
    public Button GetApplyButton()
    {
        return applyButton;
    }
    public TextMeshProUGUI GetApplyText()
    {
        return applyText;
    }
    public Button GetBuyButton()
    {
        return buyButton;
    }
    public TextMeshProUGUI GetCostText()
    {
        return costText;
    }
    public TextMeshProUGUI GetCupsText()
    {
        return cupsText;
    }
    #endregion

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
            if (notFirstActive) but.UpdateLoadedData();
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
