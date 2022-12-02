using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private int activeLobbyVehicleIndex;// индексы активной тачки и оружия - то, что отображается на подиуме
    [SerializeField] private int activeLobbyWeaponIndex;

    [SerializeField] private int saveActiveVehicleIndex;// индекс выбранной тачки, которая пойдет в игру

    [Space]
    [SerializeField] private Transform vehiclePlace;

    [Header("Lobby objects")] [Space]
    [SerializeField] private GameObject vehicleObjects;
    [SerializeField] private List<LobbyVehicle> lobbyVehicles; // потом сдеать одним GO родителем из которого получать список на старте, чтобы не 
    [SerializeField] private GameObject weaponObjects;
    [SerializeField] private List<LobbyWeapon> lobbyWeapons;// переносить элементы вручную

    [SerializeField] private GameObject activeVehicle;
    [SerializeField] private GameObject activeWeapon;

    private string vehicleSaveName = "vehicleIndex";
    private string weaponSaveName = "weaponIndex";

    public static LobbyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        GetLobbyObjects();
        LoadData();
    }

    private void Start()
    {
        CheckInactivity(lobbyVehicles);
        CheckInactivity(lobbyWeapons);

        activeVehicle = GetActiveLobbyVehicle();
        activeWeapon = GetActiveWeapon();

        ShowActiveVehicle();
        ShowActiveWeapon();

        saveActiveVehicleIndex = activeLobbyVehicleIndex;
    }
                                        /// КОГДА ВЫБОР ЗАКОНЧИТСЯ, НУЖНО БУДЕТ ОБРАТИТЬСЯ К ВЫБРАННОМУ LOBBYVEHICLE И LOBBYWEAPON
                                        /// ЧТОБЫ ВЫТАЩИТЬ ИЗ НИХ ССЫЛКИ НА ИГРОВЫЕ ПРЕФАБЫ И ПЕРЕДАТЬ ЭТИ ПРЕФАБЫ В CHARACTERMANAGER
                                        /// ЭТО ПЕРЕД СТАРТОМ УРОВНЯ, ЧТОБЫ ГРУЗИТЬ НУЖНЫЕ
    public void SetCharacter()// вызывается в момент запуска игрового уровня
    {
        GameObject _activeVehicle = GetActiveSaveVehicle();
        GameObject _activeWeapon = GetActiveWeapon();

        GameObject selectedVehicle = _activeVehicle.GetComponent<LobbyVehicle>().GetVehiclePrefab();
        GameObject selectedWeapon = _activeWeapon.GetComponent<LobbyWeapon>().GetWeaponPrefab();

        CharacterManager.Instance.SetCharacter(selectedVehicle, selectedWeapon);
    }

    public void LoadActiveSaveCharacter()// вызывается LobbyWindow при активации
    {
        CheckInactivity(lobbyVehicles);
        CheckInactivity(lobbyWeapons);

        activeLobbyVehicleIndex = saveActiveVehicleIndex;
        activeVehicle = GetActiveSaveVehicle();
        activeWeapon = GetActiveWeapon();

        ShowActiveVehicle();
        ShowActiveWeapon();
    }

    public void ChangeActiveVehicle(GameObject _lobbyVehicle)// вызывается lobbyvehicle
    {
        int newActiveIndex = FindLobbyVehicleIndex(_lobbyVehicle);
        activeLobbyVehicleIndex = newActiveIndex;

        activeVehicle.SetActive(false);
        activeVehicle = GetActiveLobbyVehicle();
        ShowActiveVehicle();
        ShowActiveWeapon();// т.к. могут быть разные точки расположения пушки

        // сейвить выбранный индекс авто
        saveActiveVehicleIndex = activeLobbyVehicleIndex;

        SaveNewVehicleActiveIndex(saveActiveVehicleIndex);
    }

    public void ShowActiveVehicleInLobby(GameObject _lobbyVehicle) // не сейвим
    {
        int newActiveIndex = FindLobbyVehicleIndex(_lobbyVehicle);
        activeLobbyVehicleIndex = newActiveIndex;

        activeVehicle.SetActive(false);
        activeVehicle = GetActiveLobbyVehicle();
        ShowActiveVehicle();
        ShowActiveWeapon();// т.к. могут быть разные точки расположения пушки
    }

    public void BackToShowActiveVehicle()
    {
        if (activeVehicle != null)
        {
            activeVehicle.SetActive(false);
            activeVehicle = GetActiveLobbyVehicle();
            ShowActiveVehicle();
            ShowActiveWeapon();// т.к. могут быть разные точки расположения пушки
        }
    }

    public void ChangeActiveWeapon(GameObject _lobbyWeapon)// вызывается lobbyWeapon
    {
        int newActiveIndex = FindLobbyWeaponIndex(_lobbyWeapon);
        activeLobbyWeaponIndex = newActiveIndex;

        activeWeapon.SetActive(false);
        activeWeapon = GetActiveWeapon();
        ShowActiveWeapon();

        // сейвить выбранный индекс пушки
        SaveNewWeaponActiveIndex(activeLobbyWeaponIndex);
    }

    public void ShowActiveWeaponInLobby(GameObject _lobbyWeapon)
    {
        int showingActiveIndex = FindLobbyWeaponIndex(_lobbyWeapon);

        activeWeapon.SetActive(false);
        activeWeapon = lobbyWeapons[showingActiveIndex].gameObject;

        Transform weaponPlace = lobbyVehicles[activeLobbyVehicleIndex].GetWeaponPlace();

        activeWeapon.transform.position = weaponPlace.position;
        activeWeapon.transform.rotation = weaponPlace.rotation;

        activeWeapon.SetActive(true);
    }

    public void BackToShowActiveWeapon()
    {
        if (activeWeapon != null)
        {
            activeWeapon.SetActive(false);
            activeWeapon = GetActiveWeapon();
            ShowActiveWeapon();
        }
    }

    public GameObject GetActiveLobbyVehicle()
    {
        GameObject activeVehicle = lobbyVehicles[activeLobbyVehicleIndex].gameObject;
        return activeVehicle;
    }
    public GameObject GetActiveSaveVehicle()
    {
        GameObject activeVehicle = lobbyVehicles[saveActiveVehicleIndex].gameObject;
        return activeVehicle;
    }

    public GameObject GetActiveWeapon()
    {
        GameObject activeWeapon = lobbyWeapons[activeLobbyWeaponIndex].gameObject;
        return activeWeapon;
    }

    public bool CurrentVehicleIsAvailable()// проверяем, что авто, под которым зашли в кастомизацию, доступо - куплено. Если нет, то нельзя покупать
    { // элементы кастомизации. Только смотреть. Кнопка покупки и активации заблочена

        var lobbyVehicleData = activeVehicle.GetComponent<LobbyVehicle>().GetLobbyVehicleData();
        bool isAvailable = lobbyVehicleData.IsVehicleAvailable;

        return isAvailable;
    }

    private void ShowActiveVehicle()
    {
        if (activeVehicle != null)
        {
            activeVehicle.transform.position = new Vector3(vehiclePlace.position.x, activeVehicle.transform.position.y,
            vehiclePlace.position.z);
            activeVehicle.transform.rotation = vehiclePlace.rotation;// вращаем саму тачку как хотим, но когда включается новая, она по дефолтной ротации

            activeVehicle.SetActive(true);
        }
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

    private void LoadData()
    {
        activeLobbyVehicleIndex = ElementsSelectedData.Instance.GetActiveSelectedIndex(vehicleSaveName);
        saveActiveVehicleIndex = activeLobbyVehicleIndex;
        activeLobbyWeaponIndex = ElementsSelectedData.Instance.GetActiveSelectedIndex(weaponSaveName);
        Debug.Log(ElementsSelectedData.Instance.GetActiveSelectedIndex(vehicleSaveName));
    }

    private void SaveNewVehicleActiveIndex(int vehicleActiveIndex)
    {
        ElementsSelectedData.Instance.SaveElevemtSelectedIndex(vehicleSaveName, vehicleActiveIndex);

        Debug.Log(name + " сохранил vehicleActiveIndex");
        Debug.Log(ElementsSelectedData.Instance.GetActiveSelectedIndex(vehicleSaveName));
    }

    private void SaveNewWeaponActiveIndex(int weaponActiveIndex)
    {
        ElementsSelectedData.Instance.SaveElevemtSelectedIndex(weaponSaveName, weaponActiveIndex);

        Debug.Log(name + " сохранил weaponActiveIndex");
        
    }
}
