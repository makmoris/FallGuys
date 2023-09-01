using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaMovementToTarget : MonoBehaviour
{
    [SerializeField] private List<Transform> targetsVariantList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            RaceDriverAI raceDriverAI = other.GetComponentInChildren<RaceDriverAI>();
            if (raceDriverAI != null)
            {
                int rand = Random.Range(0, targetsVariantList.Count);

                List<Transform> transforms = new List<Transform>();

                for (int i = 0; i < targetsVariantList[rand].childCount; i++)
                {
                    transforms.Add(targetsVariantList[rand].GetChild(i));
                }

                raceDriverAI.SetTargets(transforms);
            }
        }
    }
}
