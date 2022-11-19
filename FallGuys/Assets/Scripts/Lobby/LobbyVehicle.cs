using UnityEngine;

public class LobbyVehicle : MonoBehaviour
{
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private Transform weaponPlace;
    [SerializeField] private LobbyVehicleData vehicleData;

    public void MakeThisVehicleActive()// вызывается кнопкой
    {
        LobbyManager.Instance.ChangeActiveVehicle(this.gameObject);
    }

    public Transform GetWeaponPlace()
    {
        return weaponPlace;
    }

    public void ShowThisVehicle()
    {
        LobbyManager.Instance.ShowActiveVehicleInLobby(this.gameObject);
    }

    public void BackToActiveVehicle()
    {
        LobbyManager.Instance.BackToShowActiveVehicle();
    }

    public LobbyVehicleData GetLobbyVehicleData()
    {
        return vehicleData;
    }
}
