using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LobbyVehicleData", menuName = "Lobby Data/Weapon Data")]
public class LobbyWeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private int weaponCost;
    [SerializeField] private int weaponCupsToUnlock;
    [SerializeField] private bool isWeaponAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 
    [SerializeField] private WeaponCharacteristicsData weaponDefaultData;// ������, ������ ����� ����� ����������� �������������� �����
}
