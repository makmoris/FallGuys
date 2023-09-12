using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RingsTargetsController : MonoBehaviour
{
    [SerializeField] private RingsController ringsController;
    [SerializeField] private Transform targetsContainer;

    [Header("DEBUG")]
    [SerializeField] private List<Transform> targets;
    [SerializeField] private List<GameObject> players;
    private List<RingsDriverAI> ringsDriverAIList = new List<RingsDriverAI>();


    private void Awake()
    {
        AddChieldTargets();
    }

    private void OnEnable()
    {
        ringsController.RingActivatedEvent += RingActivated;
    }

    public void AddPlayerToTargets(GameObject playerObj)
    {
        players.Add(playerObj);
        targets.Add(playerObj.transform);

        RingsDriverAI ringsDriverAI = playerObj.GetComponentInChildren<RingsDriverAI>();
        if (ringsDriverAI != null) ringsDriverAIList.Add(ringsDriverAI);
    }

    public void SetTargetsForAIPlayers()
    {
        foreach (var driverAI in ringsDriverAIList)
        {
            driverAI.SetTargets(targets);
            driverAI.SetRings(ringsController.GetRingsList());
        }
    }

    private void AddChieldTargets()
    {
        for (int i = 0; i < targetsContainer.childCount; i++)
        {
            var target = targetsContainer.GetChild(i);

            if (target.gameObject.activeInHierarchy) targets.Add(target);
        }
    }

    private void RingActivated()
    {
        foreach (var ringsDriver in ringsDriverAIList)
        {
            ringsDriver.SelectRingToTarget();
        }
    }

    private void OnDisable()
    {
        ringsController.RingActivatedEvent -= RingActivated;
    }
}
