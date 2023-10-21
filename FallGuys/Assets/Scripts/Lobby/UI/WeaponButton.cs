using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] private LobbyWeapon lobbyWeaponOnScene;
    private LobbyWeaponData lobbyWeaponData;

    [Header("Button Components")]
    [SerializeField] private TextMeshProUGUI weaponNameOnButton;
    [SerializeField] private GameObject activeImage;
    [SerializeField] private GameObject selectImage;
    [SerializeField] private GameObject lockImage;

    private TextMeshProUGUI nameText;
    private CharacteristicVisualizer damageScale;
    private CharacteristicVisualizer rechargeTimeScale;
    private CharacteristicVisualizer attackRangeScale;

    private Button applyButton;
    private TextMeshProUGUI applyText;
    private Image applyTextPanel;
    private Button buyButton;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI cupsText;
    private Image messagePanel;
    private TextMeshProUGUI messageText;

    private Vector2 oldCupsTextPosition;

    private WeaponContent weaponContent;

    [Space]
    [Header("---------")]
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

        SendPlayerChangeGunAnalyticsEvent();
    }

    public void ShowWeapon()// вызывается кнопой при выборе пушки, чтобы посмотреть. Смотреть можно все кнопы
    {
        nameText.text = _weaponName;
        damageScale.SetScaleValue(_weaponDefaultData.Damage * -1f);
        rechargeTimeScale.SetScaleValue(_weaponDefaultData.RechargeTime);
        attackRangeScale.SetScaleValue(_weaponDefaultData.AttackRange);

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

            LoadLobbyWeaponData();

            weaponContent = transform.GetComponentInParent<WeaponContent>();

            nameText = weaponContent.GetNameText();
            damageScale = weaponContent.GetDamageScale();
            rechargeTimeScale = weaponContent.GetRechargeTimeScale();
            attackRangeScale = weaponContent.GetAttackRangeScale();

            applyButton = weaponContent.GetApplyButton();
            applyText = weaponContent.GetApplyText();
            applyTextPanel = weaponContent.GetApplyTextPanel();
            buyButton = weaponContent.GetBuyButton();
            costText = weaponContent.GetCostText();
            cupsText = weaponContent.GetCupsText();
            messagePanel = weaponContent.GetMessagePanel();
            messageText = weaponContent.GetMessageText();

            dataWasLoaded = true;

            oldCupsTextPosition = cupsText.GetComponent<CupsTextInBuyButton>().CupsTextPosition;
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
            selectImage.SetActive(true);
        }
        else
        {
            selectImage.SetActive(false);
        }
    }

    public void UpdateInfo()
    {
        var activeWeapon = GetActiveWeapon();

        if (activeWeapon == lobbyWeaponOnScene.gameObject)// значит этот цвет - эта кнопка - выбрана. Должны показать, что она сейчас активна (цвет этот)
        {
            nameText.text = _weaponName;
            damageScale.SetScaleValue(_weaponDefaultData.Damage * -1f);
            rechargeTimeScale.SetScaleValue(_weaponDefaultData.RechargeTime);
            attackRangeScale.SetScaleValue(_weaponDefaultData.AttackRange);

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

        activeImage.SetActive(isActiveWeapon);
    }

    private void CheckBuyAndApplyButtonStatus()
    {
        // если выбранное авто куплено, значит можно давать доступ к кнопкам
        if (LobbyManager.Instance.CurrentVehicleIsAvailable())
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
                    applyTextPanel.gameObject.SetActive(true);
                }
                else// если эта пушка не выбрана
                {
                    // то предлагаем его выбрать. Кнопка активна, текст на кнопке - APPLY. В статусе ничего не пишем
                    // если по ней нажать, то вызывается SetColor(); чтобы сохранить изменения 
                    applyButton.interactable = true;
                    applyText.text = "";
                    applyTextPanel.gameObject.SetActive(false);

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

                    // если денег недостаточно, то блочим кнопку (неактивная)
                    if (CurrencyManager.Instance.Gold < _weaponCost)
                    {
                        buyButton.targetGraphic.color = buyButton.colors.disabledColor;
                        MoneyNotEnough();
                    }
                    else
                    {
                        buyButton.targetGraphic.color = buyButton.colors.normalColor;
                        CallBuyButton();
                    }
                }
                else// если кубков недостаточно
                {
                    // цена показывается, но кнопка заблочена (неактивна). В статусе - NEED X КУБКОВ, ГДЕ Х - КОЛИЧЕСТВО КУБКОВ
                    //buyButton.interactable = false;
                    buyButton.targetGraphic.color = buyButton.colors.disabledColor;
                    CupsNotEnough();

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
        else // если выбранное авто не куплено, значит блочим кнопки
        {
            if (applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(false);
            if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);
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

            SendPlayerBuyGunAnalyticsEvent(_weaponCost);// analytics
        }
    }

    private void CupsNotEnough()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(ShowMessageCupsNotEnough);
    }

    private void ShowMessageCupsNotEnough()
    {
        messageText.text = "not enough cups to unlock";

        messagePanel.gameObject.SetActive(false);
        messagePanel.gameObject.SetActive(true);
    }

    private void MoneyNotEnough()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(ShowMessageMoneyNotEnough);
    }

    private void ShowMessageMoneyNotEnough()
    {
        messageText.text = "not enough money to unlock";

        messagePanel.gameObject.SetActive(false);
        messagePanel.gameObject.SetActive(true);
    }

    private GameObject GetActiveWeapon()
    {
        if (lobbyWeaponData == null) SetLobbyWeaponData(); 

        return LobbyManager.Instance.GetActiveWeapon();
    }

    private void ResetInfo()
    {
        lobbyWeaponOnScene.BackToActiveWeapon();
    }

    private void OnDisable()
    {
        ResetInfo();
    }

    private void SendPlayerBuyGunAnalyticsEvent(int spentValue)
    {
        LobbyManager.Instance.SetCharacter();
        string _new_gun_id = CharacterManager.Instance.GetPlayerWeapon().GetComponent<WeaponId>().WeaponID;

        int _gold_spent = spentValue;

        int gold = CurrencyManager.Instance.Gold;

        AnalyticsManager.Instance.PlayerBuyGun(_new_gun_id, _gold_spent, gold);
    }

    private void SendPlayerChangeGunAnalyticsEvent()
    {
        LobbyManager.Instance.SetCharacter();
        string _new_gun_id = CharacterManager.Instance.GetPlayerWeapon().GetComponent<WeaponId>().WeaponID;

        AnalyticsManager.Instance.PlayerChangedGun(_new_gun_id);
    }
}
