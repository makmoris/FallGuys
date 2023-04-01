using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class RaceSidesController : MonoBehaviour
{
    private WheelVehicle wheelVehicle;
    private Dictionary<Collider, RaceCarDriverAI> _parentsOfCollisionObjectsDict = new Dictionary<Collider, RaceCarDriverAI>();

    [SerializeField]private List<RaceCarDriverAI> vehiclesAroundList = new List<RaceCarDriverAI>();

    private void Awake()
    {
        wheelVehicle = GetComponentInParent<WheelVehicle>();
    }

    public void StayInside(string side, Collider enemyCollider)
    {
        RaceCarDriverAI raceCarDriverAI = GetParentElementFromDictionary(enemyCollider);// получили RB объекта, с которым ударились

        Debug.Log($"{raceCarDriverAI.name} въехал тачке {transform.parent.name} в {side}");

        raceCarDriverAI.Side = true;

        if(side == "LeftSide")
        {
            wheelVehicle.Steering = 1f;
        }
        if(side == "RightSide")
        {
            wheelVehicle.Steering = -1f;
        }

        if (!vehiclesAroundList.Contains(raceCarDriverAI)) vehiclesAroundList.Add(raceCarDriverAI);
    }

    public void Outside(Collider enemyCollider)
    {
        RaceCarDriverAI raceCarDriverAI = GetParentElementFromDictionary(enemyCollider);// получили RB объекта, с которым ударились

        if (vehiclesAroundList.Contains(raceCarDriverAI))
        {
            raceCarDriverAI.Side = false;

            vehiclesAroundList.Remove(raceCarDriverAI);
        }
    }

    private RaceCarDriverAI GetParentElementFromDictionary(Collider collider)
    {
        RaceCarDriverAI parentRaceCarDriverAI;

        if (_parentsOfCollisionObjectsDict.ContainsKey(collider))// если такой уже есть, то берем его родителя из списка
        {
            parentRaceCarDriverAI = _parentsOfCollisionObjectsDict[collider];
        }
        else// если нет, то добавляем его в список и используем его
        {
            RaceCarDriverAI parentB = collider.GetComponentInParent<RaceCarDriverAI>();
            _parentsOfCollisionObjectsDict.Add(collider, parentB);

            parentRaceCarDriverAI = parentB;
        }

        return parentRaceCarDriverAI;
    }
}
