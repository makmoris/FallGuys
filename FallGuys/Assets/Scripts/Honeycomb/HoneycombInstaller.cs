using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class HoneycombInstaller : Installer
{
    [Header("-----")]
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


                playerGO.transform.Find("Player Components").gameObject.SetActive(true);
                playerGO.transform.Find("AI Components").gameObject.SetActive(false);

                playerGO.GetComponent<WheelVehicle>().IsPlayer = true;

                // Set Name
                PlayerName playerName = playerGO.GetComponent<PlayerName>();
                playerName.Initialize(player.Name, cameraFollowingOnOtherPlayers.transform);
                playerName.HideNameDisplay();

                // Weapon
                Transform weaponPlace = playerGO.transform.Find("WeaponPlace");
                Weapon weapon = Instantiate(player.Weapon, weaponPlace);
                weapon.Initialize(false, playerGO.GetComponent<Collider>());
                weapon.DisableWeapon(playerGO);

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(playerGO, true);


                //// levelProgressController
                levelProgressController.AddPlayer(playerGO, true);

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
                PlayerAI playerAI = _player as PlayerAI;

                // Instantiate AI 
                //GameObject aiPlayerGO = Instantiate(honeycombDriverAIPrefab.gameObject);

                GameObject aiPlayerGO = Instantiate(playerAI.VehiclePrefab);

                aiPlayerGO.transform.Find("Player Components").gameObject.SetActive(false);
                aiPlayerGO.transform.Find("AI Components").gameObject.SetActive(true);
                AILogics aILogics = aiPlayerGO.GetComponentInChildren<AILogics>();
                aILogics.EnableHoneycombAI(aiPlayerGO, currentPlayer, frontRayLegth, sideRayLength, angleForSidesRays);

                aiPlayerGO.GetComponent<WheelVehicle>().IsPlayer = false;

                // Set Name
                PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                playerName.Initialize(playerAI.Name, cameraFollowingOnOtherPlayers.transform);

                // VehicleCustomizer
                VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                // Weapon
                Transform weaponPlaceAI = aiPlayerGO.transform.Find("WeaponPlace");
                Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                weaponAI.Initialize(true, aiPlayerGO.GetComponent<Collider>());
                weaponAI.DisableWeapon(aiPlayerGO);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(aiPlayerGO, false);

                // levelProgressController
                levelProgressController.AddPlayer(aiPlayerGO, false);

                // Targets Controller
                aiPlayerGO.GetComponentInChildren<HoneycombDriverAI>().SetTargetController(targetsControllerHoneycomb);

                // Spawn Place
                Transform spawnPlace = targetsControllerHoneycomb.GetSpawnPlace();
                Vector3 pos = spawnPlace.position;
                aiPlayerGO.transform.position = new Vector3(pos.x, 5f, pos.z);
                aiPlayerGO.transform.rotation = spawnPlace.rotation;

                // Enemy Pointer
                EnemyPointer enemyPointer = aiPlayerGO.GetComponentInChildren<EnemyPointer>(true);
                if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                enemyPointer.Initialize(levelUI);

                // Set default data
                playerAI.SetDefaultData(fallingPlatformsAIDefaultData);

                // Player Effector
                var enemyPlayerEffector = new PlayerEffector(enemyPointer, playerAI, aiPlayerGO, levelUI, currentPlayer, weaponAI, true);
            }
        }
    }
}
