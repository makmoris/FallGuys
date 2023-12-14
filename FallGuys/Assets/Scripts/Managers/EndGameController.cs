using ArcadeVP;
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
        ArcadeVehicleController arcadeVehicleController = playerVehicleClone.GetComponent<ArcadeVehicleController>();
        if (arcadeVehicleController != null) arcadeVehicleController.enabled = false;

        EngineSoundManager engineSoundManager = playerVehicleClone.GetComponent<EngineSoundManager>();
        if (engineSoundManager != null) engineSoundManager.enabled = false;

        Bumper bumper = playerVehicleClone.GetComponent<Bumper>();
        if (bumper != null) bumper.enabled = false;

        Weapon weapon = playerVehicleClone.GetComponentInChildren<Weapon>();
        if (weapon != null) weapon.enabled = false;

        Rigidbody rigidbody = playerVehicleClone.GetComponent<Rigidbody>();
        if (rigidbody != null) rigidbody.isKinematic = true;

        AudioListener audioListener = playerVehicleClone.GetComponent<AudioListener>();
        if (audioListener != null) audioListener.enabled = false;

        PlayerName playerName = playerVehicleClone.GetComponent<PlayerName>();
        if (playerName != null) playerName.HideNameDisplay();

        playerVehicleClone.transform.position = Vector3.zero;
        playerVehicleClone.transform.rotation = Quaternion.Euler(Vector3.zero);

        playerVehicleClone.transform.SetParent(vehiclePlace, false);

        Transform[] allChieldTransforms = playerVehicleClone.GetComponentsInChildren<Transform>();
        foreach (var trn in allChieldTransforms)
        {
            trn.gameObject.layer = 5;
        }
    }

    public void EnabledAudioListener()
    {
        if (playerVehicleClone.GetComponent<AudioListener>() != null) playerVehicleClone.GetComponent<AudioListener>().enabled = true;
    }
}
