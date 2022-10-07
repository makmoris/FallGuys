using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu
[CreateAssetMenu(fileName = "PlayerLimitsData")]
public class PlayerLimitsData : ScriptableObject
{
    public float MinHP;
    public float MinSpeed;
    public float MinDamage;

    public float MaxHP;
    public float MaxSpeed;
    public float MaxDamage;
}
