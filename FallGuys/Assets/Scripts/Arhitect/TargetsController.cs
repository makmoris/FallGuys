using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController : MonoBehaviour
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private List<GameObject> players;

    private void Awake()
    {
        AddChieldTargets();
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
        for (int i = 0; i < transform.childCount; i++)
        {
            targets.Add(transform.GetChild(i));
        }
    }
}
