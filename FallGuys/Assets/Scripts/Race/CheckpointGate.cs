using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGate : MonoBehaviour
{
    [SerializeField] private bool randomPath = true;
    [SerializeField] private bool firstBetterPath = false;
    [SerializeField] private bool firstWorsePath = false;

    [SerializeField] private List<Vector3> targetPositionsList = new List<Vector3>();

    private int counter;

    private void Awake()
    {
        if (randomPath)
        {
            firstBetterPath = false;
            firstWorsePath = false;
        }
        else if (firstBetterPath)
        {
            firstWorsePath = false;
        }
        else if (!firstWorsePath)
        {
            randomPath = true;
        }

        FillTargetPositionsList();
    }

    public Vector3 GetTargetPosition()
    {
        Vector3 pos = targetPositionsList[counter];

        int nextCounter = counter + 1;

        if (nextCounter < targetPositionsList.Count) counter = nextCounter;
        else counter = 0;

        return pos;
    }

    private void FillTargetPositionsList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            targetPositionsList.Add(transform.GetChild(i).transform.position);
        }

        if (randomPath)
        {
            System.Random rand = new();

            for (int i = targetPositionsList.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                var tmp = targetPositionsList[j];
                targetPositionsList[j] = targetPositionsList[i];
                targetPositionsList[i] = tmp;
            }
        }
    }
}
