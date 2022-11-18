using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Apply Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI applyText;
    [Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI cupsText;

    private List<WeaponButton> weaponButtons = new List<WeaponButton>();

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

    public void ShowWeaponWasChanged()
    {
        foreach (var but in weaponButtons)
        {
            but.ChangeShowSelectWeapon();
        }
    }

    public void InfoWasUpdate()
    {
        UpdateAllButtonInfo();
    }

    private void UpdateAllButtonInfo()
    {
        if (weaponButtons.Count == 0) SetWeaponButtonsList();

        foreach (var but in weaponButtons)
        {
            if (notFirstActive) but.UpdateLoadedData();
            but.UpdateInfo();
        }
    }

    private void SetWeaponButtonsList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            weaponButtons.Add(transform.GetChild(i).gameObject.GetComponent<WeaponButton>());
        }
    }
}
