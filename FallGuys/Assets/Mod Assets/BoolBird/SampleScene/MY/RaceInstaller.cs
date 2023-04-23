using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInstaller : MonoBehaviour
{
    [SerializeField] private GameObject player;
    RacePlayerWaipointTracker racePlayerWaipointTracker;

    private void Awake()
    {
        racePlayerWaipointTracker = player.AddComponent<RacePlayerWaipointTracker>();
    }
}
