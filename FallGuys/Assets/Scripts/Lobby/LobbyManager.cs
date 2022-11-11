using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private int activeLobbyVehicleIndex;// ������� �������� ����� � ������ - ��, ��� ������������ �� �������
    [SerializeField] private int activeLobbyWeaponIndex;

    [Space]
    [SerializeField] private Transform vehiclePlace;

    [Header("Lobby objects")] [Space]
    [SerializeField] private GameObject vehicleObjects;
    [SerializeField] private List<LobbyVehicle> lobbyVehicles; // ����� ������ ����� GO ��������� �� �������� �������� ������ �� ������, ����� �� 
    [SerializeField] private GameObject weaponObjects;
    [SerializeField] private List<LobbyWeapon> lobbyWeapons;// ���������� �������� �������

    [SerializeField] private GameObject activeVehicle;
    [SerializeField] private GameObject activeWeapon;

    private void Awake()
    {
        GetLobbyObjects();
    }

    private void Start()
    {
        CheckInactivity(lobbyVehicles);
        CheckInactivity(lobbyWeapons);

        activeVehicle = GetActiveVehicle();
        activeWeapon = GetActiveWeapon();

        ShowActiveVehicle();
        ShowActiveWeapon();
    }
                                        /// ����� ����� ����������, ����� ����� ���������� � ���������� LOBBYVEHICLE � LOBBYWEAPON
                                        /// ����� �������� �� ��� ������ �� ������� ������� � �������� ��� ������� � CHARACTERMANAGER
                                        /// ��� ����� ������� ������, ����� ������� ������

    public void ChangeActiveVehicle(GameObject _lobbyVehicle)// ���������� lobbyvehicle
    {
        int newActiveIndex = FindLobbyVehicleIndex(_lobbyVehicle);
        activeLobbyVehicleIndex = newActiveIndex;

        activeVehicle.SetActive(false);
        activeVehicle = GetActiveVehicle();
        ShowActiveVehicle();
        ShowActiveWeapon();// �.�. ����� ���� ������ ����� ������������ �����
    }

    public void ChangeActiveWeapon(GameObject _lobbyWeapon)// ���������� lobbyWeapon
    {
        int newActiveIndex = FindLobbyWeaponIndex(_lobbyWeapon);
        activeLobbyWeaponIndex = newActiveIndex;

        activeWeapon.SetActive(false);
        activeWeapon = GetActiveWeapon();
        ShowActiveWeapon();
    }

    public GameObject GetActiveVehicle()
    {
        GameObject activeVehicle = lobbyVehicles[activeLobbyVehicleIndex].gameObject;
        return activeVehicle;
    }

    public GameObject GetActiveWeapon()
    {
        GameObject activeWeapon = lobbyWeapons[activeLobbyWeaponIndex].gameObject;
        return activeWeapon;
    }

    private void ShowActiveVehicle()
    {
        activeVehicle.transform.position = new Vector3(vehiclePlace.position.x, activeVehicle.transform.position.y,
            vehiclePlace.position.z);
        activeVehicle.transform.rotation = vehiclePlace.rotation;// ������� ���� ����� ��� �����, �� ����� ���������� �����, ��� �� ��������� �������

        activeVehicle.SetActive(true);
    }

    private void ShowActiveWeapon()
    {
        Transform weaponPlace = lobbyVehicles[activeLobbyVehicleIndex].GetWeaponPlace();

        activeWeapon.transform.position = weaponPlace.position;
        activeWeapon.transform.rotation = weaponPlace.rotation;

        activeWeapon.SetActive(true);
    }

    private void CheckInactivity<T>(List<T> list) where T: MonoBehaviour
    {
        foreach (var value in list)
        {
            if (value.gameObject.activeSelf) value.gameObject.SetActive(false);
        }
    }

    private int FindLobbyVehicleIndex(GameObject _lobbyVehicle)
    {
        int index = 0;

        foreach (var vehicle in lobbyVehicles)
        {
            if (lobbyVehicles[index].gameObject == _lobbyVehicle)
            {
                break;
            }

            index++;
        }

        return index;
    }

    private int FindLobbyWeaponIndex(GameObject _lobbyWeapon)
    {
        int index = 0;

        foreach (var weapon in lobbyWeapons)
        {
            if (lobbyWeapons[index].gameObject == _lobbyWeapon)
            {
                break;
            }

            index++;
        }

        return index;
    }

    private void GetLobbyObjects()
    {
        for (int i = 0; i < vehicleObjects.transform.childCount; i++)
        {
            var veh = vehicleObjects.transform.GetChild(i).gameObject;
            lobbyVehicles.Add(veh.GetComponent<LobbyVehicle>());
        }

        for (int i = 0; i < weaponObjects.transform.childCount; i++)
        {
            var veh = weaponObjects.transform.GetChild(i).gameObject;
            lobbyWeapons.Add(veh.GetComponent<LobbyWeapon>());
        }
    }
}
