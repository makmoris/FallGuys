using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;
using Sirenix.OdinInspector;
using ArcadeVP;

public class RaceInstaller : Installer
{
    [Header("-----")]
    [Header("Race AI")]
    [SerializeField] private float customRechargeTime = 10f;
    [SerializeField] PlayerDefaultData playerAIDefaultData;

    [Header("Additional Bullet Bonuses")]
    [SerializeField] private bool useAdditionalBulletBonuses;
    [ShowIf("useAdditionalBulletBonuses", true)]
    [SerializeField] private List<AdditionalBulletBonus> additionalBulletBonusList;

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

                playerGO.GetComponentInChildren<PlayerComponents>().gameObject.SetActive(true);
                playerGO.GetComponentInChildren<AIComponents>().gameObject.SetActive(false);

                playerGO.GetComponent<ArcadeVehicleController>().IsPlayer = true;

                CarPlayerWaipointTracker carPlayerWaipointTracker = playerGO.AddComponent<CarPlayerWaipointTracker>();
                carPlayerWaipointTracker.Initialize();

                // Set Name
                PlayerName playerName = playerGO.GetComponent<PlayerName>();
                playerName.Initialize(player.Name, cameraFollowingOnOtherPlayers.transform);
                playerName.HideNameDisplay();

                // Weapon
                Transform weaponPlace = playerGO.GetComponentInChildren<WeaponPlace>().transform;
                Weapon weapon = Instantiate(player.Weapon, weaponPlace);
                weapon.Initialize(false, playerGO.GetComponent<Collider>());
                weapon.ChangeRechargeTime(customRechargeTime);
                weapon.DisableWeapon(playerGO);
                if(useAdditionalBulletBonuses) weapon.UseAutomaticAdditionalBulletBonusRecharge(additionalBulletBonusList);

                weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(playerGO, true);

                // levelProgressController
                levelProgressController.AddPlayer(playerGO, true);

                // Spawn Place
                Transform spawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
                Vector3 pos = spawnPlace.position;
                playerGO.transform.position = new Vector3(pos.x, pos.y + 5f, pos.z);
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
                aiPlayerGO.GetComponentInChildren<PlayerComponents>().gameObject.SetActive(false);
                aiPlayerGO.GetComponentInChildren<AIComponents>().gameObject.SetActive(true);
                AILogics aILogics = aiPlayerGO.GetComponentInChildren<AILogics>();
                aILogics.EnableRaceAI(aiPlayerGO, currentPlayer, frontRayLegth * 0.25f, sideRayLength * 0.25f, angleForSidesRays * 0.25f);

                aiPlayerGO.GetComponent<ArcadeVehicleController>().IsPlayer = false;

                // Set Name
                PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                playerName.Initialize(playerAI.Name, cameraFollowingOnOtherPlayers.transform);

                // VehicleCustomizer
                VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                // Weapon
                Transform weaponPlaceAI = aiPlayerGO.GetComponentInChildren<WeaponPlace>().transform;
                Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                weaponAI.Initialize(true, aiPlayerGO.GetComponent<Collider>());
                weaponAI.ChangeRechargeTime(customRechargeTime);
                weaponAI.DisableWeapon(aiPlayerGO);
                if(useAdditionalBulletBonuses) weaponAI.UseAutomaticAdditionalBulletBonusRecharge(additionalBulletBonusList);

                // Camera
                cameraFollowingOnOtherPlayers.AddDriver(aiPlayerGO, false);

                // RaceProgressController
                raceProgressController.AddPlayer(aiPlayerGO, false);

                // Spawn Place
                Transform aiSpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
                Vector3 posAI = aiSpawnPlace.position;
                aiPlayerGO.transform.position = new Vector3(posAI.x, posAI.y + 5f, posAI.z);
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
