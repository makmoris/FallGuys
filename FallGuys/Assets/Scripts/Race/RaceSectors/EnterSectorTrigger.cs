using UnityEngine;
using System;
using VehicleBehaviour;

public class EnterSectorTrigger : MonoBehaviour
{
    public event Action<WheelVehicle> CarEnteredTheSectorEvent;

    private void OnTriggerEnter(Collider other)
    {
        WheelVehicle car = other.GetComponent<WheelVehicle>();
        if (car != null)
        {
            RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();
            if (raceDriverAI != null) raceDriverAI.StopMoveForward();

            SendCarEnteredEvent(car);
        }
    }

    protected virtual void SendCarEnteredEvent(WheelVehicle car)
    {
        CarEnteredTheSectorEvent?.Invoke(car);
    }
}
