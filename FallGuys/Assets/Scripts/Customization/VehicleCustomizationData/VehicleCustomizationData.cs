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
            return vehicleCustomization_Material;
        }
    }

    public void SaveData()
    {

    }
}
