using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceObstacleDetectionAI : MonoBehaviour
{
    public float Work;
	[SerializeField] private bool inUpdate = false;
	[Space]
	[SerializeField] private string Obstacles = "Null";
    [SerializeField] private float ObstacleDistance = 0;
	[SerializeField] private float angleObstacle = 0;

	public LayerMask obstacleLayerMask;

    [Header("Raycast Directions")]
    [SerializeField] private float sidesRayLength;
    [SerializeField] private GameObject leftSideBack;
    [SerializeField] private GameObject leftSideMiddle;
    [SerializeField] private GameObject leftSideFront;
    [Space]
    [SerializeField] private GameObject rightSideBack;
    [SerializeField] private GameObject rightSideMiddle;
    [SerializeField] private GameObject rightSideFront;
    [Space]
    [SerializeField] private float frontSideRayLength;
    [SerializeField] private GameObject frontSideMiddle;
    [SerializeField] private GameObject frontSideLeft;
    [SerializeField] private GameObject frontSideLeftMiddle;
    [SerializeField] private GameObject frontSideRight;
    [SerializeField] private GameObject frontSideRightMiddle;

	[Space]
	[SerializeField] private int collisionCounter;
	private bool isTurnBack;

	private RaceAIInputs raceAIInputs;

	private Coroutine _coroutine;

    private bool isInitialized;

    private void OnEnable()
    {
        if (isInitialized)
        {
            Debug.LogError("Enable");
            _coroutine = StartCoroutine(WaitAndCheckObstacles());
        }
    }

    public void Initialize(RaceAIInputs _raceAIInputs)
    {
        raceAIInputs = _raceAIInputs;

        _coroutine = StartCoroutine(WaitAndCheckObstacles());

        isInitialized = true;
    }

	private IEnumerator WaitAndCheckObstacles()
	{
		while (true)
		{
			CheckObstacles();

			if (inUpdate) yield return new WaitForEndOfFrame();
			else yield return new WaitForSeconds(0.25f);
		}
	}

    public void CheckObstacles()
	{
        Work = Time.time;
		Obstacles = "Null";
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(frontSideLeft.transform.position, frontSideLeft.transform.forward, out hit, frontSideRayLength * 0.7f, obstacleLayerMask))
        {
            Obstacles = "Front Side Left";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 0.75f;
        }
        if (Physics.Raycast(frontSideLeftMiddle.transform.position, transform.forward, out hit, frontSideRayLength, obstacleLayerMask))
        {
            Obstacles = "front Side Left Middle";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 1f;
        }
        if (Physics.Raycast(frontSideRight.transform.position, frontSideRight.transform.forward, out hit, frontSideRayLength * 0.7f, obstacleLayerMask))
        {
            Obstacles = "front Side Right";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -0.75f;
        }
        if (Physics.Raycast(frontSideRightMiddle.transform.position, transform.forward, out hit, frontSideRayLength, obstacleLayerMask))
        {
            Obstacles = "front Side Right Middle";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -1f;
        }
        if (Physics.Raycast(frontSideMiddle.transform.position, transform.forward, out hit, frontSideRayLength * 1.25f, obstacleLayerMask)
            )
        {                   // Center
            Debug.DrawRay(hit.point, hit.normal, Color.cyan);

            angleObstacle = Vector2.SignedAngle(new Vector2(hit.normal.x, hit.normal.z), //угол между автомобилем и целевой траекторией
                new Vector2(transform.forward.x, transform.forward.z));

            if (angleObstacle < 0)
            {
                Obstacles = "front Side Middle To Left";

                raceAIInputs.obstacleSteer = -1f;
            }
            else
            {
                Obstacles = "front Side Middle To Right";

                raceAIInputs.obstacleSteer = 1f;
            }
            ObstacleDistance = hit.distance;
        }

        if (Physics.Raycast(leftSideBack.transform.position, -transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            Obstacles = "left Side Back";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 0.75f;
        }
        if (Physics.Raycast(leftSideMiddle.transform.position, -transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            Obstacles = "left Side Middle";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 0.75f;
        }
        if (Physics.Raycast(leftSideFront.transform.position, -transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            Obstacles = "left Side Front";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 0.75f;
        }

        if (Physics.Raycast(rightSideBack.transform.position, transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            Obstacles = "right Side Back";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -0.75f;
        }
        if (Physics.Raycast(rightSideMiddle.transform.position, transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            Obstacles = "right Side Middle";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -0.75f;
        }
        if (Physics.Raycast(rightSideFront.transform.position, transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            Obstacles = "right Side Front";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -0.75f;
        }


        Debug.DrawLine(frontSideMiddle.transform.position, frontSideMiddle.transform.position + transform.forward * frontSideRayLength * 1.25f, Color.red);
        Debug.DrawLine(frontSideLeft.transform.position, frontSideLeft.transform.position + frontSideLeft.transform.forward * frontSideRayLength * 0.7f, Color.red);
        Debug.DrawLine(frontSideLeftMiddle.transform.position, frontSideLeftMiddle.transform.position + transform.forward * frontSideRayLength, Color.red);
        Debug.DrawLine(frontSideRight.transform.position, frontSideRight.transform.position + frontSideRight.transform.forward * frontSideRayLength * 0.7f, Color.red);
        Debug.DrawLine(frontSideRightMiddle.transform.position, frontSideRightMiddle.transform.position + transform.forward * frontSideRayLength, Color.red);

        Debug.DrawLine(leftSideBack.transform.position, leftSideBack.transform.position - transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(leftSideMiddle.transform.position, leftSideMiddle.transform.position - transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(leftSideFront.transform.position, leftSideFront.transform.position - transform.right * sidesRayLength, Color.red);

        Debug.DrawLine(rightSideBack.transform.position, rightSideBack.transform.position + transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(rightSideMiddle.transform.position, rightSideMiddle.transform.position + transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(rightSideFront.transform.position, rightSideFront.transform.position + transform.right * sidesRayLength, Color.red);

        if (Obstacles != "Null")// если перед ботом есть препятсвие и он не сдает назад
        {
			raceAIInputs.Obstacle = true;

            //if (isTurnBack)
            //{
            //    wheelVehicle.Throttle = -1f;
            //    wheelVehicle.Steering = 0f;
            //}
            //else
            //{
            //    wheelVehicle.Throttle = 1f;
            //}
        }
        else
        {
			raceAIInputs.Obstacle = false;

			isTurnBack = false;
            collisionCounter = 0;
        }
    }

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == 6)
		{
			collisionCounter++;

			if (collisionCounter >= 50 && collisionCounter <= 300)
			{
				isTurnBack = true;
			}

			if (collisionCounter > 300 && collisionCounter <= 600)
			{
				isTurnBack = false;
			}

			if (collisionCounter > 600)
			{
				collisionCounter = 50;
			}
		}
	}

    private void OnDestroy()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
    }

    private void OnDisable()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
    }
}
