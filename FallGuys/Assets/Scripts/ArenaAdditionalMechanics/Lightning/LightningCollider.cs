using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCollider : MonoBehaviour
{
    private Lightning lightning;
    [SerializeField]private Vector3 startPosition;
    private Collider _collider;

    [SerializeField]private bool moving;
    [SerializeField] private float speed;

    private AudioSource audioSource;

    private void Awake()
    {
        lightning = transform.parent.GetComponent<Lightning>();
        _collider = GetComponent<Collider>();
        startPosition = transform.localPosition;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        moving = true;
    }

    private void OnDisable()
    {
        moving = false;
    }

    private void Update()
    {
        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0f, transform.localPosition.z),
                speed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, new Vector3(transform.localPosition.x, 0f, transform.localPosition.z)) < 0.001f)
            {
                StartCoroutine(WaitAndOff());
                moving = false;
                _collider.enabled = false;
                lightning.HideAppearancePlace();
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            lightning.MakeDamage(other.gameObject);
        }

        _collider.enabled = false;
        StartCoroutine(WaitAndOff());
        moving = false;
        lightning.HideAppearancePlace();
        audioSource.Play();
    }

    IEnumerator WaitAndOff()// чтобы молния не отключалась мгновенно
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        transform.localPosition = startPosition;
        _collider.enabled = true;
        lightning.gameObject.SetActive(false);
    }
}
