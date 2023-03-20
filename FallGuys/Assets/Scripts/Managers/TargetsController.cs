using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class TargetsController : MonoBehaviour
{
    [SerializeField] private Transform targetsContainer;
    [SerializeField] private Transform spawnPointsContainer;
    [SerializeField] private Transform bonusBoxesContainer;
    [SerializeField] private float respawnCheckRadius;

    [SerializeField] private List<Transform> targets;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private List<Transform> spawnPoints;

    public static TargetsController Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        AddChieldTargets();
        AddChieldSpawnPoints();
    }

    private void OnEnable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent += SendRespawnPositionToWheelVehicle;
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
            var target = targetsContainer.GetChild(i);

            if (target.gameObject.activeInHierarchy) targets.Add(target);
        }

        for (int i = 0; i < bonusBoxesContainer.childCount; i++)
        {
            var bonusBox = bonusBoxesContainer.GetChild(i);

            if(bonusBox.gameObject.activeInHierarchy) targets.Add(bonusBox);
        }
    }

    private void AddChieldSpawnPoints()
    {
        for (int i = 0; i < spawnPointsContainer.childCount; i++)
        {
            var spawnPoint = spawnPointsContainer.GetChild(i);

            if(spawnPoint.gameObject.activeInHierarchy) spawnPoints.Add(spawnPoint);
            //targets.Add(spawnPointsContainer.GetChild(i)); // не добавляем в качестве точек интересов для ботов спавн позиции
        }

        // перемешаем позиции спавна для рандомного появления на карте
        // создаем экземпляр класса Random для генерирования случайных чисел
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

            if(carCount == 0)// если в радиусе точки респа нету противников, значит в этой точке можем респать.
            {
                respIndex = i;
                //Debug.Log($"Позиция {i} Кол-во авто = {carCount} Прерываем цикл");
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
            //Debug.Log($"Позиция {i} Кол-во авто = {carCount}");
        }
        //Debug.Log($"Респавн в позиции {respIndex}. Количество авто в ней = {minVal}");
        return spawnPoints[respIndex];
    }

    private void SendRespawnPositionToWheelVehicle(WheelVehicle wheelVehicle)
    {
        wheelVehicle.GetRespawnTransform(GetRespawnPosition());
    }

    private void OnDisable()
    {
        WheelVehicle.NotifyGetRespanwPositionForWheelVehicleEvent -= SendRespawnPositionToWheelVehicle;
    }
}
