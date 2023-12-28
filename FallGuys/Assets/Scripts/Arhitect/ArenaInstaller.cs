using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;
using Sirenix.OdinInspector;
using ArcadeVP;

public class ArenaInstaller : Installer
{
    [Header("-----")]
    [Header("Arena Controllers")]
    [SerializeField] private ArenaTargetsController arenaTargetsController;
    [SerializeField] private ArenaSpawnController arenaSpawnController;

    [Header("Custom Weapon Values")]
    [SerializeField] private float customAttackRange = 15f;

    [Header("Arena AI")]
    [SerializeField] PlayerDefaultData playerAIDefaultData;

    //
    [Header("Damage Percentage")]
    [SerializeField] private bool useDamagePercentage;
    [ShowIf("useDamagePercentage", true)]
    [SerializeField] private List<float> damagePercetageList;

    [Header("Health Percentage")]
    [SerializeField] private bool useHealthPercentage;
    [ShowIf("useHealthPercentage", true)]
    [SerializeField] private List<float> healthPercetageList;

    private const string arenaFightNumberKey = "ArenaFightNumber";
    private int arenaFightNumber;
    //

    private int spawnCounter;

    protected override void InitializePlayers()
    {
        arenaFightNumber = PlayerPrefs.GetInt(arenaFightNumberKey, 0);

        float damagePercent = 100f;
        float healthPercent = 100f;

        if (useDamagePercentage)
        {
            if (arenaFightNumber < damagePercetageList.Count) damagePercent = damagePercetageList[arenaFightNumber];
        }

        if (useHealthPercentage)
        {
            if (arenaFightNumber < healthPercetageList.Count) healthPercent = healthPercetageList[arenaFightNumber];
        }

        arenaFightNumber++;
        PlayerPrefs.SetInt(arenaFightNumberKey, arenaFightNumber);

        foreach (var _player in playersData)
        {
            if (!_player.IsAI)
            {
                Player player = _player as Player;

                // Install player
                GameObject playerGO = Instantiate(player.VehiclePrefab);

                playerGO.GetComponentInChildren<PlayerComponents>().gameObject.SetActive(true);
                playerGO.GetComponentInChildren<AIComponents>().gameObject.SetActive(false);

                playerGO.GetComponent<ArcadeVehicleController>().IsPlayer = true;

                playerGO.GetComponentInChildren<PlayerVehicleControlValues>().UseArenaControlValues();

                // Set Name
                PlayerName playerName = playerGO.GetComponent<PlayerName>();
                playerName.Initialize(player.Name, cameraFollowingOnOtherPlayers.transform);
                playerName.HideNameDisplay();

                // Hit Sides
                playerGO.GetComponentInChildren<HitSidesController>().SetIsPlayer();

                // TargetsController
                arenaTargetsController.AddPlayerToTargets(playerGO);

                // Weapon
                Transform weaponPlace = playerGO.GetComponentInChildren<WeaponPlace>().transform;
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
                var playerEffector = new PlayerEffector(player, playerGO, levelUINotifications, levelUI, weapon);

                currentPlayer = playerGO;
            }
            else
            {
                PlayerAI playerAI = _player as PlayerAI;

                GameObject aiPlayerGO = Instantiate(playerAI.VehiclePrefab);

                aiPlayerGO.GetComponentInChildren<PlayerComponents>().gameObject.SetActive(false);
                aiPlayerGO.GetComponentInChildren<AIComponents>().gameObject.SetActive(true);
                AILogics aILogics = aiPlayerGO.GetComponentInChildren<AILogics>();
                aILogics.EnableArenaAI(aiPlayerGO, currentPlayer, frontRayLegth, sideRayLength, angleForSidesRays);

                aiPlayerGO.GetComponent<ArcadeVehicleController>().IsPlayer = false;

                aiPlayerGO.GetComponentInChildren<AIVehicleControlValues>().UseArenaControlValues();

                // Set Name
                PlayerName playerName = aiPlayerGO.GetComponent<PlayerName>();
                playerName.Initialize(playerAI.Name, cameraFollowingOnOtherPlayers.transform);

                // VehicleCustomizer
                VehicleCustomizer aiVehicleCustomizer = aiPlayerGO.GetComponent<VehicleCustomizer>();
                if (aiVehicleCustomizer != null) aiVehicleCustomizer.SetColorMaterial(playerAI.VehicleColorMaterial);

                // TargetsController
                arenaTargetsController.AddPlayerToTargets(aiPlayerGO);

                // Weapon
                Transform weaponPlaceAI = aiPlayerGO.GetComponentInChildren<WeaponPlace>().transform;
                Weapon weaponAI = Instantiate(playerAI.Weapon, weaponPlaceAI);
                weaponAI.Initialize(true, aiPlayerGO.GetComponent<Collider>());
                weaponAI.ChangeAttackRange(customAttackRange);

                if (useDamagePercentage)
                {
                    weaponAI.ChangeDamageByPercentage(damagePercent);
                }

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
                if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
                enemyPointer.Initialize(levelUI);

                // Set default data
                playerAI.SetDefaultData(playerAIDefaultData);

                if (useHealthPercentage)
                {
                    float defaultHealth = playerAI.Health;
                    float newHealth = defaultHealth * (healthPercent / 100f);
                    playerAI.SetHealth(newHealth);
                }

                // Player Effector
                var enemyPlayerEffector = new PlayerEffector(enemyPointer, playerAI, aiPlayerGO, levelUI, currentPlayer, weaponAI);
            }

            spawnCounter++;
        }

        arenaTargetsController.SetTargetsForPlayers();
    }
}
