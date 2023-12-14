using ArcadeVP;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceProgressController : LevelProgressController
{
   [Header("-----")]
    [Header("Start Sector")]
    [SerializeField] private RaceStartSector raceStartSector;
    [Space]
    [SerializeField] private float disableWeaponTimeOnStart = 5f;

    [Header("Finish Sector")]
    [SerializeField] private RaceFinishSector raceFinishSector;

    [Header("Race Settings")]
    [SerializeField] private int numberOfPlayersToStartFinishTimer = 2;
    [SerializeField] private float finishTimerSeconds = 30f;
    private event System.Action FinishTimerFinished;

    [Header("Waypoints Paths")]
    [SerializeField] private WaypointsPath waypointsPath;

    private int numberOfWinners;
    private int currentNumberOfWinners;
    private bool raceNotOver;

    private RaceProgressUIController raceProgressUIController;

    private bool currentPlayerWasFinished;

    private bool congratulationWasShowing;

    private List<GameObject> raceDriversList = new List<GameObject>();
    private Dictionary<GameObject, ArcadeVehicleController> raceDriversPlayersDictionary = new Dictionary<GameObject, ArcadeVehicleController>();
    private Dictionary<GameObject, RaceDriverAI> raceDriversAIDictionary = new Dictionary<GameObject, RaceDriverAI>();
    [SerializeField]private List<GameObject> winnersList = new List<GameObject>();

    protected override void OnEnable()
    {
        base.OnEnable();

        raceFinishSector.ThisDriverFinishedEvent += DriverFinished;
    }

    private void Awake()
    {
        raceProgressUIController = levelProgressUIController as RaceProgressUIController;

        if (!postLevelPlaceController.gameObject.activeSelf) postLevelPlaceController.gameObject.SetActive(true);

        raceProgressUIController.SetNumberOfWinners(numberOfWinners);
    }

    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        numberOfWinners = _numberOfWinners;
        raceProgressUIController.SetNumberOfWinners(numberOfWinners);
    }

    public override void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        raceDriversList.Add(playerGO);
        _playersList.Add(playerGO);

        RaceDriverAI raceDriverAI = playerGO.GetComponentInChildren<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriversAIDictionary.Add(playerGO, raceDriverAI);
            raceDriverAI.Handbrake = true;
        }
        else
        {
            ArcadeVehicleController arcadeVehicleControllerPlayer = playerGO.GetComponent<ArcadeVehicleController>();

            raceDriversPlayersDictionary.Add(playerGO, arcadeVehicleControllerPlayer);
            arcadeVehicleControllerPlayer.Handbrake = true;
        }

        if (isCurrentPlayer) _currentPlayer = playerGO;
    }

    public RaceStartSector GetRaceStartSector()
    {
        return raceStartSector;
    }

    public WaypointsPath GetWaypointsPath()
    {
        return waypointsPath;
    }

    protected override void StartGame()
    {
        foreach (var driver in raceDriversList)
        {
            if (raceDriversPlayersDictionary.ContainsKey(driver))
            {
                ArcadeVehicleController arcadeVehicleController = raceDriversPlayersDictionary[driver];

                arcadeVehicleController.Handbrake = false;
            }

            if (raceDriversAIDictionary.ContainsKey(driver))
            {
                RaceDriverAI driverAI = raceDriversAIDictionary[driver];

                driverAI.Handbrake = false;
                //driverAI.StartMoveForward();
            }

            ApplyDisableBonus(driver.gameObject, disableWeaponTimeOnStart);
        }

        raceNotOver = true;
    }

    private void ApplyDisableBonus(GameObject driverGO, float disableTime)
    {
        DisableWeaponBonus disableWeaponBonus = new(disableTime);

        Bumper bumper = driverGO.GetComponent<Bumper>();
        bumper.GetBonus(disableWeaponBonus, driverGO);
    }

    private void DriverFinished(ArcadeVehicleController arcadeVehicleController)
    {
        if (raceNotOver)
        {
            // обновляем статистику по завершенным гонку игрокам
            currentNumberOfWinners++;
            if (currentNumberOfWinners <= numberOfWinners) raceProgressUIController.UpdateNumberOfWinners(currentNumberOfWinners);

            if(currentNumberOfWinners == numberOfPlayersToStartFinishTimer)
            {
                // запуск таймера
                FinishTimerFinished += FinishTimeIsOver;
                raceProgressUIController.StartFinishTimer(finishTimerSeconds, FinishTimerFinished);
            }

            if (raceDriversAIDictionary.ContainsKey(arcadeVehicleController.gameObject))
            {
                RaceDriverAI driverAI = raceDriversAIDictionary[arcadeVehicleController.gameObject];

                //driverAI.Brake = true;
                //driverAI.SlowDownToDesiredSpeed(0f);
                driverAI.Handbrake = true;

                ApplyDisableBonus(arcadeVehicleController.gameObject, Mathf.Infinity);
            }

            arcadeVehicleController.Handbrake = true;

            if (currentNumberOfWinners < numberOfWinners)
            {
                winnersList.Add(arcadeVehicleController.gameObject);
                _winnersList.Add(arcadeVehicleController.gameObject);

                if (arcadeVehicleController.gameObject == _currentPlayer)
                {
                    currentPlayerWasFinished = true;
                    raceProgressUIController.ShowCongratilationsPanel();

                    raceProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                }
            }
            else
            {
                winnersList.Add(arcadeVehicleController.gameObject);
                _winnersList.Add(arcadeVehicleController.gameObject);

                if (arcadeVehicleController.gameObject == _currentPlayer)
                {
                    currentPlayerWasFinished = true;
                    raceProgressUIController.ShowCongratilationsPanel();

                    raceProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                }

                // если гонка завершилась, а игрок не дошел до финиша, значит он проиграл
                if (!currentPlayerWasFinished)
                {
                    raceProgressUIController.ShowLosingPanel();

                    raceProgressUIController.LoseShowingOverEvent += LoseShowingOver;
                }

                // если приехал последний противник и окно победы уже было показано игроку, то показываем окно сплющивания
                if (congratulationWasShowing) ShowPostWindow();
                // если приехал противник, но окно победы еще показывается, то ждем его завершения. По завершению окно сплющивание покажется само

                StopRacing();

                FinishTimerFinished -= FinishTimeIsOver;

                Debug.Log("Race ended");// заканчиваем гонку. Переходим на следующий этап
            }

            cameraFollowingOnOtherPlayers.RemoveDriver(arcadeVehicleController.gameObject);
        }
    }

    protected override void ShowPostWindow()
    {
        Debug.Log("SHOW POST RACING WINDOW");

        //List<GameObject> losersListWheelVehicle = new List<GameObject>();

        foreach (var driver in raceDriversList)
        {
            if (!winnersList.Contains(driver))
            {
                _losersList.Add(driver.gameObject);
                //losersListWheelVehicle.Add(driver);

                if (raceDriversPlayersDictionary.ContainsKey(driver))
                {
                    ArcadeVehicleController arcadeVehicleController = raceDriversPlayersDictionary[driver];

                    arcadeVehicleController.Handbrake = true;
                }

                if (raceDriversAIDictionary.ContainsKey(driver))
                {
                    RaceDriverAI driverAI = raceDriversAIDictionary[driver];

                    driverAI.Handbrake = true;
                }

                ApplyDisableBonus(driver.gameObject, Mathf.Infinity);
            }
        }

        foreach (var kvp in raceDriversAIDictionary)
        {
            GameObject aiDriver = kvp.Key.gameObject;

            ObstacleDetectionAIOld obstacleDetectionAI = aiDriver.GetComponentInChildren<ObstacleDetectionAIOld>();
            if (obstacleDetectionAI != null) obstacleDetectionAI.enabled = false;

            GroundDetectionAI groundDetectionAI = aiDriver.GetComponentInChildren<GroundDetectionAI>();
            if (groundDetectionAI != null) groundDetectionAI.enabled = false;

            RaceDriverAI raceDriverAI = kvp.Value;
            raceDriverAI.enabled = false;

            RaceAIInputs raceAIInputs = aiDriver.GetComponent<RaceAIInputs>();
            if (raceAIInputs != null) raceAIInputs.enabled = false;

            RaceAIWaipointTracker raceAIWaipointTracker = aiDriver.GetComponentInChildren<RaceAIWaipointTracker>();
            if (raceAIWaipointTracker != null) raceAIWaipointTracker.enabled = false;
        }

        SortLosersByPosition();
        SendListOfLosersNamesToGameManager();

        raceProgressUIController.HideCameraHint();

        postLevelPlaceController.ShowPostPlace(_winnersList, _losersList, _isCurrentPlayerWinner, _currentPlayer);
    }

    private void SortLosersByPosition()
    {
        GameObject loser;
        for (int i = 0; i < _losersList.Count; i++)
        {
            for (int j = i + 1; j < _losersList.Count; j++)
            {
                if (_losersList[j].transform.position.z < _losersList[i].transform.position.z)
                {
                    loser = _losersList[i];
                    _losersList[i] = _losersList[j];
                    _losersList[j] = loser;
                }
            }
        }
    }

    private void CongratulationsOver()
    {
        _isCurrentPlayerWinner = true;

        raceProgressUIController.CongratulationsOverEvent -= CongratulationsOver;

        congratulationWasShowing = true;

        if (raceNotOver)
        {
            // если гонка не окончена, то следим за другими
            Debug.Log("ZAPUSK CAMERY");

            cameraFollowingOnOtherPlayers.EnableObserverMode();

            raceProgressUIController.ShowCameraHint();
        }
        else
        {
            ShowPostWindow();
        }
    }

    private void LoseShowingOver()
    {
        raceProgressUIController.LoseShowingOverEvent -= LoseShowingOver;

        raceProgressUIController.ShowObserverUI();

        // гонка окончена. Показ сплющивания 
        ShowPostWindow();
    }

    private void StopRacing()
    {
        raceNotOver = false;

        StopEveryone();

        // отправляем список лузеров
    }

    private void StopEveryone()
    {
        foreach (var driver in raceDriversList) // все остановились
        {
            if (raceDriversPlayersDictionary.ContainsKey(driver))
            {
                ArcadeVehicleController arcadeVehicleController = raceDriversPlayersDictionary[driver];

                arcadeVehicleController.Handbrake = true;
            }

            if (raceDriversAIDictionary.ContainsKey(driver))
            {
                RaceDriverAI driverAI = raceDriversAIDictionary[driver];

                driverAI.Handbrake = true;
            }
        }
    }

    private void FinishTimeIsOver()
    {
        FinishTimerFinished -= FinishTimeIsOver;

        // всех остановили
        StopEveryone();

        // нашли оставшихся
        List<GameObject> remainingPlayers = new List<GameObject>();
        foreach (var player in _playersList)
        {
            if (!_winnersList.Contains(player)) remainingPlayers.Add(player);
        }

        // отсортировали оставшихся по тому, кто ближе к финишу
        GameObject remainingPlayer;
        for (int i = 0; i < remainingPlayers.Count; i++)
        {
            for (int j = i + 1; j < remainingPlayers.Count; j++)
            {
                if (remainingPlayers[j].transform.position.z > remainingPlayers[i].transform.position.z)
                {
                    remainingPlayer = remainingPlayers[i];
                    remainingPlayers[i] = remainingPlayers[j];
                    remainingPlayers[j] = remainingPlayer;
                }
            }
        }

        // взяли нужноe число оставшихся победителей
        int length = numberOfWinners - currentNumberOfWinners;
        for (int i = 0; i < length; i++)
        {
            DriverFinished(remainingPlayers[i].GetComponent<ArcadeVehicleController>());
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        raceFinishSector.ThisDriverFinishedEvent -= DriverFinished;
    }
}
