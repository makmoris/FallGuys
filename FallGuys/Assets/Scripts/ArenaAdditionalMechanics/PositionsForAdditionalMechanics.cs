using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsForAdditionalMechanics : MonoBehaviour
{
    private float minusX, plusX, minusZ, plusZ;
    private Vector3 center;

    private float objectSize;

    [SerializeField] private List<Vector3> allPositionsList = new List<Vector3>();

    public List<Vector3> GetPositions(float spawnObjectSize)
    {
        objectSize = spawnObjectSize;
        GetSidesPosition();
        GetAllPositions();

        return allPositionsList;
    }

    private void GetAllPositions()// находим все точки с учетом размеров 
    {
        float xLength = Mathf.Abs(minusX - plusX);
        float zLength = Mathf.Abs(minusZ - plusZ);

        int objectsCountOnX = Mathf.FloorToInt(xLength / objectSize);

        if (objectsCountOnX == 0)
        {
            Debug.LogError("Not enough space for Spawn Object on X axis");
            return;
        }

        int objectsCountOnZ = Mathf.FloorToInt(zLength / objectSize);

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

                allPositionsList.Add(new Vector3(xPos, center.y, zPos));
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

        minusX = centerX - (widthX / 2f) + objectSize / 2f;
        plusX = centerX + (widthX / 2f) + objectSize / 2f;
        minusZ = centerZ - (widthZ / 2f) + objectSize / 2f;
        plusZ = centerZ + (widthZ / 2f) + objectSize / 2f;
    }
}
