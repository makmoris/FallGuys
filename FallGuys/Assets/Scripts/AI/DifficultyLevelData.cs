using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty Level")]
public class DifficultyLevelData : ScriptableObject
{
    [SerializeField] private float shotDecisionSpeed;
    [SerializeField] private int playerValue;
}
