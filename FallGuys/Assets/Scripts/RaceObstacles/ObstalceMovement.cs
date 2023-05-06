using System.Collections;
using UnityEngine;

public class ObstalceMovement : MonoBehaviour
{
    [SerializeField] private Vector3 enterPosition;
    [SerializeField] private Vector3 exitPosition;
    private Vector3 targetPosition;
    [Space]
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private float minDelayTime;
    [SerializeField] private float maxDelayTime;

    [Space]

    [SerializeField]private bool obstacleAnEntryPosition;
    internal bool ObstacleAnEntryPosition
    {
        get => obstacleAnEntryPosition;
        private set => obstacleAnEntryPosition = value;
    }

    [SerializeField] private bool obstacleAnExitPosition;
    internal bool ObstacleAnExitPosition
    {
        get => obstacleAnExitPosition;
        private set => obstacleAnExitPosition = value;
    }

    internal event System.Action<GameObject> ObstacleAnEnterPositionEvent;

    internal event System.Action<GameObject> ObstacleAnExitPositionEvent;

    internal event System.Action<GameObject> ObstacleStartedMovingEvent;

    private bool canMoving;
    private bool goingToEndPosition;

    private void Start()
    {
        canMoving = true;
        transform.localPosition = GetRandomPostion();
        targetPosition = exitPosition;
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
        float rX = Random.Range(enterPosition.x, exitPosition.x);
        float rY = Random.Range(enterPosition.y, exitPosition.y);
        float rZ = Random.Range(enterPosition.z, exitPosition.z);

        Vector3 rPos = new Vector3(rX, rY, rZ);

        return rPos;
    }

    IEnumerator WaitAndGo()
    {
        if (!goingToEndPosition)
        {
            obstacleAnEntryPosition = true;
            ObstacleAnEnterPositionEvent?.Invoke(this.gameObject);
        }
        else
        {
            obstacleAnExitPosition = true;
            ObstacleAnExitPositionEvent?.Invoke(this.gameObject);
        }

        float delayTime = Random.Range(minDelayTime, maxDelayTime);

        yield return new WaitForSeconds(delayTime);

        ObstacleStartedMovingEvent?.Invoke(this.gameObject);

        if (goingToEndPosition)
        {
            targetPosition = enterPosition;
            goingToEndPosition = false;
        }
        else
        {
            targetPosition = exitPosition;
            goingToEndPosition = true;
        }

        canMoving = true;
        obstacleAnEntryPosition = false;

        obstacleAnExitPosition = false;
    }
}
