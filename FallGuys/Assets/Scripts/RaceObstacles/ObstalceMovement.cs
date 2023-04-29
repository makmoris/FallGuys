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
    [SerializeField] private float minDelayTime;
    [SerializeField] private float maxDelayTime;

    private bool canMoving;
    private bool goingToEndPosition;

    private void Start()
    {
        canMoving = true;
        transform.localPosition = GetRandomPostion();
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

    private Vector3 GetRandomPostion()
    {
        float rX = Random.Range(startPosition.x, endPosition.x);
        float rY = Random.Range(startPosition.y, endPosition.y);
        float rZ = Random.Range(startPosition.z, endPosition.z);

        Vector3 rPos = new Vector3(rX, rY, rZ);

        return rPos;
    }

    IEnumerator WaitAndGo()
    {
        float delayTime = Random.Range(minDelayTime, maxDelayTime);

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
