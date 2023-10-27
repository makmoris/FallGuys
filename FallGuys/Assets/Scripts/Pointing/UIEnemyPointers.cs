using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyPointers : MonoBehaviour
{
    // Pool
    private int poolCount = 3;
    private bool autoExpand = true;

    private PoolMono<PointerIcon> _positionPointersPool;
    //
    
    private Dictionary<EnemyPointer, PointerIcon> _positionDictionary = new Dictionary<EnemyPointer, PointerIcon>();
    private Transform currentTransform;// передавать из камеры текущую цель слежки
    [SerializeField] Camera _camera;
    [SerializeField] Transform _canvasTransform;

    [Header("Prefabs")]
    [SerializeField] PointerIcon _positionPointerPrefab;
    [SerializeField] PointerIconToAttack _attackPointerPrefab;

    private bool isNotFirstPlayer;

    private bool isInit;


    private void Awake()
    {
        if (!isInit) Initialize();
    }

    public void ChangeCurrentTransform(Transform _currentTransform)
    {
        currentTransform = _currentTransform;
    }

    public void AddEnemyPointer(EnemyPointer enemyPointer)
    {
        if (!isInit) Initialize();

        if (!_positionDictionary.ContainsKey(enemyPointer))
        {
            AddToPositionList(enemyPointer);
        }
    }

    public void ShowEnemyPositionPointer(EnemyPointer enemyPointer)
    {
        if (!isInit) Initialize();

        if (!_positionDictionary.ContainsKey(enemyPointer)) AddEnemyPointer(enemyPointer);

        PointerIcon newPointer = _positionDictionary[enemyPointer];
        newPointer.Show();
    }

    public void HideEnemyPositionPointer(EnemyPointer enemyPointer, bool remove)
    {
        if (!isInit) Initialize();

        if (!_positionDictionary.ContainsKey(enemyPointer)) AddEnemyPointer(enemyPointer);

        if (!remove)
        {
            PointerIcon newPointer = _positionDictionary[enemyPointer];
            if (newPointer != null) newPointer.Hide();
        }
        else
        {
            RemoveFromPositionList(enemyPointer);
        }
    }

    public void UpdateEnemyHP(float healthValue, EnemyPointer enemyPointer)
    {
        if (!isInit) Initialize();

        if (!_positionDictionary.ContainsKey(enemyPointer)) AddEnemyPointer(enemyPointer);

        PointerIcon pointerIcon = _positionDictionary[enemyPointer];
        pointerIcon.UpdateHealthColor(healthValue);
    }

    private void Initialize()
    {
        _attackPointerPrefab = Instantiate(_attackPointerPrefab, _canvasTransform);

        _positionPointersPool = new PoolMono<PointerIcon>(_positionPointerPrefab, poolCount, _canvasTransform);
        _positionPointersPool.autoExpand = autoExpand;

        isInit = true;
    }

    private void AddToPositionList(EnemyPointer enemyPointer)
    {
        PointerIcon newPointer = _positionPointersPool.GetFreeElement();
        _positionDictionary.Add(enemyPointer, newPointer);
        newPointer.Show();
    }

    private void RemoveFromPositionList(EnemyPointer enemyPointer)
    {
        PointerIcon pointerIcon = _positionDictionary[enemyPointer];
        
        if(pointerIcon != null)
        {
            pointerIcon.Hide();
            pointerIcon.gameObject.SetActive(false);
        }

        _positionDictionary.Remove(enemyPointer);
    }

    void LateUpdate()
    {
        // Left, Right, Down, Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);// нагружает

        foreach (var kvp in _positionDictionary)
        {
            EnemyPointer enemyPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 toEnemy = enemyPointer.transform.position - currentTransform.position;
            Ray ray = new Ray(currentTransform.position, toEnemy);

            float rayMinDistance = Mathf.Infinity;

            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                    }
                }
            }

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            Quaternion rotation;

            if (toEnemy.magnitude > rayMinDistance)
            {
                pointerIcon.OffScreenPointer();

                float angle = Vector3.Angle((enemyPointer.transform.position - currentTransform.position), currentTransform.forward);
                float angle2 = Vector3.Angle((enemyPointer.transform.position - currentTransform.position), currentTransform.right);

                if (angle2 > 90)
                {
                    angle = 360 - angle;
                }

                rotation = Quaternion.Euler(0f, 0f, -angle);

                pointerIcon.SetIconPosition(rotation);
            }
            else
            {
                pointerIcon.WithinScreenPointer();

                rotation = Quaternion.Euler(0f, 0f, 180f);

                pointerIcon.SetIconPosition(position, rotation);
            }
        }
    }

    public void ShowAttackPointer(Transform transformToAttack)
    {
        if (!isInit) Initialize();

        _attackPointerPrefab.Show();
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }
    public void ShowingAttackPointer(Transform transformToAttack)
    {
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }
    public void HideAttackPointerWithTarget(Transform transformToAttack)
    {
        _attackPointerPrefab.Hide();
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }
    public void ObjectWithAttackPointerWasDestroyed()
    {
        _attackPointerPrefab.Hide();
    }
    public void HideAttackPointer()
    {
        _attackPointerPrefab.Hide();
    }

    private Vector3 GetAttackTargetPosition(Transform transform)
    {
        Vector3 toAttackTarget = transform.position - currentTransform.position;
        Ray ray = new Ray(currentTransform.position, toAttackTarget);
        Debug.DrawRay(currentTransform.position, toAttackTarget);

        float distance = toAttackTarget.magnitude;
        Vector3 worldPosition = ray.GetPoint(distance);
        Vector3 position = _camera.WorldToScreenPoint(worldPosition);

        return position;
    }
}
