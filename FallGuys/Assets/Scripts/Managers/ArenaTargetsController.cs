using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTargetsController : MonoBehaviour
{
    [SerializeField] private Transform targetsContainer;
    [SerializeField] private Transform bonusBoxesContainer;

    [Header("DEBUG")]
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
            var arenaCarDriverAI = players[i].GetComponentInChildren<ArenaCarDriverAI>();

            if (arenaCarDriverAI != null)
            {
                arenaCarDriverAI.SetTargets(targets);
            }
        }
    }
    
    private void AddChieldTargets()
    {
        for (int i = 0; i < targetsContainer.childCount; i++)
        {
            var target = targetsContainer.GetChild(i);

            if (target.gameObject.activeInHierarchy) targets.Add(target);
        }

        for (int i = 0; i < bonusBoxesContainer.childCount; i++)
        {
            var bonusBox = bonusBoxesContainer.GetChild(i);

            if(bonusBox.gameObject.activeInHierarchy) targets.Add(bonusBox);
        }
    }
}
