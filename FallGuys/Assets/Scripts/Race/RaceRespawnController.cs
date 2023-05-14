using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRespawnController : MonoBehaviour
{
    public List<RaceRespawnZone> raceRespawnZones;
    [Space]
    public List<Transform> respawnPoints;

    internal void CarGotIntoRespawnZone(GameObject car)
    {
        car.SetActive(false);
        RespawnCar(car);
    }

    private void RespawnCar(GameObject car)
    {
        Transform spawnPoint = GetNearestRespawnPoint(car.transform.position);

        car.transform.position = spawnPoint.position;
        car.transform.rotation = spawnPoint.rotation;
        car.gameObject.SetActive(true);

        RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriverAI.Handbrake = false;
            raceDriverAI.ResetWaypointsPath();
        }
    }

    private Transform GetNearestRespawnPoint(Vector3 carPosition)
    {
        float minDistance = Mathf.Infinity;
        int indexMinDistance = 0;

        for (int i = 0; i < respawnPoints.Count; i++)
        {
            Vector3 pointPosition = respawnPoints[i].position;

            float distance = Vector3.Distance(carPosition, pointPosition);
            Debug.Log($"distance {distance} <= minDistance {minDistance} && carPosition.z {carPosition.z} >= pointPosition.z {pointPosition.z}");
            if (distance <= minDistance && carPosition.z >= pointPosition.z)
            {
                minDistance = distance;
                indexMinDistance = i;
            }
            else break;
        }

        Debug.Log($"Респавн в точке {respawnPoints[indexMinDistance].name}");

        return respawnPoints[indexMinDistance];
    }
}
