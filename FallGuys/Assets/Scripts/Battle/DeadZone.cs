using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class DeadZone : MonoBehaviour
{
    [MaxValue(0)][SerializeField] private float damage = -5f;
    [MinValue(0)][SerializeField] private float shieldTime = 4f;

    private HealthBonus healthBonus;
    private ShieldBonus shieldBonus;

    [Space]
    [Header("Arena Spawn Controller")]
    [SerializeField] private ArenaSpawnController arenaSpawnController;

    private float respawnTime;

    private List<GameObject> waitRespawnCars = new List<GameObject>();

    private void Awake()
    {
        respawnTime = 0f;

        healthBonus = new HealthBonus(damage);
        shieldBonus = new ShieldBonus(shieldTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bumper bumper = other.GetComponent<Bumper>();
        if (bumper != null)
        {
            bumper.GetBonus(healthBonus, this.gameObject);

            Debug.Log("DeadZone");
        }
    }

    public void RespawnCar(Bumper bumper)
    {
        StartCoroutine(WaitAndResp(bumper));
        waitRespawnCars.Add(bumper.gameObject);
    }

    IEnumerator WaitAndResp(Bumper bumper)
    {
        yield return new WaitForSeconds(respawnTime);

        if(bumper != null && waitRespawnCars.Contains(bumper.gameObject))
        {
            GameObject car = bumper.gameObject;

            car.SetActive(false);
            car.SetActive(true);

            Vector3 pos = arenaSpawnController.GetRespawnPosition().position;
            car.transform.position = new Vector3(pos.x, 5f, pos.z);
            car.transform.rotation = arenaSpawnController.GetRespawnPosition().rotation;
            //car.SetActive(true);
            bumper.enabled = true;// отключился при входе в зону из Bumper
            bumper.GetBonus(shieldBonus);

            waitRespawnCars.Remove(bumper.gameObject);
        }
    }
}
