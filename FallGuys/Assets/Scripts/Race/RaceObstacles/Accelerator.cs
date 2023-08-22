using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [SerializeField] private float accelerationValue = 20000f;

    private Dictionary<GameObject, Rigidbody> objectsWithRBDictionary = new Dictionary<GameObject, Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (!objectsWithRBDictionary.ContainsKey(other.gameObject))
            {
                objectsWithRBDictionary.Add(other.gameObject, other.GetComponent<Rigidbody>());
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (objectsWithRBDictionary.ContainsKey(other.gameObject))
            {
                objectsWithRBDictionary[other.gameObject].AddForce(transform.forward * accelerationValue);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (objectsWithRBDictionary.ContainsKey(other.gameObject))
            {
                objectsWithRBDictionary.Remove(other.gameObject);
            }
        }
    }
}
