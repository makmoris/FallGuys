using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseDebugIndex : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void ChooseDebugIndexMethod(int index)
    {
        FindObjectOfType<GameManager>().SetDebugIndex(index);

        text.text = $"{index + 1}";
    }
}
