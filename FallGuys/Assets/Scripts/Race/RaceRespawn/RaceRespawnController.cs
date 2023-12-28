using ArcadeVP;
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
    private Dictionary<RaceRespawnZone, RaceRespawnCheckpoint> raceRespawnZoneRaceRespawnCheckpointPairs = 
        new Dictionary<RaceRespawnZone, RaceRespawnCheckpoint>();

    private LevelProgressUIController levelProgressUIController;

    private void Awake()
    {
        raceRespawnZonesFellOffTheRoadList.AddRange(raceRespawnZonesFellOffTheRoad.transform.GetComponentsInChildren<RaceRespawnZone>());
        raceRespawnZonesStuckList.AddRange(raceRespawnZonesStuck.transform.GetComponentsInChildren<RaceRespawnZone>());

        raceRespawnZonesStuckList.AddRange(raceRespawnZonesFellOffTheRoadList);

        //SortRaceRespawnZonesList(raceRespawnZonesFellOffTheRoadList);
        //SortRaceRespawnZonesList(raceRespawnZonesStuckList);

        foreach (var zone in raceRespawnZonesFellOffTheRoadList)
        {
            raceRespawnZoneRaceRespawnCheckpointPairs.Add(zone, zone.GetComponent<RaceRespawnCheckpoint>());
        }

        levelProgressUIController = FindObjectOfType<LevelProgressUIController>();
    }

    private void OnEnable()
    {
        ArcadeVehicleController.GetRespanwPositionForArcadeVehicleControllerEvent += RespawnCarAfterStuck;
        ArcadeVehicleController.HideRespawnButtonEvent += HideRespawnButton;
        canRespawnCarAfterStuck = true;
    }

    public void CarGotIntoRespawnZone(GameObject car)
    {
        car.SetActive(false);
        //RespawnCarOnNearestRespawnPoint(true, car, raceRespawnZonesFellOffTheRoadList);
        RespawnCarOnNearestRespawnPoint(true, car, raceRespawnZonesStuckList);
    }

    private void HideRespawnButton()
    {
        levelProgressUIController.HideRespawnButton();
    }

    private void RespawnCarAfterStuck(ArcadeVehicleController arcadeVehicleController, bool showRespawnButton)
    {
        if (showRespawnButton)
        {
            levelProgressUIController.ShowRespawnButton(arcadeVehicleController);
        }
        else
        {
            arcadeVehicleController.gameObject.SetActive(false);
            //RespawnCarOnNearestRespawnPoint(false, wheelVehicle.gameObject, raceRespawnZonesStuckList);
            RespawnCarOnNearestRespawnPoint(true, arcadeVehicleController.gameObject, raceRespawnZonesStuckList);
        }
    }

    private void RespawnCarOnNearestRespawnPoint(bool respawnOnCheckPoint, GameObject car, List<RaceRespawnZone> raceRespawnZonesList)
    {
        RaceRespawnZone raceRespawnZone = new RaceRespawnZone();

        if (respawnOnCheckPoint)
        {
            raceRespawnZone = GetLastRespawnCheckpoint(car);
        }
        else
        {
            raceRespawnZone = GetNearestRespawnPoint(car.transform.position, raceRespawnZonesList);
            
        }

        Rigidbody[] rigidbodys = car.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rigidbodys)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        car.transform.position = raceRespawnZone.GetRespawnPosition();
        car.transform.rotation = raceRespawnZone.transform.rotation;
        car.transform.SetParent(null);
        car.gameObject.SetActive(true);

        RaceDriverAI raceDriverAI = car.GetComponentInChildren<RaceDriverAI>();
        if (raceDriverAI != null)
        {
            raceDriverAI.Handbrake = false;
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

    private RaceRespawnZone GetLastRespawnCheckpoint(GameObject carGO)
    {
        RaceRespawnZone lastRespawnZone = raceRespawnZonesFellOffTheRoadList[0];

        foreach (var kvp in raceRespawnZoneRaceRespawnCheckpointPairs)
        {
            RaceRespawnZone raceRespawnZone = kvp.Key;
            RaceRespawnCheckpoint raceRespawnCheckpoint = kvp.Value;

            if (raceRespawnCheckpoint.PlayerHasPassedThisCheckpointpoint(carGO))
            {
                lastRespawnZone = raceRespawnZone;
            }
            else break;
        }

        return lastRespawnZone;
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
        ArcadeVehicleController.GetRespanwPositionForArcadeVehicleControllerEvent -= RespawnCarAfterStuck;
        ArcadeVehicleController.HideRespawnButtonEvent -= HideRespawnButton;
    }
}
