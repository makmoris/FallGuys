using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSidesController : MonoBehaviour
{
    private HealthBonus healthBonus;
    private float damage;

    [SerializeField] private int minDamageSpeedValue;
    [SerializeField] private int maxDamageSpeedValue;

    private Dictionary<Collider, Bumper> _parentsOfCollisionObjectsDict = new Dictionary<Collider, Bumper>();

    private Rigidbody _rbParent;

    private bool isForwardMovement;
    private bool isBackwardMovement;

    private bool canNewHit = true;
    [SerializeField]private bool isPlayer;
    private AudioSource audioSource;

    private void Awake()
    {
        _rbParent = transform.GetComponentInParent<Rigidbody>();
    }

    public void SetIsPlayer()
    {
        isPlayer = true;
        audioSource = GetComponent<AudioSource>();
    }

    public void SideHitted(string hittedSide, Collider enemyCollider)
    {
        if (canNewHit)
        {
            if (isPlayer)
            {
                CinemachineShake.Instance.ShakeCamera();
                audioSource.Play();
            }

            Bumper enemyBumper = GetParentElementFromDictionary(enemyCollider);// �������� RB �������, � ������� ���������

            // �������, � ����� ����������� �� ��������� � ���� ������. ������ ��� ������� �����
            var velocityZ = _rbParent.velocity.normalized.z;// �������� -+ �� Z
            var directionZ = transform.forward.z;

            if ((velocityZ >= 0 && directionZ >= 0) || (velocityZ < 0 && directionZ < 0))// ���� ����� ���������, ������ ��������� ������
            {
                isForwardMovement = true;
                isBackwardMovement = false;
            }
            else // ����� �����
            {
                isBackwardMovement = true;
                isForwardMovement = false;
            }

            // �������, � ����� ����� �� ����� ���� ��������� ����. ���� ��� �������
            if (hittedSide == "BackSide")// ���� ���� �������� �� ������ ������
            {
                if (isForwardMovement)// � �� � ���� ������ ����� ������, ������ ��������� ������ ��� � ����. �� �������� ����
                {
                    Debug.Log($"{gameObject.transform.parent.name} �������� ���� �� {enemyBumper.name}, �.�. {enemyBumper.name} ������ ��� � ����");
                }
                else if (isBackwardMovement)// � �� � ���� ������ ������� �����, ������ �� ������� ����, �.�. ������ ������� ����� �����
                {
                    var val = Mathf.Abs(_rbParent.velocity.z);

                    if (val >= minDamageSpeedValue && val <= maxDamageSpeedValue)
                    {
                        damage = val * -1f;

                        healthBonus = new HealthBonus(damage);
                        enemyBumper.GetBonus(healthBonus);
                    }
                    Debug.Log($"{gameObject.transform.parent.name} ������� ���� {enemyBumper.name} � ������� {val}, �.�. {gameObject.transform.parent.name} ������ ��� � �����");
                }

                StartCoroutine(WaitToNewHit());
            }
            else if (hittedSide == "FrontSide")// ���� ���� �������� �� �������� ������
            {
                if (isForwardMovement)// � �� � ���� ������ ����� ������, ������ �� ������� ����, �.�. ������ ������� ����� �������
                {
                    var val = Mathf.Abs(_rbParent.velocity.z);

                    if (val >= minDamageSpeedValue && val <= maxDamageSpeedValue)
                    {
                        damage = val * -1f;

                        healthBonus = new HealthBonus(damage);
                        enemyBumper.GetBonus(healthBonus);
                    }
                    Debug.Log($"{gameObject.transform.parent.name} ������� ���� {enemyBumper.name} � ������� {val}, �.�. {gameObject.transform.parent.name} ����� ������� ��� �������");
                }
                else if (isBackwardMovement)// � �� � ���� ������ ������� �����, ������ �� �������� ����, �.�. ������� �����, � ��������� ������ ��� � �����
                {
                    Debug.Log($"{gameObject.transform.parent.name} �������� ���� �� {enemyBumper.name}, �.�. {enemyBumper.name} ������ ��� � �����");
                }

                StartCoroutine(WaitToNewHit());
            }
            else// ���� ���� �������� �� ����� �� ������, �� ��� � ����� ������ �������� ����, �.�. � ��� �������
            {
                Debug.Log($"{gameObject.transform.parent.name} �������� ���� � ��� �� {enemyBumper.name}");

                StartCoroutine(WaitToNewHit());
            }
        }
    }

    private Bumper GetParentElementFromDictionary(Collider collider)
    {
        Bumper parentBumper;

        if (_parentsOfCollisionObjectsDict.ContainsKey(collider))// ���� ����� ��� ����, �� ����� ��� �������� �� ������
        {
            parentBumper = _parentsOfCollisionObjectsDict[collider];
        }
        else// ���� ���, �� ��������� ��� � ������ � ���������� ���
        {
            Bumper parentB = collider.GetComponentInParent<Bumper>();
            _parentsOfCollisionObjectsDict.Add(collider, parentB);

            parentBumper = parentB;
        }

        return parentBumper;
    }

    private IEnumerator WaitToNewHit()
    {
        canNewHit = false;

        yield return new WaitForSeconds(1f);

        canNewHit = true;
    }
}
