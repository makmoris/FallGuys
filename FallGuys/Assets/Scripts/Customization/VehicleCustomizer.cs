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
    [SerializeField]private int _activeMaterialIndex;
    [SerializeField]private List<Material> _materials;

    private void Awake()
    {
        GetDataOfMaterial();
    }

    private void Start()
    {
        //GetDataOfMaterial();

        ShowActiveMaterial();
    }


    #region Material - Color

    public Material GetActiveMaterial()
    {
        return _materials[_activeMaterialIndex];
    }

    public void ShowColor(Material newMaterial)
    {
        int index = 0;

        foreach (var mat in _materials)
        {
            if (newMaterial == mat) break;

            index++;
        }

        ShowPreMaterial(index);
    }

    public void BackToActiveColor()
    {
        ShowActiveMaterial();
    }

    private void ShowPreMaterial(int indexMat)
    {
        foreach (var mR in meshRenderers)
        {
            mR.material = _materials[indexMat];
        }
    }

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

    public void SetColorMaterial(Material material)
    {
        int index = 0;

        foreach (var mat in _materials)
        {
            if (material == mat)
            {
                _activeMaterialIndex = index;
                break;
            }

            index++;
        }

        ShowActiveMaterial();
    }

    public Material GetRandomColorMaterial()
    {
        GetDataOfMaterial();

        int randomIndex = Random.Range(0, _materials.Count);
        Material rMaterial = _materials[randomIndex];

        return rMaterial;
    }

    private void ShowActiveMaterial()
    {
        foreach (var mR in meshRenderers)
        {
            if(mR != null) mR.material = _materials[_activeMaterialIndex];
        }
    }

    private void SetNewDataOfMaterial()
    {
        //vehicleCustomization_Material.materialActiveIndex = _activeMaterialIndex;
        
        vehicleCustomizationData.SaveNewSelectedActiveIndex(_activeMaterialIndex);
    }

    private void GetDataOfMaterial()
    {
        vehicleCustomization_Material = vehicleCustomizationData.VehicleCustomization_Material;

        _activeMaterialIndex = vehicleCustomization_Material.materialActiveIndex;
        _materials = vehicleCustomization_Material.materials;
    }
    #endregion
}
