using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMechanicsController : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 50;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private Lightning lightningPrefab;

    private PoolMono<Lightning> lightningPool;

    [Header("Light")]
    [SerializeField] private Light _light;
    [SerializeField] private float colorChangeTime = 1f;
    private Color startLightColor;
    [SerializeField] private Color darkLightColor;

    [Header("Mechanics Activation Time Interval")]
    [SerializeField] private int minRandomInterval = 10;
    [SerializeField] private int maxRandomInterval = 20;

    [Header("Mechanics Deactivation Time")]
    [SerializeField] private float returnTimeToOriginal = 3f;

    [Header("Lightning Strike Interval")]
    [Range(0f, 2f)]
    [SerializeField] private float lightningRandomStrikeInterval = 2f;

    [Header("Spawn")]
    [SerializeField] private float spawnObjectRadius;
    [Range(0, 100)]
    [SerializeField] private int percentageOfSpawnPositions;
    [SerializeField] private PositionControllerForAdditionalMechanics positionControllerForAdditionalMechanics;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private AudioSource audioSource;

    private void Awake()
    {
        lightningPool = new PoolMono<Lightning>(lightningPrefab, poolCount);
        lightningPool.autoExpand = autoExpand;

        positionControllerForAdditionalMechanics.PreparePositions(spawnObjectRadius);

        startLightColor = _light.color;

        audioSource = GetComponent<AudioSource>();
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


        float currTime = 0f;
        do
        {
            _light.color = Color.Lerp(startLightColor, darkLightColor, currTime / colorChangeTime);

            currTime += Time.deltaTime;
            yield return null;
        }
        while
            (currTime <= colorChangeTime);


        foreach (var position in spawnPositions)
        {
            var lightning = lightningPool.GetFreeElement();
            lightning.transform.position = position;

            float randomInterval = Random.Range(0f, lightningRandomStrikeInterval);
            lightning.StartLightningStrike(randomInterval);
        }

        audioSource.Play();

        yield return new WaitForSeconds(returnTimeToOriginal);

        float returnCurrTime = 0f;
        do
        {
            _light.color = Color.Lerp(darkLightColor, startLightColor, returnCurrTime / colorChangeTime);

            returnCurrTime += Time.deltaTime;
            yield return null;
        }
        while
            (returnCurrTime <= colorChangeTime);

        StartCoroutine(WaitAndActivateMechanic());
    }
}
