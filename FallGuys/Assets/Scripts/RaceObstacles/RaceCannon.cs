using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCannon : MonoBehaviour
{
    [SerializeField] private Transform shellPosition;
    [Space]
    [SerializeField] private List<GameObject> shellPrefabs;
    [SerializeField]private List<GameObject> shells = new List<GameObject>();
    private int shellCreateCounter;
    private Dictionary<GameObject, Rigidbody> shellsRBDictionary = new Dictionary<GameObject, Rigidbody>();
    private Dictionary<GameObject, IExplosion> shellsIExposionDictionary = new Dictionary<GameObject, IExplosion>();

    [Space]
    [SerializeField] private float timeToNextShot = 5f;

    [Header("Throwing")]
    [SerializeField] private int minThrowForce = 15;
    [SerializeField] private int maxThrowForce = 30;
    [Space]
    [SerializeField] private float minTimeToExplosion = 2f;
    [SerializeField] private float maxTimeToExplosion = 4f;

    private void Awake()
    {
        FillShellsList();
    }

    private void Start()
    {
        StartCoroutine(WaitAndShot(0f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
    }

    private void Shot()
    {
        GameObject shellGO = GetShell();
        Rigidbody shellRB = shellsRBDictionary[shellGO];

        int randomThrowForce = Random.Range(minThrowForce, maxThrowForce + 1);

        //shellGO.transform.localRotation = shellPosition.localRotation;
        shellGO.SetActive(true);

        shellRB.isKinematic = false;
        shellRB.velocity = Vector3.zero;
        //shellRB.AddRelativeForce(Vector3.forward * randomThrowForce, ForceMode.VelocityChange);
        shellRB.AddForce(transform.forward * randomThrowForce, ForceMode.VelocityChange);

        IExplosion shellExplosion = shellsIExposionDictionary[shellGO];
        if(shellExplosion != null)
        {
            float timeToExplosion = Random.Range(minTimeToExplosion, maxTimeToExplosion);
            StartCoroutine(StartExplosion(shellExplosion, timeToExplosion));
        }
    }

    private GameObject GetShell()
    {
        GameObject shellGO = null;
        GameObject neededShellType = shells[shellCreateCounter];

        foreach (var shell in shells)
        {
            if (!shell.activeInHierarchy && shell == neededShellType)
            {
                shellGO = shell;
                shellGO.transform.position = shellPosition.position;
                shellGO.transform.rotation = shellPosition.rotation;
                break;
            }
        }

        if(shellGO == null)
        {
            GameObject newShellGO = Instantiate(shellPrefabs[shellCreateCounter], shellPosition.position, shellPosition.rotation);
            newShellGO.SetActive(false);
            shells.Add(newShellGO);

            shellGO = newShellGO;

            FillShellRBDictionary(shellGO);
            FillShellsIExposionDictionary(shellGO);
        }

        if (shellCreateCounter + 1 >= shellPrefabs.Count) shellCreateCounter = 0;
        else shellCreateCounter++;

        return shellGO;
    }

    private void FillShellsList()
    {
        foreach (var shell in shellPrefabs)
        {
            GameObject shellGO = Instantiate(shell, shellPosition.position, shellPosition.rotation);
            shellGO.SetActive(false);
            shells.Add(shellGO);

            FillShellRBDictionary(shellGO);
            FillShellsIExposionDictionary(shellGO);
        }
    }

    private void FillShellRBDictionary(GameObject keyShellGO)
    {
        if (!shellsRBDictionary.ContainsKey(keyShellGO))
        {
            Rigidbody shellRB = keyShellGO.GetComponent<Rigidbody>();
            if (shellRB == null)
            {
                shellRB = keyShellGO.GetComponentInChildren<Rigidbody>(true);

                if (shellRB == null) Debug.LogError($"Object {keyShellGO} dont have RigidBody component!");
            }

            shellsRBDictionary.Add(keyShellGO, shellRB);
        }
    }
    private void FillShellsIExposionDictionary(GameObject keyShellGO)
    {
        if (!shellsIExposionDictionary.ContainsKey(keyShellGO))
        {
            IExplosion shellExplosion = keyShellGO.GetComponent<IExplosion>();
            if (shellExplosion == null)
            {
                shellExplosion = keyShellGO.GetComponentInChildren<IExplosion>(true);

                if (shellExplosion == null) return;
            }

            shellsIExposionDictionary.Add(keyShellGO, shellExplosion);
        }
    }

    private IEnumerator StartExplosion(IExplosion shellExplosion, float timeToExplosion)
    {
        yield return new WaitForSeconds(timeToExplosion);

        shellExplosion.Explode();
    }

    private IEnumerator WaitAndShot(float time)
    {
        yield return new WaitForSeconds(time);

        Shot();
        StartCoroutine(WaitAndShot(timeToNextShot));
    }
}
