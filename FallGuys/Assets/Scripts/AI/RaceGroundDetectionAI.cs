using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceGroundDetectionAI : MonoBehaviour
{
	[SerializeField] private float raycastLength = 2F;
    [Space]
    public LayerMask groundLayerMask;
    [SerializeField] private bool isGround;
    [Header("Raycast Directions")]
	[SerializeField] private Transform tiltAngle;
	[SerializeField] private Transform frontLeftTiltPosition;
	[SerializeField] private Transform frontRightTiltPosition;

    private RaceDriverAI raceDriverAI;

    private void Awake()
    {
        raceDriverAI = GetComponent<RaceDriverAI>();
    }

    private void Update()
    {
        RaycastHit hit;

        Vector3 dirTiltAngle = tiltAngle.forward;

		Vector3 posRight = frontLeftTiltPosition.position;
		Vector3 posLeft = frontRightTiltPosition.position;

        if (Physics.Raycast(posRight, dirTiltAngle, out hit, raycastLength, groundLayerMask))
        {
            //Obstacles = "Right_3";
            isGround = true;
            //raceAIInputs.obstacleSteer = 0.5f;
        }
        else isGround = false;

        if (Physics.Raycast(posLeft, dirTiltAngle, out hit, raycastLength, groundLayerMask))
        {
            //Obstacles = "Left_3";
            isGround = true;
            //raceAIInputs.obstacleSteer = -0.5f;
        }
        else isGround = false;

        raceDriverAI.IsGround = isGround;

        Debug.DrawLine(posRight, posRight + dirTiltAngle * raycastLength, Color.green);
		Debug.DrawLine(posLeft, posLeft + dirTiltAngle * raycastLength, Color.green);
	}
}
