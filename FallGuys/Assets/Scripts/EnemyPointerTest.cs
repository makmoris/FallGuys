using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPointerTest : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _camera;

    //[SerializeField] private Transform _worldPointer;
    [SerializeField] private Transform _pointerIconTransform;

    private void Update()
    {
        Vector3 fromPlayerToEnemy = transform.position - _playerTransform.position; // из конечной точки вычитаем начальную
        Ray ray = new Ray(_playerTransform.position, fromPlayerToEnemy);
        Debug.DrawRay(_playerTransform.position, fromPlayerToEnemy);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;
        int planeIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    planeIndex = i;
                }
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToEnemy.magnitude);


        Vector3 worldPosition = ray.GetPoint(minDistance);
        //_worldPointer.position = worldPosition;

        _pointerIconTransform.position = _camera.WorldToScreenPoint(worldPosition);
        _pointerIconTransform.rotation = GetIconRotation(planeIndex);
    }

    private Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex == 0)
        {
            return Quaternion.Euler(0f, 0f, 90f);
        }
        else if (planeIndex == 1)
        {
            return Quaternion.Euler(0f, 0f, -90f);
        }
        else if (planeIndex == 2)
        {
            return Quaternion.Euler(0f, 0f, 180f);
        }
        else if (planeIndex == 3)
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }

        return Quaternion.identity;
    }
}
