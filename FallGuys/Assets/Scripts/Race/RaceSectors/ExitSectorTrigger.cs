using UnityEngine;
using System;
using VehicleBehaviour;

public class ExitSectorTrigger : MonoBehaviour
{
    public event Action<WheelVehicle> CarLeftTheSectorEvent;

    private void OnTriggerEnter(Collider other)
    {
        WheelVehicle car = other.GetComponent<WheelVehicle>();
        if (car != null)
        {
            CarLeftTheSectorEvent?.Invoke(car);

            RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();
            //if (raceDriverAI != null) raceDriverAI.StartMoveForward();
        }
    }
}
