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
    [SerializeField] private GameObject platformStates;

    [Space]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject rayPlaces;
    private List<Transform> rayPlacesTransformsList = new List<Transform>();

    private FallingPlatformTargetsController fallingPlatformTargetsController;

    [Space]
    [SerializeField] private float changeStateTime = 1f;
    [SerializeField] private List<State> StatesList;
    private int stateNumber;

    private bool isPlatformNotFell = true;

    private Coroutine waitAndChangeStateCoroutine = null;

    [System.Serializable]
    public class Info
    {
        public Info(GameObject peres, GameObject sharik)
        {
            gameObjectPeresechenie = peres;
            shar = sharik;
        }

        public GameObject gameObjectPeresechenie;
        public GameObject shar;
    }

    private void Awake()
    {
        fallingPlatformTargetsController = transform.GetComponentInParent<FallingPlatformTargetsController>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool b = IsItNotAnEdgePlatform();
        }
    }

    public bool IsItNotAnEdgePlatform()
    {
        bool isItNotAnEdgePlatform = true;

        RaycastHit hit;

        foreach (var pos in rayPlacesTransformsList)
        {
            if (!Physics.Raycast(pos.position, pos.forward, out hit, 2f, layerMask))
            {
                //Renderer renderer = GetComponentInChildren<Renderer>();
                ////Renderer renderer = pos.GetComponentInChildren<Renderer>();
                //renderer.material.SetColor("_Color", Color.black);

                isItNotAnEdgePlatform = false;

                break;
            }
            else
            {
                //Renderer renderer = GetComponentInChildren<Renderer>();
                ////Renderer renderer = pos.GetComponentInChildren<Renderer>();
                //renderer.material.SetColor("_Color", Color.red);
            }

            //Debug.DrawLine(pos.position, pos.position + pos.forward * 2f, Color.red);
        }

        return isItNotAnEdgePlatform;
    }

    public void ChangeState()
    {
        if (isPlatformNotFell)
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
    }

    private IEnumerator WaitAndChangeState(bool isLastState)
    {
        yield return new WaitForSeconds(changeStateTime);

        if (isLastState)
        {
            isPlatformNotFell = false;

            platformStates.SetActive(false);
            platformTrigger.gameObject.layer = 6;

            fallingPlatformTargetsController.PlatformFell(transform);
        }
        else
        {
            platformTrigger.CheckCarsOnPlatform();
        }
    }
}
