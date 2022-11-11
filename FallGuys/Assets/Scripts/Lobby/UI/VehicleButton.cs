using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleButton : MonoBehaviour
{
    [SerializeField] private LobbyVehicle lobbyVehicleOnScene;
    [SerializeField] private LobbyVehicleData vehicleData;

    private void Awake()
    {
        SetVehicleData();
    }

    public void SelectVehicle()
    {
        // �� ������� ������ ���������� ���������, �� �� ��������. ���������� ������ ������ ����� �������

        // ������� ������� ��������. ���� ��� ���, �� ������ ��������
        // � ������ ������ ����������� ��������� ���� �� LobbyVehicleData ����� ��� ���� � ������ �� �� ������ ����� (�����, ������ � �.�.)
        lobbyVehicleOnScene.MakeThisVehicleActive();
    }

    private void SetVehicleData()
    {
        vehicleData = lobbyVehicleOnScene.GetVehicleData();
    }
}
