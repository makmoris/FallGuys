using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDrop : MonoBehaviour
{
    private OilPuddle oilPuddle;
    private Vector3 startPosition;
    private Rigidbody _rb;

    private void Awake()
    {
        oilPuddle = transform.parent.GetComponent<OilPuddle>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector3(0f, -20f, 0f);
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        oilPuddle.MakePuddle();

        gameObject.SetActive(false);
        transform.position = startPosition;
        _rb.velocity = new Vector3(0f, -20f, 0f);
    }
}
