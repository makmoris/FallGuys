using UnityEngine;
using TMPro;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private VehicleColorData colorData;
    [SerializeField] private Sprite vehicleColorImage;
    [SerializeField] private Material colorMaterial;
    [SerializeField] private TextMeshProUGUI colorNameOnButton;

    [Header("Button Components")]
    [SerializeField] private GameObject choiseImage;
    [SerializeField] private GameObject borderImage;

    [Space]
    [SerializeField] private bool isActiveColor;

    private ColorContent parentColorContent;
    [SerializeField]private VehicleCustomizer thisLobbyVehicleCustomizer;
    private TextMeshProUGUI nameText;

    [Header("Data values")]
    [SerializeField] private string _colorName;
    [SerializeField] private int _colorCost;
    [SerializeField] private int _colorCupsToUnlock;
    [SerializeField] private bool _isColorAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 

    private void Start()
    {
        SetLobbyVehicleData();
    }

    public void SetColor()// ������ ��������������� ����� ������� (����� ������), ����� ��������� � ����������� ����
    {
        parentColorContent.InfoWasUpdate();

        thisLobbyVehicleCustomizer.ChangeColor(colorMaterial);
    }

    public void ShowColor()// ���������� ������ ��� ������ �����, ����� ����������. �������� ����� ��� �����
    {
        nameText.text = _colorName;

        thisLobbyVehicleCustomizer.ShowColor(colorMaterial);

        parentColorContent.ShowColorWasChanged();
    }

    private void LoadColorData()
    {
        _colorName = colorData.ColorName;
        _colorCost = colorData.ColorCost;
        _colorCupsToUnlock = colorData.ColorCupsToUnlock;
        _isColorAvailable = colorData.IsColorAvailable;


        colorNameOnButton.text = _colorName;
    }

    private void SetLobbyVehicleData()
    {
        LoadColorData();

        parentColorContent = transform.GetComponentInParent<ColorContent>();
        thisLobbyVehicleCustomizer = parentColorContent.GetVehicleCustomizer();

        nameText = parentColorContent.GetNameText();
    }

    public void ChangeShowSelectColor()
    {
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
        }
        else
        {
            isActiveColor = false;
        }

        choiseImage.SetActive(isActiveColor);

        ChangeShowSelectColor();
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
