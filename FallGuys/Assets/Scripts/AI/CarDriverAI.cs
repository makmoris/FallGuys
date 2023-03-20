using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class CarDriverAI : MonoBehaviour
{
    [Header("Точки Интереса")]
    public List<Transform> targets;

    [Space]
    [SerializeField] private Transform targetPositionTransform;
    private Vector3 targetPosition;

    private bool targetReached;
    private bool isTargetReachedFirst;

    private WheelVehicle wheelVehicle;
    
    private bool obstacle;
    public bool Obstacle { get => obstacle;
        set => obstacle = value;
    }

    private DifficultyLevelsAI difficultyLevelAI;

    private List<Transform> bonusBoxes = new();// для дуэли, чтобы чередовал игрока с бонусами
    [SerializeField]private bool isDuel;
    [SerializeField]private int playerWasTargetCounter;
    [SerializeField] private int bonusWasTargetCounter;
    private Transform playerTransform;

    private void Awake()
    {
        wheelVehicle = GetComponent<WheelVehicle>();
    }

    private void Update()
    {
        if(!obstacle) Moving();
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

            //определяем, цель перед машиной или сзади
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
                float reverseDistance = 1f;// если дальше чем на N, то разворачиваемся, а не сдаем назад
                if (distanceToTarget > reverseDistance)// 0 - без заднего хода. Всегда в разворот (Мог застрять, сдавая назад и утыкаясь в препятствие)
                {
                    // too far to reverse
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -1f;
                }
            }

            // определяем сторону поворота 
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

        // для движения
        wheelVehicle.Throttle = forwardAmount;

        // для поворотов
        wheelVehicle.Steering = turnAmount;

        // для нитро
        //wheelVehicle.boosting = true;

        // для прыжков / С прыжками проблема, т.к. он зависит от длительности нажатия кнопки. Сделать через корутину, чтобы подержать секунду true и 
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

            if (isDuel && playerTransform != null)// для дуэли. Выбираем только между игроком и бонусами
            {
                //difficultyLevelAI.
                //int randomDuel = Random.Range(0, сложность бота, как часто он выбирает игрока на дуели);
                DuelType duelType = difficultyLevelAI.GetDuelType();
                switch (duelType)
                {
                    case DuelType.randomFromPlayerAndBonus:

                        int randVal = Random.Range(0, 2);

                        if(randVal == 0)
                        {
                            targetPositionTransform = playerTransform;
                        }
                        else
                        {
                            int rr = Random.Range(0, bonusBoxes.Count);
                            targetPositionTransform = bonusBoxes[rr];
                        }

                        break;

                    case DuelType.alternationBetweenPlayerAndBonusBonus:

                        if(playerWasTargetCounter == 0)
                        {
                            targetPositionTransform = playerTransform;
                            playerWasTargetCounter++;
                        }
                        else
                        {
                            if(bonusWasTargetCounter == 0)
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
                            Debug.Log("ИЩЕТ ИГРОКА");
                            targetPositionTransform = playerTransform;
                            playerWasTargetCounter++;
                        }
                        else
                        {
                            Debug.Log("ИЩЕТ БОНУС");
                            int rr = Random.Range(0, bonusBoxes.Count);
                            targetPositionTransform = bonusBoxes[rr];

                            playerWasTargetCounter = 0;
                        }

                        break;

                    case DuelType.playerOnly:

                        targetPositionTransform = playerTransform;
                        break;
                }

                Debug.Log("Ищем игрока");
            }
            else if(playerTransform == null)
            {
                isDuel = false;
                targetPositionTransform = targets[0];
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
            if (targets[i].gameObject == gameObject) // исключаем из целей этого же бота, чтобы не ездил сам за собой
            {
                targets.RemoveAt(i);
                break;
            }
        }

        foreach (var item in targets)
        {
            if (item.GetComponent<Animator>() != null)// пока так, потом, если понадобится, можно создать какой-нибудь скрипт и кинуть на бокс
            {
                bonusBoxes.Add(item);
            }

            if (item.GetComponent<Bumper>() != null && item.GetComponent<CarDriverAI>() == null)
            {
                playerTransform = item;
            }
        }

        // если нужно, то добавляем еще игроков, чтобы был больше шанс на него напасть
        difficultyLevelAI = GetComponent<DifficultyLevelsAI>();
        float frequencyOfTargetingPlayer = difficultyLevelAI.GetFrequencyOfTargetingPlayer();

        if(frequencyOfTargetingPlayer != 0)
        {
            //Transform _player = targets[0];// как заглушка. Находим игрока

            //foreach (var elem in targets)
            //{
            //    if(elem.GetComponent<Bumper>() != null && elem.GetComponent<CarDriverAI>() == null)
            //    {
            //        _player = elem;
            //        break;
            //    }
            //}

            int addedPlayersValue = Mathf.CeilToInt(targets.Count * frequencyOfTargetingPlayer);

            for (int i = 0; i < addedPlayersValue; i++)
            {
                targets.Add(playerTransform);
            }
        }

        int rand = Random.Range(0, targets.Count);

        targetPositionTransform = targets[rand];
        isTargetReachedFirst = true;
    }

    public void StartDuel()// вызывается LevelProgressController, когда остается один бот и игрок
    {
        isDuel = true;

        targetReached = true;
        ChooseTargetPosition(playerTransform.position);
    }

    public void SetNewPlayerTargetFromDetector(Transform transform)
    {
        if (!isDuel)// во время дуэли бот не реагирует на внезапные цели
        {
            targetPositionTransform = transform;
        }
    }

    public void DetectorLostTarget()
    {
         if(!isDuel )// во время дуэли бот не реагирует на внезапные цели
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
            //print("Защита");
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
}
