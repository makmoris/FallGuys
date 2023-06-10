using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePointerManager : MonoBehaviour
{
    private Transform _playerTransform;// сделать передачу из Installer-a
    [SerializeField] Camera _camera;
    [SerializeField] Canvas _canvas;

    [Header("Prefabs")]
    [SerializeField] PointerIcon _attackPointerPrefab;


    public static RacePointerManager Instance;
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


        _attackPointerPrefab = Instantiate(_attackPointerPrefab, _canvas.gameObject.transform);
    }


    public void SetPlayerTransform(Transform transform)
    {
        _playerTransform = transform;
    }

    public void StartShowingAttackPointer(Transform transformToAttack)
    {
        _attackPointerPrefab.Show();
        _attackPointerPrefab.SetIconPosition(GetAttackTargetPosition(transformToAttack), Quaternion.identity);
    }
    public void ShowAttackPointer(Transform transformToAttack)
    {
        Debug.Log("ASADSDASD");
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
        if (gameObject.transform == _playerTransform) this.enabled = false;
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
