using UnityEngine;
using System;
using VehicleBehaviour;
using ArcadeVP;

public class ExitSectorTrigger : MonoBehaviour
{
    public event Action<ArcadeVehicleController> CarLeftTheSectorEvent;

    private void OnTriggerEnter(Collider other)
    {
        ArcadeVehicleController car = other.GetComponent<ArcadeVehicleController>();
        if (car != null)
        {
            CarLeftTheSectorEvent?.Invoke(car);

            RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();
            //if (raceDriverAI != null) raceDriverAI.StartMoveForward();
        }
    }
}
