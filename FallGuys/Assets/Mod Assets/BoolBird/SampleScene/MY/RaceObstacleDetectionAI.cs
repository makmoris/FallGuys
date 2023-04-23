using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceObstacleDetectionAI : MonoBehaviour
{
	[SerializeField] private bool inUpdate = false;
	[Space]
	[SerializeField] private string Obstacles = "Null";
	[SerializeField] private float ObstacleDistance = 0;
	[SerializeField] private float raycastLength = 10F;
	[SerializeField] private float angleObstacle = 0;

	public LayerMask obstacleLayerMask;

	[Header("Raycast Directions")]
	[SerializeField] private GameObject frontCapsuleCollider;
	[SerializeField] private Transform frontLeftWheel;
	[SerializeField] private Transform frontRightWheel;
	[SerializeField] private Transform leftAngle;
	[SerializeField] private Transform rightAngle;

	[Space]
	[SerializeField] private int collisionCounter;
	private bool isTurnBack;

	private RaceAIInputs raceAIInputs;

	private Coroutine _coroutine;

	private void Awake()
	{
		raceAIInputs = GetComponent<RaceAIInputs>();
	}

	private void OnEnable()
	{
		_coroutine = StartCoroutine(WaitAndCheckObstacles());
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

	private void OnDestroy()
	{
		if (_coroutine != null) StopCoroutine(_coroutine);
	}

	public void CheckObstacles()
	{
		Obstacles = "Null";
		RaycastHit hit;

		Vector3 PosCenter = frontCapsuleCollider.gameObject.transform.position;
		Vector3 DirCenter = frontCapsuleCollider.gameObject.transform.forward;

		Vector3 DirAngleLeft = -leftAngle.forward;
		Vector3 DirAngleRight = -rightAngle.forward;

		Vector3 PosRight = frontLeftWheel.position;
		Vector3 PosLeft = frontRightWheel.position;

        if (Physics.Raycast(PosRight, -DirAngleRight, out hit, raycastLength * .7f, obstacleLayerMask))
        {       // Right angle
            Obstacles = "Right_3";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 0.5f;
        }

        if (Physics.Raycast(PosLeft, -DirAngleLeft, out hit, raycastLength * .7f, obstacleLayerMask))
        {           // Left angle
            Obstacles = "Left_3";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -0.5f;
        }

        if (Physics.Raycast(PosCenter, DirCenter, out hit, raycastLength, obstacleLayerMask)
            )
        {                   // Center
            Debug.DrawRay(hit.point, hit.normal, Color.cyan);

            angleObstacle = Vector2.SignedAngle(new Vector2(hit.normal.x, hit.normal.z), //угол между автомобилем и целевой траекторией
                new Vector2(DirCenter.x, DirCenter.z));

            if (angleObstacle < 0)
            {
                Obstacles = "Left_1";

                raceAIInputs.obstacleSteer = -1f;
            }
            else
            {
                Obstacles = "Right_1";

                raceAIInputs.obstacleSteer = 1f;
            }
            ObstacleDistance = hit.distance;
        }
        else if (Physics.Raycast(PosLeft, DirCenter, out hit, raycastLength * .5f, obstacleLayerMask))
        {                   // Left
            Obstacles = "Left_2";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = -1f;
        }
        else if (Physics.Raycast(PosRight, DirCenter, out hit, raycastLength * .5f, obstacleLayerMask))
        {               // Right
            Obstacles = "Right_2";
            ObstacleDistance = hit.distance;

            raceAIInputs.obstacleSteer = 1f;
        }


        //if (Physics.Raycast(PosRight, -DirAngleRight, out hit, raycastLength * .7f, obstacleLayerMask))
        //{       // Right angle
        //    Obstacles = "Right_3";
        //    ObstacleDistance = hit.distance;

        //    raceAIInputs.obstacleSteer = 0.5f;
        //}

        //if (Physics.Raycast(PosLeft, -DirAngleLeft, out hit, raycastLength * .7f, obstacleLayerMask))
        //{           // Left angle
        //    Obstacles = "Left_3";
        //    ObstacleDistance = hit.distance;

        //    raceAIInputs.obstacleSteer = -0.5f;
        //}

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

        Debug.DrawLine(PosCenter, PosCenter + DirCenter * raycastLength, Color.red);
        Debug.DrawLine(PosLeft, PosLeft + DirCenter * raycastLength * .5f, Color.red);
        Debug.DrawLine(PosRight, PosRight + DirCenter * raycastLength * .5f, Color.red);

        Debug.DrawLine(PosLeft, PosLeft - DirAngleLeft * raycastLength * .7f, Color.blue);
        Debug.DrawLine(PosRight, PosRight - DirAngleRight * raycastLength * .7f, Color.blue);
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
}
