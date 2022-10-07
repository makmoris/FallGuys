using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installer : MonoBehaviour // на пустой объект на сцене
{
    [SerializeField] private PlayerDefaultData _playerDefaultData;
    [SerializeField] private PlayerLimitsData _playerLimitsData;
    [SerializeField] private Bumper _bumper;
    [SerializeField] private GameObject[] _enemiesObjects;

    //private readonly GameController _gameController;
    private GameController _gameController;

    void Awake()
    {
        // Install player
        IPlayer _player = new Player(_playerDefaultData);

        //Debug.Log(_player.Health + " old");
        //_player.SetHealth(79);
        //Debug.Log(_player.Health + " new");

        // Install enemies
        List<IEnemyPlayer> enemies = new List<IEnemyPlayer>(_enemiesObjects.Length);
        foreach (var obj in _enemiesObjects)
        {
            //enemies.Add(new EnemyPlayer(obj));
            enemies.Add(obj.AddComponent<EnemyPlayer>());
        }

        Debug.Log(enemies[0].Health + " old Enemies");
        enemies[0].SetHealth(99);
        Debug.Log(enemies[0].Health + " new Enemies");

        var playerEffector = new PlayerEffector(_player, _bumper, _playerLimitsData);
         _gameController = new GameController(_player, enemies);
    }

    void Start()
    {
        _gameController.LaunchGame();
    }
}
