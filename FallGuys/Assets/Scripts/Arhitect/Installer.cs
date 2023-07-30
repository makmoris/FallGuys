using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class EnemiesSettings
//{
//    public GameObject _enemyPrefab;
//    public PlayerDefaultData _enemyDefaultData;
//    public PlayerLimitsData _enemyLimitsData;
//    public Weapon _enemyWeapon;
//}

public abstract class Installer : MonoBehaviour
{
    [Header("Start from lobby")]
    [SerializeField] protected bool startFromLobby;

    [Header("Scene Controllers")]
    [SerializeField] protected LevelUI levelUI;
    [SerializeField] protected LevelUINotifications levelUINotifications;
    [Space]
    [SerializeField] protected CinemachineVirtualCamera camCinema;
    [Space]
    [SerializeField] protected LevelProgressController levelProgressController;
    [SerializeField] protected PostLevelResultVisualization postLevelResultVisualization;

    protected GameManager gameManager;
    protected List<IPlayerData> playersData;
    protected GameObject currentPlayer;

    [Space]
    [SerializeField] protected GameObject _playerPrefab;
    [SerializeField] protected PlayerDefaultData _playerDefaultData;
    [SerializeField] protected PlayerLimitsData _playerLimitsData;
    [SerializeField] protected Weapon _playerWeapon;

    //[Space]
    //[SerializeField] protected List<EnemiesSettings> _enemiesSettings;
    

    protected int numberOfPlayers;

    private void Start()
    {
        Initializing();
    }

    protected abstract void InitializePlayers();

    private void Initializing()
    {
        if (startFromLobby)
        {
            LoadDataFromGameManager();
            numberOfPlayers = playersData.Count;

            levelProgressController.SetNumberOfPlayersAndWinners(playersData.Count, gameManager.GetNumberOfWinners());
            levelProgressController.GameManager = gameManager;
        }
        //else numberOfPlayers = _enemiesSettings.Count + 1;

        InitializePlayers();

        SendBattleStartAnalyticEvent();
    }

    private void LoadDataFromGameManager()
    {
        gameManager = FindObjectOfType<GameManager>();
        playersData = gameManager.Players;
    }

    protected void SendBattleStartAnalyticEvent()
    {
        string _battle_id_key = AnalyticsManager.battle_id_key;
        int _battle_id = PlayerPrefs.GetInt(_battle_id_key, 1);

        string _player_car_id = _playerPrefab.GetComponent<VehicleId>().VehicleID;

        string _player_gun_id = _playerWeapon.GetComponent<WeaponId>().WeaponID;

        int _league_id = LeagueManager.Instance.GetCurrentLeagueLevel();

        string _level_id = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        int _enemies_amount = numberOfPlayers;

        AnalyticsManager.Instance.BattleStart(_battle_id, _player_car_id, _player_gun_id, _league_id, _level_id, _enemies_amount);
    }
}
