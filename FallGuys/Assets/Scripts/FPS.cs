using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int fps;

    private void OnValidate()
    {
        Application.targetFrameRate = fps;
    }
}
