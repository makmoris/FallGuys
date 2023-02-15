using UnityEngine;

public class SpikesCollider : MonoBehaviour
{
    private SandSpikes sandSpikes;

    private void Awake()
    {
        sandSpikes = transform.parent.GetComponent<SandSpikes>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car")) sandSpikes.DamageAtPlayer();
    }
}
