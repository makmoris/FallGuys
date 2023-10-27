using UnityEngine;

public class RaceRespawnCheckpointTrigger : MonoBehaviour
{
    private RaceRespawnCheckpoint respawnCheckpoint;
    private void Awake()
    {
        respawnCheckpoint = transform.GetComponentInParent<RaceRespawnCheckpoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            respawnCheckpoint.AddPlayer(other.gameObject);
        }
    }
}
