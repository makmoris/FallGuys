using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewVehicleColorData", menuName = "Customization Data/Vehicle Color")]
public class VehicleColorData : ScriptableObject
{
    [SerializeField] private string colorName;
    [SerializeField] private int colorCost;
    [SerializeField] private int colorCupsToUnlock;
    [SerializeField] private bool isColorAvailable;// если false - то оно заблочено, нужно его купить/открыть 

    public string ColorName
    {
        get
        {
            return colorName;
        }
    }
    public int ColorCost
    {
        get
        {
            return colorCost;
        }
    }
    public int ColorCupsToUnlock
    {
        get
        {
            return colorCupsToUnlock;
        }
    }
    public bool IsColorAvailable
    {
        get
        {
            return isColorAvailable;
        }
    }
}
