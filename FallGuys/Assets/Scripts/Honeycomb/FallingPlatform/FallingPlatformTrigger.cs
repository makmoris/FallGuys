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
        if (other.CompareTag("Car"))
        {
            if (Vector3.Dot(Vector3.up, other.transform.up) < 0.15f)
            {
                other.transform.rotation = Quaternion.Euler(0f, other.transform.rotation.y, 0f);
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            }
            else
            {
                if (!carsOnPlatformList.Contains(other.gameObject))
                {
                    carsOnPlatformList.Add(other.gameObject);

                    if (carsOnPlatformList.Count == 1)
                    {
                        fallingPlatform.ChangeState();
                    }

                    Debug.Log("enter");
                }
            }
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
