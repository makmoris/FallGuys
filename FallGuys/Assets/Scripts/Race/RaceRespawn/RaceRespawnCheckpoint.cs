using System.Collections.Generic;
using UnityEngine;

public class RaceRespawnCheckpoint : MonoBehaviour
{
    private List<GameObject> playersInCheckpoint = new List<GameObject>();

    public void AddPlayer(GameObject player)
    {
        if (!playersInCheckpoint.Contains(player))
        {
            playersInCheckpoint.Add(player);

            RaceDriverAI raceDriverAI = player.GetComponentInChildren<RaceDriverAI>();
            if(raceDriverAI != null)
            {
                raceDriverAI.SaveCheckpoint();
            }
        }
    }

    public bool PlayerHasPassedThisCheckpointpoint(GameObject playerGO)
    {
        return playersInCheckpoint.Contains(playerGO);
    }
}
