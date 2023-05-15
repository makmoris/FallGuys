using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformWaitZone : MonoBehaviour
{
    [SerializeField] private float desiredSpeedInZone = 30f;
    [Space]
    [SerializeField] private RaceSectorWithDynamicPlatform raceSectorWithDynamicPlatform;
    [Space]
    [SerializeField] private List<ObstalceMovement> platforms;
    
    private List<RaceDriverAI> carsAIInWaitZone = new List<RaceDriverAI>();

    private void OnEnable()
    {
        foreach (var platform in platforms)
        {
            platform.ObstacleAnEnterPositionEvent += PlatformAnEnterPosition;
            platform.ObstacleStartedMovingEvent += PlatformStartedMoving;
        }
    }

    private void PlatformAnEnterPosition(GameObject platformGO)
    {
        if (carsAIInWaitZone.Count != 0)
        {
            ObstalceMovement platform = platformGO.GetComponent<ObstalceMovement>();

            if (platform != null)
            {
                List<RaceDriverAI> raceDriversAIList = raceSectorWithDynamicPlatform.GetCarsForThisPlatform(platform);

                if (raceDriversAIList.Count > 0)
                {
                    foreach (var car in raceDriversAIList)
                    {
                        car.Handbrake = false;
                    }
                }
            }
        }
    }

    private void PlatformStartedMoving(GameObject platformGO)
    {
        if (carsAIInWaitZone.Count != 0)
        {
            ObstalceMovement platform = platformGO.GetComponent<ObstalceMovement>();

            foreach (var carAI in carsAIInWaitZone)
            {
                ObstalceMovement platformForThisCar = raceSectorWithDynamicPlatform.GetPlatformForThisCar(carAI);

                if (platformForThisCar == platform)
                {
                    carAI.Handbrake = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            RaceDriverAI carAI = other.GetComponent<RaceDriverAI>();
            if (carAI != null)
            {
                carsAIInWaitZone.Add(carAI);

                ObstalceMovement platformForThisCar = raceSectorWithDynamicPlatform.GetPlatformForThisCar(carAI);

                if (!platformForThisCar.ObstacleAnEntryPosition)
                {
                    carAI.Handbrake = true;
                }
                else
                {
                    if (carAI.CurrentSpeed > desiredSpeedInZone)
                    {
                        carAI.SlowDownToDesiredSpeed(desiredSpeedInZone);
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            RaceDriverAI carAI = other.GetComponent<RaceDriverAI>();
            if (carAI != null)
            {
                carsAIInWaitZone.Remove(carAI);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var platform in platforms)
        {
            platform.ObstacleAnEnterPositionEvent -= PlatformAnEnterPosition;
            platform.ObstacleStartedMovingEvent -= PlatformStartedMoving;
        }
    }
}
