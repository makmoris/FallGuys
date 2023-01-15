using UnityEngine;

public class WeaponId : MonoBehaviour
{
    [SerializeField] private string _weaponId;

    public string WeaponID
    {
        get { return _weaponId; }
    }
}
