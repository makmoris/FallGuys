using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private float timeBeforeEnable = 2f;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitAndEnable());
    }

    private void OnDisable()
    {
        button.enabled = false;
    }

    IEnumerator WaitAndEnable()
    {
        yield return new WaitForSeconds(timeBeforeEnable);

        button.enabled = true;
    }
}
