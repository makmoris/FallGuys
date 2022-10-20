using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController : MonoBehaviour
{
    [SerializeField] private Transform targetsContainer;
    [SerializeField] private Transform spawnPointsContainer;

    [SerializeField] private List<Transform> targets;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private List<Transform> spawnPoints;

    private void Awake()
    {
        AddChieldTargets();
        AddChieldSpawnPoints();
    }

    public void AddPlayerToTargets(GameObject playerObj)
    {
        players.Add(playerObj);
        targets.Add(playerObj.transform);
    }

    public void SetTargetsForPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            var carDriverAI = players[i].GetComponent<CarDriverAI>();

            if (carDriverAI != null)
            {
                carDriverAI.SetTargets(targets);
            }
        }
    }

    private void AddChieldTargets()
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    targets.Add(transform.GetChild(i));
        //}

        for (int i = 0; i < targetsContainer.childCount; i++)
        {
            targets.Add(targetsContainer.GetChild(i));
        }
    }

    private void AddChieldSpawnPoints()
    {
        for (int i = 0; i < spawnPointsContainer.childCount; i++)
        {
            spawnPoints.Add(spawnPointsContainer.GetChild(i));
            targets.Add(spawnPointsContainer.GetChild(i));
        }
    }

    public Vector3 GetStartSpawnPosition(int index)
    {
        return spawnPoints[index].position;
    }

    public Vector3 GetRandomRespawnPosition()
    {
        int rand = Random.Range(0, spawnPoints.Count);
        return spawnPoints[rand].position;
    }
}
