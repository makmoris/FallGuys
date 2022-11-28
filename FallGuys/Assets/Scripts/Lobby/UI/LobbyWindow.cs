using UnityEngine;

public class LobbyWindow : MonoBehaviour
{
    private bool notFirstActive;

    private void OnEnable()
    {
        if (notFirstActive)
        {
            LoadCurrentVehicle();
        }
    }

    private void Start()
    {
        if (!notFirstActive)
        {
            LoadCurrentVehicle();
            notFirstActive = true;
        }
    }

    private void LoadCurrentVehicle()
    {
        LobbyManager.Instance.LoadActiveSaveCharacter();
    }
}
