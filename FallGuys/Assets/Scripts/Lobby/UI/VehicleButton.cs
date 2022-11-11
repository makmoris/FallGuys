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
        // ѕќ Ќј∆ј“»ё ƒќЋ∆Ќџ ќ“ќЅ–ј∆ј“№ ¬џЅ–јЌЌќ≈, Ќќ Ќ≈ ѕќ ”ѕј“№. —ќ’–јЌ≈Ќ»≈ ¬џЅќ–ј “ќЋ№ ќ ѕќ—Ћ≈ ѕќ ”ѕ »

        // сделать сначала проверку. ≈сли все гуд, то делаем активной
        // в момент выбора подт€гивать остальную инфу из LobbyVehicleData через эту тачу и кидать ее на нужные места (текст, иконка и т.д.)
        lobbyVehicleOnScene.MakeThisVehicleActive();
    }

    private void SetVehicleData()
    {
        vehicleData = lobbyVehicleOnScene.GetVehicleData();
    }
}
