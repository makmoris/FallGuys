using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{

    // Pool
    private int poolCount = 3;
    private bool autoExpand = true;

    private PoolMono<PointerIcon> _positionPointersPool;
    //
    
    private Dictionary<EnemyPointer, PointerIcon> _positionDictionary = new Dictionary<EnemyPointer, PointerIcon>();
    [SerializeField] private Transform _playerTransform;// сделать передачу из Installer-a
    [SerializeField] Camera _camera;
    [SerializeField] Transform _canvasTransform;

    //[SerializeField] AttackPointer _attackPointer;
    [SerializeField] PlayerHealthUI _playerHealthUI;
    [Header("Prefabs")]
    [SerializeField] PointerIcon _positionPointerPrefab;
    PointerIcon _pointerIcon;
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


        _attackPointerPrefab = Instantiate(_attackPointerPrefab, _canvasTransform);

        _positionPointersPool = new PoolMono<PointerIcon>(_positionPointerPrefab, poolCount, _canvasTransform);
        _positionPointersPool.autoExpand = autoExpand;
    }


    public void SetPlayerTransform(Transform transform)
    {
        _playerTransform = transform;
    }

    public void AddToPositionList(EnemyPointer enemyPointer)
    {
        //PointerIcon newPointer = Instantiate(_positionPointerPrefab, _canvasTransform);
        PointerIcon newPointer = _positionPointersPool.GetFreeElement();
        _positionDictionary.Add(enemyPointer, newPointer);
        newPointer.Show();
    }

    public void RemoveFromPositionList(EnemyPointer enemyPointer)
    {
        //Destroy(_dictionary[enemyPointer].gameObject);

        PointerIcon pointerIcon = _positionDictionary[enemyPointer];
        pointerIcon.Hide();
        pointerIcon.gameObject.SetActive(false);

        _positionDictionary.Remove(enemyPointer);
    }

    public void ShowPositionPointer(EnemyPointer enemyPointer)
    {
        PointerIcon newPointer = _positionDictionary[enemyPointer];
        newPointer.Show();
    }

    public void HidePositionPointer(EnemyPointer enemyPointer)
    {
        PointerIcon newPointer = _positionDictionary[enemyPointer];
        newPointer.Hide();
    }

    public void UpdateHealthInUI(EnemyPointer enemyPointer, float healthValue)
    {
        PointerIcon pointerIcon = _positionDictionary[enemyPointer];
        pointerIcon.UpdateHealthColor(healthValue);
    }

    public void UpdatePlayerHealthInUI(float healthValue)
    {
        _playerHealthUI.UpdatePlayerHealthUI(healthValue);
    }

    void LateUpdate()
    {
        // Left, Right, Down, Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var kvp in _positionDictionary)
        {
            EnemyPointer enemyPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 toEnemy = enemyPointer.transform.position - _playerTransform.position;
            Ray ray = new Ray(_playerTransform.position, toEnemy);
            Debug.DrawRay(_playerTransform.position, toEnemy);


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

                float angle = Vector3.Angle((enemyPointer.transform.position - _playerTransform.position), _playerTransform.forward);
                float angle2 = Vector3.Angle((enemyPointer.transform.position - _playerTransform.position), _playerTransform.right);

                if (angle2 > 90)
                {
                    angle = 360 - angle;
                }

                rotation = Quaternion.Euler(0f, 0f, -angle);

                pointerIcon.SetIconPosition(rotation);
            }
            else
            {
                pointerIcon.Show();
                pointerIcon.WithinScreenPointer();

                rotation = Quaternion.Euler(0f, 0f, 180f);

                pointerIcon.SetIconPosition(position, rotation);
            }
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
    public void ObjectWithAttackPointerWasDestroyed()
    {
        _attackPointerPrefab.Hide();
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

    private void GameOwer(GameObject gameObject)
    {
        if(gameObject.transform == _playerTransform) this.enabled = false;
    }

    private void OnEnable()
    {
        VisualIntermediary.PlayerWasDeadEvent += GameOwer;
    }

    private void OnDisable()
    {
        VisualIntermediary.PlayerWasDeadEvent -= GameOwer;
    }
}
