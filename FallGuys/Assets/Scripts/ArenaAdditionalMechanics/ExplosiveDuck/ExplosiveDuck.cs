using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDuck : Bonus, IExplosion
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    [Header("Explosion")]
    public LayerMask ignoreLayer;
    //public bool isBullet;
    [SerializeField] private float radius = 5;
    [SerializeField] private float upwards = 1;
    [SerializeField] private float force = 10;
    public ForceMode forceMode = ForceMode.VelocityChange;
    
    [Header("Throwing")]
    [SerializeField] private int minThrowForce = 20;
    [SerializeField] private int maxThrowForce = 50;
    [Space]
    [SerializeField] private float minTimeToExplosion = 2f;
    [SerializeField] private float maxTimeToExplosion = 4f;

    private Rigidbody _rb;
    private Renderer _renderer;
    private Color startColor;
    [Space]
    [SerializeField] private float colorChangeTime = 1f;
    [SerializeField] private Color explosionColor;

    [Space]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject duck;

    private Vector3 startDuckLocalPosition;
    private Quaternion startDuckLocalRotation;

    private void Awake()
    {
        _rb = duck.GetComponent<Rigidbody>();

        _renderer = duck.GetComponent<Renderer>();
        _renderer.material.shader = Shader.Find("Standard");
        startColor = _renderer.material.GetColor("_Color");

        _rb.isKinematic = true;

        startDuckLocalPosition = duck.transform.localPosition;
        startDuckLocalRotation = duck.transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ThrowDuck();
        }
    }

    public void StartThrowingDuck(float waitTime)// вызывается контроллером
    {
        duck.SetActive(false);
        StartCoroutine(WaitAndStartThrowingDuck(waitTime));
    }

    private void ThrowDuck()
    {
        gameObject.SetActive(true);
        duck.SetActive(true);

        _rb.isKinematic = false;

        int randomThrowForce = Random.Range(minThrowForce, maxThrowForce + 1);

        _rb.AddRelativeForce(-Vector3.forward * randomThrowForce, ForceMode.VelocityChange);

        float timeToExplosion = Random.Range(minTimeToExplosion, maxTimeToExplosion);
        StartCoroutine(StartExplosion(timeToExplosion));
    }

    public void Explode()
    {
        Collider[] overLappedColliders = Physics.OverlapSphere(duck.transform.position, radius);

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
                rigidbody.AddExplosionForce(force, duck.transform.position, radius, upwards, forceMode);

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

        Instantiate(explosionEffect, duck.transform.position, Quaternion.identity);

        gameObject.SetActive(false);

        duck.transform.localPosition = startDuckLocalPosition;
        duck.transform.localRotation = startDuckLocalRotation;
        _renderer.material.SetColor("_Color", startColor);
        _rb.isKinematic = true;
    }

    IEnumerator WaitAndStartThrowingDuck(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ThrowDuck();
    }

    public IEnumerator StartExplosion(float timeToExplosion)
    {
        yield return new WaitForSeconds(timeToExplosion);

        float currTime = 0f;
        do
        {
            Color _color = Color.Lerp(startColor, explosionColor, currTime / colorChangeTime);

            _renderer.material.SetColor("_Color", _color);
            
            currTime += Time.deltaTime;
            yield return null;
        }
        while
            (currTime <= colorChangeTime);

        Explode();
    }

    public override void Got()
    {
        // заглушка, чтобы не удалять объект
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(duck.transform.position, radius);
    }
}
