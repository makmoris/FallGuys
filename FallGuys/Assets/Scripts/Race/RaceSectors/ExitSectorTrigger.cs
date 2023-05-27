using UnityEngine;
using System;
using VehicleBehaviour;

public class ExitSectorTrigger : MonoBehaviour
{
    public event Action<WheelVehicle> CarLeftTheSectorEvent;

    private void OnTriggerExit(Collider other)
    {
        WheelVehicle car = other.GetComponent<WheelVehicle>();
        if (car != null)
        {
            CarLeftTheSectorEvent?.Invoke(car);

            RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
            if (raceDriverAI != null) raceDriverAI.StartMoveForward();
        }
    }
}
