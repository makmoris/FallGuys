using UnityEngine;
using System;
using VehicleBehaviour;

public class EnterSectorTrigger : MonoBehaviour
{
    internal event Action<WheelVehicle> CarEnteredTheSectorEvent;

    private void OnTriggerEnter(Collider other)
    {
        WheelVehicle car = other.GetComponent<WheelVehicle>();
        if(car != null) CarEnteredTheSectorEvent?.Invoke(car);
    }
}
