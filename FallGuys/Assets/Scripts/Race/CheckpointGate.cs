using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGate : MonoBehaviour
{

    [SerializeField] private List<Vector3> targetPositionsList = new List<Vector3>();

    private int counter;

    private void Awake()
    {

        FillTargetPositionsList();
    }

    public Vector3 GetTargetPosition(int pathNumber)
    {
        Vector3 pos = targetPositionsList[pathNumber];

        int nextCounter = counter + 1;

        if (nextCounter < targetPositionsList.Count) counter = nextCounter;
        else counter = 0;

        return targetPositionsList[pathNumber];
        //return targetPositionsList[0];
    }

    private void FillTargetPositionsList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            targetPositionsList.Add(transform.GetChild(i).transform.position);
        }
    }
}
