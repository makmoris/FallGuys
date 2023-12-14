using ArcadeVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public abstract class RaceSectorLogic : MonoBehaviour
{
    public void CarEnteredTheSectorFromMultipleEnterTrigger(ArcadeVehicleController car)
    {
        CarEnteredTheSector(car);
    }
    protected abstract void CarEnteredTheSector(ArcadeVehicleController car);
    protected abstract void CarLeftTheSector(ArcadeVehicleController car);
}
