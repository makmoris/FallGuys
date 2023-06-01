using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceStartSector : MonoBehaviour
{
    [SerializeField] private GameObject startPlacesGO;
    private List<Transform> startPlacesTransformList = new List<Transform>();


    private void Awake()
    {
        FillStartPlacesTransformList();
    }

    public Transform GetStartSpawnPlace(GameObject car)
    {
        if (startPlacesTransformList.Count > 0)
        {
            Transform trn = startPlacesTransformList[0];
            startPlacesTransformList.Remove(trn);

            RaceDriverAI raceDriverAI = car.GetComponent<RaceDriverAI>();
            if (raceDriverAI != null) raceDriverAI.Handbrake = true;

            car.GetComponent<WheelVehicle>().Handbrake = true;

            return trn;
        }
        else
        {
            Debug.LogError("Not enough Start Spawn Positions to spawn");
            return null;
        }
    }

    private void FillStartPlacesTransformList()
    {
        for (int i = 0; i < startPlacesGO.transform.childCount; i++)
        {
            startPlacesTransformList.Add(startPlacesGO.transform.GetChild(i));
        }

        System.Random rand = new();

        for (int i = startPlacesTransformList.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            var tmp = startPlacesTransformList[j];
            startPlacesTransformList[j] = startPlacesTransformList[i];
            startPlacesTransformList[i] = tmp;
        }
    }
}
