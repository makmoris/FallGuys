using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LobbyVehicleData", menuName = "Lobby Data/Vehicle Data")]
public class LobbyVehicleData : ScriptableObject
{
    [SerializeField] private string vehicleName;
    [SerializeField] private Sprite vehicleIcon;
    [SerializeField] private int vehicleCost;
    [SerializeField] private int vehicleCupsToUnlock;
    [SerializeField] private bool isVehicleAvailable;// если false - то оно заблочено, нужно его купить/открыть 
    [SerializeField] private PlayerDefaultData vehicleDefaultData;// данные, откуда можно будет подт€гивать характеристики тачки
    [Space]
    [SerializeField] private bool alwaysAvailable;// если true, значит игнорируем то, что в пам€ти. ќбъект всегда доступен

    public void LoadData()
    {
        isVehicleAvailable = ElementsAvailableData.Instance.GetAvailableStatus(name);

        if (alwaysAvailable) isVehicleAvailable = true;
    }

    public void SaveNewAwailableStatus(bool availableStatus)// вызываетс€ из ColorButton в момент покупки цвета
    {
        isVehicleAvailable = availableStatus;

        ElementsAvailableData.Instance.SaveElevemtAvailableStatus(name, isVehicleAvailable);

        Debug.Log(name + " сохранил isAvailable");
    }

    public string VehicleName
    {
        get
        {
            return vehicleName;
        }
    }
    public int VehicleCost
    {
        get
        {
            return vehicleCost;
        }
    }
    public int VehicleCupsToUnlock
    {
        get
        {
            return vehicleCupsToUnlock;
        }
    }
    public bool IsVehicleAvailable
    {
        get
        {
            return isVehicleAvailable;
        }
    }
    public PlayerDefaultData VehicleDefaultData
    {
        get
        {
            return vehicleDefaultData;
        }
    }
}
