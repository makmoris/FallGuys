using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasSizeFitter : MonoBehaviour
{
    private void Awake()
    {
        CheckSize();
    }

    private void CheckSize()
    {
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

        Vector2 referenceSize = canvasScaler.referenceResolution;
        Vector2 currentSize = new Vector2(Screen.width, Screen.height);

        float referenceValue = referenceSize.y / referenceSize.x;
        float currentValue = currentSize.y / currentSize.x;

        //if (currentValue < referenceValue) canvasScaler.matchWidthOrHeight += (referenceValue - currentValue) * 2f;
        if (currentValue < referenceValue) canvasScaler.matchWidthOrHeight = 1f;
    }
}
