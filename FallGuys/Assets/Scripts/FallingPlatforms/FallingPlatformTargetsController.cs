using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformTargetsController : MonoBehaviour
{
    [SerializeField] private List<Transform> targetsList = new List<Transform>();

    public event System.Action<Transform> PlatformFellEvent;

    private void Awake()
    {
        FallingPlatform[] allPlatforms = transform.GetComponentsInChildren<FallingPlatform>();
        foreach (var platform in allPlatforms)
        {
            if(platform.IsItNotAnEdgePlatform()) targetsList.Add(platform.transform);
        }
    }

    public void PlatformFell(Transform platformTrn)
    {
        PlatformFellEvent?.Invoke(platformTrn);
    }

    public List<Transform> GetTargets()
    {
        return targetsList;
    }
}
