using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesSettings
{
    public string _name;
    public GameObject _enemyPrefab;
    public PlayerDefaultData _enemyDefaultData;
    public PlayerLimitsData _enemyLimitsData;
    public Weapon _enemyWeapon;
}

public abstract class Installer : MonoBehaviour
{
    [Header("Start from lobby")]
    [SerializeField] protected bool startFromLobby;

    [Header("Scene Controllers")]
    [SerializeField] protected LevelUI levelUI;
    [SerializeField] protected LevelUINotifications levelUINotifications;
    [SerializeField] protected CinemachineVirtualCamera camCinema;

    [Space]
    [SerializeField] protected GameObject _playerPrefab;
    [SerializeField] protected PlayerDefaultData _playerDefaultData;
    [SerializeField] protected PlayerLimitsData _playerLimitsData;
    [SerializeField] protected Weapon _playerWeapon;

    [Space]
    [SerializeField] protected List<EnemiesSettings> _enemiesSettings;

    protected int numberOfPlayers;

    private void Start()
    {
        Initializing();
    }

    protected virtual void Initializing()
    {
        if (startFromLobby) LoadDataFromCharacterManager();// подгружаем инфу по игроку

        numberOfPlayers = _enemiesSettings.Count + 1;
    }

    private void LoadDataFromCharacterManager()
    {
        var characterManager = CharacterManager.Instance;

        _playerPrefab = characterManager.GetPlayerPrefab();
        _playerDefaultData = characterManager.GetPlayerDefaultData();
        _playerLimitsData = characterManager.GetPlayerLimitsData();
        _playerWeapon = characterManager.GetPlayerWeapon();
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
