using System.Collections.Generic;
using UnityEngine;

public class ArenaInstaller : Installer
{
    [Header("Arena Controllers")]
    public TargetsController targetsController;
    public EndGameController endGameController;

    protected override void InitializePlayers()
    {
        throw new System.NotImplementedException();
    }

    //protected override void Initializing()
    //{
    //    base.Initializing();

    //    // Install player
    //    var _playerObj = Instantiate(_playerPrefab);

    //    _playerObj.GetComponentInChildren<HitSidesController>().SetIsPlayer();

    //    targetsController.AddPlayerToTargets(_playerObj);

    //    Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
    //    Weapon weapon = Instantiate(_playerWeapon, weaponPlace);
    //    weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());

    //    weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

    //    IPlayer _player = new Player(_playerDefaultData, _playerObj, weapon);

    //    AnalyticsManager.Instance.SetCurrentPlayer(_player);

    //    camCinema.m_Follow = _playerObj.transform;
    //    camCinema.m_LookAt = _playerObj.transform;

    //    Vector3 pos = targetsController.GetStartSpawnPosition(0).position;
    //    _playerObj.transform.position = new Vector3(pos.x, 2f, pos.z);
    //    _playerObj.transform.rotation = targetsController.GetStartSpawnPosition(0).rotation;

    //    var playerEffector = new PlayerEffector(_player, _playerLimitsData, levelUINotifications, levelUI);

    //    GameObject playerObjectClone = Instantiate(_playerObj);
    //    endGameController.SetPlayerObjectClone(playerObjectClone);

    //    ArenaProgressController.Instance.SetCurrentPlayer(_player.VehiclePrefab);

    //    //Install enemies
    //    if (startFromLobby)
    //    {
    //        List<IPlayerAI> enemies = new List<IPlayerAI>(_aiPlayerSettings.Count);
    //        for (int i = 0; i < _aiPlayerSettings.Count; i++)
    //        {
    //            var enemySet = _aiPlayerSettings[i];

    //            var _enemyObj = Instantiate(enemySet._prefab);

    //            VehicleCustomizer vehicleCustomizer = _enemyObj.GetComponent<VehicleCustomizer>();
    //            if (vehicleCustomizer != null) vehicleCustomizer.SetColorMaterial(enemySet._colorMaterial);

    //            targetsController.AddPlayerToTargets(_enemyObj);

    //            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
    //            Weapon weaponAI = Instantiate(enemySet._weapon, weaponPlaceAI);
    //            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
    //            weaponAI.IsAI(true);

    //            IPlayerAI _enemy = new PlayerAI(enemySet._defaultData, _enemyObj, weaponAI);
    //            enemies.Add(_enemy);

    //            Vector3 posEnemy = targetsController.GetStartSpawnPosition(i + 1).position;
    //            _enemyObj.transform.position = new Vector3(posEnemy.x, 2f, posEnemy.z);
    //            _enemyObj.transform.rotation = targetsController.GetStartSpawnPosition(i + 1).rotation;

    //            EnemyPointer enemyPointer = _enemyObj.GetComponentInChildren<EnemyPointer>(true);
    //            if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
    //            enemyPointer.LevelUI = levelUI;

    //            var enemyPlayerEffector = new PlayerEffector(enemyPointer, _enemy, _playerLimitsData, levelUI, _player);
    //        }
    //    }
    //    else
    //    {
    //        List<IPlayerAI> enemies = new List<IPlayerAI>(_enemiesSettings.Count);
    //        for (int i = 0; i < _enemiesSettings.Count; i++)
    //        {
    //            var enemySet = _enemiesSettings[i];

    //            var _enemyObj = Instantiate(enemySet._enemyPrefab);

    //            targetsController.AddPlayerToTargets(_enemyObj);

    //            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
    //            Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
    //            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
    //            weaponAI.IsAI(true);

    //            IPlayerAI _enemy = new PlayerAI(enemySet._enemyDefaultData, _enemyObj, weaponAI);
    //            enemies.Add(_enemy);

    //            Vector3 posEnemy = targetsController.GetStartSpawnPosition(i + 1).position;
    //            _enemyObj.transform.position = new Vector3(posEnemy.x, 2f, posEnemy.z);
    //            _enemyObj.transform.rotation = targetsController.GetStartSpawnPosition(i + 1).rotation;

    //            EnemyPointer enemyPointer = _enemyObj.GetComponentInChildren<EnemyPointer>(true);
    //            if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
    //            enemyPointer.LevelUI = levelUI;

    //            var enemyPlayerEffector = new PlayerEffector(enemyPointer, _enemy, _playerLimitsData, levelUI, _player);
    //        }
    //    }

    //    targetsController.SetTargetsForPlayers();

    //    ArenaProgressController.Instance.SetNumberOfPlayers(numberOfPlayers);

    //    SendBattleStartAnalyticEvent();
    //}
}
