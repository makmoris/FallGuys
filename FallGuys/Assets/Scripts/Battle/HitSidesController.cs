using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSidesController : Bonus
{
    [SerializeField] private int minDamageSpeedValue;
    [SerializeField] private int maxDamageSpeedValue;

    private float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    private Dictionary<Collider, Bumper> _parentsOfCollisionObjectsDict = new Dictionary<Collider, Bumper>();

    private Rigidbody _rbParent;

    private bool isForwardMovement;
    private bool isBackwardMovement;

    [SerializeField] private bool canNewHit = true;

    private void Awake()
    {
        _rbParent = transform.GetComponentInParent<Rigidbody>();
    }

    public void SideHitted(string hittedSide, Collider enemyCollider)
    {
        if (canNewHit)
        {
            Bumper enemyBumper = GetParentElementFromDictionary(enemyCollider);// получили RB объекта, с которым ударились

            // смотрим, в каком направлении МЫ двигались в этот момент. Вперед или сдавали назад
            var velocityZ = _rbParent.velocity.normalized.z;// получаем -+ по Z
            var directionZ = transform.forward.z;

            if ((velocityZ >= 0 && directionZ >= 0) || (velocityZ < 0 && directionZ < 0))// если знаки совпадают, значит двигались вперед
            {
                isForwardMovement = true;
                isBackwardMovement = false;
            }
            else // сдаем назад
            {
                isBackwardMovement = true;
                isForwardMovement = false;
            }

            // смотрим, в каком месте НА НАШЕМ авто произошел удар. Куда НАС ударили
            if (hittedSide == "BackSide")// если удар пришелся на ЗАДНИЙ бампер
            {
                if (isForwardMovement)// а мы в этот момент ехали вперед, значит противник влетел нам в жопу. Мы получаем урон
                {
                    //Debug.Log($"{gameObject.transform.parent.name} получает урон от {enemyBumper.name}, т.к. {enemyBumper.name} влетел нам в жопу");
                }
                else if (isBackwardMovement)// а мы в этот момент сдавали назад, значит мы наносим урон, т.к. хотели ударить врага жопой
                {
                    var val = Mathf.Abs(_rbParent.velocity.z);

                    if (val >= minDamageSpeedValue && val <= maxDamageSpeedValue)
                    {
                        value = val;

                        enemyBumper.GetBonus(this);
                    }
                    //Debug.Log($"{gameObject.transform.parent.name} наносит урон {enemyBumper.name} в размере {val}, т.к. {gameObject.transform.parent.name} влетел ему в жопой");
                }

                StartCoroutine(WaitToNewHit());
                return;
            }
            else if (hittedSide == "FrontSide")// если удар пришелся на ПЕРЕДНИЙ бампер
            {
                if (isForwardMovement)// а мы в этот момент ехали вперед, значит мы наносим урон, т.к. хотели ударить врага передом
                {
                    var val = Mathf.Abs(_rbParent.velocity.z);

                    if (val >= minDamageSpeedValue && val <= maxDamageSpeedValue)
                    {
                        value = val;

                        enemyBumper.GetBonus(this);
                    }
                    //Debug.Log($"{gameObject.transform.parent.name} наносит урон {enemyBumper.name} в размере {val}, т.к. {gameObject.transform.parent.name} хотел ударить его передом");
                }
                else if (isBackwardMovement)// а мы в этот момент сдавали назад, значит мы получаем урон, т.к. сдавали назад, а противник влетел нам в перед
                {
                    //Debug.Log($"{gameObject.transform.parent.name} получает урон от {enemyBumper.name}, т.к. {enemyBumper.name} влетел нам в перед");
                }

                StartCoroutine(WaitToNewHit());
                return;
            }
            else// если удар пришелся на любую из сторон, то там в любом случае получаем урон, т.к. в нас влетели
            {
                //Debug.Log($"{gameObject.transform.parent.name} получает урон от в бок от {enemyBumper.name}");

                StartCoroutine(WaitToNewHit());
                return;
            }
        }
    }

    private Bumper GetParentElementFromDictionary(Collider collider)
    {
        Bumper parentBumper;

        if (_parentsOfCollisionObjectsDict.ContainsKey(collider))// если такой уже есть, то берем его родителя из списка
        {
            parentBumper = _parentsOfCollisionObjectsDict[collider];
        }
        else// если нет, то добавляем его в список и используем его
        {
            Bumper parentB = collider.GetComponentInParent<Bumper>();
            _parentsOfCollisionObjectsDict.Add(collider, parentB);

            parentBumper = parentB;
        }

        return parentBumper;
    }

    public override void Got()
    {
        // заглушка, чтобы не удалять объект
    }

    private IEnumerator WaitToNewHit()
    {
        canNewHit = false;

        yield return new WaitForSeconds(1f);

        canNewHit = true;
    }
}
