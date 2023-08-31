using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetectionAI : MonoBehaviour
{
	[SerializeField] private float raycastLength = 3F;
    [Space]
    public LayerMask groundLayerMask;
    [Header("Raycast Directions")]
	[SerializeField] private Transform tiltAngle;
	[SerializeField] private Transform frontLeftTiltPosition;
	[SerializeField] private Transform frontRightTiltPosition;

    [Header("DEBUG")]
    [SerializeField] private bool isGround;

    private RaceDriverAI raceDriverAI;


    public void Initialize(RaceDriverAI raceDriverAI)
    {
        this.raceDriverAI = raceDriverAI;
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

        if (isGround)
        {
            if (Physics.Raycast(posLeft, dirTiltAngle, out hit, raycastLength, groundLayerMask))
            {
                //Obstacles = "Left_3";
                isGround = true;
                //raceAIInputs.obstacleSteer = -0.5f;
            }
            else isGround = false;
        }

        raceDriverAI.IsGround = isGround;

        Debug.DrawLine(posRight, posRight + dirTiltAngle * raycastLength, Color.green);
		Debug.DrawLine(posLeft, posLeft + dirTiltAngle * raycastLength, Color.green);
	}
}
