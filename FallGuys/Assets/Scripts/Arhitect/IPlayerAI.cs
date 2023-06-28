using UnityEngine;

public interface IPlayerAI : IPlayerData
{
    public Material VehicleColorMaterial { get; }
    public Weapon Weapon { get; }

    public void SetDefaultData(PlayerDefaultData playerDefaultData);
}
