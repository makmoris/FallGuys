using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTargetsControllerHoneycombEnterTrigger : MonoBehaviour
{
    [SerializeField] private AdditionalTargetsControllerHoneycomb additionalTargetsControllerHoneycomb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            additionalTargetsControllerHoneycomb.PlayerEntered(other.gameObject);
        }
    }
}
