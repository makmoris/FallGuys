using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [Header("Damage")]
    [MaxValue(0)] [SerializeField] private float damage;
    private HealthBonus healthBonus;

    [Header("Disable Time")]
    [SerializeField] private float disableWeaponTime;

    [Space]
    [SerializeField] private float timeBeforeLightning;

    [Space]
    [SerializeField] private GameObject appearancePlace;
    [SerializeField] private LightningCollider lightningCollider;

    private void Awake()
    {
        healthBonus = new HealthBonus(damage);

        lightningCollider.gameObject.SetActive(false);
        appearancePlace.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LightningStrike();
        }
    }

    public void StartLightningStrike(float waitTime)// вызывается контроллером
    {
        StartCoroutine(WaitAndStartLightning(waitTime));
    }

    public void MakeDamage(GameObject damagableObj)
    {
        Bumper bumper = damagableObj.GetComponent<Bumper>();
        if (bumper != null)
        {
            bumper.GetBonus(healthBonus);

            DisableWeaponBonus disableWeaponBonus = new DisableWeaponBonus(disableWeaponTime);
            bumper.GetBonus(disableWeaponBonus, damagableObj);
        }
    }

    public void HideAppearancePlace()
    {
        appearancePlace.SetActive(false);
    }

    private void LightningStrike()
    {
        appearancePlace.SetActive(true);

        StartCoroutine(WaitAndStrike());
    }

    IEnumerator WaitAndStartLightning(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        LightningStrike();
    }

    IEnumerator WaitAndStrike()
    {
        yield return new WaitForSeconds(timeBeforeLightning);

        lightningCollider.gameObject.SetActive(true);
    }
}
