using System.Collections.Generic;
using UnityEngine;

public class SlowdownObstacle : MonoBehaviour
{
    [SerializeField] private float decelerationAmount = 1.5f;

    private GameObject ignoreParent;

    private bool useSlowAfterExit;
    private float slowTimeAfterExitForAI;
    private float slowTimeAfterExitForPlayer;

    private bool isDealsDamage;
    private float damageValue;
    private float damageInterval;

    private List<Bumper> carsInPuddleList = new List<Bumper>();

    public void SetDecelerationAmount(float decelerationAmount)
    {
        this.decelerationAmount = decelerationAmount;
    }

    public void SetIgnoreParent(GameObject parentGO)
    {
        ignoreParent = parentGO;
    }

    public void SetSlowTimeAfterExit(float slowTimeForAI, float slowTimeForPlayer)
    {
        useSlowAfterExit = true;
        slowTimeAfterExitForAI = slowTimeForAI;
        slowTimeAfterExitForPlayer = slowTimeForPlayer;
    }

    public void SetDamage(float damageValue, float damageInterval)
    {
        this.damageValue = damageValue;
        this.damageInterval = damageInterval;

        isDealsDamage = true;
    }

    private void MakeDamage(Bumper damagableObjectBumper)
    {
        var healthBonus = new HealthBonus(damageValue);

        Bumper bumper = damagableObjectBumper;
        if (bumper != null) bumper.GetBonusFromOilPuddle(healthBonus, damageInterval);
    }

    private void StopDamage(Bumper damagableObjectBumper)
    {
        Bumper bumper = damagableObjectBumper;
        if (bumper != null) bumper.StopOilPuddleCoroutine();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Bumper bumper = other.GetComponent<Bumper>();

            if (!carsInPuddleList.Contains(bumper)) carsInPuddleList.Add(bumper);

            if (ignoreParent != null)
            {
                if(other.gameObject != ignoreParent)
                {
                    if (bumper != null) bumper.StartSlowdown(decelerationAmount);

                    if (isDealsDamage) MakeDamage(bumper);
                }
            }
            else
            {
                if (bumper != null) bumper.StartSlowdown(decelerationAmount);

                if (isDealsDamage) MakeDamage(bumper);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Bumper bumper = other.GetComponent<Bumper>();

            if (carsInPuddleList.Contains(bumper)) carsInPuddleList.Remove(bumper);

            if (bumper != null)
            {
                if (useSlowAfterExit)
                {
                    if (bumper.IsPlayer)
                    {
                        bumper.WaitAndStopSlowDown(slowTimeAfterExitForPlayer);
                    }
                    else
                    {
                        bumper.WaitAndStopSlowDown(slowTimeAfterExitForAI);
                    }
                }
                else
                {
                    bumper.StopSlowDown();
                }
            }

            if (isDealsDamage) StopDamage(bumper);
        }
    }

    private void OnDisable()
    {
        ignoreParent = null;

        if (isDealsDamage)
        {
            if (carsInPuddleList.Count != 0)
            {
                foreach (var bumper in carsInPuddleList)
                {
                    StopDamage(bumper);
                }
            }
        }
    }
}
