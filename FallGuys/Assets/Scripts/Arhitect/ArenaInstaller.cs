using System.Collections.Generic;
using UnityEngine;

public class ArenaInstaller : Installer
{
    [Header("Arena Controllers")]
    public TargetsController targetsController;
    public EndGameController endGameController;

    protected override void Initializing()
    {
        base.Initializing();

        // Install player
        var _playerObj = Instantiate(_playerPrefab);

        _playerObj.GetComponentInChildren<HitSidesController>().SetIsPlayer();

        ArenaPointerManager.Instance.SetPlayerTransform(_playerObj.transform);
        targetsController.AddPlayerToTargets(_playerObj);

        Transform weaponPlace = _playerObj.transform.Find("WeaponPlace");
        Weapon weapon = Instantiate(_playerWeapon, weaponPlace);
        weapon.IsArena();
        weapon.SetParentBodyCollider(_playerObj.GetComponent<Collider>());

        IPlayer _player = new Player(_playerDefaultData, _playerObj, weapon);

        camCinema.m_Follow = _playerObj.transform;
        camCinema.m_LookAt = _playerObj.transform;

        Vector3 pos = targetsController.GetStartSpawnPosition(0).position;
        _playerObj.transform.position = new Vector3(pos.x, 2f, pos.z);
        _playerObj.transform.rotation = targetsController.GetStartSpawnPosition(0).rotation;

        var playerEffector = new PlayerEffector(false, _player, _playerLimitsData, true, gameUIManager);

        GameObject playerObjectClone = Instantiate(_playerObj);
        endGameController.SetPlayerObjectClone(playerObjectClone);

        LevelProgressController.Instance.SetCurrentPlayer(_player.Vehicle);

        //Install enemies
        List<IPlayerAI> enemies = new List<IPlayerAI>(_enemiesSettings.Count);
        for (int i = 0; i < _enemiesSettings.Count; i++)
        {
            var enemySet = _enemiesSettings[i];

            var _enemyObj = Instantiate(enemySet._enemyPrefab);

            targetsController.AddPlayerToTargets(_enemyObj);

            Transform weaponPlaceAI = _enemyObj.transform.Find("WeaponPlace");
            Weapon weaponAI = Instantiate(enemySet._enemyWeapon, weaponPlaceAI);
            weaponAI.IsArena();
            weaponAI.SetParentBodyCollider(_enemyObj.GetComponent<Collider>());
            weaponAI.IsAI(true);

            IPlayerAI _enemy = new PlayerAI(enemySet._enemyDefaultData, _enemyObj, weaponAI);
            enemies.Add(_enemy);

            Vector3 posEnemy = targetsController.GetStartSpawnPosition(i + 1).position;
            _enemyObj.transform.position = new Vector3(posEnemy.x, 2f, posEnemy.z);
            _enemyObj.transform.rotation = targetsController.GetStartSpawnPosition(i + 1).rotation;

            var enemyPlayerEffector = new PlayerEffector(true, _enemy, enemySet._enemyLimitsData, false, gameUIManager);
        }

        targetsController.SetTargetsForPlayers();

        numberOfPlayers = enemies.Count + 1;// 1 - сам игрок
        LevelProgressController.Instance.SetNumberOfPlayers(numberOfPlayers);

        SendBattleStartAnalyticEvent();
    }
}
