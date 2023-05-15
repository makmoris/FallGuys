using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatform : MonoBehaviour
{
    private RaceSectorWithDynamicPlatform raceSectorWithDynamicPlatform;
    private ObstalceMovement platformMovementParent;

    [SerializeField] private List<GameObject> carsOnPlatform = new List<GameObject>();

    private void OnEnable()
    {
        DynamicPlatformBorder.CarLeftTheDynamicPlatformBorderEvent += CheckCar;

        platformMovementParent = transform.GetComponentInParent<ObstalceMovement>();
        if (platformMovementParent != null) platformMovementParent.ObstacleAnExitPositionEvent += CarOnPlatformCanGo;
    }

    private void Awake()
    {
        raceSectorWithDynamicPlatform = transform.GetComponentInParent<RaceSectorWithDynamicPlatform>();
    }

    private void CheckCar(GameObject car)
    {
        if (carsOnPlatform.Contains(car))
        {
            car.transform.SetParent(transform);

            // когда сделали дочерним, значит авто полноценно на платформе
            raceSectorWithDynamicPlatform.CarEnteredThePlatform(car);
        }
    }

    private void CarOnPlatformCanGo(GameObject platform)
    {
        if (carsOnPlatform.Count > 0)
        {
            foreach (var car in carsOnPlatform)
            {
                RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();

                if (raceDriverAI != null)
                {
                    raceDriverAI.Handbrake = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carsOnPlatform.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carsOnPlatform.Remove(other.gameObject);
            other.gameObject.transform.SetParent(null);
        }
    }

    private void OnDisable()
    {
        DynamicPlatformBorder.CarLeftTheDynamicPlatformBorderEvent -= CheckCar;

        if (platformMovementParent != null) platformMovementParent.ObstacleAnExitPositionEvent -= CarOnPlatformCanGo;
    }
}
