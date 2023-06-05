using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRacePlace : MonoBehaviour
{
    [SerializeField] private Transform carPlace;
    [SerializeField] private PostRaceBackMovingWall backMovingWall;

    public Transform GetPlaceTransform()
    {
        return carPlace;
    }

    public void DumpThisCar(float timeBeforDumping)
    {
        backMovingWall.Dumping(timeBeforDumping);
    }
}
