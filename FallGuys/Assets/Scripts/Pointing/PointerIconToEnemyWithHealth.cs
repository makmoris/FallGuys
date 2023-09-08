using UnityEngine;

public class PointerIconToEnemyWithHealth : PointerIcon
{
    [Header("Within Screen Icons")]
    [SerializeField] private Sprite greenPointerWithinScreen;
    [SerializeField] private Sprite yellowPointerWithinScreen;
    [SerializeField] private Sprite redPointerWithinScreen;
    [Header("Off Screen Icons")]
    [SerializeField] private Sprite greenPointerOffScreen;
    [SerializeField] private Sprite yellowPointerOffScreen;
    [SerializeField] private Sprite redPointerOffScreen;

    [Header("DEBUG")]
    [SerializeField] private float startHealthValue;
    [SerializeField] private float _healthValue;
    private bool isFirstColorUpdate = true;

    public override void PointerPositionRelativeToTheScreenHasChanged()
    {
        UpdateHealthColor(_healthValue);
    }

    public override void UpdateHealthColor(float healthValue)//- To Health Pointer
    {
        if (isFirstColorUpdate)
        {
            startHealthValue = healthValue;

            if (!pointerIsOffScreen) _image.sprite = greenPointerWithinScreen;
            else _image.sprite = greenPointerOffScreen;

            isFirstColorUpdate = false;

            _healthValue = startHealthValue;
        }
        else
        {
            float healthPercent = (healthValue / startHealthValue) * 100f;

            if (healthPercent > 65)
            {
                if (!pointerIsOffScreen) _image.sprite = greenPointerWithinScreen;
                else _image.sprite = greenPointerOffScreen;
            }
            else if (healthPercent <= 65 && healthPercent > 35)
            {
                if (!pointerIsOffScreen) _image.sprite = yellowPointerWithinScreen;
                else _image.sprite = yellowPointerOffScreen;
            }
            else
            {
                if (!pointerIsOffScreen) _image.sprite = redPointerWithinScreen;
                else _image.sprite = redPointerOffScreen;
            }

            _healthValue = healthValue;
        }
    }
}
