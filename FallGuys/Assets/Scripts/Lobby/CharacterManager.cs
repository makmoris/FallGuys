using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleCharacter
{
    [SerializeField] internal GameObject vehiclePrefab;
    [SerializeField] internal PlayerDefaultData defaultData;
    [SerializeField] internal PlayerLimitsData limitsData;
}

[System.Serializable]
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

    [Space]
    [SerializeField] private VehicleCustomizationData vehicleCustomizationData;

    [Header("Player vehicles")]
    [SerializeField] private List<VehicleCharacter> playerVehicles;// храним все доступные авто в игре

    [Header("Weapons")]
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

    #region Player

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


    public Weapon GetRandomWeapon()
    {
        int randIndex = Random.Range(0, weapons.Count);

        return weapons[randIndex].weaponPrefab.GetComponent<Weapon>();
    }

    public Material GetRandomMaterial()
    {
        return vehicleCustomizationData.GetRandomMaterial();
    }
}
