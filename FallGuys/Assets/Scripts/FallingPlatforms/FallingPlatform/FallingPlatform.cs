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
        //Ray ray = new Ray(transform.position, transform.position - direction * 5f);

        //float radianDelta = Mathf.PI * 2f / 6f;

        //for (int i = 0; i < 6; i++)
        //{
        //    float radian = i * radianDelta;
        //    Vector3 direction = new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian));

        //    RaycastHit hit;
        //    if (!Physics.Raycast(transform.position, transform.position - direction, out hit, 5f, layerMask))
        //    {
        //        if (hit.transform != transform)
        //        {
        //            Debug.DrawLine(transform.position, transform.position - direction * 5f, Color.red);

        //            Renderer renderer = GetComponentInChildren<Renderer>();
        //            renderer.material.SetColor("_Color", Color.black);
        //        }
        //    }
        //    else
        //    {
        //        if (hit.transform != transform)
        //        {
        //            Debug.DrawLine(transform.position, transform.position - direction * 5f, Color.blue);

        //            Renderer renderer = GetComponentInChildren<Renderer>();
        //            renderer.material.SetColor("_Color", Color.red);

        //            //peresechenia.Add(hit.transform.gameObject);
        //        }
        //    }

        //    //Debug.DrawLine(transform.position, transform.position - direction * 5f, Color.red);
        //}




        RaycastHit hit;

        foreach (var pos in rayPlacesTransformsList)
        {
            if (!Physics.Raycast(pos.position, pos.position + pos.forward, out hit, 2f, layerMask))
            {
                Renderer renderer = pos.GetComponentInChildren<Renderer>();
                renderer.material.SetColor("_Color", Color.black);
            }
            else
            {
                Renderer renderer = pos.GetComponentInChildren<Renderer>();
                renderer.material.SetColor("_Color", Color.red);
            }

            Debug.DrawLine(pos.position, pos.position + pos.forward * 2f, Color.red);
        }


        //if (Input.GetKeyDown(KeyCode.T)) Test();
    }

    private void Test()
    {
        RaycastHit hit;

        foreach (var pos in rayPlacesTransformsList)
        {
            if (!Physics.Raycast(pos.position, pos.position + pos.forward, out hit, 2f, layerMask))
            {
                Renderer renderer = pos.GetComponentInChildren<Renderer>();
                renderer.material.SetColor("_Color", Color.black);
            }
            else
            {
                Debug.Log($"HIT {hit.transform.name}");
                Renderer renderer = pos.GetComponentInChildren<Renderer>();
                renderer.material.SetColor("_Color", Color.red);
            }

            Debug.DrawLine(pos.position, pos.position + pos.forward * 2f, Color.red);
        }
    }

    public bool IsItNotAnEdgePlatform()
    {
        bool isItNotAnEdgePlatform = true;


        float radianDelta = Mathf.PI * 2f / 6f;
        var hits = new RaycastHit2D[6];
        for (int i = 0; i < 6; i++)
        {
            float radian = i * radianDelta;
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            hits[i] = Physics2D.Raycast(transform.position, direction, 5f);

            Debug.DrawRay(hits[i].point, hits[i].normal, Color.red);
        }

        return isItNotAnEdgePlatform;
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
