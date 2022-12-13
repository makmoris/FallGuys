using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LobbyVehicleData", menuName = "Lobby Data/Weapon Data")]
public class LobbyWeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    private Sprite weaponIcon;
    [SerializeField] private int weaponCost;
    [SerializeField] private int weaponCupsToUnlock;
    [SerializeField] private bool isWeaponAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 
    [SerializeField] private WeaponCharacteristicsData weaponDefaultData;// ������, ������ ����� ����� ����������� �������������� �����
    [Space]
    [SerializeField] private bool alwaysAvailable;// ���� true, ������ ���������� ��, ��� � ������. ������ ������ ��������

    public void LoadData()
    {
        isWeaponAvailable = ElementsAvailableData.Instance.GetAvailableStatus(name);

        if (alwaysAvailable) isWeaponAvailable = true;
    }

    public void SaveNewAwailableStatus(bool availableStatus)// ���������� �� ColorButton � ������ ������� �����
    {
        isWeaponAvailable = availableStatus;

        ElementsAvailableData.Instance.SaveElevemtAvailableStatus(name, isWeaponAvailable);

        Debug.Log(name + " �������� isAvailable");
    }

    public string WeaponName
    {
        get
        {
            return weaponName;
        }
    }
    public int WeaponCost
    {
        get
        {
            return weaponCost;
        }
    }
    public int WeaponCupsToUnlock
    {
        get
        {
            return weaponCupsToUnlock;
        }
    }
    public bool IsWeaponAvailable
    {
        get
        {
            return isWeaponAvailable;
        }
    }
    public WeaponCharacteristicsData WeaponDefaultData
    {
        get
        {
            return weaponDefaultData;
        }
    }
}
