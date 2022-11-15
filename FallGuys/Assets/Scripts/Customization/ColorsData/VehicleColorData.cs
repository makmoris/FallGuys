using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewVehicleColorData", menuName = "Customization Data/Vehicle Color")]
public class VehicleColorData : ScriptableObject
{
    [SerializeField] private string colorName;
    [SerializeField] private int colorCost;
    [SerializeField] private int colorCupsToUnlock;
    [SerializeField] private bool isColorAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 
    [Space]
    [SerializeField] private bool alwaysAvailable;// ���� true, ������ ���������� ��, ��� � ������. ������ ������ ��������

    [Header("��������� ���� ��� � �� �������")]
    [SerializeField] private string saveName;


    public void LoadData()
    {
        if (saveName == "")
        {
            Debug.Log("������� saveName ��� VehicleColorData");
        }
        else
        {
            Debug.Log("Pizda");
            isColorAvailable = ElementsAvailableData.Instance.GetAvailableStatus(saveName);
            //ElementsAvailableData.Instance.GetAvailableStatus(saveName);

            if (alwaysAvailable) isColorAvailable = true;
        }
    }

    public void SaveNewAwailableStatus(bool availableStatus)// ���������� �� ColorButton � ������ ������� �����
    {
        isColorAvailable = availableStatus;

        ElementsAvailableData.Instance.SaveElevemtAvailableStatus(saveName, isColorAvailable);

        Debug.Log(name + " �������� isAvailable");
    }

    public string ColorName
    {
        get
        {
            return colorName;
        }
    }
    public int ColorCost
    {
        get
        {
            return colorCost;
        }
    }
    public int ColorCupsToUnlock
    {
        get
        {
            return colorCupsToUnlock;
        }
    }
    public bool IsColorAvailable
    {
        get
        {
            return isColorAvailable;
        }
    }
}
