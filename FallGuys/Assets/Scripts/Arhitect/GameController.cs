using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    // Слушать ивенты чанков (OnRacingFinished, OnArenaFinished)

    private IPlayer _player;
    private List<IEnemyPlayer> _enemies;

    public GameController(IPlayer player, List<IEnemyPlayer> enemies)
    {
        _player = player;
        _enemies = enemies;
    }

    public void LaunchGame()
    {

    }
}