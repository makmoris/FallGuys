using UnityEngine;

public class RaceSide : MonoBehaviour
{
    private RaceSidesController _raceSidesController;

    private void Awake()
    {
        _raceSidesController = transform.parent.GetComponent<RaceSidesController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //_raceSidesController.SideHitted(gameObject.name, other);
    }

    private void OnTriggerStay(Collider other)
    {
        _raceSidesController.StayInside(gameObject.name, other);
    }

    private void OnTriggerExit(Collider other)
    {
        _raceSidesController.Outside(other);
    }
}
