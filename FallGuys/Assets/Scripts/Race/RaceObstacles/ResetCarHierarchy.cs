using UnityEngine;

public class ResetCarHierarchy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
