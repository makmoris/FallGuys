using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private Canvas playerNameCanvas;
    [SerializeField] private TextMeshProUGUI playerNameText;

    private string playerName;
    public string Name => playerName;

    private Transform gameCameraTransform;

    private bool isInit;

    public void Initialize(string _name, Transform target)
    {
        playerName = _name;
        playerNameText.text = playerName;

        gameCameraTransform = target;

        isInit = true;
    }

    private void LateUpdate()
    {
        if (isInit)
        {
            playerNameCanvas.transform.LookAt(gameCameraTransform);
        }
    }
}
