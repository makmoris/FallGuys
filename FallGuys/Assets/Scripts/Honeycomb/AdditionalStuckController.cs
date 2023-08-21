using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalStuckController : MonoBehaviour
{
    [Header("Stucking")]
    [SerializeField] private float stuckTimeThreshold = 0.5f;
    [Space]
    [SerializeField] private float stuckTime_RolledOntoRoofOfSide;

    private void Update()
    {
        if (Vector3.Dot(Vector3.up, transform.up) < 0.15f)
        {
            //Debug.Log("Перевернулся на крышу или на бок");

            stuckTime_RolledOntoRoofOfSide += Time.deltaTime;

            if (stuckTime_RolledOntoRoofOfSide >= stuckTimeThreshold)
            {
                stuckTime_RolledOntoRoofOfSide = 0f;

                transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            }

        }
        else stuckTime_RolledOntoRoofOfSide = 0f;
    }
}
