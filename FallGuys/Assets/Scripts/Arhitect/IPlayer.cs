using UnityEngine;

public interface IPlayer : IPlayerData
{
    public GameObject VehiclePrefab { get; }
    public PlayerDefaultData PlayerDefaultData { get; }
    public Weapon Weapon { get; }
}
