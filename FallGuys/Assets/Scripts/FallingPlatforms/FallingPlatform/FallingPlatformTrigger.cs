using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformTrigger : MonoBehaviour
{
    private FallingPlatform fallingPlatform;

    [SerializeField]private List<GameObject> carsOnPlatformList = new List<GameObject>();

    private void Awake()
    {
        fallingPlatform = transform.GetComponentInParent<FallingPlatform>();
    }

    public void CheckCarsOnPlatform()
    {
        if (carsOnPlatformList.Count > 0)
        {
            foreach (var car in carsOnPlatformList)
            {
                if (car == null)
                {
                    carsOnPlatformList.Remove(car);

                    if (carsOnPlatformList.Count == 0) break;
                }
            }

            if (carsOnPlatformList.Count > 0)
            {
                fallingPlatform.ChangeState();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && !carsOnPlatformList.Contains(other.gameObject))
        {
            carsOnPlatformList.Add(other.gameObject);

            if (carsOnPlatformList.Count == 1)
            {
                fallingPlatform.ChangeState();
            }


            Debug.Log("enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car") && carsOnPlatformList.Contains(other.gameObject))
        {
            carsOnPlatformList.Remove(other.gameObject);

            Debug.Log("exit");
        }
    }
}
