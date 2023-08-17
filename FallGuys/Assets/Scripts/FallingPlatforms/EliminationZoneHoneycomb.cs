using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminationZoneHoneycomb : MonoBehaviour
{
    [SerializeField] private HoneycombProgressController honeycombProgressController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            honeycombProgressController.PlayerOut(other.gameObject);
        }
    }
}
