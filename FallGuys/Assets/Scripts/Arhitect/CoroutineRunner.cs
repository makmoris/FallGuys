using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour // �� ������ ������ �� �����
{
    private static CoroutineRunner _instance;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    public static void Run(IEnumerator enumerator)
        => _instance.StartCoroutine(enumerator);
}
