using UnityEngine;

public class Layout : MonoBehaviour
{
    [SerializeField] private LayoutWindow layoutWindow;

    private void Start()
    {
        CheckLayout();
    }

    private void CheckLayout()
    {
        layoutWindow.ShowCurrentLayout();
    }
}
