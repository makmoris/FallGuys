using UnityEngine;

public class AttackBan : MonoBehaviour
{
    private GameObject banImage;
    private GameObject currentPlayer;

    private void Awake()
    {
        banImage = transform.GetChild(0).gameObject;
        banImage.SetActive(false);
    }

    private void SetCurrentPlayer(GameObject gameObj)
    {
        currentPlayer = gameObj;
    }

    private void ShowBanImage(GameObject gameObj)
    {
        if (currentPlayer == gameObj) banImage.SetActive(true);
    }

    private void HideBanImage(GameObject gameObj)
    {
        if (currentPlayer == gameObj) banImage.SetActive(false);
    }

    private void OnEnable()
    {
        Installer.IsCurrentPlayer += SetCurrentPlayer;
        PlayerEffector.EnableWeaponEvent += HideBanImage;
        PlayerEffector.DisableWeaponEvent += ShowBanImage;
    }
    private void OnDisable()
    {
        Installer.IsCurrentPlayer -= SetCurrentPlayer;
        PlayerEffector.EnableWeaponEvent -= HideBanImage;
        PlayerEffector.DisableWeaponEvent -= ShowBanImage;
    }
}
