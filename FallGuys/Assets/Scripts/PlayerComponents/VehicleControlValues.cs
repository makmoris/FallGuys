using ArcadeVP;
using UnityEngine;

public abstract class VehicleControlValues : MonoBehaviour
{
    [Header("Arena")]
    [Min(1)][SerializeField] private float maxSpeedArena;
    [Min(1)] [SerializeField] private float accelarationArena;
    [Min(1)] [SerializeField] private float turnArena;

    [Header("Race")]
    [Min(1)] [SerializeField] private float maxSpeedRace;
    [Min(1)] [SerializeField] private float accelarationRace;
    [Min(1)] [SerializeField] private float turnRace;

    [Header("Honeycomb")]
    [Min(1)] [SerializeField] private float maxSpeedHoneycomb;
    [Min(1)] [SerializeField] private float accelarationHoneycomb;
    [Min(1)] [SerializeField] private float turnHoneycomb;

    [Header("Rings")]
    [Min(1)] [SerializeField] private float maxSpeedRings;
    [Min(1)] [SerializeField] private float accelarationRings;
    [Min(1)] [SerializeField] private float turnRings;

    private ArcadeVehicleController arcadeVehicleController;

    public void UseArenaControlValues()
    {
        arcadeVehicleController = GetComponentInParent<ArcadeVehicleController>();

        arcadeVehicleController.MaxSpeed = maxSpeedArena;
        arcadeVehicleController.Accelaration = accelarationArena;
        arcadeVehicleController.Turn = turnArena;
    }

    public void UseRaceControlValues()
    {
        arcadeVehicleController = GetComponentInParent<ArcadeVehicleController>();

        arcadeVehicleController.MaxSpeed = maxSpeedRace;
        arcadeVehicleController.Accelaration = accelarationRace;
        arcadeVehicleController.Turn = turnRace;
    }

    public void UseHoneycombControlValues()
    {
        arcadeVehicleController = GetComponentInParent<ArcadeVehicleController>();

        arcadeVehicleController.MaxSpeed = maxSpeedHoneycomb;
        arcadeVehicleController.Accelaration = accelarationHoneycomb;
        arcadeVehicleController.Turn = turnHoneycomb;
    }

    public void UseRingsControlValues()
    {
        arcadeVehicleController = GetComponentInParent<ArcadeVehicleController>();

        arcadeVehicleController.MaxSpeed = maxSpeedRings;
        arcadeVehicleController.Accelaration = accelarationRings;
        arcadeVehicleController.Turn = turnRings;
    }
}
