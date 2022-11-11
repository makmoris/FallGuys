using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorContent : MonoBehaviour
{
    [SerializeField] private LobbyVehicle vehicleForContent;


    public GameObject GetContentVehicle()
    {
        return vehicleForContent.gameObject;
    }

    public VehicleCustomizer GetVehicleCustomizer()
    {
        return vehicleForContent.GetComponent<VehicleCustomizer>();
    }
}
