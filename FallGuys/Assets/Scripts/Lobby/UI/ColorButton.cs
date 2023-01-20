using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private VehicleColorData colorData;
    private Sprite vehicleColorImage;
    [SerializeField] private Material colorMaterial;

    [Header("Button Components")]
    [SerializeField] private TextMeshProUGUI colorNameOnButton;
    [SerializeField] private GameObject activeImage;
    [SerializeField] private GameObject selectImage;
    [SerializeField] private GameObject lockImage;
    
    private TextMeshProUGUI nameText;

    private Button applyButton;
    private TextMeshProUGUI applyText;
    private Image applyTextPanel;
    private Button buyButton;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI cupsText;
    private Image messagePanel;
    private TextMeshProUGUI messageText;

    private Vector2 oldCupsTextPosition;

    [Space]
    [Header("---------")]
    [SerializeField] private bool isActiveColor;

    private ColorContent parentColorContent;
    [SerializeField]private VehicleCustomizer thisLobbyVehicleCustomizer;
    

    [Header("Data values")]
    [SerializeField] private string _colorName;
    [SerializeField] private int _colorCost;
    [SerializeField] private int _colorCupsToUnlock;
    [SerializeField] private bool _isColorAvailable;// если false - то оно заблочено, нужно его купить/открыть 

    private bool dataWasLoaded;


    public void SetColor()// должен устанавливаться одной кнопкой (кнопа выбора), чтобы сохранить и подтвердить цвет
    {
        thisLobbyVehicleCustomizer.ChangeColor(colorMaterial);

        parentColorContent.InfoWasUpdate();
    }

    public void ShowColor()// вызывается кнопой при выборе цвета, чтобы посмотреть. Смотреть можно все кнопы
    {
        nameText.text = _colorName;

        thisLobbyVehicleCustomizer.ShowColor(colorMaterial);

        parentColorContent.ShowColorWasChanged();

        CheckBuyAndApplyButtonStatus();
    }

    private void LoadColorData()
    {
        colorData.LoadData();
        
        _colorName = colorData.ColorName;
        _colorCost = colorData.ColorCost;
        _colorCupsToUnlock = colorData.ColorCupsToUnlock;
        _isColorAvailable = colorData.IsColorAvailable;


        colorNameOnButton.text = _colorName;
    }

    private void SetLobbyVehicleColorData()
    {
        if (!dataWasLoaded)
        {
            LoadColorData();

            parentColorContent = transform.GetComponentInParent<ColorContent>();
            thisLobbyVehicleCustomizer = parentColorContent.GetVehicleCustomizer();

            nameText = parentColorContent.GetNameText();

            applyButton = parentColorContent.GetApplyButton();
            applyText = parentColorContent.GetApplyText();
            applyTextPanel = parentColorContent.GetApplyTextPanel();
            buyButton = parentColorContent.GetBuyButton();
            costText = parentColorContent.GetCostText();
            cupsText = parentColorContent.GetCupsText();
            messagePanel = parentColorContent.GetMessagePanel();
            messageText = parentColorContent.GetMessageText();

            dataWasLoaded = true;

            oldCupsTextPosition = cupsText.rectTransform.anchoredPosition;
        }
    }

    public void UpdateLoadedData()
    {
        colorData.LoadData();
        _isColorAvailable = colorData.IsColorAvailable;
    }

    public void ChangeShowSelectColor()
    {
        if (nameText == null) SetLobbyVehicleColorData();

        if (nameText.text == _colorName)
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
        var activeMaterial = GetActiveMaterial();

        if (activeMaterial == colorMaterial)// значит этот цвет - эта кнопка - выбрана. Должны показать, что она сейчас активна (цвет этот)
        {
            nameText.text = _colorName;
            isActiveColor = true;

            CheckBuyAndApplyButtonStatus();

            parentColorContent.ShowColorWasChanged();
        }
        else
        {
            isActiveColor = false;
        }

        if (_isColorAvailable) lockImage.SetActive(false);
        else lockImage.SetActive(true);
        
        activeImage.SetActive(isActiveColor);
    }

    private void CheckBuyAndApplyButtonStatus()
    {
        // если выбранное авто куплено, значит можно давать доступ к кнопкам
        if (LobbyManager.Instance.CurrentVehicleIsAvailable())
        {
            if (_isColorAvailable)// если цвет открыт 
            {
                if (!applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(true);
                if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);

                if (isActiveColor)// если этот цвет уже выбран 
                {
                    // то блочим enabled кнопку и в текст статуса - APPLIED
                    applyButton.interactable = false;
                    applyText.text = "APPLIED";
                    applyTextPanel.gameObject.SetActive(true);
                }
                else// если этот цвет не выбран
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

                if (_colorCupsToUnlock <= CurrencyManager.Instance.Cups)// Смотрим, Если у игрока кубков достаточно для этого цвета, то
                {
                    // то разрешаем ему посмотреть стоимость кнопки + кнопка для покупки активна
                    buyButton.interactable = true;
                    costText.text = _colorCost.ToString();
                    cupsText.text = "PURCHASE";
                    cupsText.rectTransform.anchoredPosition = new Vector2(0, oldCupsTextPosition.y);


                    for (int i = 0; i < cupsText.transform.childCount; i++)// отключаем картинку и текст внутри
                    {
                        cupsText.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    // если денег недостаточно, то блочим кнопку (неактивная)
                    if (CurrencyManager.Instance.Gold < _colorCost)
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

                    cupsText.text = _colorCupsToUnlock.ToString();
                    costText.text = _colorCost.ToString();
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
        applyButton.onClick.AddListener(SetColor);
    }

    private void CallBuyButton()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyColor);
    }
    private void BuyColor() // игрок нажимает по кнопке покупки, в этот момент:
    {
        if (CurrencyManager.Instance.SpendGold(_colorCost))// Смотрим, если у игрока достаточно денег, чтобы купить, то 
        {
            // то продаем ему эту кнопку. Ставим _isColorAvailable этого цвета в true и сохраняем файл ScriptableObject
            _isColorAvailable = true;

            colorData.SaveNewAwailableStatus(_isColorAvailable);

            SetColor();

            SendPlayerBuySkinAnalyticsEvent(_colorCost);
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

    private Material GetActiveMaterial()
    {
        if (thisLobbyVehicleCustomizer == null) SetLobbyVehicleColorData();

        Material activeMaterial = thisLobbyVehicleCustomizer.GetActiveMaterial();

        return activeMaterial;
    }

    private void ResetInfo()
    {
        thisLobbyVehicleCustomizer.BackToActiveColor();
    }

    private void OnDisable()
    {
        ResetInfo();
    }

    private void SendPlayerBuySkinAnalyticsEvent(int spentValue)
    {
        string _new_skin_id = colorData.GetSkinID();

        int _gold_spent = spentValue;

        int gold = CurrencyManager.Instance.Gold;

        AnalyticsManager.Instance.PlayerBuySkin(_new_skin_id, _gold_spent, gold);
    }
}
