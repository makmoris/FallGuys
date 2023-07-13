using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        [SerializeField] private string stateName;
        public GameObject stateGO;
    }

    [SerializeField] private FallingPlatformTrigger platformTrigger;
    [SerializeField] private GameObject platform;

    [Space]
    [SerializeField] private float changeStateTime = 1f;
    [SerializeField] private List<State> StatesList;
    private int stateNumber;

    private Coroutine waitAndChangeStateCoroutine = null;


    private void Awake()
    {
        foreach (var state in StatesList)
        {
            state.stateGO.SetActive(false);
        }

        StatesList[0].stateGO.SetActive(true);
    }

    public void ChangeState()
    {
        StatesList[stateNumber].stateGO.SetActive(false);

        stateNumber++;

        if (waitAndChangeStateCoroutine != null && stateNumber < StatesList.Count) StopCoroutine(waitAndChangeStateCoroutine);

        if (stateNumber < StatesList.Count - 1)
        {
            StatesList[stateNumber].stateGO.SetActive(true);

            waitAndChangeStateCoroutine = StartCoroutine(WaitAndChangeState(false));
        }
        else if (stateNumber < StatesList.Count)
        {
            StatesList[stateNumber].stateGO.SetActive(true);

            waitAndChangeStateCoroutine = StartCoroutine(WaitAndChangeState(true));
        }
    }

    private IEnumerator WaitAndChangeState(bool isLastState)
    {
        yield return new WaitForSeconds(changeStateTime);

        if (isLastState)
        {
            platform.SetActive(false);
            platformTrigger.gameObject.layer = 6;
        }
        else
        {
            platformTrigger.CheckCarsOnPlatform();
        }
    }
}
