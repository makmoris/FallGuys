using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceSectorWithPaths : MonoBehaviour
{
    [SerializeField] private EnterSectorTrigger enterSectorTrigger;
    [SerializeField] private ExitSectorTrigger exitSectorTrigger;

    [Header("AI Behavior")]
    [SerializeField] private BrakeCondition brakeCondition = BrakeCondition.NeverBrake;

    [Header("Path")]
    [SerializeField] private WaypointsPathOutputType waypointsPathType = WaypointsPathOutputType.nearestPath;
    [Space]
    [SerializeField] private List<RaceWaypointsPath> raceWaypointsPathsList;
    private int queuePathIndex;

    private List<WheelVehicle> carsInSector = new List<WheelVehicle>();

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
            raceDriverAI.SetNewWaypointsPath(GetWaypointsPath(raceDriverAI.transform.position));
            raceDriverAI.brakeCondition = brakeCondition;
        }
    }

    public void CarLeftTheSector(WheelVehicle car)
    {
        carsInSector.Remove(car);
    }

    public RaceWaypointsPath GetWaypointsPath(Vector3 carPosition)
    {
        RaceWaypointsPath raceWaypointsPath = null;

        switch (waypointsPathType)
        {
            case WaypointsPathOutputType.queuePath:

                int index = queuePathIndex + 1;

                if(index < raceWaypointsPathsList.Count)
                {
                    raceWaypointsPath = raceWaypointsPathsList[index];
                    queuePathIndex = index;
                }
                else
                {
                    raceWaypointsPath = raceWaypointsPathsList[0];
                    queuePathIndex = 0;
                }

                break;

            case WaypointsPathOutputType.randomPath:

                int r = Random.Range(0, raceWaypointsPathsList.Count);
                raceWaypointsPath = raceWaypointsPathsList[r];

                break;

            case WaypointsPathOutputType.nearestPath:

                float minDistance = Mathf.Infinity;
                int indexMinDistance = 0;

                for (int i = 0; i < raceWaypointsPathsList.Count; i++)
                {
                    Vector3 pointPosition = raceWaypointsPathsList[i].GetFirstPointPosition();

                    float distance = Vector3.Distance(carPosition, pointPosition);

                    if (distance <= minDistance)
                    {
                        minDistance = distance;
                        indexMinDistance = i;
                    }
                }

                raceWaypointsPath = raceWaypointsPathsList[indexMinDistance];

                break;
        }

        return raceWaypointsPath;
    }

    private void OnDisable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent -= CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent -= CarLeftTheSector;
    }
}
