using UnityEngine;

public interface IPlayerAI : IPlayerData
{
    public GameObject VehiclePrefab { get; }
    public Material VehicleColorMaterial { get; }
    public Weapon Weapon { get; }

    public void SetDefaultData(PlayerDefaultData playerDefaultData);
}
