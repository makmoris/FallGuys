using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusExplosion : AdditionalBulletBonus
{
    private float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusExplosion;

    [Space]
    public LayerMask ignoreLayer; // ignore AttackPointer
    //public bool isBullet;
    [SerializeField] private float radius;
    [SerializeField] private float upwards;
    [SerializeField] private float force;

    [SerializeField] private GameObject explosionEffect;

    public ForceMode forceMode = ForceMode.VelocityChange;

    public override void PlayEffect(Vector3 effectPosition)
    {
        Debug.LogError("Playu");
        transform.localPosition = effectPosition;
        Explode();
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
    }
}
