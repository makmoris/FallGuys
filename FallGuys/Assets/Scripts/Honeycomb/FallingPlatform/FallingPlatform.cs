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

    [SerializeField] private float delayBeforeTriggering = 0.25f;
    [SerializeField] private float changeStateTime = 1f;
    [SerializeField] private List<State> StatesList;
    private int stateNumber;

    private bool isPlatformNotFell = true;
    [SerializeField]private bool isPlatformCompletedStateChange = true;

    [Header("-----")]
    [SerializeField] private FallingPlatformTrigger platformTrigger;
    [SerializeField] private GameObject platformStates;

    [Space]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject rayPlaces;
    private List<Transform> rayPlacesTransformsList = new List<Transform>();

    private TargetsControllerHoneycomb targetsControllerHoneycomb;

    private Coroutine waitAndChangeStateCoroutine = null;
    private Coroutine delayCoroutine = null;

    private void Awake()
    {
        targetsControllerHoneycomb = transform.GetComponentInParent<TargetsControllerHoneycomb>();

        Transform[] rayPlacesTransforms = rayPlaces.GetComponentsInChildren<Transform>();
        for (int i = 0; i < rayPlacesTransforms.Length; i++)
        {
            if(i != 0) rayPlacesTransformsList.Add(rayPlacesTransforms[i]);
        }

        foreach (var state in StatesList)
        {
            state.stateGO.SetActive(false);
        }

        StatesList[0].stateGO.SetActive(true);
    }

    public bool IsItNotAnEdgePlatform()
    {
        bool isItNotAnEdgePlatform = true;

        RaycastHit hit;

        foreach (var pos in rayPlacesTransformsList)
        {
            if (!Physics.Raycast(pos.position, pos.forward, out hit, 2f, layerMask))
            {
                isItNotAnEdgePlatform = false;

                break;
            }
        }

        return isItNotAnEdgePlatform;
    }

    public void ChangeState()
    {
        StartCoroutine(Delay());
    }

    private void ChangeStateAfterDelay()
    {
        if (isPlatformNotFell && isPlatformCompletedStateChange)
        {
            if (stateNumber < StatesList.Count) StatesList[stateNumber].stateGO.SetActive(false);

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

            WaitForStateChangeToComplete();
        }
    }

    private void WaitForStateChangeToComplete()
    {
        isPlatformCompletedStateChange = false;
        StartCoroutine(WaitAndCompleteTheStateChanging());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayBeforeTriggering);

        ChangeStateAfterDelay();
    }

    private IEnumerator WaitAndChangeState(bool isLastState)
    {
        yield return new WaitForSeconds(changeStateTime);

        if (isLastState)
        {
            isPlatformNotFell = false;

            platformStates.SetActive(false);
            platformTrigger.gameObject.layer = 6;

            targetsControllerHoneycomb.PlatformFell(transform);
        }
        else
        {
            platformTrigger.CheckCarsOnPlatform();
        }
    }

    private IEnumerator WaitAndCompleteTheStateChanging()
    {
        yield return new WaitForSeconds(changeStateTime);

        isPlatformCompletedStateChange = true;
        platformTrigger.CheckCarsOnPlatform();
    }
}
