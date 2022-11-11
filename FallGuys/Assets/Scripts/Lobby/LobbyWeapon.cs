using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyWeapon : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyManager;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private LobbyWeaponData weaponData;

    public void MakeThisWeaponActive()// вызывается кнопкой
    {
        lobbyManager.ChangeActiveWeapon(this.gameObject);
    }

}
