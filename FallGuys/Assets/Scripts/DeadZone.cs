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
        Vector3 pos = targetsController.GetRandomRespawnPosition();
        gameObject.transform.position = new Vector3(pos.x, 25f, pos.z);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        gameObject.SetActive(true);
    }
}
