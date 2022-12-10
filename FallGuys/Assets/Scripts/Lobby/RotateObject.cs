using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObject : MonoBehaviour
{
    //mobile
    private Touch touch;
    private Quaternion rotationY;
    [SerializeField] private float rotateSpeedModifier = 0.05f;

    //PC
    [SerializeField] private float pc_rotateSpeedModifier = 15f;

    private Transform _transform;

    private bool isMobile;
    private bool canRotate;

    private void Awake()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (isMobile)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !canRotate && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    canRotate = true;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (canRotate)
                    {
                        rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedModifier, 0f);

                        _transform.rotation = rotationY * _transform.rotation;
                    }
                }

                if (touch.phase == TouchPhase.Ended && canRotate)
                {
                    canRotate = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !canRotate && !EventSystem.current.IsPointerOverGameObject())
            {
                canRotate = true;
            }

            if (Input.GetMouseButton(0))
            {
                if (canRotate)
                {
                    float rotX = Input.GetAxis("Mouse X") * pc_rotateSpeedModifier;

                    _transform.rotation = Quaternion.AngleAxis(-rotX, new Vector3(0f, 1f, 0f)) * _transform.rotation;
                }
            }

            if (Input.GetMouseButtonUp(0) && canRotate)
            {
                canRotate = false;
            }
        }
    }
}
