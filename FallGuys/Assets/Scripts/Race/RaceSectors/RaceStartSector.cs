using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartSector : MonoBehaviour
{
    [SerializeField] private bool isRandomPlaces;
    [SerializeField] private GameObject startPlacesGO;
    private List<Transform> startPlacesTransformList = new List<Transform>();

    private void Awake()
    {
        FillStartPlacesTransformList();
    }

    public void SetPlayersCount(int playersCount)
    {
        if (playersCount <= startPlacesTransformList.Count)
        {
            if(playersCount < startPlacesTransformList.Count)
            {
                int elementsToRemoveValue = startPlacesTransformList.Count - playersCount;

                Debug.LogError($"StartPlaces = {startPlacesTransformList.Count}");

                startPlacesTransformList.RemoveRange(playersCount, elementsToRemoveValue);

                ShaffleStartPlacesTransformList();

                Debug.LogError($"StartPlaces = {startPlacesTransformList.Count}");
            }
        }
        else
        {
            Debug.LogError("Players Count more than Spawn Places");
            return;
        }
    }

    public Transform GetStartSpawnPlace()
    {
        if (startPlacesTransformList.Count > 0)
        {
            Transform trn = startPlacesTransformList[0];
            startPlacesTransformList.Remove(trn);

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

        if (isRandomPlaces)
        {
            ShaffleStartPlacesTransformList();
        }
    }

    private void ShaffleStartPlacesTransformList()
    {
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
