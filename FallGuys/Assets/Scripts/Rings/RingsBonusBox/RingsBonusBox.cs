using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsBonusBox : MonoBehaviour
{
    [SerializeField] private VremBonusBox vremBonusBox;
    [Space]

    [SerializeField] private float boxRespawnTime = 5f;
    [Space]
    [SerializeField] private List<AdditionalBulletBonus> additionalBulletBonusesList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Weapon weapon = other.GetComponentInChildren<Weapon>();

            weapon.SetAdditionalBulletBonus(GetRandomAdditionalBonus(), true);

            CoroutineRunner.Run(WaitAndRespawn());
        }
    }

    private AdditionalBulletBonus GetRandomAdditionalBonus()
    {
        int rand = Random.Range(0, additionalBulletBonusesList.Count);

        return additionalBulletBonusesList[rand];
    }

    IEnumerator WaitAndRespawn()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(boxRespawnTime);
        gameObject.SetActive(true);
        if (vremBonusBox != null)  vremBonusBox.ShowBox();
    }
}

