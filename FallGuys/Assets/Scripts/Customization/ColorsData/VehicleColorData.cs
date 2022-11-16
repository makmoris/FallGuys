using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewVehicleColorData", menuName = "Customization Data/Vehicle Color")]
public class VehicleColorData : ScriptableObject
{
    [SerializeField] private string colorName;
    [SerializeField] private int colorCost;
    [SerializeField] private int colorCupsToUnlock;
    [SerializeField] private bool isColorAvailable;// если false - то оно заблочено, нужно его купить/открыть 
    [Space]
    [SerializeField] private bool alwaysAvailable;// если true, значит игнорируем то, что в памяти. Объект всегда доступен

                // name каждого объекта всегда уникальный, т.к. юнити не позволяет создавать два объекта с одинаковым именем. люблю ее

    public void LoadData()
    {
        isColorAvailable = ElementsAvailableData.Instance.GetAvailableStatus(name);

        if (alwaysAvailable) isColorAvailable = true;
    }

    public void SaveNewAwailableStatus(bool availableStatus)// вызывается из ColorButton в момент покупки цвета
    {
        isColorAvailable = availableStatus;

        ElementsAvailableData.Instance.SaveElevemtAvailableStatus(name, isColorAvailable);

        Debug.Log(name + " сохранил isAvailable");
    }

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
