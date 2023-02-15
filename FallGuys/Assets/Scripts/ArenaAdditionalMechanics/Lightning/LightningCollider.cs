using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCollider : MonoBehaviour
{
    private Lightning lightning;
    private Vector3 startPosition;
    private Collider _collider;

    [SerializeField]private bool moving;
    [SerializeField] private float speed;

    private void Awake()
    {
        lightning = transform.parent.GetComponent<Lightning>();
        _collider = GetComponent<Collider>();
        startPosition = transform.position;
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0f, transform.position.z),
                speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, 0f, transform.position.z)) < 0.001f)
            {
                StartCoroutine(WaitAndOff());
                moving = false;
                _collider.enabled = false;
                lightning.HideAppearancePlace();
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
    }

    IEnumerator WaitAndOff()// чтобы молния не отключалась мгновенно
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        transform.position = startPosition;
        _collider.enabled = true;
    }
}
