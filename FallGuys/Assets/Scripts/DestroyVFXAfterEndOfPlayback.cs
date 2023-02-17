using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyVFXAfterEndOfPlayback : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float duration;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        duration = _particleSystem.duration;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
