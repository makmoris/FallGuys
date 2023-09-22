using UnityEngine;

public class SlowdownObstacle : MonoBehaviour
{
    [SerializeField] private float decelerationAmount = 1.5f;

    public void SetDecelerationAmount(float decelerationAmount)
    {
        this.decelerationAmount = decelerationAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Bumper bumper = other.GetComponent<Bumper>();
            if (bumper != null) bumper.StartSlowdown(decelerationAmount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
    if (other.CompareTag("Car"))
    {
        Bumper bumper = other.GetComponent<Bumper>();
        if (bumper != null) bumper.StopSlowDown();
    }
}
}
