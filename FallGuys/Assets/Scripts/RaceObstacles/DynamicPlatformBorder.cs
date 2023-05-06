using UnityEngine;
using System;

public class DynamicPlatformBorder : MonoBehaviour
{
    public static event Action<GameObject> CarLeftTheDynamicPlatformBorderEvent;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car")) CarLeftTheDynamicPlatformBorderEvent?.Invoke(other.gameObject);
    }
}
