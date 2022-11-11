using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyVehicle : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyManager;
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private Transform weaponPlace;
    [SerializeField] private LobbyVehicleData vehicleData;



    public void MakeThisVehicleActive()// вызывается кнопкой
    {
        lobbyManager.ChangeActiveVehicle(this.gameObject);
    }

    public Transform GetWeaponPlace()
    {
        return weaponPlace;
    }

    public LobbyVehicleData GetVehicleData()
    {
        return vehicleData;
    }
}
