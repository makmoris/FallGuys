using PunchCars.DifficultyAILevels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class ArenaCarDriverAI : DriverAI
{
    [Header("DEBUG")]
    public List<Transform> targets;

    [Space]
    [SerializeField] private Transform targetPositionTransform;
    private Vector3 targetPosition;

    private bool targetReached;
    private bool isTargetReachedFirst;

    [SerializeField]private WheelVehicle wheelVehicle;
    
    //private bool obstacle;
    //public bool Obstacle { get => obstacle;
    //    set => obstacle = value;
    //}

    private ArenaDifficultyLevelsAI arenaDifficultyLevelAI;

    [SerializeField]private List<Transform> bonusBoxes = new();// ��� �����, ����� ��������� ������ � ��������
    [SerializeField]private bool isDuel;
    [SerializeField]private int playerWasTargetCounter;
    [SerializeField] private int bonusWasTargetCounter;
    private Transform playerTransform;
    private Transform targetForDuelTransform;

    //private void Awake()
    //{
    //    wheelVehicle = GetComponentInParent<WheelVehicle>();
    //}

    public override void Initialize(GameObject aiPlayerGO, GameObject currentPlayerGO, EnumDifficultyAILevels difficultyAILevel)
    {
        wheelVehicle = aiPlayerGO.GetComponent<WheelVehicle>();

        if(currentPlayerGO != null) playerTransform = currentPlayerGO.transform;
    }

    private void Update()
    {
        if(!obstacle) Moving();
        else
        {
            wheelVehicle.Steering = obstacleSteer;
            wheelVehicle.Throttle = 1f;
        }
    }
    
    private void Moving()
    {
        if (targetPositionTransform != null) ChooseTargetPosition(targetPositionTransform.position);
        else CheckTargetListOnNullComponent();

        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 6f;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget > reachedTargetDistance)
        {
            // still too far, keep going

            //����������, ���� ����� ������� ��� �����
            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            if (dot > 0)
            {
                // target in front
                forwardAmount = 1f;
            }
            else
            {
                // target in behind
                float reverseDistance = 1f;// ���� ������ ��� �� N, �� ���������������, � �� ����� �����
                if (distanceToTarget > reverseDistance)// 0 - ��� ������� ����. ������ � �������� (��� ��������, ������ ����� � �������� � �����������)
                {
                    // too far to reverse
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -1f;
                }
            }

            // ���������� ������� �������� 
            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir == 0 || (angleToDir > -15f && angleToDir < 15f))
            {
                turnAmount = 0;
            }
            else
            {
                if (angleToDir > 0)
                {
                    turnAmount = 1f;
                }
                else
                {
                    turnAmount = -1f;
                }
            }
            //print(angleToDir);
            isTargetReachedFirst = true;
        }
        else
        {
            // reached target

            if (wheelVehicle.Speed > 1f)// ���� �������� ������ 1, �� �������� 
            {
                forwardAmount = -1f;
            }
            else
            {
                forwardAmount = 0f;
            }
            turnAmount = 0f;

            if (isTargetReachedFirst)
            {
                targetReached = true;
                isTargetReachedFirst = false;

                //print(gameObject.name + " ������ " + targetPositionTransform.name + "   - isTargetReachedFirst");
            }
        }

        // ��� ��������
        wheelVehicle.Throttle = forwardAmount;

        // ��� ���������
        wheelVehicle.Steering = turnAmount;

        // ��� �����
        //wheelVehicle.boosting = true;

        // ��� ������� / � �������� ��������, �.�. �� ������� �� ������������ ������� ������. ������� ����� ��������, ����� ��������� ������� true � 
        // ��������� � false
        //wheelVehicle.jumping = true;
    }

    private void ChooseTargetPosition(Vector3 targetPosition)
    {
        if (targetReached)// ���� ���� ����������, �� �������� �����
        {
            int rand = Random.Range(0, targets.Count);

            if (targetPositionTransform != targets[rand])
            {
                targetPositionTransform = targets[rand];
            }
            else
            {
                if (rand == 0)
                {
                    targetPositionTransform = targets[rand + 1];
                }
                else if (rand == targets.Count)
                {
                    targetPositionTransform = targets[rand - 1];
                }
            }

            if (isDuel)
            {
                if (targetForDuelTransform != null)// ��� �����. �������� ������ ����� ������� � ��������
                {
                    //difficultyLevelAI.
                    //int randomDuel = Random.Range(0, ��������� ����, ��� ����� �� �������� ������ �� �����);
                    DuelType duelType = arenaDifficultyLevelAI.GetDuelType();
                    switch (duelType)
                    {
                        case DuelType.randomFromPlayerAndBonus:

                            int randVal = Random.Range(0, 2);

                            if (randVal == 0)
                            {
                                targetPositionTransform = targetForDuelTransform;
                            }
                            else
                            {
                                int rr = Random.Range(0, bonusBoxes.Count);
                                targetPositionTransform = bonusBoxes[rr];
                            }

                            break;

                        case DuelType.alternationBetweenPlayerAndBonusBonus:

                            if (playerWasTargetCounter == 0)
                            {
                                targetPositionTransform = targetForDuelTransform;
                                playerWasTargetCounter++;
                            }
                            else
                            {
                                if (bonusWasTargetCounter == 0)
                                {
                                    int rr = Random.Range(0, bonusBoxes.Count);
                                    targetPositionTransform = bonusBoxes[rr];
                                    bonusWasTargetCounter++;
                                }
                                else if (bonusWasTargetCounter == 1)
                                {
                                    int rr = Random.Range(0, bonusBoxes.Count);
                                    targetPositionTransform = bonusBoxes[rr];

                                    playerWasTargetCounter = 0;
                                    bonusWasTargetCounter = 0;
                                }
                            }

                            break;

                        case DuelType.alternationBetweenPlayerAndBonus:

                            if (playerWasTargetCounter == 0)
                            {
                                targetPositionTransform = targetForDuelTransform;
                                playerWasTargetCounter++;
                            }
                            else
                            {
                                int rr = Random.Range(0, bonusBoxes.Count);
                                targetPositionTransform = bonusBoxes[rr];

                                playerWasTargetCounter = 0;
                            }

                            break;

                        case DuelType.playerOnly:

                            targetPositionTransform = targetForDuelTransform;
                            break;
                    }
                }
                else if (targetForDuelTransform == null)
                {
                    isDuel = false;
                    //targetPositionTransform = targets[0];
                }
            }

            targetReached = false;

            StartCoroutine(CheckWhatTargetChoose());
        }
        else
        {
            this.targetPosition = targetPosition;
        }
    }

    public void SetTargets(List<Transform> _targets)
    {
        targets = _targets.GetRange(0, _targets.Count);

        
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].gameObject == wheelVehicle.gameObject) // ��������� �� ����� ����� �� ����, ����� �� ����� ��� �� �����
            {
                targets.RemoveAt(i);
                break;
            }
        }

        foreach (var item in targets)
        {
            if (item.GetComponent<BonusBox>() != null)// ���� ���, �����, ���� �����������, ����� ������� �����-������ ������ � ������ �� ����
            {
                bonusBoxes.Add(item);
            }

            //if (item.GetComponent<Bumper>() != null && item.GetComponent<ArenaCarDriverAI>() == null)
            //{
            //    playerTransform = item;
            //}
        }

        // ���� �����, �� ��������� ��� �������, ����� ��� ������ ���� �� ���� �������
        arenaDifficultyLevelAI = GetComponent<ArenaDifficultyLevelsAI>();
        float frequencyOfTargetingPlayer = arenaDifficultyLevelAI.GetFrequencyOfTargetingPlayer();

        if(frequencyOfTargetingPlayer != 0)
        {
            //Transform _player = targets[0];// ��� ��������. ������� ������

            //foreach (var elem in targets)
            //{
            //    if(elem.GetComponent<Bumper>() != null && elem.GetComponent<CarDriverAI>() == null)
            //    {
            //        _player = elem;
            //        break;
            //    }
            //}

            int addedPlayersValue = Mathf.CeilToInt(targets.Count * frequencyOfTargetingPlayer);

            if(playerTransform != null)
            {
                for (int i = 0; i < addedPlayersValue; i++)
                {
                    targets.Add(playerTransform);
                }
            }
        }

        int rand = Random.Range(0, targets.Count);

        targetPositionTransform = targets[rand];
        isTargetReachedFirst = true;
    }

    public void StartDuel(Transform _targetDuelTransform)// ���������� LevelProgressController, ����� �������� ���� ��� � �����
    {
        isDuel = true;

        targetReached = true;

        targetForDuelTransform = _targetDuelTransform;
        ChooseTargetPosition(targetForDuelTransform.position);
    }

    public void SetNewPlayerTargetFromDetector(Transform transform)
    {
        if (!isDuel)// �� ����� ����� ��� �� ��������� �� ��������� ����
        {
            targetPositionTransform = transform;
        }
    }

    public void DetectorLostTarget()
    {
         if(!isDuel )// �� ����� ����� ��� �� ��������� �� ��������� ����
        {
            targetReached = true;
            ChooseTargetPosition(targetPositionTransform.position);
        }
    }

    private void RemoveFromTargetsList(GameObject removedObj)
    {
        if(removedObj.transform == targetPositionTransform)
        {
            targetReached = true;
            ChooseTargetPosition(targetPositionTransform.position);
        }

        targets.Remove(removedObj.transform);
    }

    private void CheckTargetListOnNullComponent()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
                targetReached = true;
                ChooseTargetPosition(Vector3.zero);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetPositionTransform)
        {
            targetReached = true;
        }
    }

    IEnumerator CheckWhatTargetChoose()
    {
        yield return new WaitForSeconds(1f);
        if (!targetReached && !isTargetReachedFirst)
        {
            //print("������");
            targetReached = true;
        }
    }

    private void OnEnable()
    {
        VisualIntermediary.PlayerWasDeadEvent += RemoveFromTargetsList;
    }

    private void OnDisable()
    {
        VisualIntermediary.PlayerWasDeadEvent -= RemoveFromTargetsList;
    }

    #region Difficulty AI Levels
    public override void SetLowDifficultyAILevel()
    {
       
    }

    public override void SetNormalDifficultyAILevel()
    {
        
    }

    public override void SetHighDifficultyAILevel()
    {
        
    }
    #endregion
}
