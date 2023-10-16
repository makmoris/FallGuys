using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPuddle : Bonus
{
    [SerializeField] private float damageInterval = 0.5f;// пауза между получением урона пока игрок в луже
    [SerializeField] private float decelerationAmount = 2.5f;// величина замедления
    [Space]
    [SerializeField] private float timeBeforeDropTheOilDrop;
    [SerializeField] private float puddleLiveTime;
    [Space]
    [SerializeField] private GameObject appearancePlace;
    [SerializeField] private OilDrop oilDrop;
    [SerializeField] private List<GameObject> puddleVariants;
    private GameObject currentPuddle;

    private void Awake()
    {
        oilDrop.gameObject.SetActive(false);
        appearancePlace.SetActive(false);
        HidePuddlesInList();
        SetValuesInOilPuddleColliders();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartOilPuddleAppearance();
        }
    }

    public void StartOilPuddleAppearanceFromController(float waitTime)// вызывается контроллером
    {
        StartCoroutine(WaitAndStartOilPuddleAppearanceFromController(waitTime));
    }

    public void MakeDamage(GameObject damagableObject)
    {
        Bumper bumper = damagableObject.GetComponent<Bumper>();
        if (bumper != null) bumper.GetBonusFromOilPuddle(this, damageInterval);
    }

    public void StopDamage(GameObject damagableObject)
    {
        Bumper bumper = damagableObject.GetComponent<Bumper>();
        if (bumper != null) bumper.StopOilPuddleCoroutine();
    }

    private void StartOilPuddleAppearance()
    {
        currentPuddle = GetCurrentPuddle();// выбираем лужу

        appearancePlace.SetActive(true);

        Invoke("DropTheOilDrop", timeBeforeDropTheOilDrop);
    }

    private void DropTheOilDrop()
    {
        oilDrop.gameObject.SetActive(true);// сбрасываем каплю
    }

    public void MakePuddle()// вызывается OilDrop, когда капля упала на земплю
    {
        // анимация разливания лужи
        appearancePlace.SetActive(false);
        StartCoroutine(PuddleMakingAnimation());
    }

    private GameObject GetCurrentPuddle()// выбираем каплю из списка
    {
        int rand = Random.Range(0, puddleVariants.Count);
        return puddleVariants[rand];
    }

    private void HidePuddlesInList()
    {
        if(puddleVariants.Count != 0)
        {
            foreach (var item in puddleVariants)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private void SetValuesInOilPuddleColliders()
    {
        if (puddleVariants.Count != 0)
        {
            foreach (var item in puddleVariants)
            {
                OilPuddleCollider oilPuddleCollider = item.transform.GetComponentInChildren<OilPuddleCollider>();
                oilPuddleCollider.SetValues(decelerationAmount);
            }
        }
    }

    IEnumerator WaitAndStartOilPuddleAppearanceFromController(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        StartOilPuddleAppearance();
    }
    IEnumerator PuddleMakingAnimation()
    {
        currentPuddle.SetActive(true);
        currentPuddle.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
        for (float t = 0; t < 1f; t += Time.deltaTime * 3f)
        {
            currentPuddle.transform.localScale = Vector3.one * t;
            yield return null;
        }
        currentPuddle.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(puddleLiveTime);

        for (float t = 0; t < 1f; t += Time.deltaTime * 3f)
        {
            currentPuddle.transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }

        currentPuddle.SetActive(false);

        gameObject.SetActive(false);
    }
}
