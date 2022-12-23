using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LayoutWindow : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject arrows;
    [SerializeField] private GameObject leftRightJoystick;
    [SerializeField] private GameObject allJoystick;
    [SerializeField] private GameObject gasButton;
    [SerializeField] private GameObject backButton;
    [Header("Window")]
    [SerializeField] private Image arrowsButton;
    [SerializeField] private Image leftRightJoystickButton;
    [SerializeField] private Image allJoystickButton;

    private Vector3 activeSize = new Vector3(1.05f, 1.05f, 1f);
    private Vector3 deactiveSize = Vector3.one;

    private Color activeBackColor = new Color(37f / 255f, 59f / 255f, 89f / 255f);
    private Color deactiveBackColor = new Color(23f / 255f, 37f / 255f, 55f / 255f);

    private Color activeTextColor = Color.white;
    private Color deactiveTextColor = new Color(214f / 255f, 214f / 255f, 214f / 255f);

    private string key = "Layout";
    private int layoutIndex;

    private void OnEnable()
    {
        ShowCurrentLayout();
    }

    public void SetArrowsButtonActiveLayout()
    {
        arrowsButton.rectTransform.localScale = activeSize;
        arrowsButton.color = activeBackColor;
        var _text = arrowsButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = activeTextColor;
        var _activeBorder = arrowsButton.transform.Find("ActiveBorder").gameObject;
        _activeBorder.SetActive(true);

        arrows.SetActive(true);
        leftRightJoystick.SetActive(false);
        allJoystick.SetActive(false);
        gasButton.SetActive(true);
        backButton.SetActive(true);

        DeactiveLeftRightJoysticButton();
        DeactiveAllJoysticButton();

        layoutIndex = 0;
        PlayerPrefs.SetInt(key, layoutIndex);
    }
    public void SetLeftRightJoystickButtonActiveLayout()
    {
        leftRightJoystickButton.rectTransform.localScale = activeSize;
        leftRightJoystickButton.color = activeBackColor;
        var _text = leftRightJoystickButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = activeTextColor;
        var _activeBorder = leftRightJoystickButton.transform.Find("ActiveBorder").gameObject;
        _activeBorder.SetActive(true);

        arrows.SetActive(false);
        leftRightJoystick.SetActive(true);
        allJoystick.SetActive(false);
        gasButton.SetActive(true);
        backButton.SetActive(true);

        DeactiveArrowsButton();
        DeactiveAllJoysticButton();

        layoutIndex = 1;
        PlayerPrefs.SetInt(key, layoutIndex);
    }
    public void SetAllJoystickButtonActiveLayout()
    {
        allJoystickButton.rectTransform.localScale = activeSize;
        allJoystickButton.color = activeBackColor;
        var _text = allJoystickButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = activeTextColor;
        var _activeBorder = allJoystickButton.transform.Find("ActiveBorder").gameObject;
        _activeBorder.SetActive(true);

        arrows.SetActive(false);
        leftRightJoystick.SetActive(false);
        allJoystick.SetActive(true);
        gasButton.SetActive(false);
        backButton.SetActive(false);

        DeactiveArrowsButton();
        DeactiveLeftRightJoysticButton();

        layoutIndex = 2;
        PlayerPrefs.SetInt(key, layoutIndex);
    }


    private void DeactiveArrowsButton()
    {
        arrowsButton.rectTransform.localScale = deactiveSize;
        arrowsButton.color = deactiveBackColor;
        var _text = arrowsButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = deactiveTextColor;
        var _activeBorder = arrowsButton.transform.Find("ActiveBorder").gameObject;
        _activeBorder.SetActive(false);
    }
    private void DeactiveLeftRightJoysticButton()
    {
        leftRightJoystickButton.rectTransform.localScale = deactiveSize;
        leftRightJoystickButton.color = deactiveBackColor;
        var _text = leftRightJoystickButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = deactiveTextColor;
        var _activeBorder = leftRightJoystickButton.transform.Find("ActiveBorder").gameObject;
        _activeBorder.SetActive(false);
    }
    private void DeactiveAllJoysticButton()
    {
        allJoystickButton.rectTransform.localScale = deactiveSize;
        allJoystickButton.color = deactiveBackColor;
        var _text = allJoystickButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = deactiveTextColor;
        var _activeBorder = allJoystickButton.transform.Find("ActiveBorder").gameObject;
        _activeBorder.SetActive(false);
    }

    public void ShowCurrentLayout()
    {
        layoutIndex = PlayerPrefs.GetInt(key, 0);

        switch (layoutIndex)
        {
            case 0:// arrows
                SetArrowsButtonActiveLayout();
                break;

            case 1:// leftRught
                SetLeftRightJoystickButtonActiveLayout();
                break;

            case 2:// allJoystick
                SetAllJoystickButtonActiveLayout();
                break;
        }
    }
}
