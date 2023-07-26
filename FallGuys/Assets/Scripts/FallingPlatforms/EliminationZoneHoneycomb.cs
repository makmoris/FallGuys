using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminationZoneHoneycomb : MonoBehaviour
{
    [SerializeField] private ProgressControllerHoneycomb progressControllerHoneycomb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            progressControllerHoneycomb.PlayerOut(other.gameObject);
        }
    }
}
