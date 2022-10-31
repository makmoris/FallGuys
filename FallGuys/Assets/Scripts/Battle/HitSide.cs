using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSide : MonoBehaviour
{
    private HitSidesController _sidesController;

    private void Awake()
    {
        _sidesController = transform.parent.GetComponent<HitSidesController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _sidesController.SideHitted(gameObject.name, other);
    }
}
