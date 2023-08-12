using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsManager : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    [Header("Reward For Place")]
    [SerializeField] internal List<PlaceAwards> placeAwards;

    private static AwardsManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }



    [System.Serializable]
    public class PlaceAwards
    {
        [SerializeField] private string place;
        [SerializeField] internal int goldsValue;
        [SerializeField] internal int cupsValue;
    }
}
