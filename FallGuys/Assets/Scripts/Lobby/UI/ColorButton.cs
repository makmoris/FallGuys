using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private VehicleColorData colorData;
    [SerializeField] private Sprite vehicleColorImage;
    [SerializeField] private Material colorMaterial;
    [SerializeField] private TextMeshProUGUI colorNameOnButton;

    [Header("Button Components")]
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

    [Space]
    [SerializeField] private bool isActiveColor;

    private ColorContent parentColorContent;
    [SerializeField]private VehicleCustomizer thisLobbyVehicleCustomizer;
    

    [Header("Data values")]
    [SerializeField] private string _colorName;
    [SerializeField] private int _colorCost;
    [SerializeField] private int _colorCupsToUnlock;
    [SerializeField] private bool _isColorAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 

    private bool dataWasLoaded;
    private void Start()
    {
        SetLobbyVehicleData();
    }

    public void SetColor()// ������ ��������������� ����� ������� (����� ������), ����� ��������� � ����������� ����
    {
        thisLobbyVehicleCustomizer.ChangeColor(colorMaterial);

        parentColorContent.InfoWasUpdate();
    }

    public void ShowColor()// ���������� ������ ��� ������ �����, ����� ����������. �������� ����� ��� �����
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

    private void SetLobbyVehicleData()
    {
        if (!dataWasLoaded)
        {
            LoadColorData();

            parentColorContent = transform.GetComponentInParent<ColorContent>();
            thisLobbyVehicleCustomizer = parentColorContent.GetVehicleCustomizer();

            nameText = parentColorContent.GetNameText();

            applyButton = parentColorContent.GetApplyButton();
            applyText = parentColorContent.GetApplyText();
            buyButton = parentColorContent.GetBuyButton();
            costText = parentColorContent.GetCostText();
            cupsText = parentColorContent.GetCupsText();

            dataWasLoaded = true;

            oldCupsTextPosition = cupsText.rectTransform.anchoredPosition;
        }
    }

    public void ChangeShowSelectColor()
    {
        if (nameText == null) SetLobbyVehicleData();

        if (nameText.text == _colorName)
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
        var activeMaterial = GetActiveMaterial();

        if (activeMaterial == colorMaterial)// ������ ���� ���� - ��� ������ - �������. ������ ��������, ��� ��� ������ ������� (���� ����)
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

        choiseImage.SetActive(isActiveColor);
    }

    private void CheckBuyAndApplyButtonStatus()
    {
        if (_isColorAvailable)// ���� ���� ������ 
        {
            if (!applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(true);
            if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);

            if (isActiveColor)// ���� ���� ���� ��� ������ 
            {
                // �� ������ enabled ������ � � ����� ������� - APPLIED
                applyButton.interactable = false;
                applyText.text = "APPLIED";
            }
            else// ���� ���� ���� �� ������
            {
                // �� ���������� ��� �������. ������ �������, ����� �� ������ - APPLY. � ������� ������ �� �����
                // ���� �� ��� ������, �� ���������� SetColor(); ����� ��������� ��������� 
                applyButton.interactable = true;
                applyText.text = "APPLY";

                CallApplyButton();
            }
        }
        else // ���� ���� ������
        {
            if (!buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(true);
            if (applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(false);

            if (_colorCupsToUnlock <= CurrencyManager.Instance.Cups)// �������, ���� � ������ ������ ���������� ��� ����� �����, ��
            {
                // �� ��������� ��� ���������� ��������� ������ + ������ ��� ������� �������
                buyButton.interactable = true;
                costText.text = _colorCost.ToString();
                cupsText.text = "PURCHASE";
                cupsText.rectTransform.anchoredPosition = new Vector2(0, oldCupsTextPosition.y);
                

                for (int i = 0; i < cupsText.transform.childCount; i++)// ��������� �������� � ����� ������
                {
                    cupsText.transform.GetChild(i).gameObject.SetActive(false);
                }

                CallBuyButton();
            }
            else// ���� ������ ������������
            {
                // ���� ������������, �� ������ ��������� (���������). � ������� - NEED X ������, ��� � - ���������� ������
                buyButton.interactable = false;
                cupsText.text = _colorCupsToUnlock.ToString();
                costText.text = _colorCost.ToString();
                cupsText.rectTransform.anchoredPosition = oldCupsTextPosition;

                for (int i = 0; i < cupsText.transform.childCount; i++)// �������� �������� � ����� ������
                {
                    cupsText.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
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
    private void BuyColor() // ����� �������� �� ������ �������, � ���� ������:
    {
        if (CurrencyManager.Instance.SpendGold(_colorCost))// �������, ���� � ������ ���������� �����, ����� ������, �� 
        {
            // �� ������� ��� ��� ������. ������ _isColorAvailable ����� ����� � true � ��������� ���� ScriptableObject
            _isColorAvailable = true;
            SetColor();
            colorData.SaveNewAwailableStatus(_isColorAvailable);
        }
        else
        {
            // ���� ����� ������������, �� ������ ������ (����������)
            
        }
    }

    private Material GetActiveMaterial()
    {
        if (thisLobbyVehicleCustomizer == null) SetLobbyVehicleData();

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
}
