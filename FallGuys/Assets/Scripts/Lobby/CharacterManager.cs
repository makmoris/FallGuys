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
    [SerializeField] private int vehicleIndex;// ������ � ��������� ��� ������. �� ��� ������� ���������� ������� ������ ������
    [SerializeField] private int weaponIndex;

    [Header("Player vehicles")]
    [SerializeField] private List<VehicleCharacter> playerVehicles;// ������ ��� ��������� ���� � ����

    [Header("Race AI vehicles")]
    [SerializeField] private List<VehicleCharacter> raceAIVehicles;

    [Header("Arena AI vehicles")]
    [SerializeField] private List<VehicleCharacter> arenaAIVehicles;

    [Header("Weapons")]
    [SerializeField] private List<WeaponCharacter> weapons;// ������ ��� ��������� ����� � ����


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

    public void SetCharacter(GameObject selectedVehicle, GameObject selectedWeapon)// ���������� �� ����� ���������
    {
        FindVehicleIndex(selectedVehicle);
        FindWeaponIndex(selectedWeapon);
    }

    private void FindVehicleIndex(GameObject _vehiclePrefab)
    {
        int index = 0;

        foreach (var vehicle in playerVehicles)
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

    public GameObject GetRandomRaceAIPrefab()
    {
        int randomIndex = UnityEngine.Random.Range(0, raceAIVehicles.Count);

        GameObject raceAIPrefab = raceAIVehicles[randomIndex].vehiclePrefab;

        return raceAIPrefab;
    }

    public Material GetRandomColorMaterial()
    {
        Material material = null; 

        VehicleCustomizer raceAIVehicleCustomizer = playerVehicles[vehicleIndex].vehiclePrefab.GetComponent<VehicleCustomizer>();
        if (raceAIVehicleCustomizer != null)
        {
             material = raceAIVehicleCustomizer.GetRandomColorMaterial();
        }

        return material;
    }

    public Weapon GetRandomWeapon()
    {
        int rIndex = UnityEngine.Random.Range(0, weapons.Count);

        return weapons[rIndex].weaponPrefab.GetComponent<Weapon>();
    }

    #region forInstaller

    public GameObject GetPlayerPrefab()
    {
        return playerVehicles[vehicleIndex].vehiclePrefab;
    }

    public PlayerDefaultData GetPlayerDefaultData()
    {
        return playerVehicles[vehicleIndex].defaultData;
    }

    public PlayerLimitsData GetPlayerLimitsData()
    {
        return playerVehicles[vehicleIndex].limitsData;
    }

    public Weapon GetPlayerWeapon()
    {
        return weapons[weaponIndex].weaponPrefab.GetComponent<Weapon>();
    }

    #endregion
}
