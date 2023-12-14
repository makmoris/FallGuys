using ArcadeVP;
using UnityEngine;
using VehicleBehaviour;

public class DirectionSetterHorizontalDP : MonoBehaviour
{
    [SerializeField] private ExitSectorTrigger exitSectorTrigger;

    private Transform carTransform;
    [SerializeField]private GameObject carGO;
    private bool startDirecting;

    private void OnEnable()
    {
        exitSectorTrigger.CarLeftTheSectorEvent += CarLeftTheSector;
    }

    public void SetCarTransform(Transform _carTransform)
    {
        carTransform = _carTransform;
        carGO = _carTransform.gameObject;
        startDirecting = true;
    }

    private void Update()
    {
        if (startDirecting)
        {
            transform.position = new Vector3(carTransform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void CarLeftTheSector(ArcadeVehicleController car)
    {
        if(car.gameObject == carGO) Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        exitSectorTrigger.CarLeftTheSectorEvent -= CarLeftTheSector;
    }
}
