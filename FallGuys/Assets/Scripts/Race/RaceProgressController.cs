using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceProgressController : LevelProgressController
{
    [Header("Camera")]
    [SerializeField] private CameraFollowingOnOtherPlayers gameCamCinema;

    [Header("Start Sector")]
    [SerializeField] private RaceStartSector raceStartSector;
    [Space]
    [SerializeField] private float disableWeaponTimeOnStart = 5f;

    [Header("Finish Sector")]
    [SerializeField] private RaceFinishSector raceFinishSector;
    
    [Header("Race Settings")]
    [SerializeField]private int numberOfWinners;
    private int currentNumberOfWinners;
    private bool raceNotOver;

    [Header("Race Progress UI Controller")]
    [SerializeField] private RaceProgressUIController raceProgressUIController;

    [Header("Post Race Place Controller")]
    [SerializeField] private PostRacePlaceController postRacePlaceController;

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
        if (!postRacePlaceController.gameObject.activeSelf) postRacePlaceController.gameObject.SetActive(true);

        raceProgressUIController.SetNumberOfWinners(numberOfWinners);
    }

    public void SetNumberOfWinners(int _numberOfWinners)
    {
        numberOfWinners = _numberOfWinners;
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
            raceDriverAI.Brake = true;
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

                driverAI.Brake = false;
                driverAI.StartMoveForward();
            }

            ApplyDisableBonus(driver.gameObject, disableWeaponTimeOnStart);
        }

        raceNotOver = true;
    }

    private void ApplyDisableBonus(GameObject driverGO, float disableTime)
    {
        DisableWeaponBonus disableWeaponBonus = new();
        disableWeaponBonus.Type = BonusType.DisableWeapon;
        disableWeaponBonus.Value = disableTime;

        Bumper bumper = driverGO.GetComponent<Bumper>();
        bumper.GetBonusWithGameObject(disableWeaponBonus, driverGO);
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

                //driverAI.Brake = true;
                driverAI.SlowDownToDesiredSpeed(0f);

                ApplyDisableBonus(wheelVehicleDriver.gameObject, Mathf.Infinity);
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


        List<WheelVehicle> losersList = new List<WheelVehicle>();

        foreach (var driver in raceDriversList)
        {
            if (!winnersList.Contains(driver))
            {
                losersList.Add(driver);

                ApplyDisableBonus(driver.gameObject, Mathf.Infinity);
            }
        }

        foreach (var kvp in raceDriversAIDictionary)
        {
            GameObject aiDriver = kvp.Key.gameObject;

            RaceObstacleDetectionAI raceObstacleDetectionAI = aiDriver.GetComponent<RaceObstacleDetectionAI>();
            if (raceObstacleDetectionAI != null) raceObstacleDetectionAI.enabled = false;

            RaceGroundDetectionAI raceGroundDetectionAI = aiDriver.GetComponent<RaceGroundDetectionAI>();
            if (raceGroundDetectionAI != null) raceGroundDetectionAI.enabled = false;

            RaceDriverAI raceDriverAI = kvp.Value;
            raceDriverAI.enabled = false;

            RaceAIInputs raceAIInputs = aiDriver.GetComponent<RaceAIInputs>();
            if (raceAIInputs != null) raceAIInputs.enabled = false;

            RaceAIWaipointTracker raceAIWaipointTracker = aiDriver.GetComponent<RaceAIWaipointTracker>();
            if (raceAIWaipointTracker != null) raceAIWaipointTracker.enabled = false;
        }

        postRacePlaceController.StartPostRacing(winnersList, losersList, numberOfWinners, currentPlayerWasFinished);
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

                driverAI.Brake = true;
            }
        }
    }

    private void OnDisable()
    {
        raceProgressUIController.RaceCanStartEvent -= StartRacing;
        raceFinishSector.ThisDriverFinishedEvent -= DriverFinished;
    }
}
