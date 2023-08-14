using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class ArenaProgressController : LevelProgressController
{
    [Header("-----")]
    [Header("Camera")]
    [SerializeField] private CameraFollowingOnOtherPlayers cameraFollowingOnOtherPlayers;

    private ArenaProgressUIController arenaProgressUIController;

    [Space]
    [SerializeField] private int numberOfPlayers;
    [SerializeField] private int numberOfFrags;
    private int numberOfWinners;

    [SerializeField] private int amountOfGoldReward;
    [SerializeField] private int amountOfCupReward;

    private int playerFinishPlace;
    private int startNumberOfPlayers;

    [Header("Pause When Observe Mode Enabled")]
    [SerializeField] private float pauseBeforShowingPostWindowForObserver = 1.5f;

    private GameObject currentPlayer;

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

    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        numberOfPlayers = _numberOfPlayers;
        numberOfFrags = 0;
        startNumberOfPlayers = _numberOfPlayers;

        numberOfWinners = _numberOfWinners;

        UpdateLeftText();
        UpdateFragText();
    }

    public override void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        _playersList.Add(playerGO);

        if (isCurrentPlayer) currentPlayer = playerGO;
    }

    public void AddFrag()// вызывается из VisualIntermediary
    {
        numberOfFrags++;

        amountOfGoldReward += LeagueManager.Instance.GetGoldRewardForFrag();
        amountOfCupReward += LeagueManager.Instance.GetCupRewardForFrag();
        Debug.Log("Add FRAG");
        UpdateFragText();
    }

    public void AddGold(int value)// вызывается из PlayerEffector за подбор бонуса
    {
        amountOfGoldReward += value;
    }

    private void ReduceNumberOfPlayers(GameObject deadPlayer)
    {
        numberOfPlayers--;
        if (numberOfPlayers < 0) numberOfPlayers = 0;

        UpdateLeftText();

        _losersList.Add(deadPlayer);
        _playersList.Remove(deadPlayer);

        deadPlayer.GetComponent<WheelVehicle>().Handbrake = true;

        if (deadPlayer == currentPlayer)
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
            foreach (var player in _playersList)
            {
                if (player == currentPlayer)
                {
                    _isCurrentPlayerWinner = true;

                    // показываем окно победы игроку
                    arenaProgressUIController.CongratulationsOverEvent += CongratulationsOver;
                    arenaProgressUIController.ShowCongratilationsPanel();
                }

                _winnersList.Add(player);

                player.GetComponent<WheelVehicle>().Handbrake = true;
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
            ArenaCarDriverAI carDriverAIFirst = _playersList[0].GetComponent<ArenaCarDriverAI>();
            if (carDriverAIFirst != null) carDriverAIFirst.StartDuel(_playersList[1].transform);

            ArenaCarDriverAI carDriverAISecond = _playersList[1].GetComponent<ArenaCarDriverAI>();
            if (carDriverAISecond != null) carDriverAISecond.StartDuel(_playersList[0].transform);
        }
    }

    private void CalculateReward(int place)
    {
        playerFinishPlace = place;

        amountOfGoldReward += LeagueManager.Instance.ReceiveGoldsAsReward(place);
        amountOfCupReward += LeagueManager.Instance.ReceiveCupsAsReward(place);

        Debug.Log($"Награда: Золото - {amountOfGoldReward}; Кубки - {amountOfCupReward}");

        CurrencyManager.Instance.AddGold(amountOfGoldReward);
        CurrencyManager.Instance.AddCup(amountOfCupReward);

        if (place == 1)
        {
            BattleStatisticsManager.Instance.AddFirstPlace();
        }

        BattleStatisticsManager.Instance.AddBattle();
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

        cameraFollowingOnOtherPlayers.RemoveDriver(currentPlayer);

        arenaProgressUIController.ShowObserverUI();
        cameraFollowingOnOtherPlayers.EnableObserverMode();
    }

    protected override void ShowPostWindow()
    {
        SendListOfLosersNamesToGameManager();

        postLevelPlaceController.ShowPostPlace(_winnersList, _losersList, _isCurrentPlayerWinner);
    }

    IEnumerator WaitAndShowPostWindow()
    {
        yield return new WaitForSeconds(pauseBeforShowingPostWindowForObserver);
        ShowPostWindow();
    }

    private void OnEnable()
    {
        VisualIntermediary.PlayerWasDeadEvent += ReduceNumberOfPlayers;
    }
    private void OnDisable()
    {
        VisualIntermediary.PlayerWasDeadEvent -= ReduceNumberOfPlayers;
    }

    public void SendBattleFinishAnalyticEvent()// вызывается из VisualIntermediary после AddFrag()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _league_id = LeagueManager.Instance.GetCurrentLeagueLevel();

        string _level_id = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        int _enemies_amount = startNumberOfPlayers;

        int _player_took_place = playerFinishPlace;

        int _player_kills_amount = numberOfFrags;

        int _player_gold_earn = amountOfGoldReward;

        int player_cups_earned = amountOfCupReward;

        AnalyticsManager.Instance.BattleFinish(_battle_id, _player_car_id, _player_gun_id, _league_id, _level_id, _enemies_amount, _player_took_place,
            _player_kills_amount, _player_gold_earn, player_cups_earned);

        _battle_id++;
        PlayerPrefs.SetInt(_battle_id_key, _battle_id);
    }

    public void SendPlayerKillEnemyAnalyticEvent(GameObject killedEnemyObj)// вызывается из VisualIntermediary после AddFrag()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = AnalyticsManager.Instance.GetCurrentPlayerCarId();
        string _player_gun_id = AnalyticsManager.Instance.GetCurrentPlayerGunId();

        int _player_hp_left = AnalyticsManager.Instance.GetCurrentPlayerHealth();

        int _player_kills_amount = numberOfFrags;

        int _player_gold_earn = amountOfGoldReward;

        int _enemies_left = numberOfPlayers - 2;

        string _killed_enemy_car_id = killedEnemyObj.GetComponent<VehicleId>().VehicleID;

        string _killed_enemy_gun_id = killedEnemyObj.transform.GetComponentInChildren<Weapon>().GetComponent<WeaponId>().WeaponID;

        AnalyticsManager.Instance.PlayerKillEnemy(_battle_id, _player_car_id, _player_gun_id, _player_hp_left, _player_kills_amount,
            _player_gold_earn, _enemies_left, _killed_enemy_car_id, _killed_enemy_gun_id);
    }
}
