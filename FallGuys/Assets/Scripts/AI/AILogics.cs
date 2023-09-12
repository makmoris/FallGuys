using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogics : MonoBehaviour
{
    [SerializeField] private ObstacleDetectionAI obstacleDetection;
    [SerializeField] private GroundDetectionAI groundDetectionAI;

    private void Awake()
    {
        groundDetectionAI.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // From Installer
    public void EnableArenaAI(GameObject aiPlayerGO, GameObject currentPlayerGO, float frontSideRayLength, float sidesRayLength, float angleForSides)
    {
        ArenaCarDriverAI arenaAI;

        for (int i = 0; i < transform.childCount; i++)
        {
            arenaAI = transform.GetChild(i).GetComponent<ArenaCarDriverAI>();

            if (arenaAI != null)
            {
                arenaAI.gameObject.SetActive(true);
                arenaAI.Initialize(aiPlayerGO, currentPlayerGO);
                obstacleDetection.SetAILogic(arenaAI, frontSideRayLength, sidesRayLength, angleForSides);
            }
        }
    }

    public void EnableRaceAI(GameObject aiPlayerGO, GameObject currentPlayerGO, float frontSideRayLength, float sidesRayLength, float angleForSides)
    {
        RaceDriverAI raceAI;

        for (int i = 0; i < transform.childCount; i++)
        {
            raceAI = transform.GetChild(i).GetComponent<RaceDriverAI>();

            if (raceAI != null)
            {
                raceAI.gameObject.SetActive(true);
                raceAI.Initialize(aiPlayerGO, currentPlayerGO);
                obstacleDetection.SetAILogic(raceAI, frontSideRayLength, sidesRayLength, angleForSides);

                groundDetectionAI.Initialize(raceAI);
            }
        }
    }

    public void EnableHoneycombAI(GameObject aiPlayerGO, GameObject currentPlayerGO, float frontSideRayLength, float sidesRayLength, float angleForSides)
    {
        HoneycombDriverAI HoneycombAI;

        for (int i = 0; i < transform.childCount; i++)
        {
            HoneycombAI = transform.GetChild(i).GetComponent<HoneycombDriverAI>();

            if (HoneycombAI != null)
            {
                HoneycombAI.gameObject.SetActive(true);
                HoneycombAI.Initialize(aiPlayerGO, currentPlayerGO);
                obstacleDetection.SetAILogic(HoneycombAI, frontSideRayLength, sidesRayLength, angleForSides);
            }
        }
    }

    public void EnableRingsAI(GameObject aiPlayerGO, GameObject currentPlayerGO, float frontSideRayLength, float sidesRayLength, float angleForSides)
    {
        RingsDriverAI ringsAI;

        for (int i = 0; i < transform.childCount; i++)
        {
            ringsAI = transform.GetChild(i).GetComponent<RingsDriverAI>();

            if (ringsAI != null)
            {
                ringsAI.gameObject.SetActive(true);
                ringsAI.Initialize(aiPlayerGO, currentPlayerGO);
                obstacleDetection.SetAILogic(ringsAI, frontSideRayLength, sidesRayLength, angleForSides);
            }
        }
    }
}
