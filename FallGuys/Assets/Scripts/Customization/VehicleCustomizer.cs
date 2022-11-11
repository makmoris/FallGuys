using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleCustomizer : MonoBehaviour // кидаем на префаб LobbyVehicle и префаб игрового Vehicle
{
    [Header("Data")]
    [SerializeField] private VehicleCustomizationData vehicleCustomizationData;

    // Material - Color
    private VehicleCustomization_Material vehicleCustomization_Material;

    [Header("Color Customization")]
    [SerializeField] private List<MeshRenderer> meshRenderers;

    [Header("Не трогать. Изменять через Data файл")]
    [SerializeField] private int _activeMaterialIndex;
    [SerializeField] private List<Material> _materials;

    private void Start()
    {
        GetDataOfMaterial();

        ShowActiveMaterial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _activeMaterialIndex++;

            if (_activeMaterialIndex > _materials.Count - 1)
            {
                _activeMaterialIndex = 0;
            }

            ShowActiveMaterial();
            SetNewDataOfMaterial();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _activeMaterialIndex--;

            if (_activeMaterialIndex < 0)
            {
                _activeMaterialIndex = _materials.Count - 1;
            }

            ShowActiveMaterial();
            SetNewDataOfMaterial();
        }
    }
    #region Material - Color
    
    public void ChangeColor(Material newMaterial)
    {
        int index = 0;

        foreach (var mat in _materials)
        {
            if (newMaterial == mat)
            {
                _activeMaterialIndex = index;
                break;
            }

            index++;
        }

        ShowActiveMaterial();
        SetNewDataOfMaterial();
    }

    private void ShowActiveMaterial()
    {
        foreach (var mR in meshRenderers)
        {
            mR.material = _materials[_activeMaterialIndex];
        }
    }

    private void SetNewDataOfMaterial()
    {
        vehicleCustomization_Material.materialActiveIndex = _activeMaterialIndex;
    }

    private void GetDataOfMaterial()
    {
        vehicleCustomization_Material = vehicleCustomizationData.VehicleCustomization_Material;

        _activeMaterialIndex = vehicleCustomization_Material.materialActiveIndex;
        _materials = vehicleCustomization_Material.materials;
    }
    #endregion
}
