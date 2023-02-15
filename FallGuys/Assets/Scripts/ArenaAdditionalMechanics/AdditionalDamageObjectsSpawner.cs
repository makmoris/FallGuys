using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalDamageObjectsSpawner : MonoBehaviour
{
    [Header("Lightning Pool")] //   ����� ���� ���� �� �����. ���� ������ ������ ������ ������� ���� �������� ������ ��� ������� ���
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = true;
    //[SerializeField] private Lightning lightningPrefab;

    //private PoolMono<Lightning> lightningPool;
    [Space]

    [SerializeField] private int count;
    [SerializeField] private float minusX, plusX, minusZ, plusZ;
    [SerializeField] private Vector3 center;


    private List<GameObject> spawnedObjects = new List<GameObject>();


    [SerializeField] private float step = 0.5f;
    [SerializeField] private List<Vector3> attackPositionsList;

    [SerializeField] private List<float> xPositions;
    [SerializeField] private List<float> zPositions;


    

    [SerializeField] private List<Vector3> allPositions;

    private void Awake()
    {
        //lightningPool = new PoolMono<Lightning>(lightningPrefab, poolCount);
        //lightningPool.autoExpand = autoExpand;
    }

    private void Start()
    {
        GetSidesPosition();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FillAllPositions();
        }
    }


    private void FillAllPositions()
    {

        //����� ����� ������� �������� ���������� � ������������
        float xLength = Mathf.Abs(minusX - plusX);
        float zLength = Mathf.Abs(minusZ - plusZ);

        Debug.Log($"Length X = {xLength}");

        //for (int x = 0; x <)
        //{
        //    for ()
        //    {

        //    }
        //}
    }



    private void FillAttackPositionsList()
    {
        // �������� �� ������� ������� �� x � z
        for (int i = 0; i < count; i++)
        {
            float rX = Random.Range(minusX, plusX);
            xPositions.Add(rX);

            float rZ = Random.Range(minusZ, plusZ);
            zPositions.Add(rZ);
        }

        // ��������� �� �����������
        xPositions.Sort();
        zPositions.Sort();

        // ��������� ��� ����� �������, ����� �� ������������� ���� �� �����
        for (int i = 0; i < xPositions.Count - 1; i++)
        {
            if (xPositions[i] > (xPositions[i + 1] - step))
            {
                Debug.Log($"���� = {xPositions[i + 1]}");
                xPositions[i + 1] = xPositions[i] + step;
                Debug.Log($"����� = {xPositions[i + 1]}");


            }
        }

        for (int i = 0; i < zPositions.Count - 1; i++)
        {
            if (zPositions[i] > (zPositions[i + 1] - step))
            {
                Debug.Log($"���� = {zPositions[i + 1]}");
                zPositions[i + 1] = zPositions[i] + step;
                Debug.Log($"����� = {zPositions[i + 1]}");
            }
        }

        // ��������� ������ �������
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(xPositions[i], center.y, zPositions[i]);

            attackPositionsList.Add(pos);
        }

        Spawn();
    }


    private void Spawn()
    {
        if (spawnedObjects.Count != 0)
        {
            foreach (var item in spawnedObjects)
            {
                item.SetActive(false);
            }

            spawnedObjects.RemoveRange(0, spawnedObjects.Count);// ������� ���������
        }

        for (int i = 0; i < count; i++)
        {
            //var lightning = lightningPool.GetFreeElement();
            //lightning.transform.position = attackPositionsList[i];

            //spawnedObjects.Add(lightning.gameObject);
        }
    }

    private Vector3 GetRandomPositionInsideSpawner()
    {
        Vector3 rPos;

        float rX = Random.Range(minusX, plusX);
        float rZ = Random.Range(minusZ, plusZ);

        rPos = new Vector3(rX, center.y, rZ); // � ���� ����� ������� ����� ������� �������. ���� ���� ������ �������, �� ������ ��������, ����������

        

        return rPos;
    }

    private void GetSidesPosition()
    {
        float widthX = transform.localScale.x;
        float widthZ = transform.localScale.z;

        float centerX = transform.position.x;
        float centerZ = transform.position.z;
        center = new Vector3(centerX, 0f, centerZ);

        minusX = centerX - (widthX / 2f);
        plusX = centerX + (widthX / 2f);
        minusZ = centerZ - (widthZ / 2f);
        plusZ = centerZ + (widthZ / 2f);
    }
}
