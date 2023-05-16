using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontalDynamicPlatformParent : MonoBehaviour
{
    private RaceSectorWithFrontalDynamicPlatform raceSectorWithDynamicPlatform;
    private ObstalceMovement platformMovementParent;

    [SerializeField] private List<GameObject> carsOnPlatform = new List<GameObject>();

    private void OnEnable()
    {
        platformMovementParent = transform.GetComponentInParent<ObstalceMovement>();
        if (platformMovementParent != null) platformMovementParent.ObstacleAnEndPositionEvent += CarOnPlatformCanGo;
    }

    private void Awake()
    {
        raceSectorWithDynamicPlatform = transform.GetComponentInParent<RaceSectorWithFrontalDynamicPlatform>();
    }

    private void CheckCar(GameObject car)
    {
        if (carsOnPlatform.Contains(car))
        {
            car.transform.SetParent(transform);

            // когда сделали дочерним, значит авто полноценно на платформе
            if (raceSectorWithDynamicPlatform != null) raceSectorWithDynamicPlatform.CarEnteredThePlatform(car);
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
            CheckCar(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carsOnPlatform.Remove(other.gameObject);
        }
    }

    private void OnDisable()
    {
        if (platformMovementParent != null) platformMovementParent.ObstacleAnEndPositionEvent -= CarOnPlatformCanGo;
    }
}
