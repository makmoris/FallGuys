using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public abstract class RaceSectorLogic : MonoBehaviour
{
    public void CarEnteredTheSectorFromMultipleEnterTrigger(WheelVehicle car)
    {
        CarEnteredTheSector(car);
    }
    protected abstract void CarEnteredTheSector(WheelVehicle car);
    protected abstract void CarLeftTheSector(WheelVehicle car);
}
