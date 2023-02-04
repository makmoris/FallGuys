using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class EndGameController : MonoBehaviour
{
    private GameObject playerVehicleClone;

    [SerializeField] private Transform vehiclePlace;

    public void SetPlayerObjectClone(GameObject playerObj)
    {
        playerVehicleClone = playerObj;

        MakeEndGameObject();
    }

    private void MakeEndGameObject()
    {
        if (playerVehicleClone.GetComponent<WheelVehicle>() != null) playerVehicleClone.GetComponent<WheelVehicle>().enabled = false;
        if (playerVehicleClone.GetComponent<EngineSoundManager>() != null) playerVehicleClone.GetComponent<EngineSoundManager>().enabled = false;
        if (playerVehicleClone.GetComponent<Bumper>() != null) playerVehicleClone.GetComponent<Bumper>().enabled = false;
        if (playerVehicleClone.GetComponentInChildren<Weapon>() != null) playerVehicleClone.GetComponentInChildren<Weapon>().enabled = false;
        if (playerVehicleClone.GetComponent<Rigidbody>() != null) playerVehicleClone.GetComponent<Rigidbody>().isKinematic = true;
        if (playerVehicleClone.GetComponent<AudioListener>() != null) playerVehicleClone.GetComponent<AudioListener>().enabled = false;

        playerVehicleClone.transform.position = Vector3.zero;
        playerVehicleClone.transform.rotation = Quaternion.Euler(Vector3.zero);

        playerVehicleClone.transform.SetParent(vehiclePlace, false);

        Transform[] allChieldTransforms = playerVehicleClone.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldTransforms)
        {
            trn.gameObject.layer = 5;
        }
    }
}
