using UnityEngine;

public class PointerIconToEnemy : PointerIcon
{
    [Header("Target Icons")]
    [SerializeField] private Sprite targetEnemyPointerWithinScreen;
    [SerializeField] private Sprite targetEnemyPointerOffScreen;

    public override void PointerPositionRelativeToTheScreenHasChanged()
    {
        UpdateTargetPointer();
    }

    private void UpdateTargetPointer()//- to TargetPointer
    {
        if (!pointerIsOffScreen) _image.sprite = targetEnemyPointerWithinScreen;
        else _image.sprite = targetEnemyPointerOffScreen;
    }
}
