using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController : MonoBehaviour
{
    [SerializeField] private Transform targetsContainer;
    [SerializeField] private Transform spawnPointsContainer;
    [SerializeField] private float respawnCheckRadius;

    [SerializeField] private List<Transform> targets;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private List<Transform> spawnPoints;

    private void Awake()
    {
        AddChieldTargets();
        AddChieldSpawnPoints();
    }

    public void AddPlayerToTargets(GameObject playerObj)
    {
        players.Add(playerObj);
        targets.Add(playerObj.transform);
    }

    public void SetTargetsForPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            var carDriverAI = players[i].GetComponent<CarDriverAI>();

            if (carDriverAI != null)
            {
                carDriverAI.SetTargets(targets);
            }
        }
    }

    private void AddChieldTargets()
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    targets.Add(transform.GetChild(i));
        //}

        for (int i = 0; i < targetsContainer.childCount; i++)
        {
            targets.Add(targetsContainer.GetChild(i));
        }
    }

    private void AddChieldSpawnPoints()
    {
        for (int i = 0; i < spawnPointsContainer.childCount; i++)
        {
            spawnPoints.Add(spawnPointsContainer.GetChild(i));
            targets.Add(spawnPointsContainer.GetChild(i));
        }

        // ���������� ������� ������ ��� ���������� ��������� �� �����
        // ������� ��������� ������ Random ��� ������������� ��������� �����
        System.Random rand = new();

        for (int i = spawnPoints.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            var tmp = spawnPoints[j];
            spawnPoints[j] = spawnPoints[i];
            spawnPoints[i] = tmp;
        }
    }

    public Transform GetStartSpawnPosition(int index)
    {
        return spawnPoints[index];
    }

    public Transform GetRespawnPosition()
    {
        int minVal = spawnPoints.Count;
        int respIndex = 0;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Collider[] overLappedColliders = Physics.OverlapSphere(spawnPoints[i].position, respawnCheckRadius);

            int carCount = 0;

            foreach (var val in overLappedColliders)
            {
                if (val.CompareTag("Car"))
                {
                    carCount++;
                }
            }

            if(carCount == 0)// ���� � ������� ����� ����� ���� �����������, ������ � ���� ����� ����� �������.
            {
                respIndex = i;
                //Debug.Log($"������� {i} ���-�� ���� = {carCount} ��������� ����");
                break;
            }
            else
            {
                if (carCount < minVal)
                {
                    minVal = carCount;
                    respIndex = i;
                }
            }
            //Debug.Log($"������� {i} ���-�� ���� = {carCount}");
        }
        //Debug.Log($"������� � ������� {respIndex}. ���������� ���� � ��� = {minVal}");
        return spawnPoints[respIndex];
    }
}
