using System.Collections;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    [SerializeField] private Quaternion startPosition;
    [SerializeField] private Quaternion endPosition;
    private Vector3 targetPosition;
    [Space]
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private float delayTime;

    private bool canMoving;
    private bool goingToEndPosition;

    private void Start()
    {
        canMoving = true;
        targetPosition = endPosition.eulerAngles;
        goingToEndPosition = true;
    }

    private void FixedUpdate()
    {
        if (canMoving)
        {
            var step = speed * Time.deltaTime;
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, targetPosition, step);

            if (Vector3.Distance(transform.localEulerAngles, targetPosition) < 0.001f)
            {
                transform.localEulerAngles = targetPosition;
                StartCoroutine(WaitAndGo());
                canMoving = false;
            }
        }
    }

    IEnumerator WaitAndGo()
    {
        yield return new WaitForSeconds(delayTime);

        if (goingToEndPosition)
        {
            targetPosition = startPosition.eulerAngles;
            goingToEndPosition = false;
        }
        else
        {
            targetPosition = endPosition.eulerAngles;
            goingToEndPosition = true;
        }

        canMoving = true;
    }
}
