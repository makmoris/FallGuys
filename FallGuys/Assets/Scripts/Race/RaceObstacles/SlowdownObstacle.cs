using UnityEngine;

public class SlowdownObstacle : MonoBehaviour
{
    [SerializeField] private float decelerationAmount = 1.5f;

    private GameObject ignoreParent;

    public void SetDecelerationAmount(float decelerationAmount)
    {
        this.decelerationAmount = decelerationAmount;
    }

    public void SetIgnoreParent(GameObject parentGO)
    {
        ignoreParent = parentGO;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if(ignoreParent != null)
            {
                if(other.gameObject != ignoreParent)
                {
                    Bumper bumper = other.GetComponent<Bumper>();
                    if (bumper != null) bumper.StartSlowdown(decelerationAmount);
                }
            }
            else
            {
                Bumper bumper = other.GetComponent<Bumper>();
                if (bumper != null) bumper.StartSlowdown(decelerationAmount);
            }
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

    private void OnDisable()
    {
        ignoreParent = null;
    }
}
