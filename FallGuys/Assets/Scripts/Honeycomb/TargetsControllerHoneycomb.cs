using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsControllerHoneycomb : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPlaces;
    private List<Transform> spawnPlacesClone = new List<Transform>();

    [SerializeField]protected List<Transform> targetsList = new List<Transform>();

    public event System.Action<Transform> PlatformFellEvent;

    protected virtual void Awake()
    {
        FallingPlatform[] allPlatforms = transform.GetComponentsInChildren<FallingPlatform>();
        foreach (var platform in allPlatforms)
        {
            if(platform.IsItNotAnEdgePlatform()) targetsList.Add(platform.transform);
        }

        DisableSpawnPlaces();
    }

    public void PlatformFell(Transform platformTrn)
    {
        PlatformFellEvent?.Invoke(platformTrn);
    }

    public List<Transform> GetTargets()
    {
        return targetsList;
    }

    public Transform GetSpawnPlace()
    {
        if (spawnPlaces.Count > 0)
        {
            int rand = Random.Range(0, spawnPlaces.Count);

            Transform spawnPlace = spawnPlaces[rand];

            spawnPlaces.Remove(spawnPlace);

            return spawnPlace;
        }
        else return null;
    }

    public List<Transform> GetSpawnPlacesList()
    {
        return spawnPlaces;
    }

    public void EnableSpawnPlaces()
    {
        foreach (var spawnPlace in spawnPlacesClone)
        {
            spawnPlace.GetComponentInChildren<Collider>().enabled = true;
        }
    }

    private void DisableSpawnPlaces()
    {
        foreach (var spawnPlace in spawnPlaces)
        {
            spawnPlace.GetComponentInChildren<Collider>().enabled = false;
            spawnPlacesClone.Add(spawnPlace);
        }
    }
}
