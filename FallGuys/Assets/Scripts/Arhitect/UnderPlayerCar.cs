using ArcadeVP;
using UnityEngine;
using VehicleBehaviour;

public class UnderPlayerCar : MonoBehaviour
{
    private ArcadeVehicleController arcadeVehicleController;
    [SerializeField] private int immobilityValue;

    private void Awake()
    {
        arcadeVehicleController = transform.GetComponentInParent<ArcadeVehicleController>();
    }

    private void OnTriggerStay(Collider other)
    {
        immobilityValue++;

        if (immobilityValue == 150)
        {
            immobilityValue = 0;
            arcadeVehicleController.PlayerStuckUnder();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        immobilityValue = 0;
    }
}
