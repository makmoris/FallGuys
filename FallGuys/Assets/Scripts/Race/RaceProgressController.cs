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

    [Header("Race Settings")]
    [SerializeField] private int numberOfWinners = 6;
    private int currentNumberOfWinners;

    [Header("Race Progress UI Controller")]
    [SerializeField] private RaceProgressUIController raceProgressUIController;

    private List<WheelVehicle> raceDriversList = new List<WheelVehicle>();
    private Dictionary<WheelVehicle, RaceDriverAI> raceDriversAIDictionary = new Dictionary<WheelVehicle, RaceDriverAI>();
    private List<WheelVehicle> winnersList = new List<WheelVehicle>();

    private void OnEnable()
    {
        raceProgressUIController.RaceCanStartEvent += StartRacing;
        raceFinishSector.ThisDriverFinishedEvent += DriverFinished;
    }

    private void Awake()
    {
        raceProgressUIController.SetNumberOfWinners(numberOfWinners);
    }

    public void AddPlayer(GameObject playerGO)
    {
        WheelVehicle wheelVehiclePlayer = playerGO.GetComponent<WheelVehicle>();

        raceDriversList.Add(wheelVehiclePlayer);

        RaceDriverAI raceDriverAI = playerGO.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriversAIDictionary.Add(wheelVehiclePlayer, raceDriverAI);
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

            if (raceDriversAIDictionary.ContainsKey(driver))
            {
                RaceDriverAI driverAI = raceDriversAIDictionary[driver];

                driverAI.Handbrake = false;
                driverAI.StartMoveForward();
            }
        }
    }

    private void DriverFinished(WheelVehicle wheelVehicleDriver)
    {
        // обновляем статистику по завершенным гонку игрокам
        currentNumberOfWinners++;
        if (currentNumberOfWinners <= numberOfWinners) raceProgressUIController.UpdateNumberOfWinners(currentNumberOfWinners);

        if (raceDriversAIDictionary.ContainsKey(wheelVehicleDriver))
        {
            RaceDriverAI driverAI = raceDriversAIDictionary[wheelVehicleDriver];

            driverAI.Handbrake = true;
        }

        wheelVehicleDriver.Handbrake = true;

        if (currentNumberOfWinners < numberOfWinners) winnersList.Add(wheelVehicleDriver);
        else Debug.Log("Race ended");
    }

    private void OnDisable()
    {
        raceProgressUIController.RaceCanStartEvent -= StartRacing;
        raceFinishSector.ThisDriverFinishedEvent -= DriverFinished;
    }
}
