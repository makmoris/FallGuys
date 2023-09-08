using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsController : MonoBehaviour
{
    private List<Ring> ringsList = new List<Ring>();

    private void Awake()
    {
        FillRingList();
    }

    public void Initialize(LevelUI levelUI)
    {
        InitializeRings(levelUI);
    }

    private void InitializeRings(LevelUI levelUI)
    {
        foreach (var ring in ringsList)
        {
            ring.Initialize(levelUI);
        }
    }

    private void FillRingList()
    {
        Ring[] rings = transform.GetComponentsInChildren<Ring>();
        foreach (var ring in rings)
        {
            ringsList.Add(ring);
        }
    }
}
