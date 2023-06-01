using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using VehicleBehaviour;

[Serializable]
public class RaceEnemiesSettings
{
    public string _name;
    public GameObject _enemyPrefab;
}
public class RaceInstaller : MonoBehaviour
{
    [Header("Start from lobby")]
    public bool startFromLobby;

    public static event Action<GameObject> IsCurrentPlayer;

    [Header("Scene Controllers")]
    [SerializeField] private RaceStartSector raceStartSector;
    [SerializeField] private CinemachineVirtualCamera camCinema;

    [Space]
    [SerializeField] private GameObject _playerPrefab;


    [SerializeField] private List<RaceEnemiesSettings> _enemiesSettings;

    //private readonly GameController _gameController;
    private GameController _gameController;

    private int numberOfPlayers;

    void Start()
    {
        if (startFromLobby) LoadDataFromCharacterManager();// подгружаем инфу по игроку

        // Install player
        var _playerObj = Instantiate(_playerPrefab);

        //Camera.main.GetComponent<CameraFollow>().SetTarget(_playerObj.transform);
        camCinema.m_Follow = _playerObj.transform;
        camCinema.m_LookAt = _playerObj.transform;

        Transform spawnPlace = raceStartSector.GetStartSpawnPlace();
        Vector3 pos = spawnPlace.position;
        _playerObj.transform.position = new Vector3(pos.x, 5f, pos.z);
        _playerObj.transform.rotation = spawnPlace.rotation;

        //Install enemies
        for (int i = 0; i < _enemiesSettings.Count; i++)
        {
            var enemySet = _enemiesSettings[i];

            var _enemyObj = Instantiate(enemySet._enemyPrefab);

            Transform enemySpawnPlace = raceStartSector.GetStartSpawnPlace();
            Vector3 posEnemy = enemySpawnPlace.position;
            _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);
            _enemyObj.transform.rotation = enemySpawnPlace.rotation;
        }

        IsCurrentPlayer?.Invoke(_playerObj);    // раскидываем всем подпищикам игрока, чтобы знали, какая тачка игрок, а какая нет
        // для мультиплеера. Т.к. в мультиплеере все тачки будут тачкой Player и нужно понять, кто конкретный игрок, за которого Я играю
    }

    private void LoadDataFromCharacterManager()
    {
        var characterManager = CharacterManager.Instance;

        _playerPrefab = characterManager.GetPlayerPrefab();
    }
}
