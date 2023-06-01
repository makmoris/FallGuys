using UnityEngine;
using VehicleBehaviour;

public class RaceFinishTrigger : MonoBehaviour
{
    RaceFinishSector raceFinishSector;

    private void Awake()
    {
        raceFinishSector = transform.GetComponentInParent<RaceFinishSector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            WheelVehicle wheelVehicleDriver = other.GetComponent<WheelVehicle>();

            if (wheelVehicleDriver != null)
            {
                raceFinishSector.FinishTrigger(wheelVehicleDriver);
            }
        }
    }
}
