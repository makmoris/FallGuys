using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class HoneycombProgressController : LevelProgressController
{
    [Header("-----")]
    [Header("Pause When Observe Mode Enabled")]
    [SerializeField] private float pauseBeforShowingPostWindowForObserver = 1.5f;
    [Space]
    [SerializeField] private float disableWeaponTimeOnStart = 5f;

    [Header("Targets Controller")]
    [SerializeField] private TargetsControllerHoneycomb targetsControllerHoneycomb;

    private HoneycombProgressUIController honeycombProgressUIController;

    private int numberOfWinners;
    private int numberOfFallens;// сколько должно упасть
    private int currentNumberOfFallens;

    protected virtual void Awake()
    {
        honeycombProgressUIController = levelProgressUIController as HoneycombProgressUIController;
    }


    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        numberOfWinners = _numberOfWinners;
        numberOfFallens = _numberOfPlayers - _numberOfWinners;
        honeycombProgressUIController.SetNumberOfFallens(numberOfFallens);

        foreach (var player in _playersList)
        {
            ApplyDisableBonus(player, disableWeaponTimeOnStart);
        }
    }

    public override void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        _playersList.Add(playerGO);

        if (isCurrentPlayer) _currentPlayer = playerGO;

        WheelVehicle wheelVehicle = playerGO.GetComponent<WheelVehicle>();
        wheelVehicle.Handbrake = true;
    }

    protected override void StartGame()
    {
        foreach (var player in _playersList)
        {
            WheelVehicle wheelVehicle = player.GetComponent<WheelVehicle>();
            wheelVehicle.Handbrake = false;
        }

        targetsControllerHoneycomb.EnableSpawnPlaces();
    }

    public void PlayerOut(GameObject playerGO)
    {
        if (!_isGameEnded)
        {
            _losersList.Add(playerGO);
            _playersList.Remove(playerGO);

            currentNumberOfFallens++;

            playerGO.SetActive(false);
            playerGO.GetComponent<WheelVehicle>().Handbrake = true;

            ApplyDisableBonus(playerGO, Mathf.Infinity);

            if (playerGO == _currentPlayer)
            {
                // показываем луз окно игроку
                honeycombProgressUIController.LoseShowingOverEvent += LoseShowingOver;
                honeycombProgressUIController.ShowLosingPanel();
            }
            else cameraFollowingOnOtherPlayers.RemoveDriver(playerGO);

            if (currentNumberOfFallens == numberOfFallens)
            {
                _isGameEnded = true;

                foreach (var player in _playersList)
                {
                    _winnersList.Add(player);

                    player.GetComponent<WheelVehicle>().Handbrake = true;

                    if(player == _currentPlayer)
                    {
                        _isCurrentPlayerWinner = true;

                        honeycombProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                        honeycombProgressUIController.ShowCongratilationsPanel();
                    }

                    ApplyDisableBonus(player, Mathf.Infinity);
                }

                if (!_isCurrentPlayerWinner)
                {
                    StartCoroutine(WaitAndShowPostWindow());
                }
            }

            // update ui
            honeycombProgressUIController.UpdateNamberOfFallens(currentNumberOfFallens);
        }
    }

    protected override void ShowPostWindow()
    {
        SendListOfLosersNamesToGameManager();

        postLevelPlaceController.ShowPostPlace(_winnersList, _losersList, _isCurrentPlayerWinner, _currentPlayer);
    }

    private void ApplyDisableBonus(GameObject driverGO, float disableTime)
    {
        DisableWeaponBonus disableWeaponBonus = new();
        disableWeaponBonus.Type = BonusType.DisableWeapon;
        disableWeaponBonus.Value = disableTime;

        Bumper bumper = driverGO.GetComponent<Bumper>();
        bumper.GetBonusWithGameObject(disableWeaponBonus, driverGO);
    }

    private void LoseShowingOver()
    {
        SendListOfLosersNamesToGameManager();

        honeycombProgressUIController.LoseShowingOverEvent -= LoseShowingOver;

        cameraFollowingOnOtherPlayers.RemoveDriver(_currentPlayer);

        honeycombProgressUIController.ShowObserverUI();
        cameraFollowingOnOtherPlayers.EnableObserverMode();
    }

    private void CongratulationsOver()
    {
        honeycombProgressUIController.CongratulationsOverEvent -= CongratulationsOver;

        if (_isCurrentPlayerWinner)
        {
            ShowPostWindow();
        }
    }

    IEnumerator WaitAndShowPostWindow()
    {
        yield return new WaitForSeconds(pauseBeforShowingPostWindowForObserver);
        ShowPostWindow();
    }

}
