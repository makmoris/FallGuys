using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSpikesMechanicsController : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 50;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private SandSpikes sandSpikesPrefab;

    private PoolMono<SandSpikes> sandSpikesPool;


    [Header("Mechanics Activation Time Interval")]
    [SerializeField] private int minRandomInterval = 10;
    [SerializeField] private int maxRandomInterval = 20;

    [Header("Mechanics Deactivation Time")]
    [SerializeField] private float returnTimeToOriginal = 3f;

    [Header("Sand Spikes Appearance Interval")]
    [Range(0f, 2f)]
    [SerializeField] private float SandSpikesRandomAppearanceInterval = 2f;

    [Header("Spawn")]
    [SerializeField] private float spawnObjectRadius;
    [Range(0, 100)]
    [SerializeField] private int percentageOfSpawnPositions;
    [SerializeField] private PositionControllerForAdditionalMechanics positionControllerForAdditionalMechanics;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private void Awake()
    {
        sandSpikesPool = new PoolMono<SandSpikes>(sandSpikesPrefab, poolCount);
        sandSpikesPool.autoExpand = autoExpand;

        positionControllerForAdditionalMechanics.PreparePositions(spawnObjectRadius);
    }

    private void Start()
    {
        StartCoroutine(WaitAndActivateMechanic());
    }

    IEnumerator WaitAndActivateMechanic()
    {
        float randWaitTime = Random.Range(minRandomInterval, maxRandomInterval + 1);

        if (spawnPositions.Count > 0) spawnPositions.Clear();
        spawnPositions = positionControllerForAdditionalMechanics.GetRandomSpawnPositions(percentageOfSpawnPositions);

        yield return new WaitForSeconds(randWaitTime);

        foreach (var position in spawnPositions)
        {
            var sandSpikes = sandSpikesPool.GetFreeElement();
            sandSpikes.transform.position = position;

            float randomInterval = Random.Range(0f, SandSpikesRandomAppearanceInterval);
            sandSpikes.StartSandSpikesAppearanceFromController(randomInterval);
        }

        yield return new WaitForSeconds(returnTimeToOriginal);

        StartCoroutine(WaitAndActivateMechanic());
    }
}
