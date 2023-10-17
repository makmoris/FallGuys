using UnityEngine;

public class BonusBoxTrigger : MonoBehaviour
{
    private BonusBox _bonusBox;

    private void Awake()
    {
        _bonusBox = GetComponentInParent<BonusBox>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            _bonusBox.ApplyBonus(other.gameObject);
        }
    }
}
