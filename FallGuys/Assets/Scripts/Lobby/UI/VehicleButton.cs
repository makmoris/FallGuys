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
        // �� ������� ������ ���������� ���������, �� �� ��������. ���������� ������ ������ ����� �������

        // ������� ������� ��������. ���� ��� ���, �� ������ ��������
        // � ������ ������ ����������� ��������� ���� �� LobbyVehicleData ����� ��� ���� � ������ �� �� ������ ����� (�����, ������ � �.�.)
        LobbyManager.Instance.SetCharacter();
        string previousCarId = CharacterManager.Instance.GetPlayerPrefab().GetComponent<VehicleId>().VehicleID;

        lobbyVehicleOnScene.MakeThisVehicleActive();
        vehicleContent.InfoWasUpdate();

        SendPlayerChangeCarAnalyticsEvent(previousCarId);
    }
    public void ShowVehicle()// ���������� ������ ��� ������ �����, ����� ����������. �������� ����� ��� �����
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

        if (activeLobbyVehicle == lobbyVehicleOnScene.gameObject)// ������ ���� ���� - ��� ������ - �������. ������ ��������, ��� ��� ������ ������� (���� ����)
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
        if (_isVehicleAvailable)// ���� ����� ������� 
        {
            if (!applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(true);
            if (buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(false);

            if (isActiveVehicle)// ���� ��� ������ ��� �������
            {
                // �� ������ enabled ������ � � ����� ������� - APPLIED
                applyButton.interactable = false;
                applyText.text = "APPLIED";
                applyTextPanel.gameObject.SetActive(true);
            }
            else// ���� ��� ������ �� �������
            {
                // �� ���������� ��� �������. ������ �������, ����� �� ������ - APPLY. � ������� ������ �� �����
                // ���� �� ��� ������, �� ���������� SetColor(); ����� ��������� ��������� 
                applyButton.interactable = true;
                applyText.text = "";
                applyTextPanel.gameObject.SetActive(false);

                CallApplyButton();
            }
        }
        else // ���� ����� �������
        {
            if (!buyButton.gameObject.activeSelf) buyButton.gameObject.SetActive(true);
            if (applyButton.gameObject.activeSelf) applyButton.gameObject.SetActive(false);

            if (_vehicleCupsToUnlock <= CurrencyManager.Instance.Cups)// �������, ���� � ������ ������ ���������� ��� ���� �����, ��
            {
                // �� ��������� ��� ���������� ��������� ������ + ������ ��� ������� �������
                buyButton.interactable = true;
                costText.text = _vehicleCost.ToString();
                cupsText.text = "PURCHASE";
                cupsText.rectTransform.anchoredPosition = new Vector2(0, oldCupsTextPosition.y);


                for (int i = 0; i < cupsText.transform.childCount; i++)// ��������� �������� � ����� ������
                {
                    cupsText.transform.GetChild(i).gameObject.SetActive(false);
                }

                // ���� ����� ������������, �� ������ ������ (����������)
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
            else// ���� ������ ������������
            {
                // ���� ������������, �� ������ ��������� (���������). � ������� - NEED X ������, ��� � - ���������� ������
                //buyButton.interactable = false;
                buyButton.targetGraphic.color = buyButton.colors.disabledColor;
                CupsNotEnough();

                cupsText.text = _vehicleCupsToUnlock.ToString();
                costText.text = _vehicleCost.ToString();
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
        applyButton.onClick.AddListener(SelectVehicle);
    }

    private void CallBuyButton()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyWeapon);
    }
    private void BuyWeapon() // ����� �������� �� ������ �������, � ���� ������:
    {
        if (CurrencyManager.Instance.SpendGold(_vehicleCost))// �������, ���� � ������ ���������� �����, ����� ������, �� 
        {
            // �� ������� ��� ��� ������. ������ _isColorAvailable ����� ����� � true � ��������� ���� ScriptableObject
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

    private GameObject GetActiveLobbyVehicle()// ��, ������� � ����� ������� (����� ���� �� �������)
    {
        if (lobbyVehicleData == null) SetLobbyVehicleData();

        return LobbyManager.Instance.GetActiveLobbyVehicle();
    }

    private GameObject GetActiveSaveVehicle()// ��, ������� �������� ��������� � ������� ������ � ����
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
