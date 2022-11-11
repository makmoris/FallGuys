using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewVehicleColorData", menuName = "Customization Data/Vehicle Color")]
public class VehicleColorData : ScriptableObject
{
    [SerializeField] private string colorName;
    [SerializeField] private int colorCost;
    [SerializeField] private int colorCupsToUnlock;
    [SerializeField] private bool isColorAvailable;// ���� false - �� ��� ���������, ����� ��� ������/������� 
}
