using ArcadeVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceFinishSector : MonoBehaviour
{
    private List<ArcadeVehicleController> finishedDriversList = new List<ArcadeVehicleController>();

    public event System.Action<ArcadeVehicleController> ThisDriverFinishedEvent;

    public void FinishTrigger(ArcadeVehicleController arcadeVehicleController)
    {
        if (!finishedDriversList.Contains(arcadeVehicleController))
        {
            finishedDriversList.Add(arcadeVehicleController);

            //Rigidbody rigidbody = arcadeVehicleController.GetComponent<Rigidbody>();

            //rigidbody.velocity = Vector3.zero;
            //rigidbody.angularVelocity = Vector3.zero;

            arcadeVehicleController.gameObject.SetActive(false);

            ThisDriverFinishedEvent?.Invoke(arcadeVehicleController);
        }
    }
}
