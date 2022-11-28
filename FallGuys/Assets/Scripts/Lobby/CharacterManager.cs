using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class VehicleCharacter
{
    [SerializeField] private string name;
    [SerializeField] internal GameObject vehiclePrefab;
    [SerializeField] private PlayerDefaultData defaultData;
    [SerializeField] private PlayerLimitsData limitsData;
}

[Serializable]
public class WeaponCharacter
{
    [SerializeField] private string name;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private WeaponCharacteristicsData weaponData;
}


public class CharacterManager : MonoBehaviour
{
    [SerializeField] private int vehicleIndex;// ������ � ��������� ��� ������. �� ��� ������� ���������� ������� ������ ������
    [SerializeField] private int weaponIndex;

    [SerializeField] private List<VehicleCharacter> vehicles;// ������ ��� ��������� ���� � ����

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

    public void FindVehicleIndex(GameObject _vehiclePrefab)
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
}
