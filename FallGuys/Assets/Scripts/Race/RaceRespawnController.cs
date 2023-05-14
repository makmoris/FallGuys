using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRespawnController : MonoBehaviour
{
    public List<RaceRespawnDeadZone> raceRespawnDeadZones;
    [Space]
    public List<RaceRespawnZone> raceRespawnZones;

    internal void CarGotIntoRespawnZone(GameObject car)
    {
        car.SetActive(false);
        RespawnCar(car);
    }

    private void RespawnCar(GameObject car)
    {
        RaceRespawnZone raceRespawnZone = GetNearestRespawnPoint(car.transform.position);

        car.transform.position = raceRespawnZone.GetRespawnPosition();
        car.transform.rotation = raceRespawnZone.transform.rotation;
        car.gameObject.SetActive(true);

        RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriverAI.Handbrake = false;
            raceDriverAI.ResetWaypointsPath();
        }
    }

    private RaceRespawnZone GetNearestRespawnPoint(Vector3 carPosition)
    {
        float minDistance = Mathf.Infinity;
        int indexMinDistance = 0;

        for (int i = 0; i < raceRespawnZones.Count; i++)
        {
            Vector3 pointPosition = raceRespawnZones[i].transform.position;

            float distance = Vector3.Distance(carPosition, pointPosition);
            
            if (distance <= minDistance && carPosition.z >= pointPosition.z)
            {
                minDistance = distance;
                indexMinDistance = i;
            }
            else break;
        }

        return raceRespawnZones[indexMinDistance];
    }
}
