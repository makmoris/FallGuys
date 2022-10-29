using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VehicleBehaviour.Utils;

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


public class Installer : MonoBehaviour // на пустой объект на сцене
{
    public TargetsController targetsController;

    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerDefaultData _playerDefaultData;
    [SerializeField] private PlayerLimitsData _playerLimitsData;
    [SerializeField] private Weapon _playerWeaon;// потом добавить и противникам
    private Bumper _playerBumper;
    private VisualIntermediary _playerVisualIntermediary;

    [SerializeField] private List<EnemiesSettings> _enemiesSettings;

    //private readonly GameController _gameController;
    private GameController _gameController;

    void Start()
    {
        // Install player
        IPlayer _player = new Player(_playerDefaultData);
        var _playerObj = Instantiate(_playerPrefab);
        _playerBumper = _playerObj.GetComponent<Bumper>();
        _playerVisualIntermediary = _playerObj.GetComponent<VisualIntermediary>();

        PointerManager.Instance.SetPlayerTransform(_playerObj.transform);
        targetsController.AddPlayerToTargets(_playerObj);

        // ставим пушку
        Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        Weapon weapon = Instantiate(_playerWeaon, weaponPlace);
        weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());

        Camera.main.GetComponent<CameraFollow>().SetTarget(_playerObj.transform);

        Vector3 pos = targetsController.GetStartSpawnPosition(0);
        _playerObj.transform.position = new Vector3(pos.x, 25f, pos.z);

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

            // ставим пушку боту
            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
            Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
            weaponAI.IsAI(true);

            Vector3 posEnemy = targetsController.GetStartSpawnPosition(i + 1);
            _enemyObj.transform.position = new Vector3(posEnemy.x, 25f, posEnemy.z);

            var enemyPlayerEffector = new PlayerEffector(_enemy, enemySet._enemyBumper, enemySet._enemyLimitsData, enemySet._enemyVisualIntermediary);
        }
        
        _gameController = new GameController(_player, enemies);

        targetsController.SetTargetsForPlayers();
    }

    //void Start()
    //{
    //    _gameController.LaunchGame();
    //}
}
