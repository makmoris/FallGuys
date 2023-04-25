using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> carsOnPlatform = new List<GameObject>();

    private void OnEnable()
    {
        DynamicPlatformBorder.CarLeftTheDynamicPlatformBorderEvent += CheckCar;
    }

    private void CheckCar(GameObject car)
    {
        if (carsOnPlatform.Contains(car))
        {
            car.transform.SetParent(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        carsOnPlatform.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        carsOnPlatform.Remove(other.gameObject);
        other.gameObject.transform.SetParent(null);
    }

    private void OnDisable()
    {
        DynamicPlatformBorder.CarLeftTheDynamicPlatformBorderEvent -= CheckCar;
    }
}
