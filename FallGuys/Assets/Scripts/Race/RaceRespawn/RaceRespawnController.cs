using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceRespawnController : MonoBehaviour
{
    private float pauseTimeAfterRespawnCarAfterStuck = 1f;
    private bool canRespawnCarAfterStuck = true;
    //[Space]
    //[SerializeField] private List<RaceRespawnDeadZone> raceRespawnDeadZones;
    [Header("Fell Of The Road")]
    [SerializeField] private GameObject raceRespawnZonesFellOffTheRoad;
    [Header("Stuck")]
    [SerializeField] private GameObject raceRespawnZonesStuck;
    [Header("DEBAG")]
    [SerializeField] private List<RaceRespawnZone> raceRespawnZonesFellOffTheRoadList;
    [SerializeField] private List<RaceRespawnZone> raceRespawnZonesStuckList;

    private void Awake()
    {
        raceRespawnZonesFellOffTheRoadList.AddRange(raceRespawnZonesFellOffTheRoad.transform.GetComponentsInChildren<RaceRespawnZone>());
        raceRespawnZonesStuckList.AddRange(raceRespawnZonesStuck.transform.GetComponentsInChildren<RaceRespawnZone>());

        raceRespawnZonesStuckList.AddRange(raceRespawnZonesFellOffTheRoadList);

        SortRaceRespawnZonesList(raceRespawnZonesFellOffTheRoadList);
        SortRaceRespawnZonesList(raceRespawnZonesStuckList);
    }

    private void OnEnable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent += RespawnCarAfterStuck;
        canRespawnCarAfterStuck = true;
    }

    public void CarGotIntoRespawnZone(GameObject car)
    {
        car.SetActive(false);
        RespawnCar(car, raceRespawnZonesFellOffTheRoadList);
    }

    private void RespawnCarAfterStuck(WheelVehicle wheelVehicle)
    {
        //if (canRespawnCarAfterStuck)
        //{
        //    //wheelVehicle.transform.rotation = Quaternion.Euler(0f, wheelVehicle.transform.rotation.y, 0f);
        //    //wheelVehicle.transform.position = new Vector3(wheelVehicle.transform.position.x, wheelVehicle.transform.position.y,
        //    //    wheelVehicle.transform.position.z);

        //    //RespawnCar(wheelVehicle.gameObject, raceRespawnZonesStuck);

        //    //StartCoroutine(PauseAfterRespawnCarAfterStuck());
        //}

        wheelVehicle.gameObject.SetActive(false);
        RespawnCar(wheelVehicle.gameObject, raceRespawnZonesStuckList);
    }

    private void RespawnCar(GameObject car, List<RaceRespawnZone> raceRespawnZonesList)
    {
        RaceRespawnZone raceRespawnZone = GetNearestRespawnPoint(car.transform.position, raceRespawnZonesList);

        car.transform.position = raceRespawnZone.GetRespawnPosition();
        car.transform.rotation = raceRespawnZone.transform.rotation; 
        car.transform.SetParent(null);
        car.gameObject.SetActive(true);

        RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriverAI.Brake = false;
            raceDriverAI.WasRespawn();
        }
    }

    private RaceRespawnZone GetNearestRespawnPoint(Vector3 carPosition, List<RaceRespawnZone> raceRespawnZonesList)
    {
        //float minDistance = Mathf.Infinity;
        int indexMinDistance = 0;

        for (int i = 0; i < raceRespawnZonesList.Count; i++)
        {
            Vector3 pointPosition = raceRespawnZonesList[i].transform.position;

            //float distance = Vector3.Distance(carPosition, pointPosition);
            
            if (/*distance <= minDistance && */carPosition.z >= pointPosition.z)
            {
                //minDistance = distance;
                indexMinDistance = i;
            }
            else break;
        }

        return raceRespawnZonesList[indexMinDistance];
    }

    private void SortRaceRespawnZonesList(List<RaceRespawnZone> raceRespawnZonesList)
    {
        for (int i = 0; i < raceRespawnZonesList.Count; i++)
            for (int j = 0; j < raceRespawnZonesList.Count - i - 1; j++)
            {
                if (raceRespawnZonesList[j].transform.position.z > raceRespawnZonesList[j + 1].transform.position.z)
                {
                    var temp = raceRespawnZonesList[j];
                    raceRespawnZonesList[j] = raceRespawnZonesList[j + 1];
                    raceRespawnZonesList[j + 1] = temp;
                }
            }
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
