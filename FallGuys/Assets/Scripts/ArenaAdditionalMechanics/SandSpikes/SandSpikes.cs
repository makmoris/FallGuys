using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSpikes : Bonus
{
    public LayerMask ignoreLayer;
    [SerializeField] private float radius;
    [SerializeField] private float upwards;
    [SerializeField] private float force;
    public ForceMode forceMode = ForceMode.VelocityChange;
    [Space]

    [SerializeField] private float timeBeforeAppearance;
    [SerializeField] private float triggerTime = 0.5f;
    [SerializeField] private float colliderTime = 2.5f;
    [SerializeField] private GameObject appearancePlace;

    [SerializeField] private GameObject spikesEffectGO;
    private ParticleSystem spikesEffect;
    private CapsuleCollider spikesCollider;

    private AudioSource audioSource;

    private void Awake()
    {
        appearancePlace.SetActive(false);

        spikesEffect = spikesEffectGO.GetComponent<ParticleSystem>();
        spikesCollider = spikesEffectGO.GetComponent<CapsuleCollider>();

        spikesCollider.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartSpikesAppearance();
        }
    }

    public void StartSandSpikesAppearanceFromController(float waitTime)// вызывается контроллером
    {
        StartCoroutine(WaitAndStartSandSpikesAppearFromController(waitTime));
    }

    private void StartSpikesAppearance()
    {
        appearancePlace.SetActive(true);
        StartCoroutine(WaitAndSpikesAppear());
    }

    public void DamageAtPlayer()// вызывается SpikesCollider по триггеру
    {
        Debug.Log($"Подбрасываем и дамагаем");
        TossPlayers();
    }

    private void TossPlayers()
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
    }

    IEnumerator WaitAndStartSandSpikesAppearFromController(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        StartSpikesAppearance();
    }

    IEnumerator WaitAndSpikesAppear()
    {
        yield return new WaitForSeconds(timeBeforeAppearance);

        audioSource.Play();

        appearancePlace.SetActive(false);
        spikesEffect.Play();
        spikesCollider.enabled = true;
        spikesCollider.isTrigger = true;

        yield return new WaitForSeconds(triggerTime);
        spikesCollider.isTrigger = false;

        yield return new WaitForSeconds(colliderTime - triggerTime);
        spikesCollider.enabled = false;

        yield return new WaitForSeconds(1f); // для пула
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
