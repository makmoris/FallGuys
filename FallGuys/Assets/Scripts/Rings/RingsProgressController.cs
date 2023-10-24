using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VehicleBehaviour;

public class RingsProgressController : LevelProgressController
{
    [Header("-----")]
    [Header("Game Timer")]
    [SerializeField] private float playTimeSeconds = 60f;
    [SerializeField] private GameTimer gameTimer;

    [Header("Points for Ring")]
    [SerializeField] private int pointsValue = 1;

    private Dictionary<GameObject, int> playerPointsDictionary = new Dictionary<GameObject, int>();

    private RingsProgressUIController ringsProgressUIController;

    private int numberOfWinners;
    private bool gameUntilTheFirstWinner;

    private bool isObserverMode;
    [SerializeField]private GameObject currentFollowingPlayer;

    private event System.Action GameTimerFinishedEvent;

    protected virtual void Awake()
    {
        ringsProgressUIController = levelProgressUIController as RingsProgressUIController;
    }

    public override void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        _playersList.Add(playerGO);

        if (isCurrentPlayer) _currentPlayer = playerGO;

        playerPointsDictionary.Add(playerGO, 0);

        WheelVehicle wheelVehicle = playerGO.GetComponent<WheelVehicle>();
        wheelVehicle.Handbrake = true;
    }

    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        numberOfWinners = _numberOfWinners;

        ringsProgressUIController.SetNumberOfWinners(numberOfWinners);
    }

    protected override void StartGame()
    {
        foreach (var player in _playersList)
        {
            WheelVehicle wheelVehicle = player.GetComponent<WheelVehicle>();
            wheelVehicle.Handbrake = false;
        }

        if(_currentPlayer != null)
        {
            string nearestEnemyName = _playersList[_playersList.Count - 1].GetComponent<PlayerName>().Name;
            ringsProgressUIController.UpdatePointsText(_playersList.Count, 0, 1, 0, "You", nearestEnemyName, false);
        }

        //gameTimer.GameTimerFinishedEvent += GameTimeIsOver;
        GameTimerFinishedEvent += GameTimeIsOver;
        gameTimer.StartTimer(playTimeSeconds, GameTimerFinishedEvent);
    }

    protected override void ShowPostWindow()
    {
        if (isObserverMode) cameraFollowingOnOtherPlayers.StartFollowingANewPlayerEvent -= CurrentFollowingPlayerChanged;

        SendListOfLosersNamesToGameManager();

        postLevelPlaceController.ShowPostPlace(_winnersList, _losersList, _isCurrentPlayerWinner, _currentPlayer);
    }

    public void AddPointsToPlayer(GameObject playerGO)
    {
        playerPointsDictionary[playerGO] += pointsValue;

        // проверяем таблицу игроков, меняем местами, отображаем в ui
        UpdatePlayerPlacesList();
    }

    public void IsObserverMode(bool isObserverMode)
    {
        this.isObserverMode = isObserverMode;

        if (isObserverMode)
        {
            cameraFollowingOnOtherPlayers.StartFollowingANewPlayerEvent += CurrentFollowingPlayerChanged;
            currentFollowingPlayer = cameraFollowingOnOtherPlayers.GetCurrentDriver();
        }
        else currentFollowingPlayer = _currentPlayer;
    }

    private void GameTimeIsOver()
    {
        //gameTimer.GameTimerFinishedEvent -= GameTimeIsOver;
        GameTimerFinishedEvent -= GameTimeIsOver;

        SortTheWinners();
    }

    private void SortTheWinners()
    {
        List<KeyValuePair<GameObject, int>> sortingPlayersPlaces = playerPointsDictionary.OrderByDescending(d => d.Value).ToList();

        if (numberOfWinners == 1)
        {
            if (sortingPlayersPlaces[0].Value == sortingPlayersPlaces[1].Value)
            {
                // если равные очки, то продолжаем игру до первого победителя
                gameUntilTheFirstWinner = true;
                return;
            }
            else
            {
                _winnersList.Add(sortingPlayersPlaces[0].Key);
                if (sortingPlayersPlaces[0].Key == _currentPlayer) _isCurrentPlayerWinner = true;

                for (int i = 1; i < sortingPlayersPlaces.Count; i++)
                {
                    _losersList.Add(sortingPlayersPlaces[i].Key);
                }
            }
        }
        else
        {
            for (int i = 0; i < numberOfWinners; i++)
            {
                _winnersList.Add(sortingPlayersPlaces[i].Key);
                if (sortingPlayersPlaces[i].Key == _currentPlayer) _isCurrentPlayerWinner = true;
            }

            for (int i = numberOfWinners; i < sortingPlayersPlaces.Count; i++)
            {
                _losersList.Add(sortingPlayersPlaces[i].Key);
            }
        }

        _losersList.Reverse();

        if (_isCurrentPlayerWinner)
        {
            ringsProgressUIController.CongratulationsOverEvent += CongratulationsOver;
            ringsProgressUIController.ShowCongratilationsPanel();
        }
        else
        {
            ringsProgressUIController.LoseShowingOverEvent += LoseShowingOver;
            ringsProgressUIController.ShowLosingPanel();
        }

        foreach (var player in _playersList)
        {
            player.GetComponent<WheelVehicle>().Handbrake = true;
        }
    }

    private void UpdatePlayerPlacesList()
    {
        List<KeyValuePair<GameObject, int>> sortingPlayersPlaces = playerPointsDictionary.OrderByDescending(d => d.Value).ToList();

        if(currentFollowingPlayer == null && isObserverMode) currentFollowingPlayer = cameraFollowingOnOtherPlayers.GetCurrentDriver();

        if (sortingPlayersPlaces[0].Key == currentFollowingPlayer)
        {
            string currentFollowingPlayerName = currentFollowingPlayer.GetComponent<PlayerName>().Name;
            if (currentFollowingPlayerName == "Player") currentFollowingPlayerName = "You";

            string nearestEnemyName = sortingPlayersPlaces[1].Key.GetComponent<PlayerName>().Name;

            ringsProgressUIController.UpdatePointsText(1, playerPointsDictionary[currentFollowingPlayer], 2, sortingPlayersPlaces[1].Value,
                currentFollowingPlayerName, nearestEnemyName, true);
        }
        else
        {
            int playerPlace = 0;
            int playerPoints = 0;

            for (int i = 0; i < sortingPlayersPlaces.Count; i++)
            {
                if(sortingPlayersPlaces[i].Key == currentFollowingPlayer)
                {
                    playerPlace = i + 1;
                    playerPoints = sortingPlayersPlaces[i].Value;

                    break;
                }
            }

            string currentFollowingPlayerName = currentFollowingPlayer.GetComponent<PlayerName>().Name;
            if (currentFollowingPlayerName == "Player") currentFollowingPlayerName = "You";

            string nearestEnemyName = sortingPlayersPlaces[0].Key.GetComponent<PlayerName>().Name;

            bool isCurrentFollowingPlayerPass;
            if (playerPlace <= numberOfWinners) isCurrentFollowingPlayerPass = true;
            else isCurrentFollowingPlayerPass = false;

            ringsProgressUIController.UpdatePointsText(playerPlace, playerPoints, 1, sortingPlayersPlaces[0].Value,
                currentFollowingPlayerName, nearestEnemyName, isCurrentFollowingPlayerPass);
        }

        if (gameUntilTheFirstWinner) SortTheWinners();
    }

    private void CurrentFollowingPlayerChanged(GameObject currentFollowingPlayer)
    {
        this.currentFollowingPlayer = currentFollowingPlayer;
        UpdatePlayerPlacesList();
    }

    private void CongratulationsOver()
    {
        ringsProgressUIController.CongratulationsOverEvent -= CongratulationsOver;

        ShowPostWindow();
    }

    private void LoseShowingOver()
    {
        ringsProgressUIController.LoseShowingOverEvent -= LoseShowingOver;

        ringsProgressUIController.ShowObserverUI();
        cameraFollowingOnOtherPlayers.EnableObserverMode();

        ShowPostWindow();
    }
}
