using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VehicleCustomization_Material
{
    [SerializeField] internal int materialActiveIndex;// индекс выбранного материала
    [SerializeField] internal List<Material> materials; // материалы должны быть одни для всех машин в игре
}


[CreateAssetMenu(fileName = "New VehicleCustomizationData", menuName = "Customization Data/Vehicle Customization Agent")]
public class VehicleCustomizationData : ScriptableObject
{
    [SerializeField] private VehicleCustomization_Material vehicleCustomization_Material;


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
        vehicleCustomization_Material.materialActiveIndex = ElementsSelectedData.Instance.GetActiveSelectedIndex(name);
    }

    public void SaveNewSelectedActiveIndex(int activeIndex)// вызывается из ColorButton в момент покупки цвета
    {
        vehicleCustomization_Material.materialActiveIndex = activeIndex;

        ElementsSelectedData.Instance.SaveElevemtSelectedIndex(name, vehicleCustomization_Material.materialActiveIndex);

        Debug.Log(name + " сохранил activeIndex");
    }
}
