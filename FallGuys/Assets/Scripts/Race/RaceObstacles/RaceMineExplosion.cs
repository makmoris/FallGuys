using System.Collections.Generic;
using UnityEngine;

public class RaceMineExplosion : MonoBehaviour
{
    public LayerMask ignoreLayer; // ignore AttackPointer
    //public bool isBullet;
    [SerializeField] private float radius;
    [SerializeField] private float upwards;
    [SerializeField] private float force;

    [SerializeField] private GameObject explosionEffect;

    public ForceMode forceMode = ForceMode.VelocityChange;

    [SerializeField] private ParticleSystem mineActiveEffect;
    private AudioSource audioSource;
    [SerializeField] private AudioClip mineActiveSound;

    private RaceMine raceMine;

    private void Awake()
    {
        raceMine = GetComponentInParent<RaceMine>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        mineActiveEffect.Simulate(1f, true, true);
        mineActiveEffect.Play();

        audioSource.clip = mineActiveSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void Explode()
    {
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, radius);

        List<Collider> collidersWithoutAttackPointers = new List<Collider>();
        List<Collider> collidersWithAttackPointers = new List<Collider>();

        foreach (var overLappedCollider in overLappedColliders)
        {
            int _ignoreLayer = ignoreLayer.value;
            int _objLayer = overLappedCollider.gameObject.layer;
            int _objLayerMask = 1 << _objLayer;

            if (_objLayerMask != _ignoreLayer)
            {
                collidersWithoutAttackPointers.Add(overLappedCollider);
            }
            else
            {
                collidersWithAttackPointers.Add(overLappedCollider);
                overLappedCollider.gameObject.SetActive(false);
            }
        }

        foreach (var collider in collidersWithoutAttackPointers)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius, upwards, forceMode);
            }

        }

        foreach (var collider in collidersWithAttackPointers)
        {
            collider.gameObject.SetActive(true);
        }

        collidersWithoutAttackPointers.RemoveRange(0, collidersWithoutAttackPointers.Count);
        collidersWithAttackPointers.RemoveRange(0, collidersWithAttackPointers.Count);

        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        raceMine.ReloadMine(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Explode();
        }
    }
}
