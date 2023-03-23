using UnityEngine;
using VehicleBehaviour;

public class UnderPlayerCar : MonoBehaviour
{
    private WheelVehicle wheelVehicle;
    [SerializeField] private int immobilityValue;

    private void Awake()
    {
        wheelVehicle = transform.GetComponentInParent<WheelVehicle>();
    }

    private void OnTriggerStay(Collider other)
    {
        immobilityValue++;

        if (immobilityValue == 150)
        {
            immobilityValue = 0;
            wheelVehicle.PlayerStuckUnder();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        immobilityValue = 0;
    }
}
