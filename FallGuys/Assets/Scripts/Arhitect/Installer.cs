using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemiesSettings
{
    public string _name;
    public GameObject _enemyPrefab;
    public PlayerDefaultData _enemyDefaultData;
    public PlayerLimitsData _enemyLimitsData;
    internal Bumper _enemyBumper;
}


public class Installer : MonoBehaviour // на пустой объект на сцене
{
    public TargetsController targetsController;

    [Space]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerDefaultData _playerDefaultData;
    [SerializeField] private PlayerLimitsData _playerLimitsData;
    private Bumper _playerBumper;

    [SerializeField] private List<EnemiesSettings> _enemiesSettings;

    //private readonly GameController _gameController;
    private GameController _gameController;

    void Awake()
    {
        // Install player
        IPlayer _player = new Player(_playerDefaultData);
        var _playerObj = Instantiate(_playerPrefab);
        _playerBumper = _playerObj.GetComponent<Bumper>();

        PointerManager.Instance.SetPlayerTransform(_playerObj.transform);
        targetsController.AddPlayerToTargets(_playerObj);


        //Install enemies
        List<IEnemyPlayer> enemies = new List<IEnemyPlayer>(_enemiesSettings.Count);
        for (int i = 0; i < _enemiesSettings.Count; i++)
        {
            var enemySet = _enemiesSettings[i];

            var _enemyObj = Instantiate(enemySet._enemyPrefab);
            enemySet._enemyBumper = _enemyObj.GetComponent<Bumper>();

            IEnemyPlayer _enemy = new EnemyPlayer(enemySet._enemyDefaultData, _enemyObj);
            enemies.Add(_enemy);

            targetsController.AddPlayerToTargets(_enemyObj);

            var enemyPlayerEffector = new PlayerEffector(_enemy, enemySet._enemyBumper, enemySet._enemyLimitsData);
        }

        var playerEffector = new PlayerEffector(_player, _playerBumper, _playerLimitsData);
        _gameController = new GameController(_player, enemies);

        targetsController.SetTargetsForPlayers();
    }

    void Start()
    {
        _gameController.LaunchGame();
    }
}
