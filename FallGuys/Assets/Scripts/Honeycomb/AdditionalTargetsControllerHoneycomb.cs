using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTargetsControllerHoneycomb : TargetsControllerHoneycomb
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayerEntered(GameObject playerGO)
    {
        HoneycombDriverAI honeycombDriverAI = playerGO.GetComponentInChildren<HoneycombDriverAI>();
        if(honeycombDriverAI != null)
        {
            honeycombDriverAI.ResetTargetController(this);
        }
    }
}
