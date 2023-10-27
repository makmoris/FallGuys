using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRespawnZone : MonoBehaviour
{
    private float minusX, plusX, minusZ, plusZ;
    private Vector3 center;

    private float randomShiftInPosition = 0.5f;

    [SerializeField] private float step = 5f;

    [SerializeField] private List<Vector3> allPositionsList = new List<Vector3>();

    private List<Vector3> busyPositionsList = new List<Vector3>();

    private void Awake()
    {
        GetSidesPosition();
        GetAllPositions();
        ShuffleList(allPositionsList);
    }

    public Vector3 GetRespawnPosition()
    {
        int rand = Random.Range(0, allPositionsList.Count);
        Vector3 pos = allPositionsList[rand];
        busyPositionsList.Add(pos);
        allPositionsList.Remove(pos);

        if (allPositionsList.Count == 0)
        {
            allPositionsList.AddRange(busyPositionsList);
            busyPositionsList.Clear();
        }

        return pos;
    }

    private void GetAllPositions()// находим все точки с учетом размеров 
    {
        float xLength = Mathf.Abs(minusX - plusX);
        float zLength = Mathf.Abs(minusZ - plusZ);

        int objectsCountOnX = Mathf.FloorToInt(xLength / step);

        if (objectsCountOnX == 0)
        {
            Debug.LogError("Not enough space for Spawn Object on X axis");
            return;
        }

        int objectsCountOnZ = Mathf.FloorToInt(zLength / step);

        if (objectsCountOnZ == 0)
        {
            Debug.LogError("Not enough space for Spawn Object on Z axis");
            return;
        }

        float spawnStepX = xLength / objectsCountOnX;// шаг спавна 
        float spawnStepZ = zLength / objectsCountOnZ;


        //float xPos = plusX;
        for (int x = 0; x < objectsCountOnX; x++)
        {
            float xPos;
            if (minusX <= plusX)
            {
                xPos = minusX + spawnStepX * x;
            }
            else
            {
                xPos = plusX + spawnStepX * x;
            }

            for (int z = 0; z < objectsCountOnZ; z++)
            {
                float zPos;
                if (minusZ <= plusZ)
                {
                    zPos = minusZ + spawnStepZ * z;
                }
                else
                {
                    zPos = plusZ + spawnStepZ * z;
                }

                float randXShift = Random.Range(-randomShiftInPosition, randomShiftInPosition);
                float randZShift = Random.Range(-randomShiftInPosition, randomShiftInPosition);

                allPositionsList.Add(new Vector3(xPos + randXShift, center.y, zPos + randZShift));
            }
        }

        Debug.Log($"Objects count on x = {objectsCountOnX}; Object count on z = {objectsCountOnZ}");
        Debug.Log($"spawn step x = {spawnStepX}; spawn step z = {spawnStepZ}");
    }

    private void GetSidesPosition()
    {
        float widthX = transform.localScale.x;
        float widthZ = transform.localScale.z;

        float centerX = transform.position.x;
        float centerZ = transform.position.z;
        center = new Vector3(centerX, transform.position.y, centerZ);

        minusX = centerX - (widthX / 2f) + step / 2f;
        plusX = centerX + (widthX / 2f) + step / 2f;
        minusZ = centerZ - (widthZ / 2f) + step / 2f;
        plusZ = centerZ + (widthZ / 2f) + step / 2f;
    }

    private void ShuffleList(List<Vector3> list)
    {
        System.Random rand = new();

        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            var tmp = list[j];
            list[j] = list[i];
            list[i] = tmp;
        }
    }
}
