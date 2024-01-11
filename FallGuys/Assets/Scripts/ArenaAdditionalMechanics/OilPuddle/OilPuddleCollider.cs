using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPuddleCollider : MonoBehaviour
{
    private OilPuddle oilPuddle;

    private float decelerationAmount;

    private List<GameObject> carsInPuddleList = new List<GameObject>();

    private void Awake()
    {
        oilPuddle = transform.GetComponentInParent<OilPuddle>();
    }

    public void SetValues(float _decelerationAmount)
    {
        decelerationAmount = _decelerationAmount;
    }

    public void CheckCarsInOilPuddleAndDeactivate()
    {
        if (carsInPuddleList.Count != 0)
        {
            foreach (var car in carsInPuddleList)
            {
                oilPuddle.StopDamage(car);
            }
        }

        oilPuddle.DeactivateOilPuddle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (!carsInPuddleList.Contains(other.gameObject)) carsInPuddleList.Add(other.gameObject);

            Rigidbody rb = other.GetComponent<Rigidbody>();

            Shield shield = other.GetComponentInChildren<Shield>(true);
            if(!shield.gameObject.activeSelf) rb.drag = decelerationAmount;

            oilPuddle.MakeDamage(other.gameObject);

            Debug.Log($"{other.name} ¬˙Âı‡Î ‚ ÎÛÊÛ");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (carsInPuddleList.Contains(other.gameObject)) carsInPuddleList.Remove(other.gameObject);

            oilPuddle.StopDamage(other.gameObject);

            Debug.Log($"{other.name} œŒ »Õ”À ÎÛÊÛ");
        }
    }
}
