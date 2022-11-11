using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] private LobbyWeapon lobbyWeaponOnScene;

    public void SelectWeapon()
    {
        // сделать сначала проверку. Если все гуд, то делаем активной
        // в момент выбора подтягивать остальную инфу из LobbyVehicleData через эту тачу и кидать ее на нужные места (текст, иконка и т.д.)
        lobbyWeaponOnScene.MakeThisWeaponActive();
    }
}
