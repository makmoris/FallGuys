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
    internal UIIntermediary _enemyUIIntermediary;
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
    private UIIntermediary _playerUIIntermediary;

    [SerializeField] private List<EnemiesSettings> _enemiesSettings;

    //private readonly GameController _gameController;
    private GameController _gameController;

    void Awake()
    {
        // Install player
        IPlayer _player = new Player(_playerDefaultData);
        Vector3 pos = targetsController.GetStartSpawnPosition(0);
        var _playerObj = Instantiate(_playerPrefab, new Vector3(pos.x, 5f, pos.z), Quaternion.identity);
        _playerBumper = _playerObj.GetComponent<Bumper>();
        _playerUIIntermediary = _playerObj.GetComponent<UIIntermediary>();

        PointerManager.Instance.SetPlayerTransform(_playerObj.transform);
        targetsController.AddPlayerToTargets(_playerObj);

        // ставим пушку
        Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        Weapon weapon = Instantiate(_playerWeaon, weaponPlace);
        weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());

        Camera.main.GetComponent<CameraFollow>().SetTarget(_playerObj.transform);

        var playerEffector = new PlayerEffector(_player, _playerBumper, _playerLimitsData, _playerUIIntermediary);

        //Install enemies
        List<IEnemyPlayer> enemies = new List<IEnemyPlayer>(_enemiesSettings.Count);
        for (int i = 0; i < _enemiesSettings.Count; i++)
        {
            var enemySet = _enemiesSettings[i];

            Vector3 posEnemy = targetsController.GetStartSpawnPosition(i + 1);
            var _enemyObj = Instantiate(enemySet._enemyPrefab, new Vector3(posEnemy.x, 5f, posEnemy.z), Quaternion.identity);
            enemySet._enemyBumper = _enemyObj.GetComponent<Bumper>();
            enemySet._enemyUIIntermediary = _enemyObj.GetComponent<UIIntermediary>();

            IEnemyPlayer _enemy = new EnemyPlayer(enemySet._enemyDefaultData, _enemyObj);
            enemies.Add(_enemy);

            targetsController.AddPlayerToTargets(_enemyObj);

            // ставим пушку боту
            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
            Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
            weaponAI.IsAI(true);

            

            var enemyPlayerEffector = new PlayerEffector(_enemy, enemySet._enemyBumper, enemySet._enemyLimitsData, enemySet._enemyUIIntermediary);
        }
        
        _gameController = new GameController(_player, enemies);

        targetsController.SetTargetsForPlayers();
    }

    void Start()
    {
        _gameController.LaunchGame();
    }
}
