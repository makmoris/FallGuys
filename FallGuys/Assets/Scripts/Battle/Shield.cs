using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void Awake()
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
    }
}
