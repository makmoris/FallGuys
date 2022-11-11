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
    [SerializeField] private bool isVehicleAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 
    [SerializeField] private PlayerDefaultData vehicleDefaultData;// ������, ������ ����� ����� ����������� �������������� �����
}
