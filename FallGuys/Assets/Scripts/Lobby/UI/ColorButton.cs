using UnityEngine;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private VehicleColorData colorData;
    [SerializeField] private Sprite vehicleColorImage;
    [SerializeField] private Material colorMaterial;

    private ColorContent parentColorContent;
    private VehicleCustomizer thisLobbyVehicle;

    private void Start()
    {
        parentColorContent = transform.GetComponentInParent<ColorContent>();
        thisLobbyVehicle = parentColorContent.GetVehicleCustomizer();
    }

    public void SetColor()
    {
        thisLobbyVehicle.ChangeColor(colorMaterial);
    }
}
