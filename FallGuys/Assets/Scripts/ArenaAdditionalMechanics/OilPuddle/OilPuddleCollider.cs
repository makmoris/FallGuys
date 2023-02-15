using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPuddleCollider : MonoBehaviour
{
    private OilPuddle oilPuddle;

    private float decelerationAmount;

    private Dictionary<GameObject, float> oldDragDictionary = new Dictionary<GameObject, float>();

    private void Awake()
    {
        oilPuddle = transform.GetComponentInParent<OilPuddle>();
    }

    public void SetValues(float _decelerationAmount)
    {
        decelerationAmount = _decelerationAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(!oldDragDictionary.ContainsKey(other.gameObject)) oldDragDictionary.Add(other.gameObject, rb.drag);

            Shield shield = other.GetComponentInChildren<Shield>(true);
            if(!shield.gameObject.activeSelf) rb.drag = decelerationAmount;

            oilPuddle.MakeDamage(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.drag = oldDragDictionary[other.gameObject];

            oilPuddle.StopDamage(other.gameObject);
        }
    }

}
