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
    public Weapon _enemyWeapon;
}
public class RaceInstaller : MonoBehaviour
{
    [Header("Start from lobby")]
    public bool startFromLobby;

    //public static event Action<GameObject> IsCurrentPlayer;

    [Header("Scene Controllers")]
    [SerializeField] RaceProgressController raceProgressController;
    [SerializeField] private CinemachineVirtualCamera camCinema;

    [Space]
    [SerializeField] private PlayerDefaultData _playerDefaultData;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Weapon _playerWeaon;

    [SerializeField] private List<RaceEnemiesSettings> _enemiesSettings;

    private int numberOfPlayers;

    void Start()
    {
        if (startFromLobby) LoadDataFromCharacterManager();// подгружаем инфу по игроку

        // Install player
        var _playerObj = Instantiate(_playerPrefab);

        RacePointerManager.Instance.SetPlayerTransform(_playerObj.transform);

        Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        Weapon weapon = Instantiate(_playerWeaon, weaponPlace);
        weapon.IsRace();
        weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());


        IPlayer player = new Player(_playerDefaultData, _playerObj, weapon);

        camCinema.m_Follow = _playerObj.transform;
        camCinema.m_LookAt = _playerObj.transform;

        raceProgressController.AddPlayer(_playerObj);//
        raceProgressController.SetCurrentPlayer(_playerObj);//

        Transform spawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();//
        Vector3 pos = spawnPlace.position;//
        _playerObj.transform.position = new Vector3(pos.x, 5f, pos.z);//
        _playerObj.transform.rotation = spawnPlace.rotation;//

        //Install enemies
        for (int i = 0; i < _enemiesSettings.Count; i++)
        {
            var enemySet = _enemiesSettings[i];

            var _enemyObj = Instantiate(enemySet._enemyPrefab);

            raceProgressController.AddPlayer(_enemyObj);

            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
            Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
            weaponAI.IsRace();
            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
            weaponAI.IsAI(true);

            Transform enemySpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
            Vector3 posEnemy = enemySpawnPlace.position;
            _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);
            _enemyObj.transform.rotation = enemySpawnPlace.rotation;
        }

        //IsCurrentPlayer?.Invoke(_playerObj);    // раскидываем всем подпищикам игрока, чтобы знали, какая тачка игрок, а какая нет
        // для мультиплеера. Т.к. в мультиплеере все тачки будут тачкой Player и нужно понять, кто конкретный игрок, за которого Я играю
    }

    private void LoadDataFromCharacterManager()
    {
        var characterManager = CharacterManager.Instance;

        _playerPrefab = characterManager.GetPlayerPrefab();
        _playerWeaon = characterManager.GetPlayerWeapon();
    }
}
