using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusExplosion : AdditionalBulletBonus
{
    [Header("Ignore Parent")]
    public bool isIgnoreParent;

    private HealthBonus healthBonus;
    [Space]

    public LayerMask ignoreLayer; // ignore AttackPointer
    //public bool isBullet;
    [SerializeField] private float radius;
    [SerializeField] private float upwards;
    [SerializeField] private float force;

    [SerializeField] private GameObject explosionEffect;

    public ForceMode forceMode = ForceMode.VelocityChange;

    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusExplosion;

    public override void PlayEffect(Vector3 effectPosition, GameObject parentGO)
    {
        transform.localPosition = effectPosition;
        Explode(parentGO);
    }

    public override Bonus GetBonus()
    {
        healthBonus = new HealthBonus(0);
        return healthBonus;
    }

    private void Explode(GameObject parentGO)
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
                if (isIgnoreParent)
                {
                    if(rigidbody.gameObject != parentGO)
                    {
                        rigidbody.AddExplosionForce(force, transform.position, radius, upwards, forceMode);
                    }
                }
                else
                {
                    rigidbody.AddExplosionForce(force, transform.position, radius, upwards, forceMode);
                }

                //Bumper bumper = collider.GetComponent<Bumper>();
                //if (bumper != null) bumper.GetBonus(healthBonus);
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
