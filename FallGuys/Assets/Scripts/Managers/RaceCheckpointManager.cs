using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpointManager : MonoBehaviour
{
    [SerializeField] private List<CheckpointGate> checkpointGatesList = new List<CheckpointGate>();

    public static RaceCheckpointManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        FillCheckpointsGatesList();
    }

    public List<CheckpointGate> GetCheckpointsList()
    {
        return checkpointGatesList;
    }

    private void FillCheckpointsGatesList()
    {
        CheckpointGate[] checkpointGates = transform.GetComponentsInChildren<CheckpointGate>();

        for (int i = 0; i < checkpointGates.Length; i++)
        {
            checkpointGatesList.Add(checkpointGates[i]);
        }

        checkpointGatesList.Reverse();
    }
}
