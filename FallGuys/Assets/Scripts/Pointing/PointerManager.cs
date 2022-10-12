using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [SerializeField] PointerIcon _positionPointerPrefab;
    private Dictionary<EnemyPointer, PointerIcon> _dictionary = new Dictionary<EnemyPointer, PointerIcon>();
    [SerializeField] Transform _playerTransform;// сделать передачу из Installer-a
    [SerializeField] Camera _camera;
    [SerializeField] Transform _canvasTransform;

    //[SerializeField] AttackPointer _attackPointer;
    [SerializeField] PointerIcon _attackPointerPrefab;

    public static PointerManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _attackPointerPrefab = Instantiate(_attackPointerPrefab, _canvasTransform);
    }

    public void SetPlayerTransform(Transform transform)
    {
        _playerTransform = transform;
    }

    public void AddToList(EnemyPointer enemyPointer)
    {
        PointerIcon newPointer = Instantiate(_positionPointerPrefab, _canvasTransform);
        _dictionary.Add(enemyPointer, newPointer);
    }

    public void RemoveFromList(EnemyPointer enemyPointer)
    {
        //Destroy(_dictionary[enemyPointer].gameObject);
        _dictionary.Remove(enemyPointer);
    }

    void LateUpdate()
    {
        // Left, Right, Down, Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var kvp in _dictionary)
        {
            EnemyPointer enemyPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 toEnemy = enemyPointer.transform.position - _playerTransform.position;
            Ray ray = new Ray(_playerTransform.position, toEnemy);
            Debug.DrawRay(_playerTransform.position, toEnemy);


            float rayMinDistance = Mathf.Infinity;
            int index = 0;

            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }
                }
            }

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            Quaternion rotation = GetIconRotation(index);

            if (toEnemy.magnitude > rayMinDistance)
            {
                pointerIcon.Show();
            }
            else
            {
                pointerIcon.Hide();
            }

            pointerIcon.SetIconPosition(position, rotation);
        }

    }

    public void StartShowingAttackPointer(Transform transformToAttack)
    {
        _attackPointerPrefab.Show();
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }
    public void ShowAttackPointer(Transform transformToAttack)
    {
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }
    public void FinishShowingAttackPointer(Transform transformToAttack)
    {
        _attackPointerPrefab.Hide();
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }

    private Vector3 GetAttackTargetPosition(Transform transform)
    {
        Vector3 toAttackTarget = transform.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, toAttackTarget);
        Debug.DrawRay(_playerTransform.position, toAttackTarget);

        float distance = toAttackTarget.magnitude;
        Vector3 worldPosition = ray.GetPoint(distance);
        Vector3 position = _camera.WorldToScreenPoint(worldPosition);

        return position;
    }

    Quaternion GetIconRotation(int planeIndex)
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
            return Quaternion.Euler(0f, 0f, 180);
        }
        else if (planeIndex == 3)
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        return Quaternion.identity;
    }
}
