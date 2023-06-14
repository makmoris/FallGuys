using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class VehicleCharacter
{
    [SerializeField] private string name;
    [SerializeField] internal GameObject vehiclePrefab;
    [SerializeField] internal PlayerDefaultData defaultData;
    [SerializeField] internal PlayerLimitsData limitsData;
}

[Serializable]
public class WeaponCharacter
{
    [SerializeField] private string name;
    [SerializeField] internal GameObject weaponPrefab;
    //[SerializeField] internal WeaponCharacteristicsData weaponData;
}


public class CharacterManager : MonoBehaviour
{
    [SerializeField] private int vehicleIndex;// сейвим и загружаем эти данные. По ним находим актуальный вариант выбора игрока
    [SerializeField] private int weaponIndex;

    [SerializeField] private List<VehicleCharacter> vehicles;// храним все доступные авто в игре

    [SerializeField] private List<WeaponCharacter> weapons;// храним все доступные пушки в игре


    public static CharacterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetCharacter(GameObject selectedVehicle, GameObject selectedWeapon)// вызывается из лобби менеджера
    {
        FindVehicleIndex(selectedVehicle);
        FindWeaponIndex(selectedWeapon);
    }

    private void FindVehicleIndex(GameObject _vehiclePrefab)
    {
        int index = 0;

        foreach (var vehicle in vehicles)
        {
            if (vehicle.vehiclePrefab == _vehiclePrefab)
            {
                vehicleIndex = index;
                break;
            }

            index++;
        }
    }

    private void FindWeaponIndex(GameObject _weaponPrefab)
    {
        int index = 0;

        foreach (var weapon in weapons)
        {
            if (weapon.weaponPrefab == _weaponPrefab)
            {
                weaponIndex = index;
                break;
            }

            index++;
        }
    }

    #region forInstaller

    public GameObject GetPlayerPrefab()
    {
        return vehicles[vehicleIndex].vehiclePrefab;
    }

    public PlayerDefaultData GetPlayerDefaultData()
    {
        return vehicles[vehicleIndex].defaultData;
    }

    public PlayerLimitsData GetPlayerLimitsData()
    {
        return vehicles[vehicleIndex].limitsData;
    }

    public Weapon GetPlayerWeapon()
    {
        return weapons[weaponIndex].weaponPrefab.GetComponent<Weapon>();
    }

    #endregion
}
