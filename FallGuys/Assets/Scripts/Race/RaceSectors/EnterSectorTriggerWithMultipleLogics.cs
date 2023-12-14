using ArcadeVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class EnterSectorTriggerWithMultipleLogics : EnterSectorTrigger
{
    private List<RaceSectorLogic> raceSectorLogics = new List<RaceSectorLogic>();

    private Dictionary<ArcadeVehicleController, RaceSectorLogic> carAndLogicDictionary = new Dictionary<ArcadeVehicleController, RaceSectorLogic>();

    private void Awake()
    {
        RaceSectorLogic[] allRaceSectorLogicComponents = transform.GetComponentsInParent<RaceSectorLogic>();
        foreach (var component in allRaceSectorLogicComponents)
        {
            raceSectorLogics.Add(component);
        }
    }

    protected override void SendCarEnteredEvent(ArcadeVehicleController car)
    {
        if (!carAndLogicDictionary.ContainsKey(car))
        {
            int rand = Random.Range(0, raceSectorLogics.Count);

            carAndLogicDictionary.Add(car, raceSectorLogics[rand]);

            raceSectorLogics[rand].CarEnteredTheSectorFromMultipleEnterTrigger(car);
        }
        else
        {
            carAndLogicDictionary[car].CarEnteredTheSectorFromMultipleEnterTrigger(car);
        }
    }
}
