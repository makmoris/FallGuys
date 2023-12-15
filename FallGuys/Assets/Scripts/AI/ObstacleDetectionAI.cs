using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetectionAI : MonoBehaviour
{
	[SerializeField] private bool inUpdate = false;
	[Space]
    [SerializeField] private LayerMask obstacleLayerMask;

    [Header("Raycast Directions")]
    private float sidesRayLength;
    [SerializeField] private GameObject leftSideBack;
    [SerializeField] private GameObject leftSideMiddle;
    [SerializeField] private GameObject leftSideFront;
    [Space]
    [SerializeField] private GameObject rightSideBack;
    [SerializeField] private GameObject rightSideMiddle;
    [SerializeField] private GameObject rightSideFront;
    
    private float frontSideRayLength;
    [Space]
    [SerializeField] private GameObject frontSideMiddle;
    [SerializeField] private GameObject frontSideLeftMiddle;
    [SerializeField] private GameObject frontSideRightMiddle;
    [Space]
    [SerializeField] private GameObject frontSideLeft_Angle;
    [SerializeField] private GameObject frontSideRight_Angle;

    [Header("DEBUG")]
    [SerializeField] private string Obstacles = "Null";
    [SerializeField] private float ObstacleDistance = 0;
    [SerializeField] private float angleObstacle = 0;

    [SerializeField] private int collisionCounter;
    private bool isTurnBack;

    private DriverAI driverAI;

    private Coroutine _coroutine;

    private bool isAILogicSetted;

    private void OnEnable()
    {
        if (isAILogicSetted)
        {
            _coroutine = StartCoroutine(WaitAndCheckObstacles());
        }
    }

    public void SetAILogic(DriverAI driverAI, float frontSideRayLength, float sidesRayLength, float angleForSides)
    {
        this.driverAI = driverAI;

        this.frontSideRayLength = frontSideRayLength;
        this.sidesRayLength = sidesRayLength;

        frontSideLeft_Angle.transform.rotation = Quaternion.Euler(0f, -angleForSides, 0f);
        frontSideRight_Angle.transform.rotation = Quaternion.Euler(0f, angleForSides, 0f);

        if(frontSideRayLength <= 0 || sidesRayLength <= 0 || angleForSides == 0)
        {
            Debug.LogError($"These values cannot be <= 0 (angleForSides must != 0); Values from Installer; frontSideRayLength = {frontSideRayLength}, " +
                $" sidesRayLength = {sidesRayLength}, angleForSides = {angleForSides}");
        }

        _coroutine = StartCoroutine(WaitAndCheckObstacles());

        isAILogicSetted = true;
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
        Obstacles = "Null";
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(frontSideLeft_Angle.transform.position, frontSideLeft_Angle.transform.forward, out hit, frontSideRayLength * 0.7f, obstacleLayerMask))
        {
            if(driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "Front Side Left";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = 0.75f;
            }
        }
        if (Physics.Raycast(frontSideLeftMiddle.transform.position, transform.forward, out hit, frontSideRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "front Side Left Middle";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = 1f;
            }
        }
        if (Physics.Raycast(frontSideRight_Angle.transform.position, frontSideRight_Angle.transform.forward, out hit, frontSideRayLength * 0.7f, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "front Side Right";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = -0.75f;
            }
        }
        if (Physics.Raycast(frontSideRightMiddle.transform.position, transform.forward, out hit, frontSideRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "front Side Right Middle";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = -1f;
            }
        }
        if (Physics.Raycast(frontSideMiddle.transform.position, transform.forward, out hit, frontSideRayLength * 1.25f, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                // Center
                Debug.DrawRay(hit.point, hit.normal, Color.cyan);

                angleObstacle = Vector2.SignedAngle(new Vector2(hit.normal.x, hit.normal.z), //угол между автомобилем и целевой траекторией
                    new Vector2(transform.forward.x, transform.forward.z));

                if (angleObstacle < 0)
                {
                    Obstacles = "front Side Middle To Left";

                    driverAI.ObstacleSteer = -1f;
                }
                else
                {
                    Obstacles = "front Side Middle To Right";

                    driverAI.ObstacleSteer = 1f;
                }
                ObstacleDistance = hit.distance;
            }
        }

        if (Physics.Raycast(leftSideBack.transform.position, -transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "left Side Back";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = 0.75f;
            }
        }
        if (Physics.Raycast(leftSideMiddle.transform.position, -transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "left Side Middle";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = 0.75f;
            }
        }
        if (Physics.Raycast(leftSideFront.transform.position, -transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "left Side Front";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = 0.75f;
            }
        }

        if (Physics.Raycast(rightSideBack.transform.position, transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "right Side Back";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = -0.75f;
            }
        }
        if (Physics.Raycast(rightSideMiddle.transform.position, transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "right Side Middle";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = -0.75f;
            }
        }
        if (Physics.Raycast(rightSideFront.transform.position, transform.right, out hit, sidesRayLength, obstacleLayerMask))
        {
            if (driverAI.gameObject != hit.transform.gameObject)
            {
                Obstacles = "right Side Front";
                ObstacleDistance = hit.distance;

                driverAI.ObstacleSteer = -0.75f;
            }
        }


        Debug.DrawLine(frontSideMiddle.transform.position, frontSideMiddle.transform.position + transform.forward * frontSideRayLength * 1.25f, Color.red);
        Debug.DrawLine(frontSideLeft_Angle.transform.position, frontSideLeft_Angle.transform.position + frontSideLeft_Angle.transform.forward * frontSideRayLength * 0.7f, Color.red);
        Debug.DrawLine(frontSideLeftMiddle.transform.position, frontSideLeftMiddle.transform.position + transform.forward * frontSideRayLength, Color.red);
        Debug.DrawLine(frontSideRight_Angle.transform.position, frontSideRight_Angle.transform.position + frontSideRight_Angle.transform.forward * frontSideRayLength * 0.7f, Color.red);
        Debug.DrawLine(frontSideRightMiddle.transform.position, frontSideRightMiddle.transform.position + transform.forward * frontSideRayLength, Color.red);

        Debug.DrawLine(leftSideBack.transform.position, leftSideBack.transform.position - transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(leftSideMiddle.transform.position, leftSideMiddle.transform.position - transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(leftSideFront.transform.position, leftSideFront.transform.position - transform.right * sidesRayLength, Color.red);

        Debug.DrawLine(rightSideBack.transform.position, rightSideBack.transform.position + transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(rightSideMiddle.transform.position, rightSideMiddle.transform.position + transform.right * sidesRayLength, Color.red);
        Debug.DrawLine(rightSideFront.transform.position, rightSideFront.transform.position + transform.right * sidesRayLength, Color.red);

        if (Obstacles != "Null")// если перед ботом есть препятсвие и он не сдает назад
        {
            driverAI.Obstacle = true;

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
            driverAI.Obstacle = false;

            isTurnBack = false;
            collisionCounter = 0;
        }
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
