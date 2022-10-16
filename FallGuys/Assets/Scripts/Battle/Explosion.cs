using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public bool isBullet;
    [SerializeField] private float radius;
    [SerializeField] private float upwards;
    [SerializeField] private float force;

    [SerializeField] private float timeBeforExplotion;

    [SerializeField] private GameObject explosionEffect;

    private bool explosionDone;

    public ForceMode forceMode = ForceMode.VelocityChange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExplodeWithDelay();
        }
    }

    // метод вызываетс€ пулей при соприкосновении коллайдеров. ≈сли это бочка, то можешь дать врем€ перед взырвом. ≈сли игрок - взрыв сразу
    public void ExplodeWithDelay()
    {
        //if (explosionDone) return;

        //explosionDone = true;
        Invoke("Explode", timeBeforExplotion);
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void Explode()
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
            if (rigidbody != null) rigidbody.AddExplosionForce(force, transform.position, radius, upwards, forceMode);
        }

        foreach (var collider in collidersWithAttackPointers)
        {
            collider.gameObject.SetActive(true);
        }

        collidersWithoutAttackPointers.RemoveRange(0, collidersWithoutAttackPointers.Count);
        collidersWithAttackPointers.RemoveRange(0, collidersWithAttackPointers.Count);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
