using System.Collections.Generic;
using UnityEngine;

public class RaceInstaller : Installer
{
    [Header("Race Controllers")]
    [SerializeField] RaceProgressController raceProgressController;

    protected override void Initializing()
    {
        base.Initializing();

        // Install player
        var _playerObj = Instantiate(_playerPrefab);

        Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        Weapon weapon = Instantiate(_playerWeapon, weaponPlace);
        weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());
        weapon.DisableWeapon(_playerObj);

        weapon.GetComponentInChildren<AttackTargetDetector>().LevelUI = levelUI;

        IPlayer _player = new Player(_playerDefaultData, _playerObj, weapon);

        camCinema.m_Follow = _playerObj.transform;
        camCinema.m_LookAt = _playerObj.transform;

        raceProgressController.AddPlayer(_playerObj);
        raceProgressController.SetCurrentPlayer(_playerObj);

        Transform spawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
        Vector3 pos = spawnPlace.position;
        _playerObj.transform.position = new Vector3(pos.x, 5f, pos.z);
        _playerObj.transform.rotation = spawnPlace.rotation;

        var playerEffector = new PlayerEffector(_player, _playerLimitsData, levelUINotifications, levelUI, true);

        //Install enemies
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
        List<IPlayerAI> enemies = new List<IPlayerAI>(_aiPlayerSettings.Count);
        for (int i = 0; i < _aiPlayerSettings.Count; i++)
        {
            var enemySet = _aiPlayerSettings[i];

            var _enemyObj = Instantiate(enemySet._prefab);

            VehicleCustomizer vehicleCustomizer = _enemyObj.GetComponent<VehicleCustomizer>();
            if (vehicleCustomizer != null) vehicleCustomizer.SetColorMaterial(enemySet._colorMaterial);

            raceProgressController.AddPlayer(_enemyObj);

            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
            Weapon weaponAI = Instantiate(enemySet._weapon, weaponPlaceAI);
            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
            weaponAI.IsAI(true);
            weaponAI.DisableWeapon(_enemyObj);

            IPlayerAI _enemy = new PlayerAI(enemySet._defaultData, _enemyObj, weaponAI);
            enemies.Add(_enemy);

            Transform enemySpawnPlace = raceProgressController.GetRaceStartSector().GetStartSpawnPlace();
            Vector3 posEnemy = enemySpawnPlace.position;
            _enemyObj.transform.position = new Vector3(posEnemy.x, 5f, posEnemy.z);
            _enemyObj.transform.rotation = enemySpawnPlace.rotation;

            EnemyPointer enemyPointer = _enemyObj.GetComponentInChildren<EnemyPointer>(true);
            if (!enemyPointer.gameObject.activeSelf) enemyPointer.gameObject.SetActive(true);
            enemyPointer.LevelUI = levelUI;

            var enemyPlayerEffector = new PlayerEffector(enemyPointer, _enemy, _playerLimitsData, levelUI, _player, true);
        }

        SendBattleStartAnalyticEvent();
    }
}
