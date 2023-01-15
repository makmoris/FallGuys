using UnityEngine;

public class VehicleId : MonoBehaviour
{
    [SerializeField] private string _vehicleId;

    public string VehicleID
    {
        get { return _vehicleId; }
    }
}
