using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : Bonus
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    [Space]
    public float respawnTime;
    public TargetsController targetsController;

    [SerializeField]private List<GameObject> destroyedObjects;

    private void OnTriggerEnter(Collider other)
    {
        Bumper bumper = other.GetComponent<Bumper>();
        if (bumper != null)
        {
            bumper.gameObject.SetActive(false);
            StartCoroutine(WaitAndResp(bumper.gameObject));
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
    }

    private bool IsPlayerDead(GameObject playerGO)
    {
        foreach (var obj in destroyedObjects)
        {
            if (obj == playerGO) return true;
        }

        return false;
    }

    IEnumerator WaitAndResp(GameObject gameObject)
    {
        yield return new WaitForSeconds(respawnTime);
        
        if (!IsPlayerDead(gameObject))
        {
            Vector3 pos = targetsController.GetRandomRespawnPosition();
            gameObject.transform.position = new Vector3(pos.x, 5f, pos.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.SetActive(true);
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
