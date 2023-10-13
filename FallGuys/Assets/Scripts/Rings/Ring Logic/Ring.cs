using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private EnemyPointer enemyPointer;
    private AttackPointer attackPointer;
    private RingsController ringsController;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private float rechargeTime;


    private bool isActive;
    public bool IsActive { get => isActive; }

    private void Awake()
    {
        enemyPointer = GetComponentInChildren<EnemyPointer>();
        attackPointer = GetComponentInChildren<AttackPointer>();

        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        effect.SetActive(false);
    }

    public void Initialize(LevelUI levelUI, RingsController ringsController, float rechargeTime)
    {
        enemyPointer.Initialize(levelUI);
        this.ringsController = ringsController;
        this.rechargeTime = rechargeTime;

        DeactivateRing(false);
    }

    public void PlayerKnockedDownTheRing(GameObject playerGO)
    {
        isActive = false;

        ringsController.TheRingIsKnockedOff(playerGO);
        DeactivateRing(true);

        effect.SetActive(false);
        effect.SetActive(true);
    }

    public void ActivateRing()
    {
        isActive = true;

        _meshRenderer.enabled = true;
        _collider.enabled = true;

        enemyPointer.gameObject.SetActive(true);
        attackPointer.gameObject.SetActive(true);
    }

    private void DeactivateRing(bool withRecharge)
    {
        _meshRenderer.enabled = false;
        _collider.enabled = false;

        enemyPointer.gameObject.SetActive(false);
        attackPointer.gameObject.SetActive(false);

        if(withRecharge) StartCoroutine(RechargeRing());
    }

    private void RingHasRecharged()
    {
        ringsController.RingHasRecharged(this);
    }

    IEnumerator RechargeRing()
    {
        yield return new WaitForSeconds(rechargeTime);

        RingHasRecharged();
    }
}
