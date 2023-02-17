using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDuckMechanicsController : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 25;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private ExplosiveDuck explosiveDuckPrefab;

    private PoolMono<ExplosiveDuck> explosiveDuckPool;

    [Header("Mechanics Activation Time Interval")]
    [SerializeField] private int minRandomInterval = 10;
    [SerializeField] private int maxRandomInterval = 20;

    [Header("Mechanics Deactivation Time")]
    [SerializeField] private float returnTimeToOriginal = 3f;

    [Header("Explosive Duck Appearance Interval")]
    [Range(0f, 2f)]
    [SerializeField] private float ExplosiveDuckRandomAppearanceInterval = 2f;

    [Header("Spawn")]
    [SerializeField] private float spawnObjectRadius;
    [Range(0, 100)]
    [SerializeField] private int percentageOfSpawnPositions = 25;
    [SerializeField] private PositionControllerForAdditionalMechanics positionControllerForAdditionalMechanics;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private void Awake()
    {
        explosiveDuckPool = new PoolMono<ExplosiveDuck>(explosiveDuckPrefab, poolCount);
        explosiveDuckPool.autoExpand = autoExpand;

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
            var explosiveDuck = explosiveDuckPool.GetFreeElement();
            explosiveDuck.transform.position = position;

            float randomInterval = Random.Range(0f, ExplosiveDuckRandomAppearanceInterval);
            explosiveDuck.StartThrowingDuck(randomInterval);
            
            explosiveDuck.transform.LookAt(Vector3.zero);
        }

        yield return new WaitForSeconds(returnTimeToOriginal);

        StartCoroutine(WaitAndActivateMechanic());
    }
}
