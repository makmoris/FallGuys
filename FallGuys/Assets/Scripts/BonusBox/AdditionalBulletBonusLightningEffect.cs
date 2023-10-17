using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusLightningEffect : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private bool moving;
    [SerializeField] private float speed;

    private AudioSource audioSource;
    private AdditionalBulletBonusLightning additionalBulletBonusLightning;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        additionalBulletBonusLightning = GetComponentInParent<AdditionalBulletBonusLightning>();
        startPosition = transform.localPosition;
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
                audioSource.Play();
            }
        }
    }

    IEnumerator WaitAndOff()// чтобы молния не отключалась мгновенно
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        transform.localPosition = startPosition;
        Destroy(additionalBulletBonusLightning);
    }
}
