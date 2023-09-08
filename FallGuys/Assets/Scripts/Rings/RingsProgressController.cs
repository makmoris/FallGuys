using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RingsProgressController : LevelProgressController
{
    private RaceProgressUIController raceProgressUIController;

    protected virtual void Awake()
    {
        raceProgressUIController = levelProgressUIController as RaceProgressUIController;
    }

    public override void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        _playersList.Add(playerGO);

        if (isCurrentPlayer) _currentPlayer = playerGO;

        WheelVehicle wheelVehicle = playerGO.GetComponent<WheelVehicle>();
        wheelVehicle.Handbrake = true;
    }

    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        //throw new System.NotImplementedException();
    }

    protected override void StartGame()
    {
        foreach (var player in _playersList)
        {
            WheelVehicle wheelVehicle = player.GetComponent<WheelVehicle>();
            wheelVehicle.Handbrake = false;
        }
    }

    protected override void ShowPostWindow()
    {
        //throw new System.NotImplementedException();
    }
}
