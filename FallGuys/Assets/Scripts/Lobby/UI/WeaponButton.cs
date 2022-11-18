using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] private LobbyWeapon lobbyWeaponOnScene;
    private LobbyWeaponData lobbyWeaponData;
    private LobbyManager lobbyManager;

    [Header("Button Components")]
    [SerializeField] private TextMeshProUGUI weaponNameOnButton;
    [SerializeField] private GameObject choiseImage;
    [SerializeField] private GameObject borderImage;
    [SerializeField] private GameObject lockImage;

    private TextMeshProUGUI nameText;

    private Button applyButton;
    private TextMeshProUGUI applyText;
    private Button buyButton;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI cupsText;

    private Vector2 oldCupsTextPosition;

    private WeaponContent weaponContent;

    [Space]
    [SerializeField] private bool isActiveWeapon;

    [Header("Data values")]
    [SerializeField] private string _weaponName;
    //[SerializeField] private Sprite weaponIcon;
    [SerializeField] private int _weaponCost;
    [SerializeField] private int _weaponCupsToUnlock;
    [SerializeField] private bool _isWeaponAvailable;
    [SerializeField] private WeaponCharacteristicsData _weaponDefaultData;

    private bool dataWasLoaded;

    public void SelectWeapon()
    {
        // сделать сначала проверку. Если все гуд, то делаем активной
        // в момент выбора подтягивать остальную инфу из LobbyVehicleData через эту тачу и кидать ее на нужные места (текст, иконка и т.д.)
        lobbyWeaponOnScene.MakeThisWeaponActive();
        weaponContent.InfoWasUpdate();
    }

    public void ShowWeapon()// вызывается кнопой при выборе пушки, чтобы посмотреть. Смотреть можно все кнопы
    {
        nameText.text = _weaponName;

        lobbyWeaponOnScene.ShowThisWeapon();

        weaponContent.ShowWeaponWasChanged();

        CheckBuyAndApplyButtonStatus();
    }

    private void LoadLobbyWeaponData()
    {
        lobbyWeaponData.LoadData();

        _weaponName = lobbyWeaponData.WeaponName;
        _weaponCost = lobbyWeaponData.WeaponCost;
        _weaponCupsToUnlock = lobbyWeaponData.WeaponCupsToUnlock;
        _isWeaponAvailable = lobbyWeaponData.IsWeaponAvailable;
        _weaponDefaultData = lobbyWeaponData.WeaponDefaultData;


        weaponNameOnButton.text = _weaponName;
    }

    private void SetLobbyWeaponData()
    {
        if (!dataWasLoaded)
        {
            lobbyWeaponData = lobbyWeaponOnScene.GetLobbyWeaponData();
            lobbyManager = lobbyWeaponOnScene.GetLobbyManager();

            LoadLobbyWeaponData();

            weaponContent = transform.GetComponentInParent<WeaponContent>();

            nameText = weaponContent.GetNameText();

            applyButton = weaponContent.GetApplyButton();
            applyText = weaponContent.GetApplyText();
            buyButton = weaponContent.GetBuyButton();
            costText = weaponContent.GetCostText();
            cupsText = weaponContent.GetCupsText();

            dataWasLoaded = true;

            oldCupsTextPosition = cupsText.rectTransform.anchoredPosition;
        }
    }

    public void UpdateLoadedData()
    {
        lobbyWeaponData.LoadData();
        _isWeaponAvailable = lobbyWeaponData.IsWeaponAvailable;
    }

    public void ChangeShowSelectWeapon()
    {
        if (nameText == null) SetLobbyWeaponData();

        if (nameText.text == _weaponName)
        {
            borderImage.SetActive(true);
        }
        else
        {
            borderImage.SetActive(false);
        }
    }

    public void UpdateInfo()
    {
        var activeWeapon = GetActiveWeapon();

        if (activeWeapon == lobbyWeaponOnScene.gameObject)// значит этот цвет - эта кнопка - выбрана. Должны показать, что она сейчас активна (цвет этот)
        {
            nameText.text = _weaponName;
            isActiveWeapon = true;

            CheckBuyAndApplyButtonStatus();

            weaponContent.ShowWeaponWasChanged();
        }
        else
        {
            isActiveWeapon = false;
        }

        if (_isWeaponAvailable) lockImage.SetActive(false);
        else lockImage.SetActive(true);

        choiseImage.SetActive(isActiveWeapon);
    }

    private void CheckBuyAndApplyButtonStatus()
    {
        if (_isWeaponAvailable)// если пушка открыта 
        {
            if (!applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(true);
            if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);

            if (isActiveWeapon)// если эта пушка уже выбрана
            {
                // то блочим enabled кнопку и в текст статуса - APPLIED
                applyButton.interactable = false;
                applyText.text = "APPLIED";
            }
            else// если эта пушка не выбрана
            {
                // то предлагаем его выбрать. Кнопка активна, текст на кнопке - APPLY. В статусе ничего не пишем
                // если по ней нажать, то вызывается SetColor(); чтобы сохранить изменения 
                applyButton.interactable = true;
                applyText.text = "APPLY";

                CallApplyButton();
            }
        }
        else // если цвет закрыт
        {
            if (!buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(true);
            if (applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(false);

            if (_weaponCupsToUnlock <= CurrencyManager.Instance.Cups)// Смотрим, Если у игрока кубков достаточно для этой пушки, то
            {
                // то разрешаем ему посмотреть стоимость кнопки + кнопка для покупки активна
                buyButton.interactable = true;
                costText.text = _weaponCost.ToString();
                cupsText.text = "PURCHASE";
                cupsText.rectTransform.anchoredPosition = new Vector2(0, oldCupsTextPosition.y);


                for (int i = 0; i < cupsText.transform.childCount; i++)// отключаем картинку и текст внутри
                {
                    cupsText.transform.GetChild(i).gameObject.SetActive(false);
                }

                CallBuyButton();
            }
            else// если кубков недостаточно
            {
                // цена показывается, но кнопка заблочена (неактивна). В статусе - NEED X КУБКОВ, ГДЕ Х - КОЛИЧЕСТВО КУБКОВ
                buyButton.interactable = false;
                cupsText.text = _weaponCupsToUnlock.ToString();
                costText.text = _weaponCost.ToString();
                cupsText.rectTransform.anchoredPosition = oldCupsTextPosition;

                for (int i = 0; i < cupsText.transform.childCount; i++)// включаем картинку и текст внутри
                {
                    cupsText.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
    private void CallApplyButton()
    {
        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(SelectWeapon);
    }

    private void CallBuyButton()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyWeapon);
    }
    private void BuyWeapon() // игрок нажимает по кнопке покупки, в этот момент:
    {
        if (CurrencyManager.Instance.SpendGold(_weaponCost))// Смотрим, если у игрока достаточно денег, чтобы купить, то 
        {
            // то продаем ему эту кнопку. Ставим _isColorAvailable этого цвета в true и сохраняем файл ScriptableObject
            _isWeaponAvailable = true;

            lobbyWeaponData.SaveNewAwailableStatus(_isWeaponAvailable);

            SelectWeapon();
        }
        else
        {
            // если денег недостаточно, то блочим кнопку (неактивная)

        }
    }

    private GameObject GetActiveWeapon()
    {
        if (lobbyManager == null) SetLobbyWeaponData(); 

        return lobbyManager.GetActiveWeapon();
    }

    private void ResetInfo()
    {
        lobbyWeaponOnScene.BackToActiveWeapon();
    }

    private void OnDisable()
    {
        ResetInfo();
    }
}
