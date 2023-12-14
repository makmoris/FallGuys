using UnityEngine;
using System;
using VehicleBehaviour;
using ArcadeVP;

public class EnterSectorTrigger : MonoBehaviour
{
    public event Action<ArcadeVehicleController> CarEnteredTheSectorEvent;

    private void OnTriggerEnter(Collider other)
    {
        ArcadeVehicleController car = other.GetComponent<ArcadeVehicleController>();
        if (car != null)
        {
            RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();
            //if (raceDriverAI != null) raceDriverAI.StopMoveForward();

            SendCarEnteredEvent(car);
        }
    }

    protected virtual void SendCarEnteredEvent(ArcadeVehicleController car)
    {
        CarEnteredTheSectorEvent?.Invoke(car);
    }
}
