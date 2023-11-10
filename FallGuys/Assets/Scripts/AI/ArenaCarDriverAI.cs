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

    [SerializeField]private List<Transform> bonusBoxes = new();// дл€ дуэли, чтобы чередовал игрока с бонусами
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

            //определ€ем, цель перед машиной или сзади
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
                float reverseDistance = 1f;// если дальше чем на N, то разворачиваемс€, а не сдаем назад
                if (distanceToTarget > reverseDistance)// 0 - без заднего хода. ¬сегда в разворот (ћог застр€ть, сдава€ назад и утыка€сь в преп€тствие)
                {
                    // too far to reverse
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -1f;
                }
            }

            // определ€ем сторону поворота 
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

            if (wheelVehicle.Speed > 1f)// если скорость больше 1, то тормозим 
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

                //print(gameObject.name + " догнал " + targetPositionTransform.name + "   - isTargetReachedFirst");
            }
        }

        // дл€ движени€
        wheelVehicle.Throttle = forwardAmount;

        // дл€ поворотов
        wheelVehicle.Steering = turnAmount;

        // дл€ нитро
        //wheelVehicle.boosting = true;

        // дл€ прыжков / — прыжками проблема, т.к. он зависит от длительности нажати€ кнопки. —делать через корутину, чтобы подержать секунду true и 
        // отключить в false
        //wheelVehicle.jumping = true;
    }

    private void ChooseTargetPosition(Vector3 targetPosition)
    {
        if (targetReached)// если цель достигнута, то выбираем новую
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
                if (targetForDuelTransform != null)// дл€ дуэли. ¬ыбираем только между игроком и бонусами
                {
                    //difficultyLevelAI.
                    //int randomDuel = Random.Range(0, сложность бота, как часто он выбирает игрока на дуели);
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
            if (targets[i].gameObject == wheelVehicle.gameObject) // исключаем из целей этого же бота, чтобы не ездил сам за собой
            {
                targets.RemoveAt(i);
                break;
            }
        }

        foreach (var item in targets)
        {
            if (item.GetComponent<BonusBox>() != null)// пока так, потом, если понадобитс€, можно создать какой-нибудь скрипт и кинуть на бокс
            {
                bonusBoxes.Add(item);
            }

            //if (item.GetComponent<Bumper>() != null && item.GetComponent<ArenaCarDriverAI>() == null)
            //{
            //    playerTransform = item;
            //}
        }

        // если нужно, то добавл€ем еще игроков, чтобы был больше шанс на него напасть
        arenaDifficultyLevelAI = GetComponent<ArenaDifficultyLevelsAI>();
        float frequencyOfTargetingPlayer = arenaDifficultyLevelAI.GetFrequencyOfTargetingPlayer();

        if(frequencyOfTargetingPlayer != 0)
        {
            //Transform _player = targets[0];// как заглушка. Ќаходим игрока

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

    public void StartDuel(Transform _targetDuelTransform)// вызываетс€ LevelProgressController, когда остаетс€ один бот и игрок
    {
        isDuel = true;

        targetReached = true;

        targetForDuelTransform = _targetDuelTransform;
        ChooseTargetPosition(targetForDuelTransform.position);
    }

    public void SetNewPlayerTargetFromDetector(Transform transform)
    {
        if (!isDuel)// во врем€ дуэли бот не реагирует на внезапные цели
        {
            targetPositionTransform = transform;
        }
    }

    public void DetectorLostTarget()
    {
         if(!isDuel )// во врем€ дуэли бот не реагирует на внезапные цели
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
            //print("«ащита");
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
