using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private Canvas playerNameCanvas;
    [SerializeField] private TextMeshProUGUI playerNameText;

    private string playerName;
    public string Name => playerName;

    private Transform target;

    private bool isInit;

    public void Initialize(string _name, Transform target)
    {
        playerName = _name;
        playerNameText.text = playerName;

        this.target = target;

        isInit = true;
    }

    private void LateUpdate()
    {
        if (isInit)
        {
            playerNameCanvas.transform.LookAt(target);
        }
    }
}
