using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformsInstaller : Installer
{
    [Header("Falling Platform Controllers")]
    [SerializeField] FallingPlatformTargetsController fallingPlatformTargetsController;

    [Header("Falling Platform AI")]
    [SerializeField] FallingPlatformsDriverAI fallingPlatformsDriverAIPrefab;
    [SerializeField] PlayerDefaultData fallingPlatformsAIDefaultData;

    protected override void InitializePlayers()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject aiPlayerGO = Instantiate(fallingPlatformsDriverAIPrefab.gameObject);

            aiPlayerGO.transform.position = new Vector3(1 + 3 * i, 5f, 1 + 3 * i);

            aiPlayerGO.GetComponent<FallingPlatformsDriverAI>().SetTargetController(fallingPlatformTargetsController);
        }
    }
}
