using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsManager : MonoBehaviour
{
    [System.Serializable]
    public class PlaceAwards
    {
        [SerializeField] private string place;
        [SerializeField] internal int goldsValue;
        [SerializeField] internal int cupsValue;
    }

    [Header("Reward For Place")]
    [SerializeField] internal List<PlaceAwards> placeAwards;


    public int GetGoldsAward(int placeIndex)
    {
        return placeAwards[placeIndex].goldsValue;
    }

    public int GetCupsAward(int placeIndex)
    {
        return placeAwards[placeIndex].cupsValue;
    }
}
