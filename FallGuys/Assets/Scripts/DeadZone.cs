using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public TargetsController targetsController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bumper>() != null)
        {
            other.gameObject.SetActive(false);
            StartCoroutine(WaitAndResp(other.gameObject));
        }
    }

    IEnumerator WaitAndResp(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = targetsController.GetRandomRespawnPosition();
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        gameObject.SetActive(true);
    }
}
