using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsController : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private int pointsValue = 1;
    [Header("Ring")]
    [SerializeField] private int numberOfActivatedRings = 1;
    [SerializeField] private float ringRechargeTime = 1f;

    private int numberOfCurrentActivatedRings;

    private List<Ring> ringsList = new List<Ring>();
    private List<Ring> ringsReadyForActivationList = new List<Ring>();
    private List<Ring> activatedRingsList = new List<Ring>();

    private Dictionary<GameObject, int> playerPointsDictionary = new Dictionary<GameObject, int>();

    public System.Action RingActivatedEvent;

    private void Awake()
    {
        FillRingList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var ring in ringsReadyForActivationList)
            {
                ring.ActivateRing();
            }
        }
    }

    public void Initialize(LevelUI levelUI, List<GameObject> playersList)
    {
        InitializeRings(levelUI);
        FillPlayerPointsDictionary(playersList);
        SelectRingsToActivate();
    }

    public void TheRingIsKnockedOff(Ring ring, GameObject playerGO)
    {
        playerPointsDictionary[playerGO] += pointsValue;

        numberOfCurrentActivatedRings--;
        SelectRingsToActivate();
    }

    public void RingHasRecharged(Ring ring)
    {
        activatedRingsList.Remove(ring);
        ringsReadyForActivationList.Add(ring);

        SelectRingsToActivate();
    }
    
    public List<Ring> GetRingsList()
    {
        return ringsList;
    }
    private void SelectRingsToActivate()
    {
        if(numberOfCurrentActivatedRings < numberOfActivatedRings)
        {
            int neededRingsCount = numberOfActivatedRings - numberOfCurrentActivatedRings;

            for (int i = 0; i < neededRingsCount; i++)
            {
                if (ringsReadyForActivationList.Count > 0)
                {
                    int randRingIndex = Random.Range(0, ringsReadyForActivationList.Count);

                    Ring ringForActivation = ringsReadyForActivationList[randRingIndex];

                    ringsReadyForActivationList.Remove(ringForActivation);
                    activatedRingsList.Add(ringForActivation);

                    ringForActivation.ActivateRing();

                    numberOfCurrentActivatedRings++;

                    RingActivatedEvent?.Invoke();
                }
            }
        }
    }

    private void FillPlayerPointsDictionary(List<GameObject> playersList)
    {
        foreach (var player in playersList)
        {
            playerPointsDictionary.Add(player, 0);
        }
    }

    private void InitializeRings(LevelUI levelUI)
    {
        foreach (var ring in ringsReadyForActivationList)
        {
            ring.Initialize(levelUI, this, ringRechargeTime);
        }
    }

    private void FillRingList()
    {
        Ring[] rings = transform.GetComponentsInChildren<Ring>();
        foreach (var ring in rings)
        {
            ringsList.Add(ring);
            ringsReadyForActivationList.Add(ring);
        }
    }
}
