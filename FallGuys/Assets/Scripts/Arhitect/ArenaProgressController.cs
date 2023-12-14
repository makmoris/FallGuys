using ArcadeVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class ArenaProgressController : LevelProgressController
{
    private ArenaProgressUIController arenaProgressUIController;

    [Header("-----")]
    [SerializeField] private int numberOfPlayers;
    [SerializeField] private int numberOfFrags;
    private int numberOfWinners;

    [SerializeField] private int amountOfGoldReward;
    [SerializeField] private int amountOfCupReward;

    private int playerFinishPlace;
    private int startNumberOfPlayers;

    [Header("Pause When Observe Mode Enabled")]
    [SerializeField] private float pauseBeforShowingPostWindowForObserver = 1.5f;

    public static ArenaProgressController Instance { get; private set; }
    private void Awake()
    {
        arenaProgressUIController = levelProgressUIController as ArenaProgressUIController;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        VisualIntermediary.PlayerWasDeadEvent += ReduceNumberOfPlayers;
    }

    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        numberOfPlayers = _numberOfPlayers;
        numberOfFrags = 0;
        startNumberOfPlayers = _numberOfPlayers;

        numberOfWinners = _numberOfWinners;

        UpdateLeftText();
        UpdateFragText();

        if (numberOfPlayers == 2)
        {
            StartDuel();
        }
    }

    public override void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        _playersList.Add(playerGO);

        if (isCurrentPlayer) _currentPlayer = playerGO;

        ArcadeVehicleController arcadeVehicleController = playerGO.GetComponent<ArcadeVehicleController>();
        arcadeVehicleController.Handbrake = true;
    }

    public void AddFrag()// вызывается из VisualIntermediary
    {
        numberOfFrags++;

        amountOfGoldReward += LeagueManager.Instance.GetGoldRewardForFrag();
        amountOfCupReward += LeagueManager.Instance.GetCupRewardForFrag();
        Debug.Log("Add FRAG");
        UpdateFragText();

        _gameManager.SetNumberOfFrags(numberOfFrags);
    }

    public void AddGold(int value)// вызывается из PlayerEffector за подбор бонуса
    {
        amountOfGoldReward += value;
    }

    protected override void StartGame()
    {
        foreach (var player in _playersList)
        {
            ArcadeVehicleController arcadeVehicleController = player.GetComponent<ArcadeVehicleController>();
            arcadeVehicleController.Handbrake = false;
        }
    }

    private void ReduceNumberOfPlayers(GameObject deadPlayer)
    {
        if (!_isGameEnded)
        {
            numberOfPlayers--;
            if (numberOfPlayers < 0) numberOfPlayers = 0;

            UpdateLeftText();

            _losersList.Add(deadPlayer);
            _playersList.Remove(deadPlayer);

            deadPlayer.GetComponent<ArcadeVehicleController>().Handbrake = true;

            if (deadPlayer == _currentPlayer)
            {
                // показываем луз окно игроку
                arenaProgressUIController.LoseShowingOverEvent += LoseShowingOver;
                arenaProgressUIController.ShowLosingPanel();
            }
            else
            {
                cameraFollowingOnOtherPlayers.RemoveDriver(deadPlayer);
            }

            if (numberOfPlayers == numberOfWinners)
            {
                _isGameEnded = true;

                foreach (var player in _playersList)
                {
                    if (player == _currentPlayer)
                    {
                        _isCurrentPlayerWinner = true;

                        // показываем окно победы игроку
                        arenaProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                        arenaProgressUIController.ShowCongratilationsPanel();
                    }

                    _winnersList.Add(player);

                    player.GetComponent<ArcadeVehicleController>().Handbrake = true;
                }

                if (!_isCurrentPlayerWinner)
                {
                    StartCoroutine(WaitAndShowPostWindow());
                }

                //DisabledAllChildElements();
                //CalculateReward(numberOfPlayers);
                //StartCoroutine(WaitAndShowWinWindow());
            }
            else if (numberOfPlayers == 2)   // сделать список игроков и брать оттуда двух оставшихся
            {
                StartDuel();
            }
        }
    }

    private void StartDuel()
    {
        ArenaCarDriverAI carDriverAIFirst = _playersList[0].GetComponentInChildren<ArenaCarDriverAI>();
        if (carDriverAIFirst != null) carDriverAIFirst.StartDuel(_playersList[1].transform);

        ArenaCarDriverAI carDriverAISecond = _playersList[1].GetComponentInChildren<ArenaCarDriverAI>();
        if (carDriverAISecond != null) carDriverAISecond.StartDuel(_playersList[0].transform);
    }

    private void CalculateReward(int place)
    {
        playerFinishPlace = place;

        amountOfGoldReward += LeagueManager.Instance.ReceiveGoldsAsReward(place);
        amountOfCupReward += LeagueManager.Instance.ReceiveCupsAsReward(place);

        Debug.Log($"Награда: Золото - {amountOfGoldReward}; Кубки - {amountOfCupReward}");

        CurrencyManager.Instance.AddGold(amountOfGoldReward);
        CurrencyManager.Instance.AddCup(amountOfCupReward);
    }

    private void DisabledAllChildElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject chield = transform.GetChild(i).gameObject;
            if (chield.activeSelf) chield.SetActive(false);
        }
    }

    private void UpdateLeftText()
    {
        arenaProgressUIController.UpdateLeftText(numberOfPlayers);
    }

    private void UpdateFragText()
    {
        arenaProgressUIController.UpdateFragText(numberOfFrags);
    }

    public int GetCurrentNumberOfFrags()
    {
        return numberOfFrags;
    }
    public int GetCurrentAmountOfGoldReward()
    {
        return amountOfGoldReward;
    }
    public int GetCurrentEnemiesLeft()
    {
        return numberOfPlayers - 1;
    }

    private void CongratulationsOver()
    {
        arenaProgressUIController.CongratulationsOverEvent -= CongratulationsOver;

        if (_isCurrentPlayerWinner)
        {
            ShowPostWindow();
        }
    }

    private void LoseShowingOver()
    {
        SendListOfLosersNamesToGameManager();

        arenaProgressUIController.LoseShowingOverEvent -= LoseShowingOver;

        cameraFollowingOnOtherPlayers.RemoveDriver(_currentPlayer);

        arenaProgressUIController.ShowObserverUI();
        cameraFollowingOnOtherPlayers.EnableObserverMode();
    }

    protected override void ShowPostWindow()
    {
        SendListOfLosersNamesToGameManager();

        postLevelPlaceController.ShowPostPlace(_winnersList, _losersList, _isCurrentPlayerWinner, _currentPlayer);
    }

    IEnumerator WaitAndShowPostWindow()
    {
        yield return new WaitForSeconds(pauseBeforShowingPostWindowForObserver);
        ShowPostWindow();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        VisualIntermediary.PlayerWasDeadEvent -= ReduceNumberOfPlayers;
    }
}
