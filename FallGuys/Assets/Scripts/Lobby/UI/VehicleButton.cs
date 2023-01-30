using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleButton : MonoBehaviour
{
    [SerializeField] private LobbyVehicle lobbyVehicleOnScene;
    private LobbyVehicleData lobbyVehicleData;

    [Header("Button Components")]
    [SerializeField] private TextMeshProUGUI vehicleNameOnButton;
    [SerializeField] private GameObject activeImage;
    [SerializeField] private GameObject selectImage;
    [SerializeField] private GameObject lockImage;

    private TextMeshProUGUI nameText;
    //private CharacteristicVisualizer hpScale;

    private Button applyButton;
    private TextMeshProUGUI applyText;
    private Image applyTextPanel;
    private Button buyButton;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI cupsText;
    private Image messagePanel;
    private TextMeshProUGUI messageText;

    private Vector2 oldCupsTextPosition;

    private VehicleContent vehicleContent;

    [Space]
    [Header("---------")]
    [SerializeField] private bool isActiveVehicle;

    [Header("Data values")]
    [SerializeField] private string _vehicleName;
    //[SerializeField] private Sprite weaponIcon;
    [SerializeField] private int _vehicleCost;
    [SerializeField] private int _vehicleCupsToUnlock;
    [SerializeField] private bool _isVehicleAvailable;
    [SerializeField] private PlayerDefaultData _vehicleDefaultData;

    private bool dataWasLoaded;

    public void SelectVehicle()
    {
        // ПО НАЖАТИЮ ДОЛЖНЫ ОТОБРАЖАТЬ ВЫБРАННОЕ, НО НЕ ПОКУПАТЬ. СОХРАНЕНИЕ ВЫБОРА ТОЛЬКО ПОСЛЕ ПОКУПКИ

        // сделать сначала проверку. Если все гуд, то делаем активной
        // в момент выбора подтягивать остальную инфу из LobbyVehicleData через эту тачу и кидать ее на нужные места (текст, иконка и т.д.)
        LobbyManager.Instance.SetCharacter();
        string previousCarId = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;

        lobbyVehicleOnScene.MakeThisVehicleActive();
        vehicleContent.InfoWasUpdate();

        SendPlayerChangeCarAnalyticsEvent(previousCarId);
    }
    public void ShowVehicle()// вызывается кнопой при выборе пушки, чтобы посмотреть. Смотреть можно все кнопы
    {
        nameText.text = _vehicleName;
        //hpScale.SetScaleValue(_vehicleDefaultData.DefaultHP);

        lobbyVehicleOnScene.ShowThisVehicle();

        vehicleContent.ShowVehicleWasChanged();

        CheckBuyAndApplyButtonStatus();
    }

    private void LoadLobbyVehicleData()
    {
        lobbyVehicleData.LoadData();

        _vehicleName = lobbyVehicleData.VehicleName;
        _vehicleCost = lobbyVehicleData.VehicleCost;
        _vehicleCupsToUnlock = lobbyVehicleData.VehicleCupsToUnlock;
        _isVehicleAvailable = lobbyVehicleData.IsVehicleAvailable;
        _vehicleDefaultData = lobbyVehicleData.VehicleDefaultData;


        vehicleNameOnButton.text = _vehicleName;
    }

    private void SetLobbyVehicleData()
    {
        if (!dataWasLoaded)
        {
            lobbyVehicleData = lobbyVehicleOnScene.GetLobbyVehicleData();

            LoadLobbyVehicleData();

            vehicleContent = transform.GetComponentInParent<VehicleContent>();

            nameText = vehicleContent.GetNameText();
            //hpScale = vehicleContent.GetHPScale();

            applyButton = vehicleContent.GetApplyButton();
            applyText = vehicleContent.GetApplyText();
            applyTextPanel = vehicleContent.GetApplyTextPanel();
            buyButton = vehicleContent.GetBuyButton();
            costText = vehicleContent.GetCostText();
            cupsText = vehicleContent.GetCupsText();
            messagePanel = vehicleContent.GetMessagePanel();
            messageText = vehicleContent.GetMessageText();

            dataWasLoaded = true;

            oldCupsTextPosition = cupsText.GetComponent<CupsTextInBuyButton>().CupsTextPosition;
        }
    }

    public void UpdateLoadedData()
    {
        lobbyVehicleData.LoadData();
        _isVehicleAvailable = lobbyVehicleData.IsVehicleAvailable;
    }

    public void ChangeShowSelectVehicle()
    {
        if (nameText == null) SetLobbyVehicleData();

        if (nameText.text == _vehicleName)
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
        var activeLobbyVehicle = GetActiveLobbyVehicle();
        var saveLobbyVehicle = GetActiveSaveVehicle();

        if (activeLobbyVehicle == lobbyVehicleOnScene.gameObject)// значит этот цвет - эта кнопка - выбрана. Должны показать, что она сейчас активна (цвет этот)
        {
            nameText.text = _vehicleName;
            //hpScale.SetScaleValue(_vehicleDefaultData.DefaultHP);

            if (saveLobbyVehicle == lobbyVehicleOnScene.gameObject) isActiveVehicle = true;

            CheckBuyAndApplyButtonStatus();

            vehicleContent.ShowVehicleWasChanged();
        }
        else
        {
            if (saveLobbyVehicle != lobbyVehicleOnScene.gameObject) isActiveVehicle = false;
        }

        if (_isVehicleAvailable) lockImage.SetActive(false);
        else lockImage.SetActive(true);


        if (saveLobbyVehicle == lobbyVehicleOnScene.gameObject) activeImage.SetActive(true);
        else activeImage.SetActive(false);

        //choiseImage.SetActive(isActiveVehicle);
    }

    private void CheckBuyAndApplyButtonStatus()
    {
        if (_isVehicleAvailable)// если тачка открыта 
        {
            if (!applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(true);
            if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);

            if (isActiveVehicle)// если эта тачкаа уже выбрана
            {
                // то блочим enabled кнопку и в текст статуса - APPLIED
                applyButton.interactable = false;
                applyText.text = "APPLIED";
                applyTextPanel.gameObject.SetActive(true);
            }
            else// если эта тачкаа не выбрана
            {
                // то предлагаем его выбрать. Кнопка активна, текст на кнопке - APPLY. В статусе ничего не пишем
                // если по ней нажать, то вызывается SetColor(); чтобы сохранить изменения 
                applyButton.interactable = true;
                applyText.text = "";
                applyTextPanel.gameObject.SetActive(false);

                CallApplyButton();
            }
        }
        else // если тачка закрыта
        {
            if (!buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(true);
            if (applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(false);

            if (_vehicleCupsToUnlock <= CurrencyManager.Instance.Cups)// Смотрим, Если у игрока кубков достаточно для этой пушки, то
            {
                // то разрешаем ему посмотреть стоимость кнопки + кнопка для покупки активна
                buyButton.interactable = true;
                costText.text = _vehicleCost.ToString();
                cupsText.text = "PURCHASE";
                cupsText.rectTransform.anchoredPosition = new Vector2(0, oldCupsTextPosition.y);


                for (int i = 0; i < cupsText.transform.childCount; i++)// отключаем картинку и текст внутри
                {
                    cupsText.transform.GetChild(i).gameObject.SetActive(false);
                }

                // если денег недостаточно, то блочим кнопку (неактивная)
                if (CurrencyManager.Instance.Gold < _vehicleCost)
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

                cupsText.text = _vehicleCupsToUnlock.ToString();
                costText.text = _vehicleCost.ToString();
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
        applyButton.onClick.AddListener(SelectVehicle);
    }

    private void CallBuyButton()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyWeapon);
    }
    private void BuyWeapon() // игрок нажимает по кнопке покупки, в этот момент:
    {
        if (CurrencyManager.Instance.SpendGold(_vehicleCost))// Смотрим, если у игрока достаточно денег, чтобы купить, то 
        {
            // то продаем ему эту кнопку. Ставим _isColorAvailable этого цвета в true и сохраняем файл ScriptableObject
            _isVehicleAvailable = true;

            lobbyVehicleData.SaveNewAwailableStatus(_isVehicleAvailable);

            SelectVehicle();

            SendPlayerBuyCarAnalyticsEvent(_vehicleCost);
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

    private GameObject GetActiveLobbyVehicle()// та, которая в лобби выбрана (может быть не куплена)
    {
        if (lobbyVehicleData == null) SetLobbyVehicleData();

        return LobbyManager.Instance.GetActiveLobbyVehicle();
    }

    private GameObject GetActiveSaveVehicle()// та, которая выбранна полностью и которая пойдет в игру
    {
        if (lobbyVehicleData == null) SetLobbyVehicleData();

        return LobbyManager.Instance.GetActiveSaveVehicle();
    }

    private void ResetInfo()
    {
        lobbyVehicleOnScene.BackToActiveVehicle();
    }

    private void OnDisable()
    {
        ResetInfo();
    }

    private void SendPlayerBuyCarAnalyticsEvent(int spentValue)
    {
        LobbyManager.Instance.SetCharacter();
        string _new_car_id = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;

        int _gold_spent = spentValue;

        int gold = CurrencyManager.Instance.Gold;

        AnalyticsManager.Instance.PlayerBuyCar(_new_car_id, _gold_spent, gold);
    }

    private void SendPlayerChangeCarAnalyticsEvent(string _old_car_id)
    {
        LobbyManager.Instance.SetCharacter();
        string _new_car_id = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;

        AnalyticsManager.Instance.PlayerChangedCar(_new_car_id, _old_car_id);
    }
}
