using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

[System.Serializable]
internal class PathAndPlatform
{
    [SerializeField] internal RaceWaypointsPath path;
    [SerializeField] internal ObstalceMovement platform;
}

public class RaceSectorWithDynamicPlatform : MonoBehaviour
{
    [SerializeField] private EnterSectorTrigger enterSectorTrigger;
    [SerializeField] private ExitSectorTrigger exitSectorTrigger;

    [Header("Platforms")]
    [SerializeField] private List<PathAndPlatform> pathAndPlatformList;
    [Space]
    [SerializeField] private List<WheelVehicle> carsInSector = new List<WheelVehicle>();

    private Dictionary<RaceDriverAI, PathAndPlatform> raceDriverAIWithPathAndPlatformDictionary = new Dictionary<RaceDriverAI, PathAndPlatform>();

    private void OnEnable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent += CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent += CarLeftTheSector;
    }

    internal void CarEnteredThePlatform(GameObject enteredCar)
    {
        RaceDriverAI raceDriverAI = enteredCar.GetComponent<RaceDriverAI>();

        if(raceDriverAI != null)
        {
            raceDriverAI.Handbrake = true;
        }
    }

    internal ObstalceMovement GetPlatformForThisCar(RaceDriverAI raceDriverAI)
    {
        return raceDriverAIWithPathAndPlatformDictionary[raceDriverAI].platform;
    }

    internal List<RaceDriverAI> GetCarsForThisPlatform(ObstalceMovement platform)
    {
        PathAndPlatform _pathAndPlatform = null;

        foreach (var plat in pathAndPlatformList)
        {
            if (platform == plat.platform)
            {
                _pathAndPlatform = plat;
                break;
            }
        }

        if (_pathAndPlatform != null)
        {
            List<RaceDriverAI> raceDriverAIList = new List<RaceDriverAI>();

            foreach (var kvp in raceDriverAIWithPathAndPlatformDictionary)
            {
                if (kvp.Value == _pathAndPlatform)
                {
                    raceDriverAIList.Add(kvp.Key);
                }
            }

            return raceDriverAIList;
        }

        return null;
    }

    private void CarEnteredTheSector(WheelVehicle car)
    {
        carsInSector.Add(car);

        RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            AddToRaceDriverAIWithPathAndPlatformDictionary(raceDriverAI);
            raceDriverAI.SetNewWaypointsPath(GetWaypointsPath(raceDriverAI));
        }
    }

    private void CarLeftTheSector(WheelVehicle car)
    {
        carsInSector.Remove(car);
        // отписаться от событий плафтормы и удалить из всех списокв и словарей
    }

    private RaceWaypointsPath GetWaypointsPath(RaceDriverAI raceDriverAI)
    {
        return raceDriverAIWithPathAndPlatformDictionary[raceDriverAI].path;
    }

    private void AddToRaceDriverAIWithPathAndPlatformDictionary(RaceDriverAI carAI)
    {
        int i = Random.Range(0, pathAndPlatformList.Count);

        raceDriverAIWithPathAndPlatformDictionary.Add(carAI, pathAndPlatformList[i]);
    }

    private void CheckingNeedToWait(RaceDriverAI raceDriverAI)
    {
        bool canEnterToPlanform = raceDriverAIWithPathAndPlatformDictionary[raceDriverAI].platform.ObstacleAnEntryPosition;

        if (!canEnterToPlanform)
        {
            //raceDriverAI.EnteredTheBrakeZone();
            raceDriverAI.Handbrake = true;
        }
    }

    private void OnDisable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent -= CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent -= CarLeftTheSector;
    }
}
