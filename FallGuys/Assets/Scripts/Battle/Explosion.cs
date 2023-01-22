using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Bonus
{
    

    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    [Space]
    public LayerMask ignoreLayer;
    //public bool isBullet;
    [SerializeField] private float radius;
    [SerializeField] private float upwards;
    [SerializeField] private float force;

    [SerializeField] private float timeBeforExplotion;

    [SerializeField] private GameObject explosionEffect;

    public ForceMode forceMode = ForceMode.VelocityChange;

    private bool explosionDone;

    [Header("Mine or Bomb")]
    [SerializeField] private bool isMine;

    // метод вызывается пулей при соприкосновении коллайдеров. Если это бочка, то можешь дать время перед взырвом. Если игрок - взрыв сразу
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
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius, upwards, forceMode);

                Bumper bumper = collider.GetComponent<Bumper>();
                if (bumper != null) bumper.GetBonus(this);
            }
                
        }

        foreach (var collider in collidersWithAttackPointers)
        {
            collider.gameObject.SetActive(true);
        }

        collidersWithoutAttackPointers.RemoveRange(0, collidersWithoutAttackPointers.Count);
        collidersWithAttackPointers.RemoveRange(0, collidersWithAttackPointers.Count);

        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    public override void Got()
    {
        // заглушка, чтобы не удалять объект
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMine) ExplodeWithDelay();
    }
}
