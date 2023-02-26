using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //private AudioSource audioSource;

    private void Awake()
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);

        //audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
       // if(audioSource == null) audioSource = GetComponent<AudioSource>();

       // audioSource.Play();
    }
}
