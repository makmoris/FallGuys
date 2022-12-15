using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationVariant
{
    [SerializeField] internal int neededLeagueLevel;// необходимый уровень лиги для этого варианта
    [SerializeField] internal GameObject variant;
}

public class LeagueLocationVariantController : MonoBehaviour
{
    [SerializeField] private List<LocationVariant> locationVariants;

    private void Awake()
    {
        ChooseVariant();
    }

    private void ChooseVariant()
    {
        int currentLeagueLevel = LeagueManager.Instance.GetCurrentLeagueLevel();

        int _variantIndex = 0;

        for (int i = 0; i < locationVariants.Count; i++)
        {
            if (currentLeagueLevel >= locationVariants[i].neededLeagueLevel) _variantIndex++;
            else break;
        }
        Debug.Log(_variantIndex - 1);
        locationVariants[_variantIndex - 1].variant.SetActive(true);
    }
}
