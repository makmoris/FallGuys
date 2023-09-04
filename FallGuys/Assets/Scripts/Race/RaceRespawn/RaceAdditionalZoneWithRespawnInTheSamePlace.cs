using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceAdditionalZoneWithRespawnInTheSamePlace : MonoBehaviour
{
    private List<GameObject> carsInZone = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (!carsInZone.Contains(other.gameObject)) carsInZone.Add(other.gameObject);
        }
    }

    public bool CheckCarInZone(GameObject carGO)
    {
        if (carsInZone.Contains(carGO)) return true;
        else return false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (carsInZone.Contains(other.gameObject)) carsInZone.Remove(other.gameObject);
        }
    }
}
