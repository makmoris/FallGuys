using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDynamicPlatformParent : MonoBehaviour
{
    private RaceSectorWithHorizontalDynamicPlatform raceSectorWithHorizontalDynamicPlatform;
    private float desiredSpeed;

    [SerializeField] private List<GameObject> carsOnPlatform = new List<GameObject>();
    private Dictionary<GameObject, RaceDriverAI> raceDriversDictionary = new Dictionary<GameObject, RaceDriverAI>();


    private void Awake()
    {
        raceSectorWithHorizontalDynamicPlatform = transform.GetComponentInParent<RaceSectorWithHorizontalDynamicPlatform>();
        desiredSpeed = raceSectorWithHorizontalDynamicPlatform.DesiredSpeedOnPlatform;
    }

    private void CheckCar(GameObject car)
    {
        if (carsOnPlatform.Contains(car))
        {
            car.transform.SetParent(transform);

            // когда сделали дочерним, значит авто полноценно на платформе
            //if (raceSectorWithDynamicPlatform != null) raceSectorWithDynamicPlatform.CarEnteredThePlatform(car);
        }
    }

    private void CarOnPlatformCanGo(GameObject platform)
    {
        if (carsOnPlatform.Count > 0)
        {
            foreach (var car in carsOnPlatform)
            {
                RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();

                if (raceDriverAI != null)
                {
                    raceDriverAI.Brake = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (raceDriversDictionary.ContainsKey(other.gameObject))
        {
            RaceDriverAI raceDriverAI = raceDriversDictionary[other.gameObject];
            
            if(raceDriverAI.CurrentSpeed > desiredSpeed)
            {
                raceDriverAI.SlowDownToDesiredSpeed(desiredSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carsOnPlatform.Add(other.gameObject);
            CheckCar(other.gameObject);

            RaceDriverAI raceDriverAI = other.GetComponentInChildren<RaceDriverAI>();

            if (raceDriverAI != null)
            {
                if (!raceDriversDictionary.ContainsKey(other.gameObject))
                {
                    raceDriversDictionary.Add(other.gameObject, raceDriverAI);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carsOnPlatform.Remove(other.gameObject);

            if (raceDriversDictionary.ContainsKey(other.gameObject))
            {
                raceDriversDictionary.Remove(other.gameObject);
            }
        }
    }

}
