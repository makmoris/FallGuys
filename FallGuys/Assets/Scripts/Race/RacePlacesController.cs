using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadeVP;

public class RacePlacesController : MonoBehaviour
{
    [System.Serializable]
    public class PercentageWithDistance
    {
        public float distance;
        public float slowPercent;
    }

    [SerializeField] private float trackingFrequency = 1f;

    [Space]
    [SerializeField] private List<PercentageWithDistance> percentageWithDistancesList;

    private Dictionary<ArcadeVehicleController, WaypointProgressTracker> carAIWaypointTrackerDictionary = new();

    private CarPlayerWaipointTracker carPlayerWaipointTracker;

    private Coroutine checkCoroutine;

    public void AddPlayer(GameObject playerGO, bool isCurrentPlayer)
    {
        if (isCurrentPlayer)
        {
            carPlayerWaipointTracker = playerGO.GetComponent<CarPlayerWaipointTracker>();
        }
        else
        {
            ArcadeVehicleController arcadeVehicleController = playerGO.GetComponent<ArcadeVehicleController>();
            WaypointProgressTracker waypointProgressTracker = playerGO.GetComponentInChildren<WaypointProgressTracker>();
            waypointProgressTracker.Initialize();

            carAIWaypointTrackerDictionary.Add(arcadeVehicleController, waypointProgressTracker);
        }
    }

    public void StartTrackingPlaces()
    {
        if(percentageWithDistancesList.Count > 0)
        {
            checkCoroutine = StartCoroutine(CheckPlacesCoroutine());
        }
    }

    public void StopTrackingPlaces()
    {
        if (checkCoroutine != null) StopCoroutine(checkCoroutine);
    }

    private void CheckPlaces()
    {
        foreach (var dict in carAIWaypointTrackerDictionary)
        {
            ArcadeVehicleController arcadeVehicleController = dict.Key;
            WaypointProgressTracker waypointProgressTracker = dict.Value;

            float distance = waypointProgressTracker.ProgressDistance - carPlayerWaipointTracker.ProgressDistance;

            int currentIndex = 999;

            for (int i = 0; i < percentageWithDistancesList.Count; i++)
            {
                if (distance > percentageWithDistancesList[i].distance)
                {
                    currentIndex = i;
                }
                else break;
            }

            if(currentIndex < 999)
            {
                float slowPercent = percentageWithDistancesList[currentIndex].slowPercent / 100f;

                arcadeVehicleController.UpdateSpeedPercent(slowPercent);

                Debug.LogError($"{arcadeVehicleController.GetComponent<PlayerName>().Name} оторвался от игрока на {distance}." +
                    $" Замедляем его скорость на {slowPercent} процентов.");
            }
            else
            {
                arcadeVehicleController.UpdateSpeedPercent(1f);
            }
        }
    }

    IEnumerator CheckPlacesCoroutine()
    {
        yield return new WaitForSeconds(trackingFrequency);

        CheckPlaces();

        checkCoroutine = StartCoroutine(CheckPlacesCoroutine());
    }
}
