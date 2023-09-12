using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceInstaller : Installer
{
    [Header("-----")]
    [Header("Race AI")]
    [SerializeField] PlayerDefaultData playerAIDefaultData;

    protected override void InitializePlayers()
    {
        RaceProgressController raceProgressController = levelProgressController as RaceProgressController;

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
                weapon.SetParentBodyCollider(playerGO.GetComponent<Collider>());
                weapon.DisableWeapon(playerGO);

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(playerGO, true);

                // levelProgressController
                levelProgressController.AddPlayer(playerGO, true);

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
                PlayerAI playerAI = _player as PlayerAI;

                GameObject aiPlayerGO = Instantiate(playerAI.VehiclePrefab);

                // Test
                aiPlayerGO.transform.Find("Player Components").gameObject.SetActive(false);
                aiPlayerGO.transform.Find("AI Components").gameObject.SetActive(true);
                AILogics aILogics = aiPlayerGO.GetComponentInChildren<AILogics>();
                aILogics.EnableRaceAI(aiPlayerGO, aiPlayerGO, frontRayLegth, sideRayLength, angleForSidesRays);

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
                weaponAI.SetParentBodyCollider(aiPlayerGO.GetComponent<Collider>());
                weaponAI.IsAI(true);
                weaponAI.DisableWeapon(aiPlayerGO);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(aiPlayerGO, false);

                // RaceProgressController
                raceProgressController.AddPlayer(aiPlayerGO, false);

                // Spawn Place
                Transform aiSpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
                Vector3 posAI = aiSpawnPlace.position;
                aiPlayerGO.transform.position = new Vector3(posAI.x, 5f, posAI.z);
                aiPlayerGO.transform.rotation = aiSpawnPlace.rotation;

                // Enemy Pointer
                EnemyPointer enemyPointer = aiPlayerGO.GetComponentInChildren<EnemyPointer>(true);
                if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                enemyPointer.Initialize(levelUI);

                // Set default data
                playerAI.SetDefaultData(playerAIDefaultData);

    
                // Player Effector
                var enemyPlayerEffector = new PlayerEffector(enemyPointer, playerAI, aiPlayerGO, levelUI, currentPlayer, weaponAI, true);
            }
        }
    }
}
