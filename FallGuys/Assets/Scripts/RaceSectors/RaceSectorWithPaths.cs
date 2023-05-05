using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceSectorWithPaths : MonoBehaviour
{
    [SerializeField] private EnterSectorTrigger enterSectorTrigger;
    [SerializeField] private ExitSectorTrigger exitSectorTrigger;
    [Space]
    [SerializeField] private List<RaceWaypointsPath> raceWaypointsPaths;

    [SerializeField]private List<WheelVehicle> carsInSector = new List<WheelVehicle>();

    private void OnEnable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent += CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent += CarLeftTheSector;
    }

    public void CarEnteredTheSector(WheelVehicle car)
    {
        carsInSector.Add(car);

        RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriverAI.SetNewWaypointsPath(GetWaypointsPath());
        }
    }

    public void CarLeftTheSector(WheelVehicle car)
    {
        carsInSector.Remove(car);
    }

    public RaceWaypointsPath GetWaypointsPath()
    {
        int i = Random.Range(0, raceWaypointsPaths.Count);
        return raceWaypointsPaths[i];
    }

    private void OnDisable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent -= CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent -= CarLeftTheSector;
    }
}
