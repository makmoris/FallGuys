using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleButton : MonoBehaviour
{
    [SerializeField] private LobbyVehicle lobbyVehicleOnScene;
    private LobbyVehicleData lobbyVehicleData;
    private LobbyManager lobbyManager;

    [Header("Button Components")]
    [SerializeField] private TextMeshProUGUI weaponNameOnButton;
    [SerializeField] private GameObject choiseImage;
    [SerializeField] private GameObject borderImage;
    [SerializeField] private GameObject lockImage;

    private TextMeshProUGUI nameText;

    private Button applyButton;
    private TextMeshProUGUI applyText;
    private Button buyButton;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI cupsText;

    private Vector2 oldCupsTextPosition;

    private WeaponContent weaponContent;

    [Space]
    [SerializeField] private bool isActiveWeapon;

    [Header("Data values")]
    [SerializeField] private string _vehicleName;
    //[SerializeField] private Sprite weaponIcon;
    [SerializeField] private int _vehicleCost;
    [SerializeField] private int _vehicleCupsToUnlock;
    [SerializeField] private bool _isVehicleAvailable;
    [SerializeField] private WeaponCharacteristicsData _vehicleDefaultData;

    private bool dataWasLoaded;

    public void SelectVehicle()
    {
        // ѕќ Ќј∆ј“»ё ƒќЋ∆Ќџ ќ“ќЅ–ј∆ј“№ ¬џЅ–јЌЌќ≈, Ќќ Ќ≈ ѕќ ”ѕј“№. —ќ’–јЌ≈Ќ»≈ ¬џЅќ–ј “ќЋ№ ќ ѕќ—Ћ≈ ѕќ ”ѕ »

        // сделать сначала проверку. ≈сли все гуд, то делаем активной
        // в момент выбора подт€гивать остальную инфу из LobbyVehicleData через эту тачу и кидать ее на нужные места (текст, иконка и т.д.)
        lobbyVehicleOnScene.MakeThisVehicleActive();
    }

    private void SetVehicleData()
    {
        lobbyVehicleData = lobbyVehicleOnScene.GetVehicleData();
    }
}
