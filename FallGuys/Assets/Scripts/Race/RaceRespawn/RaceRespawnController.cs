using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceRespawnController : MonoBehaviour
{
    [SerializeField] private float pauseTimeAfterRespawnCarAfterStuck = 1f;
    [SerializeField] private bool canRespawnCarAfterStuck = true;
    [Space]
    [SerializeField] private List<RaceRespawnDeadZone> raceRespawnDeadZones;
    [Space]
    [SerializeField] private List<RaceRespawnZone> raceRespawnZones;

    private void OnEnable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent += RespawnCarAfterStuck;
        canRespawnCarAfterStuck = true;
    }

    public void CarGotIntoRespawnZone(GameObject car)
    {
        car.SetActive(false);
        RespawnCar(car);
    }

    private void RespawnCarAfterStuck(WheelVehicle wheelVehicle)
    {
        if (canRespawnCarAfterStuck)
        {
            RespawnCar(wheelVehicle.gameObject);

            StartCoroutine(PauseAfterRespawnCarAfterStuck());
        }
    }

    private void RespawnCar(GameObject car)
    {
        RaceRespawnZone raceRespawnZone = GetNearestRespawnPoint(car.transform.position);

        car.transform.position = raceRespawnZone.GetRespawnPosition();
        car.transform.rotation = raceRespawnZone.transform.rotation; 
        car.transform.SetParent(null);
        car.gameObject.SetActive(true);

        RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriverAI.Brake = false;
            raceDriverAI.WasRespawn();
        }
    }

    private RaceRespawnZone GetNearestRespawnPoint(Vector3 carPosition)
    {
        //float minDistance = Mathf.Infinity;
        int indexMinDistance = 0;

        for (int i = 0; i < raceRespawnZones.Count; i++)
        {
            Vector3 pointPosition = raceRespawnZones[i].transform.position;

            //float distance = Vector3.Distance(carPosition, pointPosition);
            
            if (/*distance <= minDistance && */carPosition.z >= pointPosition.z)
            {
                //minDistance = distance;
                indexMinDistance = i;
            }
            else break;
        }

        return raceRespawnZones[indexMinDistance];
    }

    IEnumerator PauseAfterRespawnCarAfterStuck()
    {
        canRespawnCarAfterStuck = false;
        yield return new WaitForSeconds(pauseTimeAfterRespawnCarAfterStuck);
        canRespawnCarAfterStuck = true;
    }

    private void OnDisable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent -= RespawnCarAfterStuck;
    }
}
