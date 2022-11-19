using UnityEngine;

public class LobbyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private LobbyWeaponData weaponData;

    public void MakeThisWeaponActive()// вызывается кнопкой
    {
        LobbyManager.Instance.ChangeActiveWeapon(this.gameObject);
    }

    public void ShowThisWeapon()
    {
        LobbyManager.Instance.ShowActiveWeaponInLobby(this.gameObject);
    }

    public void BackToActiveWeapon()
    {
        LobbyManager.Instance.BackToShowActiveWeapon();
    }

    public LobbyWeaponData GetLobbyWeaponData()
    {
        return weaponData;
    }
}
