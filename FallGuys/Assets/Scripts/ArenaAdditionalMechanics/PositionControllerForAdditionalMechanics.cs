using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionControllerForAdditionalMechanics : MonoBehaviour
{
    private float spawnObjectSize;

    [Range(0f, 1f)]
    [SerializeField] private float randomShiftInPosition;
    [Space]

    [SerializeField] private List<Vector3> allSpawnPositions = new List<Vector3>();

    private List<PositionsForAdditionalMechanics> positionsForAdditionalMechanicsList = new List<PositionsForAdditionalMechanics>();

    private bool allPositionsFound;

    //private void Awake()
    //{
    //    FillPositionsForAdditionalMechanicsList();
    //    FillSpawnPositionsList();
    //}

    public void PreparePositions(float _spawnObjectSize)// стартовая точка
    {
        spawnObjectSize = _spawnObjectSize;

        FillPositionsForAdditionalMechanicsList();
        FillSpawnPositionsList();
    }

    // приходит процент от позиций. Т.е. если 50%, то выдаем список в количестве 50% от spawnPositions
    public List<Vector3> GetRandomSpawnPositions(int percentageOfPositions)
    {
        var _spawnPosList = allSpawnPositions.GetRange(0, allSpawnPositions.Count);
        
        for (int i = 0; i < _spawnPosList.Count; i++)
        {
            var temp = _spawnPosList[i];
            int randomIndex = Random.Range(i, _spawnPosList.Count);
            _spawnPosList[i] = _spawnPosList[randomIndex];
            _spawnPosList[randomIndex] = temp;
        }

        int count = Mathf.RoundToInt(allSpawnPositions.Count * percentageOfPositions / 100f);

        List<Vector3> newRandomPositionsList = new List<Vector3>();

        while (count > 0)
        {
            count--;

            var randIndex = Random.Range(0, _spawnPosList.Count);

            Vector3 pos = _spawnPosList[randIndex];

            float randXShift = Random.Range(-randomShiftInPosition, randomShiftInPosition);
            float randZShift = Random.Range(-randomShiftInPosition, randomShiftInPosition);

            pos = new Vector3(pos.x + randXShift, pos.y, pos.z + randZShift);

            newRandomPositionsList.Add(pos);

            _spawnPosList.RemoveAt(randIndex);
        }
        
        return newRandomPositionsList;
    }

    private void FillSpawnPositionsList()
    {
        foreach (var item in positionsForAdditionalMechanicsList)
        {
            List<Vector3> positionsList = item.GetPositions(spawnObjectSize);
            allSpawnPositions.AddRange(positionsList);
        }
        
        allPositionsFound = true;
    }

    private void FillPositionsForAdditionalMechanicsList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            PositionsForAdditionalMechanics positionsForAdditionalMechanics = transform.GetChild(i).GetComponent<PositionsForAdditionalMechanics>();
            if (positionsForAdditionalMechanics != null) positionsForAdditionalMechanicsList.Add(positionsForAdditionalMechanics);
        }
    }
}
