using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI applyText;
    [Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI cupsText;

    private List<VehicleButton> vehicleButtons = new List<VehicleButton>();

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
        if (!notFirstActive)
        {
            UpdateAllButtonInfo();
            notFirstActive = true;
        }
    }

    #region GET
    public TextMeshProUGUI GetNameText()
    {
        return nameText;
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

    public void ShowVehicleWasChanged()
    {
        foreach (var but in vehicleButtons)
        {
            but.ChangeShowSelectVehicle();
        }
    }

    public void InfoWasUpdate()
    {
        UpdateAllButtonInfo();
    }

    private void UpdateAllButtonInfo()
    {
        if (vehicleButtons.Count == 0) SetVehicleButtonsList();

        foreach (var but in vehicleButtons)
        {
            if (notFirstActive) but.UpdateLoadedData();
            but.UpdateInfo();
        }
    }

    private void SetVehicleButtonsList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            vehicleButtons.Add(transform.GetChild(i).gameObject.GetComponent<VehicleButton>());
        }
    }
}
