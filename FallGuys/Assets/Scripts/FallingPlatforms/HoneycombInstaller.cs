using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneycombInstaller : Installer
{
    [Header("Falling Platform Controllers")]
    [SerializeField] TargetsControllerHoneycomb targetsControllerHoneycomb;

    [Header("Falling Platform AI")]
    [SerializeField] HoneycombDriverAI honeycombDriverAIPrefab;
    [SerializeField] PlayerDefaultData fallingPlatformsAIDefaultData;

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
                playerName.Initialize(player.Name, cameraFollowingOnOtherPlayers.transform);
                playerName.HideNameDisplay();

                // Weapon
                Transform weaponPlace = playerGO.transform.Find("WeaponPlace");
                Weapon weapon = Instantiate(player.Weapon, weaponPlace);
                weapon.SetParentBodyCollider(playerGO.GetComponent<Collider>());
                weapon.DisableWeapon(playerGO);

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(playerGO, true);


                //// levelProgressController
                levelProgressController.AddPlayer(playerGO);
                levelProgressController.SetCurrentPlayer(playerGO);

                // Spawn Place
                Transform spawnPlace = targetsControllerHoneycomb.GetSpawnPlace();
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
                    GameObject aiPlayerGO = Instantiate(honeycombDriverAIPrefab.gameObject);

                    // Set Name
                    PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                    playerName.Initialize(playerAI.Name, cameraFollowingOnOtherPlayers.transform);

                    // VehicleCustomizer
                    VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                    if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                    // Weapon
                    Transform weaponPlaceAI = aiPlayerGO.transform.Find("WeaponPlace");
                    Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                    weaponAI.SetParentBodyCollider(aiPlayerGO.GetComponent<Collider>());
                    weaponAI.IsAI(true);
                    weaponAI.DisableWeapon(aiPlayerGO);

                    // Camera
                    cameraFollowingOnOtherPlayers.AddDriver(aiPlayerGO, false);

                    // levelProgressController
                    levelProgressController.AddPlayer(aiPlayerGO);

                    // Targets Controller
                    aiPlayerGO.GetComponent<HoneycombDriverAI>().SetTargetController(targetsControllerHoneycomb);

                    // Spawn Place
                    Transform spawnPlace = targetsControllerHoneycomb.GetSpawnPlace();
                    Vector3 pos = spawnPlace.position;
                    aiPlayerGO.transform.position = new Vector3(pos.x, 5f, pos.z);
                    aiPlayerGO.transform.rotation = spawnPlace.rotation;

                    // Enemy Pointer
                    EnemyPointer enemyPointer = aiPlayerGO.GetComponentInChildren<EnemyPointer>(true);
                    if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                    enemyPointer.LevelUI = levelUI;

                    // Set default data
                    playerAI.SetDefaultData(fallingPlatformsAIDefaultData);

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

        //    for (int i = 0; i < 6; i++)
        //{
        //    GameObject aiPlayerGO = Instantiate(fallingPlatformsDriverAIPrefab.gameObject);

        //    aiPlayerGO.GetComponent<FallingPlatformsDriverAI>().SetTargetController(targetsControllerHoneycomb);

        //    // Spawn Place
        //    Transform spawnPlace = targetsControllerHoneycomb.GetSpawnPlace();
        //    Vector3 pos = spawnPlace.position;
        //    aiPlayerGO.transform.position = new Vector3(pos.x, 5f, pos.z);
        //    aiPlayerGO.transform.rotation = spawnPlace.rotation;
        //}
    }
}
