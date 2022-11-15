using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VehicleCustomization_Material
{
    [SerializeField] internal int materialActiveIndex;// ������ ���������� ���������
    [SerializeField] internal List<Material> materials; // ��������� ������ ���� ���� ��� ���� ����� � ����
}


[CreateAssetMenu(fileName = "New VehicleCustomizationData", menuName = "Customization Data/Vehicle Customization Agent")]
public class VehicleCustomizationData : ScriptableObject
{
    [SerializeField] private VehicleCustomization_Material vehicleCustomization_Material;

    [Header("��������� ���� ��� � �� �������")]
    [SerializeField] private string saveName;

    public VehicleCustomization_Material VehicleCustomization_Material
    {
        get
        {
            LoadData();
            return vehicleCustomization_Material;
        }
    }

    private void LoadData()
    {
        if (saveName == "")
        {
            Debug.Log("������� saveName ��� VehicleColorData");
        }
        else
        {
            vehicleCustomization_Material.materialActiveIndex = ElementsSelectedData.Instance.GetActiveSelectedIndex(saveName);
        }
    }

    public void SaveNewSelectedActiveIndex(int activeIndex)// ���������� �� ColorButton � ������ ������� �����
    {
        
        if (saveName != "")
        {
            vehicleCustomization_Material.materialActiveIndex = activeIndex;

            ElementsSelectedData.Instance.SaveElevemtSelectedIndex(saveName, vehicleCustomization_Material.materialActiveIndex);

            Debug.Log(name + " �������� activeIndex");
        }
    }
}
