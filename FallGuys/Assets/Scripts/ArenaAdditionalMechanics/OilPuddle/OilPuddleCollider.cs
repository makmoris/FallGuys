using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPuddleCollider : MonoBehaviour
{
    private OilPuddle oilPuddle;

    private float decelerationAmount;

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

            Shield shield = other.GetComponentInChildren<Shield>(true);
            if(!shield.gameObject.activeSelf) rb.drag = decelerationAmount;

            oilPuddle.MakeDamage(other.gameObject);

            Debug.Log($"{other.name} ¬ъехал в лужу");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            oilPuddle.StopDamage(other.gameObject);

            Debug.Log($"{other.name} ѕќ »Ќ”Ћ лужу");
        }
    }

}
