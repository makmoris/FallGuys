using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class ArenaSpawnController : MonoBehaviour
{
    [SerializeField] private Transform spawnPointsContainer;
    [SerializeField] private float respawnCheckRadius = 10f;

    [Header("DEBUG")]
    [SerializeField] private List<Transform> spawnPoints;

    private void Awake()
    {
        AddChieldSpawnPoints();
    }

    private void OnEnable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent += SendRespawnPositionToWheelVehicle;
    }

    public Transform GetStartSpawnPosition(int index)
    {
        return spawnPoints[index];
    }

    public Transform GetRespawnPosition()
    {
        int minVal = spawnPoints.Count;
        int respIndex = 0;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Collider[] overLappedColliders = Physics.OverlapSphere(spawnPoints[i].position, respawnCheckRadius);

            int carCount = 0;

            foreach (var val in overLappedColliders)
            {
                if (val.CompareTag("Car"))
                {
                    carCount++;
                }
            }

            if (carCount == 0)// если в радиусе точки респа нету противников, значит в этой точке можем респать.
            {
                respIndex = i;
                break;
            }
            else
            {
                if (carCount < minVal)
                {
                    minVal = carCount;
                    respIndex = i;
                }
            }
        }
        
        return spawnPoints[respIndex];
    }

    private void AddChieldSpawnPoints()
    {
        for (int i = 0; i < spawnPointsContainer.childCount; i++)
        {
            var spawnPoint = spawnPointsContainer.GetChild(i);

            if (spawnPoint.gameObject.activeInHierarchy) spawnPoints.Add(spawnPoint);
        }

        System.Random rand = new();

        for (int i = spawnPoints.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            var tmp = spawnPoints[j];
            spawnPoints[j] = spawnPoints[i];
            spawnPoints[i] = tmp;
        }
    }

    private void SendRespawnPositionToWheelVehicle(WheelVehicle wheelVehicle)
    {
        wheelVehicle.GetRespawnTransform(GetRespawnPosition());
    }

    private void OnDisable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent -= SendRespawnPositionToWheelVehicle;
    }
}
