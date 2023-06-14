using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VehicleBehaviour;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI fragText;

    [SerializeField]private int numberOfPlayers;
    [SerializeField]private int numberOfFrags;

    [SerializeField]private int amountOfGoldReward;
    [SerializeField]private int amountOfCupReward;

    private int playerFinishPlace;
    private int startNumberOfPlayers;

    [Header("Win/Lose")]
    [SerializeField] Camera gameCamera;
    [SerializeField] Camera endGameCamera;
    [Header("End Game Controller")]
    [SerializeField] private EndGameController endGameController;
    [Header("Win window")]
    [SerializeField] private float pauseBeforShowingWinWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private TextMeshProUGUI fragWWText;
    [SerializeField] private TextMeshProUGUI goldWWText;
    [SerializeField] private TextMeshProUGUI cupsWWText;
    [Header("Lose Window")]
    [SerializeField] private float pauseBeforShowingLoseWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private TextMeshProUGUI fragLWText;
    [SerializeField] private TextMeshProUGUI goldLWText;
    [SerializeField] private TextMeshProUGUI cupsLWText;


    private GameObject playerGO;
    private bool playerWasDead;

    public static LevelProgressController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (!gameCamera.gameObject.activeSelf) gameCamera.gameObject.SetActive(true);
        if (endGameCamera.gameObject.activeSelf) endGameCamera.gameObject.SetActive(false);

        //GameCameraAudioListenerController.Instance.DeactivateAudioListener();// изначально листенер только на самом авто игрока, чтобы правильно слышать звуки
    }


    public void SetNumberOfPlayers(int _numberOfPlayers)// стартовая точка
    {
        numberOfPlayers = _numberOfPlayers;
        numberOfFrags = 0;
        startNumberOfPlayers = _numberOfPlayers;

        UpdateLeftText();
        UpdateFragText();
    }

    public void AddFrag()// вызывается из VisualIntermediary
    {
        numberOfFrags++;

        amountOfGoldReward += LeagueManager.Instance.GetGoldRewardForFrag();
        amountOfCupReward += LeagueManager.Instance.GetCupRewardForFrag();
        Debug.Log("Add FRAG");
        UpdateFragText();
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

    public void AddGold(int value)// вызывается из PlayerEffector за подбор бонуса
    {
        amountOfGoldReward += value;
    }

    private void ReduceNumberOfPlayers(GameObject deadPlayer)
    {
        numberOfPlayers--;
        if (numberOfPlayers < 0) numberOfPlayers = 0;

        UpdateLeftText();

        if(deadPlayer == playerGO)
        {
            GameCameraAudioListenerController.Instance.ActivateAudioListener();// включаем на игровой камере, чтобы услышать звук взрыва авто. Т.к. листенер на авто уничтожается
            // значит окно поражения. Игра закончена
            Debug.Log($"GameOver. Занял {numberOfPlayers + 1} место");

            DisabledAllChildElements();
            CalculateReward(numberOfPlayers + 1);
            StartCoroutine(WaitAndShowLoseWindow());
            playerWasDead = true;
        }
        else if (numberOfPlayers == 1 && !playerWasDead)
        {
            // если остался один игрок, т.е. numberOfPlayers = 1, то кидаем окно победы
            Debug.Log($"Win. Занял {numberOfPlayers} место");

            DisabledAllChildElements();
            CalculateReward(numberOfPlayers);
            StartCoroutine(WaitAndShowWinWindow());
        }

        if (numberOfPlayers == 2 && !playerWasDead)
        {
            GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
            
            ArenaCarDriverAI carDriverAI = null;

            foreach(var car in cars)
            {
                var _carAI = car.GetComponent<ArenaCarDriverAI>();
                if (_carAI != null && car != deadPlayer)
                {
                    carDriverAI = _carAI;
                    Debug.Log($"Duel with {carDriverAI.name}");
                    break;
                }
            }

            if(carDriverAI != null)
            {
                carDriverAI.StartDuel();
            }
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

        if(place == 1)
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

    public void SetCurrentPlayer(GameObject playerObj)
    {
        playerGO = playerObj;
    }

    private void UpdateLeftText()
    {
        leftText.text = $"LEFT: {numberOfPlayers}";
    }

    private void UpdateFragText()
    {
        fragText.text = $"{numberOfFrags}";
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

    IEnumerator WaitAndShowWinWindow()
    {
        if(playerGO.GetComponent<WheelVehicle>() != null) playerGO.GetComponent<WheelVehicle>().Handbrake = true;

        if (!winWindow.activeSelf) winWindow.SetActive(true);
        if (loseWindow.activeSelf) loseWindow.SetActive(false);

        fragWWText.text = $"{numberOfFrags}";
        goldWWText.text = $"{amountOfGoldReward}";
        cupsWWText.text = $"{amountOfCupReward}";

        yield return new WaitForSeconds(pauseBeforShowingWinWindow);

        gameCamera.gameObject.SetActive(false);
        GameCameraAudioListenerController.Instance.DeactivateAudioListener();// выключаем на игровой камере

        endGameController.EnabledAudioListener();// подрубаем листенер на финишной тачке
        endGameCamera.gameObject.SetActive(true);
        playerGO.SetActive(false);

        //MusicManager.Instance.StopMusicPlaying();
        MusicManager.Instance.StopSoundsPlaying();
        MusicManager.Instance.PlayWinMusic();

        // запуск окна оценки
        InAppReviewsManager.Instance.ShowReviewWindow();

        SendBattleFinishAnalyticEvent();
    }

    IEnumerator WaitAndShowLoseWindow()
    {
        if (!loseWindow.activeSelf) loseWindow.SetActive(true);
        if (winWindow.activeSelf) winWindow.SetActive(false);

        fragLWText.text = $"{numberOfFrags}";
        goldLWText.text = $"{amountOfGoldReward}";
        cupsLWText.text = $"{amountOfCupReward}";

        yield return new WaitForSeconds(pauseBeforShowingLoseWindow);

        gameCamera.gameObject.SetActive(false);
        GameCameraAudioListenerController.Instance.DeactivateAudioListener();// выключаем на игровой камере

        endGameController.EnabledAudioListener();// подрубаем листенер на финишной тачке
        endGameCamera.gameObject.SetActive(true);

        MusicManager.Instance.StopMusicPlaying();
        MusicManager.Instance.StopSoundsPlaying();

        SendBattleFinishAnalyticEvent();
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
}
