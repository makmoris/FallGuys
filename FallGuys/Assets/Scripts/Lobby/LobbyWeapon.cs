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

    public void ShowThisWeapon()
    {
        lobbyManager.ShowActiveWeaponInLobby(this.gameObject);
    }

    public void BackToActiveWeapon()
    {
        lobbyManager.BackToShowActiveWeapon();
    }

    public LobbyWeaponData GetLobbyWeaponData()
    {
        return weaponData;
    }

    public LobbyManager GetLobbyManager()
    {
        return lobbyManager;
    }
}
