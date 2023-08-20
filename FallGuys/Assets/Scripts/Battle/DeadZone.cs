using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DeadZone : Bonus
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    [Header("Add Shield Bonus")]
    [SerializeField] private ShieldDeadZone shieldBonus;

    [Space]
    private float respawnTime;

    private List<GameObject> waitRespawnCars = new List<GameObject>();
    private List<GameObject> destroyedObjects = new List<GameObject>();

    private void Awake()
    {
        respawnTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bumper bumper = other.GetComponent<Bumper>();
        if (bumper != null)
        {
            //bumper.gameObject.SetActive(false);
            StartCoroutine(WaitAndResp(bumper));
            waitRespawnCars.Add(bumper.gameObject);
            Debug.Log("DeadZone");
        }
    }

    public override void Got()
    {
        // заглушка, чтобы не удалять объект
    }

    private void PlayerWasDestroy(GameObject playerGO)
    {
        destroyedObjects.Add(playerGO);

        if (waitRespawnCars.Contains(playerGO)) waitRespawnCars.Remove(playerGO);
    }

    private bool IsPlayerDead(GameObject playerGO)
    {
        foreach (var obj in destroyedObjects)
        {
            if (obj == playerGO) return true;
        }

        return false;
    }

    IEnumerator WaitAndResp(Bumper bumper)
    {
        yield return new WaitForSeconds(respawnTime);

        if(bumper != null && waitRespawnCars.Contains(bumper.gameObject))
        {
            GameObject car = bumper.gameObject;
            
            Vector3 pos = TargetsController.Instance.GetRespawnPosition().position;
            car.transform.position = new Vector3(pos.x, 5f, pos.z);
            car.transform.rotation = TargetsController.Instance.GetRespawnPosition().rotation;
            //car.SetActive(true);
            bumper.enabled = true;// отключился при входе в зону из Bumper
            bumper.GetBonus(shieldBonus);

            waitRespawnCars.Remove(bumper.gameObject);
        }
    }

    private void OnEnable()
    {
        VisualIntermediary.PlayerWasDeadEvent += PlayerWasDestroy;
    }

    private void OnDisable()
    {
        VisualIntermediary.PlayerWasDeadEvent -= PlayerWasDestroy;
    }
}
