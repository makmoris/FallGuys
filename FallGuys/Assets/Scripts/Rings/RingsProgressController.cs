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
    }

    protected override void StartGame()
    {
        foreach (var player in _playersList)
        {
            WheelVehicle wheelVehicle = player.GetComponent<WheelVehicle>();
            wheelVehicle.Handbrake = false;
        }

        ringsProgressUIController.UpdatePointsText(_playersList.Count, 0, 1, 0);

        gameTimer.GameTimerFinishedEvent += GameTimeIsOver;
        gameTimer.StartTimer(playTimeSeconds);
    }

    protected override void ShowPostWindow()
    {
        SendListOfLosersNamesToGameManager();

        postLevelPlaceController.ShowPostPlace(_winnersList, _losersList, _isCurrentPlayerWinner);
    }

    public void AddPointsToPlayer(GameObject playerGO)
    {
        playerPointsDictionary[playerGO] += pointsValue;

        // проверяем таблицу игроков, меняем местами, отображаем в ui
        UpdatePlayerPlacesList();
    }

    private void GameTimeIsOver()
    {
        gameTimer.GameTimerFinishedEvent -= GameTimeIsOver;

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

        Debug.LogError($"Winners list count = {_winnersList.Count}; losers count = {_losersList.Count}");
    }

    private void UpdatePlayerPlacesList()
    {
        List<KeyValuePair<GameObject, int>> sortingPlayersPlaces = playerPointsDictionary.OrderByDescending(d => d.Value).ToList();


        if (sortingPlayersPlaces[0].Key == _currentPlayer)
        {
            ringsProgressUIController.UpdatePointsText(1, playerPointsDictionary[_currentPlayer], 2, sortingPlayersPlaces[1].Value);
        }
        else
        {
            int playerPlace = 0;
            int playerPoints = 0;

            for (int i = 0; i < sortingPlayersPlaces.Count; i++)
            {
                if(sortingPlayersPlaces[i].Key == _currentPlayer)
                {
                    playerPlace = i + 1;
                    playerPoints = sortingPlayersPlaces[i].Value;

                    break;
                }
            }

            ringsProgressUIController.UpdatePointsText(playerPlace, playerPoints, 1, sortingPlayersPlaces[0].Value);
        }

        if (gameUntilTheFirstWinner) SortTheWinners();
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
