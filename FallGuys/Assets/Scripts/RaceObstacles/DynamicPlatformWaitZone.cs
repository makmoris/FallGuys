using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformWaitZone : MonoBehaviour
{
    [SerializeField] private float desiredSpeedInZone = 30f;
    [Space]
    [SerializeField] private RaceSectorWithDynamicPlatform raceSectorWithDynamicPlatform;
    [Space]
    [SerializeField] private List<ObstalceMovement> platforms;
    [Space]
    [SerializeField] private List<RaceDriverAI> carsAIInWaitZone = new List<RaceDriverAI>();

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
                        Debug.Log(car.name + "Can go to platform");
                        car.Handbrake = false;
                    }
                }

                Debug.Log(platform.name);
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
                    Debug.Log(carAI.name + " Сразу залетает на платформу"); // можно добавить какую-нибудь проверку на скорость. Если больше N, то тормоз
                    if (carAI.CurrentSpeed > desiredSpeedInZone)
                    {
                        Debug.Log($"Скорость {carAI.name} = {carAI.CurrentSpeed} больше чем допустимая {desiredSpeedInZone}");
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
