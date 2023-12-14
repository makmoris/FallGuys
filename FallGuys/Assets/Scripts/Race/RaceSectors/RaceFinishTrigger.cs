using ArcadeVP;
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
            ArcadeVehicleController arcadeVehicleController = other.GetComponent<ArcadeVehicleController>();

            if (arcadeVehicleController != null)
            {
                raceFinishSector.FinishTrigger(arcadeVehicleController);
            }
        }
    }
}
