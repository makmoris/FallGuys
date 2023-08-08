using System.Collections.Generic;
using UnityEngine;

public class ArenaInstaller : Installer
{
    [Header("-----")]
    [Header("Arena Controllers")]
    [SerializeField] private TargetsController targetsController;
    [SerializeField] private EndGameController endGameController;

    [Header("Arena AI")]
    [SerializeField] ArenaCarDriverAI arenaCarDriverAI;
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

                // Set Name
                PlayerName playerName = playerGO.GetComponent<PlayerName>();
                playerName.Initialize(player.Name, cameraFollowingOnOtherPlayers.transform);
                playerName.HideNameDisplay();

                // Hit Sides
                playerGO.GetComponentInChildren<HitSidesController>().SetIsPlayer();

                // TargetsController
                targetsController.AddPlayerToTargets(playerGO);

                // Weapon
                Transform weaponPlace = playerGO.transform.Find("WeaponPlace");
                Weapon weapon = Instantiate(player.Weapon, weaponPlace);
                weapon.SetParentBodyCollider(playerGO.GetComponent<Collider>());

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Analytics
                //AnalyticsManager.Instance.SetCurrentPlayer(player);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(playerGO, true);

                // ArenaProgressController
                levelProgressController.AddPlayer(playerGO, true);

                // Spawn Place
                Vector3 pos = targetsController.GetStartSpawnPosition(spawnCounter).position;
                playerGO.transform.position = new Vector3(pos.x, 2f, pos.z);
                playerGO.transform.rotation = targetsController.GetStartSpawnPosition(spawnCounter).rotation;

                // Player Effector
                var playerEffector = new PlayerEffector(player, playerGO, levelUINotifications, levelUI, weapon);

                currentPlayer = playerGO;

                GameObject playerObjectClone = Instantiate(playerGO);
                endGameController.SetPlayerObjectClone(playerObjectClone);
            }
            else
            {
                PlayerAI playerAI = _player as PlayerAI;

                // Instantiate AI 
                GameObject aiPlayerGO = Instantiate(arenaCarDriverAI.gameObject);

                // Set Name
                PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                playerName.Initialize(playerAI.Name, cameraFollowingOnOtherPlayers.transform);

                // VehicleCustomizer
                VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                // TargetsController
                targetsController.AddPlayerToTargets(aiPlayerGO);

                // Weapon
                Transform weaponPlaceAI = aiPlayerGO.transform.Find("WeaponPlace");
                Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                weaponAI.SetParentBodyCollider(aiPlayerGO.GetComponent<Collider>());
                weaponAI.IsAI(true);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(aiPlayerGO, false);

                // ArenaProgressController
                levelProgressController.AddPlayer(aiPlayerGO, false);

                // Spawn Place
                Vector3 posEnemy = targetsController.GetStartSpawnPosition(spawnCounter).position;
                aiPlayerGO.transform.position = new Vector3(posEnemy.x, 2f, posEnemy.z);
                aiPlayerGO.transform.rotation = targetsController.GetStartSpawnPosition(spawnCounter).rotation;

                // Enemy Pointer
                EnemyPointer enemyPointer = aiPlayerGO.GetComponentInChildren<EnemyPointer>(true);
                if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                enemyPointer.LevelUI = levelUI;

                // Set default data
                playerAI.SetDefaultData(playerAIDefaultData);

                // Player Effector
                var enemyPlayerEffector = new PlayerEffector(enemyPointer, playerAI, aiPlayerGO, levelUI, currentPlayer, weaponAI);
            }

            spawnCounter++;
        }

        targetsController.SetTargetsForPlayers();
    }
}
