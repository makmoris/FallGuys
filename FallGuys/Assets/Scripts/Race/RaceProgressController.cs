using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceProgressController : MonoBehaviour
{
    [Header("Start Sector")]
    [SerializeField] private RaceStartSector raceStartSector;

    [Header("Finish Sector")]
    [SerializeField] private RaceFinishSector raceFinishSector;

    [Header("Race Progress UI Controller")]
    [SerializeField] private RaceProgressUIController raceProgressUIController;

    private List<WheelVehicle> raceDriversList = new List<WheelVehicle>();
    private List<RaceDriverAI> raceDriversAIList = new List<RaceDriverAI>();

    private void OnEnable()
    {
        raceProgressUIController.RaceCanStartEvent += StartRacing;
    }

    public void AddPlayer(GameObject playerGO)
    {
        WheelVehicle wheelVehiclePlayer = playerGO.GetComponent<WheelVehicle>();

        raceDriversList.Add(wheelVehiclePlayer);

        RaceDriverAI raceDriverAI = playerGO.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriversAIList.Add(raceDriverAI);
            raceDriverAI.Handbrake = true;
        }

        wheelVehiclePlayer.Handbrake = true;
    }

    public RaceStartSector GetRaceStartSector()
    {
        return raceStartSector;
    }

    private void StartRacing()
    {
        foreach (var driver in raceDriversList)
        {
            driver.Handbrake = false;
        }

        foreach (var driverAI in raceDriversAIList)
        {
            driverAI.Handbrake = false;
            driverAI.StartMoveForward();
        }
    }

    private void OnDisable()
    {
        raceProgressUIController.RaceCanStartEvent -= StartRacing;
    }
}
