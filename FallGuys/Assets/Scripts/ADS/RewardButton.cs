using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardButton : Reward
{
    private Button _button;
    public Button Button { get => _button; private set { } }

    public void Initialize()
    {
        _button = GetComponent<Button>();
    }
}
