using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostPlace : MonoBehaviour
{
    [SerializeField] private Transform carPlace;
    [SerializeField] private PostRaceBackMovingWall backMovingWall;
    [Space]
    [SerializeField] private Material cellWithPlayerMaterial;

    private MeshRenderer _meshRenderer;
    private MeshRenderer _backWallMeshRengerer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _backWallMeshRengerer = backMovingWall.gameObject.GetComponent<MeshRenderer>();
    }

    public Transform GetPlaceTransform()
    {
        return carPlace;
    }

    public void DumpThisCar(float timeBeforDumping)
    {
        backMovingWall.Dumping(timeBeforDumping);
    }

    public void RecolorCellForPlayer()
    {
        _meshRenderer.material = cellWithPlayerMaterial;
        _backWallMeshRengerer.material = cellWithPlayerMaterial;
    }
}
