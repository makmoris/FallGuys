using UnityEngine;

public class CupsTextInBuyButton : MonoBehaviour
{
    private Vector2 cupsTextPosition;
    public Vector2 CupsTextPosition
    {
        get { return cupsTextPosition; }
    }

    private void Awake()
    {
        cupsTextPosition = GetComponent<RectTransform>().anchoredPosition;
    }
}
