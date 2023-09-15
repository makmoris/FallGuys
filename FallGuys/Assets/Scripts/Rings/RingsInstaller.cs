using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RingsInstaller : Installer
{
    [Header("-----")]
    [Header("Rings Controllers")]
    [SerializeField] private RingsTargetsController ringsTargetsController;
    [SerializeField] private RingsController ringsController;
    [SerializeField] private ArenaSpawnController arenaSpawnController;

    [Header("Rings AI")]
    [SerializeField] private float customAttackRange = 20f;
    [SerializeField] PlayerDefaultData playerAIDefaultData;

    private int spawnCounter;

    protected override void InitializePlayers()
    {
        foreach (var _player in playersData)
        {
            if (!_player.IsAI)
            {
                Player player = _player as Player;

                // Install player
                GameObject playerGO = Instantiate(player.VehiclePrefab);

                playerGO.transform.Find("Player Components").gameObject.SetActive(true);
                playerGO.transform.Find("AI Components").gameObject.SetActive(false);

                playerGO.GetComponent<WheelVehicle>().IsPlayer = true;

                // Set Name
                PlayerName playerName = playerGO.GetComponent<PlayerName>();
                playerName.Initialize(player.Name, cameraFollowingOnOtherPlayers.transform);
                playerName.HideNameDisplay();

                // Hit Sides
                playerGO.GetComponentInChildren<HitSidesController>().SetIsPlayer();

                // TargetsController
                ringsTargetsController.AddPlayerToTargets(playerGO);

                // Weapon
                Transform weaponPlace = playerGO.transform.Find("WeaponPlace");
                Weapon weapon = Instantiate(player.Weapon, weaponPlace);
                weapon.Initialize(false, playerGO.GetComponent<Collider>());
                weapon.ChangeAttackRange(customAttackRange);

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Analytics
                //AnalyticsManager.Instance.SetCurrentPlayer(player);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(playerGO, true);

                // ArenaProgressController
                levelProgressController.AddPlayer(playerGO, true);

                // Spawn Place
                Vector3 pos = arenaSpawnController.GetStartSpawnPosition(spawnCounter).position;
                playerGO.transform.position = new Vector3(pos.x, 2f, pos.z);
                playerGO.transform.rotation = arenaSpawnController.GetStartSpawnPosition(spawnCounter).rotation;

                // Player Effector
                var playerEffector = new PlayerEffector(player, playerGO, levelUINotifications, levelUI, weapon, true);

                currentPlayer = playerGO;
            }
            else
            {
                PlayerAI playerAI = _player as PlayerAI;

                GameObject aiPlayerGO = Instantiate(playerAI.VehiclePrefab);

                aiPlayerGO.transform.Find("Player Components").gameObject.SetActive(false);
                aiPlayerGO.transform.Find("AI Components").gameObject.SetActive(true);
                AILogics aILogics = aiPlayerGO.GetComponentInChildren<AILogics>();
                aILogics.EnableRingsAI(aiPlayerGO, currentPlayer, frontRayLegth, sideRayLength, angleForSidesRays);

                aiPlayerGO.GetComponent<WheelVehicle>().IsPlayer = false;

                // Set Name
                PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                playerName.Initialize(playerAI.Name, cameraFollowingOnOtherPlayers.transform);

                // VehicleCustomizer
                VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                // TargetsController
                ringsTargetsController.AddPlayerToTargets(aiPlayerGO);

                // Weapon
                Transform weaponPlaceAI = aiPlayerGO.transform.Find("WeaponPlace");
                Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                weaponAI.Initialize(true, aiPlayerGO.GetComponent<Collider>());
                weaponAI.ChangeAttackRange(customAttackRange);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(aiPlayerGO, false);

                // ArenaProgressController
                levelProgressController.AddPlayer(aiPlayerGO, false);

                // Spawn Place
                Vector3 posEnemy = arenaSpawnController.GetStartSpawnPosition(spawnCounter).position;
                aiPlayerGO.transform.position = new Vector3(posEnemy.x, 2f, posEnemy.z);
                aiPlayerGO.transform.rotation = arenaSpawnController.GetStartSpawnPosition(spawnCounter).rotation;

                // Enemy Pointer
                EnemyPointer enemyPointer = aiPlayerGO.GetComponentInChildren<EnemyPointer>(true);
                //if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                //enemyPointer.Initialize(levelUI);

                // Set default data
                playerAI.SetDefaultData(playerAIDefaultData);

                // Player Effector
                var enemyPlayerEffector = new PlayerEffector(enemyPointer, playerAI, aiPlayerGO, levelUI, currentPlayer, weaponAI, true);
            }

            spawnCounter++;
        }

        ringsTargetsController.SetTargetsForAIPlayers();

        RingsProgressController ringsProgressController = levelProgressController as RingsProgressController;
        ringsProgressController.IsObserverMode(gameManager.IsObserverMode);

        ringsController.Initialize(ringsProgressController, levelUI);
    }
}
