using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceSectorWithHorizontalDynamicPlatform : MonoBehaviour
{
    [SerializeField] private EnterSectorTrigger enterSectorTrigger;
    [SerializeField] private ExitSectorTrigger exitSectorTrigger;

    [Space]
    [SerializeField] private float desiredSpeedOnPlatform = 20f;
    public float DesiredSpeedOnPlatform
    {
        get => desiredSpeedOnPlatform;
    }

    [Header("Targets")]
    [SerializeField] private List<Transform> targetsList;

    private List<WheelVehicle> carsInSector = new List<WheelVehicle>();

    private void OnEnable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent += CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent += CarLeftTheSector;
    }

    private void CarEnteredTheSector(WheelVehicle car)
    {
        if (!carsInSector.Contains(car))
        {
            carsInSector.Add(car);

            RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
            if (raceDriverAI != null)
            {
                List<Transform> newList = new();

                foreach (var elem in targetsList)
                {
                    DirectionSetterHorizontalDP directionSetterHorizontalDP = elem.GetComponent<DirectionSetterHorizontalDP>();

                    if (directionSetterHorizontalDP != null)
                    {
                        DirectionSetterHorizontalDP newDirectionSetter = Instantiate(directionSetterHorizontalDP,
                            elem.position, elem.rotation, elem.parent);

                        newList.Add(newDirectionSetter.transform);

                        newDirectionSetter.SetCarTransform(car.transform);
                    }
                    else
                    {
                        newList.Add(elem);
                    }
                }

                raceDriverAI.SetTargets(newList);

                RaceGroundDetectionAI raceGroundDetectionAI = car.GetComponent<RaceGroundDetectionAI>();
                raceGroundDetectionAI.enabled = true;
            }
        }
    }

    private void CarLeftTheSector(WheelVehicle car)
    {
        carsInSector.Remove(car);

        RaceGroundDetectionAI raceGroundDetectionAI = car.GetComponent<RaceGroundDetectionAI>();
        raceGroundDetectionAI.enabled = false;

    }

    private void OnDisable()
    {
        enterSectorTrigger.CarEnteredTheSectorEvent -= CarEnteredTheSector;
        exitSectorTrigger.CarLeftTheSectorEvent -= CarLeftTheSector;
    }
}
