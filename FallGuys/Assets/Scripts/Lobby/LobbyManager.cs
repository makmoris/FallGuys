using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private int activeLobbyVehicleIndex;// индексы активной тачки и оружия - то, что отображается на подиуме
    [SerializeField] private int activeLobbyWeaponIndex;

    [Space]
    [SerializeField] private Transform vehiclePlace;

    [Header("Lobby objects")] [Space]
    [SerializeField] private GameObject vehicleObjects;
    [SerializeField] private List<LobbyVehicle> lobbyVehicles; // потом сдеать одним GO родителем из которого получать список на старте, чтобы не 
    [SerializeField] private GameObject weaponObjects;
    [SerializeField] private List<LobbyWeapon> lobbyWeapons;// переносить элементы вручную

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
                                        /// КОГДА ВЫБОР ЗАКОНЧИТСЯ, НУЖНО БУДЕТ ОБРАТИТЬСЯ К ВЫБРАННОМУ LOBBYVEHICLE И LOBBYWEAPON
                                        /// ЧТОБЫ ВЫТАЩИТЬ ИЗ НИХ ССЫЛКИ НА ИГРОВЫЕ ПРЕФАБЫ И ПЕРЕДАТЬ ЭТИ ПРЕФАБЫ В CHARACTERMANAGER
                                        /// ЭТО ПЕРЕД СТАРТОМ УРОВНЯ, ЧТОБЫ ГРУЗИТЬ НУЖНЫЕ

    public void ChangeActiveVehicle(GameObject _lobbyVehicle)// вызывается lobbyvehicle
    {
        int newActiveIndex = FindLobbyVehicleIndex(_lobbyVehicle);
        activeLobbyVehicleIndex = newActiveIndex;

        activeVehicle.SetActive(false);
        activeVehicle = GetActiveVehicle();
        ShowActiveVehicle();
        ShowActiveWeapon();// т.к. могут быть разные точки расположения пушки
    }

    public void ChangeActiveWeapon(GameObject _lobbyWeapon)// вызывается lobbyWeapon
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
        activeVehicle.transform.rotation = vehiclePlace.rotation;// вращаем саму тачку как хотим, но когда включается новая, она по дефолтной ротации

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
