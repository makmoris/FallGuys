using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example
public interface IEnemyPlayer : IPlayer
{
    public GameObject EnemyObject { get; }

    void Destroy();
}
