using UnityEngine;

public class RaceRespawnZone : MonoBehaviour
{
    private RaceRespawnController raceRespawnController;

    private void Awake()
    {
        raceRespawnController = transform.GetComponentInParent<RaceRespawnController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            raceRespawnController.CarGotIntoRespawnZone(other.gameObject);
        }
    }
}
