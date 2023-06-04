using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceProgressController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CameraFollowingOnOtherPlayers gameCamCinema;

    [Header("Start Sector")]
    [SerializeField] private RaceStartSector raceStartSector;

    [Header("Finish Sector")]
    [SerializeField] private RaceFinishSector raceFinishSector;
    
    [Header("Race Settings")]
    [SerializeField] private int numberOfWinners = 6;
    private int currentNumberOfWinners;
    private bool raceNotOver;

    [Header("Race Progress UI Controller")]
    [SerializeField] private RaceProgressUIController raceProgressUIController;

    private GameObject currentPlayer;
    private bool currentPlayerWasFinished;

    private bool congratulationWasShowing;

    private List<WheelVehicle> raceDriversList = new List<WheelVehicle>();
    private Dictionary<WheelVehicle, RaceDriverAI> raceDriversAIDictionary = new Dictionary<WheelVehicle, RaceDriverAI>();
    [SerializeField]private List<WheelVehicle> winnersList = new List<WheelVehicle>();

    private void OnEnable()
    {
        raceProgressUIController.RaceCanStartEvent += StartRacing;
        raceFinishSector.ThisDriverFinishedEvent += DriverFinished;
    }

    private void Awake()
    {
        raceProgressUIController.SetNumberOfWinners(numberOfWinners);
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentPlayer = player;
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

        gameCamCinema.AddDriver(playerGO);
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

        raceNotOver = true;
    }

    private void DriverFinished(WheelVehicle wheelVehicleDriver)
    {
        if (raceNotOver)
        {
            // ��������� ���������� �� ����������� ����� �������
            currentNumberOfWinners++;
            if (currentNumberOfWinners <= numberOfWinners) raceProgressUIController.UpdateNumberOfWinners(currentNumberOfWinners);

            if (raceDriversAIDictionary.ContainsKey(wheelVehicleDriver))
            {
                RaceDriverAI driverAI = raceDriversAIDictionary[wheelVehicleDriver];

                driverAI.Handbrake = true;
            }

            wheelVehicleDriver.Handbrake = true;

            if (currentNumberOfWinners < numberOfWinners)
            {
                winnersList.Add(wheelVehicleDriver);

                if (wheelVehicleDriver.gameObject == currentPlayer)
                {
                    currentPlayerWasFinished = true;
                    raceProgressUIController.ShowPlayerCongratulations();

                    raceProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                }
            }
            else
            {
                winnersList.Add(wheelVehicleDriver);

                if (wheelVehicleDriver.gameObject == currentPlayer)
                {
                    currentPlayerWasFinished = true;
                    raceProgressUIController.ShowPlayerCongratulations();

                    raceProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                }

                // ���� ����� �����������, � ����� �� ����� �� ������, ������ �� ��������
                if (!currentPlayerWasFinished)
                {
                    raceProgressUIController.ShowPlayerLose();

                    raceProgressUIController.LoseShowingOverEvent += LoseShowingOver;
                }

                // ���� ������� ��������� ��������� � ���� ������ ��� ���� �������� ������, �� ���������� ���� �����������
                if (congratulationWasShowing) ShowPostRacingWindow();
                // ���� ������� ���������, �� ���� ������ ��� ������������, �� ���� ��� ����������. �� ���������� ���� ����������� ��������� ����

                StopRacing();

                Debug.Log("Race ended");// ����������� �����. ��������� �� ��������� ����
            }

            gameCamCinema.RemoveDriver(wheelVehicleDriver.gameObject);
        }
    }

    private void ShowPostRacingWindow()
    {
        Debug.Log("SHOW POST RACING WINDOW");
        gameCamCinema.CanFollowOnOtherPlayers = false;
        raceProgressUIController.ShowPostRacingWindow();
    }

    private void CongratulationsOver()
    {
        raceProgressUIController.CongratulationsOverEvent -= CongratulationsOver;

        congratulationWasShowing = true;

        if (raceNotOver)
        {
            // ���� ����� �� ��������, �� ������ �� �������
            Debug.Log("ZAPUSK CAMERY");

            gameCamCinema.CanFollowOnOtherPlayers = true;
            gameCamCinema.ChangeTarget();

            raceProgressUIController.ShowCameraHintText();
        }
        else
        {
            ShowPostRacingWindow();
        }
    }

    private void LoseShowingOver()
    {
        raceProgressUIController.LoseShowingOverEvent -= LoseShowingOver;

        // ����� ��������. ����� ����������� 
        ShowPostRacingWindow();
    }

    private void StopRacing()
    {
        raceNotOver = false;

        foreach (var driver in raceDriversList) // ��� ������������
        {
            driver.Handbrake = true;

            if (raceDriversAIDictionary.ContainsKey(driver))
            {
                RaceDriverAI driverAI = raceDriversAIDictionary[driver];

                driverAI.Handbrake = true;
            }
        }
    }

    private void OnDisable()
    {
        raceProgressUIController.RaceCanStartEvent -= StartRacing;
        raceFinishSector.ThisDriverFinishedEvent -= DriverFinished;
    }
}
