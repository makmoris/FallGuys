using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VehicleBehaviour.Utils;
using Cinemachine;

[Serializable]
public class EnemiesSettings
{
    public string _name;
    public GameObject _enemyPrefab;
    public PlayerDefaultData _enemyDefaultData;
    public PlayerLimitsData _enemyLimitsData;
    public Weapon _enemyWeapon;
    internal Bumper _enemyBumper;
    internal VisualIntermediary _enemyVisualIntermediary;
}


public class Installer : MonoBehaviour 
{
    [Header("Start from lobby")]
    public bool startFromLobby;

    public static event Action<GameObject> IsCurrentPlayer;

    [Space]
    public TargetsController targetsController;

    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerDefaultData _playerDefaultData;
    [SerializeField] private PlayerLimitsData _playerLimitsData;
    [SerializeField] private Weapon _playerWeaon;
    private Bumper _playerBumper;
    private VisualIntermediary _playerVisualIntermediary;

    [Space]
    [SerializeField] private CinemachineVirtualCamera camCinema;

    [SerializeField] private List<EnemiesSettings> _enemiesSettings;

    //private readonly GameController _gameController;
    private GameController _gameController;

    private int numberOfPlayers;

    void Start()
    {
        if(startFromLobby) LoadDataFromCharacterManager();// подгружаем инфу по игроку

        // Install player
        IPlayer _player = new Player(_playerDefaultData);
        var _playerObj = Instantiate(_playerPrefab);
        _playerBumper = _playerObj.GetComponent<Bumper>();
        _playerVisualIntermediary = _playerObj.GetComponent<VisualIntermediary>();

        PointerManager.Instance.SetPlayerTransform(_playerObj.transform);
        targetsController.AddPlayerToTargets(_playerObj);

        // ������ �����
        Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        Weapon weapon = Instantiate(_playerWeaon, weaponPlace);
        weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());

        //Camera.main.GetComponent<CameraFollow>().SetTarget(_playerObj.transform);
        camCinema.m_Follow = _playerObj.transform;
        camCinema.m_LookAt = _playerObj.transform;

        Vector3 pos = targetsController.GetStartSpawnPosition(0);
        _playerObj.transform.position = new Vector3(pos.x, 5f, pos.z);

        var playerEffector = new PlayerEffector(_player, _playerBumper, _playerLimitsData, _playerVisualIntermediary);

        //Install enemies
        List<IEnemyPlayer> enemies = new List<IEnemyPlayer>(_enemiesSettings.Count);
        for (int i = 0; i < _enemiesSettings.Count; i++)
        {
            var enemySet = _enemiesSettings[i];

            var _enemyObj = Instantiate(enemySet._enemyPrefab);
            enemySet._enemyBumper = _enemyObj.GetComponent<Bumper>();
            enemySet._enemyVisualIntermediary = _enemyObj.GetComponent<VisualIntermediary>();

            IEnemyPlayer _enemy = new EnemyPlayer(enemySet._enemyDefaultData, _enemyObj);
            enemies.Add(_enemy);

            targetsController.AddPlayerToTargets(_enemyObj);

            // ������ ����� ����
            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
            Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
            weaponAI.IsAI(true);

            Vector3 posEnemy = targetsController.GetStartSpawnPosition(i + 1);
            _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);

            var enemyPlayerEffector = new PlayerEffector(_enemy, enemySet._enemyBumper, enemySet._enemyLimitsData, enemySet._enemyVisualIntermediary);
        }
        
        _gameController = new GameController(_player, enemies);

        targetsController.SetTargetsForPlayers();

        IsCurrentPlayer?.Invoke(_playerObj);    // раскидываем всем подпищикам игрока, чтобы знали, какая тачка игрок, а какая нет
        // для мультиплеера. Т.к. в мультиплеере все тачки будут тачкой Player и нужно понять, кто конкретный игрок, за которого Я играю

        numberOfPlayers = enemies.Count + 1;// 1 - сам игрок
        LevelProgressController.Instance.SetNumberOfPlayers(numberOfPlayers);
    }

    private void LoadDataFromCharacterManager()
    {
        var characterManager = CharacterManager.Instance;

        _playerPrefab = characterManager.GetPlayerPrefab();
        _playerDefaultData = characterManager.GetPlayerDefaultData();
        _playerLimitsData = characterManager.GetPlayerLimitsData();
        _playerWeaon = characterManager.GetPlayerWeapon();
    }

    //void Start()
    //{
    //    _gameController.LaunchGame();
    //}
}
