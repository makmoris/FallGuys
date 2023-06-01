using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceFinishSector : MonoBehaviour
{
    private List<WheelVehicle> finishedDriversList = new List<WheelVehicle>();

    public event System.Action<WheelVehicle> ThisDriverFinishedEvent;

    public void FinishTrigger(WheelVehicle wheelVehicleDriver)
    {
        if (!finishedDriversList.Contains(wheelVehicleDriver))
        {
            finishedDriversList.Add(wheelVehicleDriver);

            ThisDriverFinishedEvent?.Invoke(wheelVehicleDriver);
        }
    }
}
