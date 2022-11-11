using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] private LobbyWeapon lobbyWeaponOnScene;

    public void SelectWeapon()
    {
        // ������� ������� ��������. ���� ��� ���, �� ������ ��������
        // � ������ ������ ����������� ��������� ���� �� LobbyVehicleData ����� ��� ���� � ������ �� �� ������ ����� (�����, ������ � �.�.)
        lobbyWeaponOnScene.MakeThisWeaponActive();
    }
}
