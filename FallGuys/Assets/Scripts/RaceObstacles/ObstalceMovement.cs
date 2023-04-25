using System.Collections;
using UnityEngine;

public class ObstalceMovement : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
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
        targetPosition = endPosition;
        goingToEndPosition = true;
    }

    private void FixedUpdate()
    {
        if (canMoving)
        {
            var step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, step);

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.001f)
            {
                transform.localPosition = targetPosition;
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
            targetPosition = startPosition;
            goingToEndPosition = false;
        }
        else
        {
            targetPosition = endPosition;
            goingToEndPosition = true;
        }

        canMoving = true;
    }
}
