using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed;
    public float currentSpeed = 5;
    private float previousSpeed;
    float distanceTravelled;

    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += currentSpeed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            //transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    public void SlowDown()
    {
        previousSpeed = currentSpeed;
        currentSpeed = 0f;
    }

    public void ReturnNormalSpeed()
    {
        currentSpeed = speed;
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
