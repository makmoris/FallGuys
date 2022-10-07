using UnityEngine;

public enum AmountOfCars
{
    None,
    Two = 2,
    Four = 4,
    Eight = 8
}

public class RivalsSpawner : MonoBehaviour
{
    public Transform[] spawnPointsPositionArray;

    [Space]
    [SerializeField] private AmountOfCars _amountOfCars = AmountOfCars.None;

    [Header("Rival")]
    public GameObject rivalPrefab;
    public Transform rivalsContainer;

    private void Start()
    {
        switch (_amountOfCars)
        {
            case AmountOfCars.Two:

                SpawnRivals(0, 1);
                break;

            case AmountOfCars.Four:

                SpawnRivals(0, 3);
                break;

            case AmountOfCars.Eight:

                SpawnRivals(4, 11);
                break;
        }
    }


    private void SpawnRivals(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            Instantiate(rivalPrefab, spawnPointsPositionArray[i].position, Quaternion.identity, rivalsContainer);
        }
    }

}
