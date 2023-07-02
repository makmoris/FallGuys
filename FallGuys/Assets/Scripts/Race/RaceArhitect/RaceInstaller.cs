using System.Collections.Generic;
using UnityEngine;

public class RaceInstaller : Installer
{
    [Header("Race Controllers")]
    [SerializeField] RaceProgressController raceProgressController;

    [Header("Race AI")]
    [SerializeField] RaceDriverAI raceDriverAIPrefab;
    [SerializeField] PlayerDefaultData playerAIDefaultData;

    protected override void InitializePlayers()
    {
        foreach (var _player in playersData)
        {
            if (!_player.IsAI)
            {
                Player player = _player as Player;

                // Instantiate Player
                GameObject playerGO = Instantiate(player.VehiclePrefab);

                // Set Name
                PlayerName playerName = playerGO.GetComponent<PlayerName>();
                playerName.Initialize(player.Name, camCinema.transform);

                // Weapon
                Transform weaponPlace = playerGO.transform.Find("WeaponPlace");
                Weapon weapon = Instantiate(player.Weapon, weaponPlace);
                weapon.SetParentBodyCollider(playerGO.GetComponent<Collider>());
                weapon.DisableWeapon(playerGO);

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Cinemachine
                camCinema.m_Follow = playerGO.transform;
                camCinema.m_LookAt = playerGO.transform;

                // PostLevelResultVisualization
                postLevelResultVisualization.GameManager = gameManager;

                // RaceProgressController
                //raceProgressController.GameManager = gameManager;
                if (startFromLobby) raceProgressController.SetNumberOfWinners(gameManager.GetNumberOfWinners());

                raceProgressController.AddPlayer(playerGO);
                raceProgressController.SetCurrentPlayer(playerGO);

                // Spawn Place
                Transform spawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
                Vector3 pos = spawnPlace.position;
                playerGO.transform.position = new Vector3(pos.x, 5f, pos.z);
                playerGO.transform.rotation = spawnPlace.rotation;

                // Player Effector
                var playerEffector = new PlayerEffector(player, playerGO, levelUINotifications, levelUI, weapon, true);

                currentPlayer = playerGO;
            }
            else
            {
                if (startFromLobby)
                {
                    PlayerAI playerAI = _player as PlayerAI;

                    // Instantiate AI 
                    GameObject aiPlayerGO = Instantiate(raceDriverAIPrefab.gameObject);

                    // Set Name
                    PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                    playerName.Initialize(playerAI.Name, camCinema.transform);

                    // VehicleCustomizer
                    VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                    if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                    // Weapon
                    Transform weaponPlaceAI = aiPlayerGO.transform.Find("WeaponPlace");
                    Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                    weaponAI.SetParentBodyCollider(aiPlayerGO.GetComponent<Collider>());
                    weaponAI.IsAI(true);
                    weaponAI.DisableWeapon(aiPlayerGO);

                    // RaceProgressController
                    raceProgressController.AddPlayer(aiPlayerGO);

                    // Spawn Place
                    Transform aiSpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
                    Vector3 posAI = aiSpawnPlace.position;
                    aiPlayerGO.transform.position = new Vector3(posAI.x, 5f, posAI.z);
                    aiPlayerGO.transform.rotation = aiSpawnPlace.rotation;

                    // Enemy Pointer
                    EnemyPointer enemyPointer = aiPlayerGO.GetComponentInChildren<EnemyPointer>(true);
                    if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                    enemyPointer.LevelUI = levelUI;

                    // Set default data
                    playerAI.SetDefaultData(playerAIDefaultData);

                    // Player Effector
                    var enemyPlayerEffector = new PlayerEffector(enemyPointer, playerAI, aiPlayerGO, levelUI, currentPlayer, weaponAI, true);
                }
                else
                {
                    //List<IPlayerAI> enemies = new List<IPlayerAI>(_enemiesSettings.Count);
                    //for (int i = 0; i < _enemiesSettings.Count; i++)
                    //{
                    //    var enemySet = _enemiesSettings[i];

                    //    var _enemyObj = Instantiate(enemySet._enemyPrefab);

                    //    raceProgressController.AddPlayer(_enemyObj);

                    //    Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
                    //    Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
                    //    weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
                    //    weaponAI.IsAI(true);
                    //    weaponAI.DisableWeapon(_enemyObj);

                    //    IPlayerAI _enemy = new PlayerAI(enemySet._enemyDefaultData, _enemyObj, weaponAI);
                    //    enemies.Add(_enemy);

                    //    Transform enemySpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
                    //    Vector3 posEnemy = enemySpawnPlace.position;
                    //    _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);
                    //    _enemyObj.transform.rotation = enemySpawnPlace.rotation;

                    //    EnemyPointer enemyPointer = _enemyObj.GetComponentInChildren<EnemyPointer>(true);
                    //    if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                    //    enemyPointer.LevelUI = levelUI;

                    //    var enemyPlayerEffector = new PlayerEffector(enemyPointer, _enemy, _playerLimitsData, levelUI, _player, true);
                    //}
                }
            }
        }

        // Install player
        //var _playerObj = Instantiate(_playerPrefab);

        //Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        //Weapon weapon = Instantiate(_playerWeapon, weaponPlace);
        //weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());
        //weapon.DisableWeapon(_playerObj);

        //weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

        //IPlayer _player = new Player(_playerDefaultData, _playerObj, weapon);

        //camCinema.m_Follow = _playerObj.transform;
        //camCinema.m_LookAt = _playerObj.transform;

        //postLevelResultVisualization.GameManager = gameManager;

        //raceProgressController.GameManager = gameManager;
        //if (startFromLobby) raceProgressController.SetNumberOfWinners(gameManager.GetNumberOfWinners());

        //raceProgressController.AddPlayer(_playerObj);
        //raceProgressController.SetCurrentPlayer(_playerObj);

        //Transform spawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
        //Vector3 pos = spawnPlace.position;
        //_playerObj.transform.position = new Vector3(pos.x, 5f, pos.z);
        //_playerObj.transform.rotation = spawnPlace.rotation;

        //var playerEffector = new PlayerEffector(_player, _playerLimitsData, levelUINotifications, levelUI, true);

        // Install enemies
        //if (startFromLobby)
        //{
        //    List<IPlayerAI> enemies = new List<IPlayerAI>(_aiPlayerSettings.Count);
        //    for (int i = 0; i < _aiPlayerSettings.Count; i++)
        //    {
        //        var enemySet = _aiPlayerSettings[i];

        //        var _enemyObj = Instantiate(enemySet._prefab);

        //        VehicleCustomizer vehicleCustomizer = _enemyObj.GetComponent<VehicleCustomizer>();
        //        if (vehicleCustomizer != null) vehicleCustomizer.SetColorMaterial(enemySet._colorMaterial);

        //        raceProgressController.AddPlayer(_enemyObj);

        //        Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
        //        Weapon weaponAI = Instantiate(enemySet._weapon, weaponPlaceAI);
        //        weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
        //        weaponAI.IsAI(true);
        //        weaponAI.DisableWeapon(_enemyObj);

        //        IPlayerAI _enemy = new PlayerAI(enemySet._defaultData, _enemyObj, weaponAI);
        //        enemies.Add(_enemy);

        //        Transform enemySpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
        //        Vector3 posEnemy = enemySpawnPlace.position;
        //        _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);
        //        _enemyObj.transform.rotation = enemySpawnPlace.rotation;

        //        EnemyPointer enemyPointer = _enemyObj.GetComponentInChildren<EnemyPointer>(true);
        //        if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
        //        enemyPointer.LevelUI = levelUI;

        //        var enemyPlayerEffector = new PlayerEffector(enemyPointer, _enemy, _playerLimitsData, levelUI, _player, true);
        //    }
        //}
        //else
        //{
        //    List<IPlayerAI> enemies = new List<IPlayerAI>(_enemiesSettings.Count);
        //    for (int i = 0; i < _enemiesSettings.Count; i++)
        //    {
        //        var enemySet = _enemiesSettings[i];

        //        var _enemyObj = Instantiate(enemySet._enemyPrefab);

        //        raceProgressController.AddPlayer(_enemyObj);

        //        Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
        //        Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
        //        weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
        //        weaponAI.IsAI(true);
        //        weaponAI.DisableWeapon(_enemyObj);

        //        IPlayerAI _enemy = new PlayerAI(enemySet._enemyDefaultData, _enemyObj, weaponAI);
        //        enemies.Add(_enemy);

        //        Transform enemySpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
        //        Vector3 posEnemy = enemySpawnPlace.position;
        //        _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);
        //        _enemyObj.transform.rotation = enemySpawnPlace.rotation;

        //        EnemyPointer enemyPointer = _enemyObj.GetComponentInChildren<EnemyPointer>(true);
        //        if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
        //        enemyPointer.LevelUI = levelUI;

        //        var enemyPlayerEffector = new PlayerEffector(enemyPointer, _enemy, _playerLimitsData, levelUI, _player, true);
        //    }
        //}
    }
}
