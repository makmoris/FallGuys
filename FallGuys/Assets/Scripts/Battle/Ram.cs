using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    public float velocity;
    public Vector3 vec;
    //public Vector3 acceleration;
    //public Vector3 distancemoved = Vector3.zero;
    //public Vector3 lastdistancemoved = Vector3.zero;
    //public Vector3 last;

    //private void Start()
    //{
    //    last = transform.position;
    //}

    //void Update()
    //{
    //    distancemoved = (transform.position - last) * Time.deltaTime;
    //    acceleration = distancemoved - lastdistancemoved;
    //    lastdistancemoved = distancemoved;
    //    last = transform.position;
    //}

    private void Update()
    {
        velocity = GetComponent<Rigidbody>().velocity.z;
        vec = transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Sides"))
            //Debug.Log($"{gameObject.name} ударил {other.transform.parent.name} в {other.name}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"{gameObject.name} ударил {collision.transform.parent.name} в {collision.transform.name}");
        //Debug.Log($"{gameObject.name} ударил {collision.gameObject.name}");
        //Vector3 toEnemy = collision.transform.position - transform.position;
        //Ray hit = new Ray(transform.position, toEnemy);

        //Vector3 localPoint = hit.direction;
        //if (Mathf.Abs(localPoint.x) == 0.5f) Debug.Log("Right/left");
        //else if (Mathf.Abs(localPoint.y) == 0.5f) Debug.Log("Top/bottom");
        //else if (Mathf.Abs(localPoint.z) == 0.5f) Debug.Log("Front/rear");

        //Vector3 localPoint = hit.direction;
        //Vector3 localDir = localPoint.normalized;

        //float upDot = Vector3.Dot(localDir, Vector3.up);
        //fwdDot = Vector3.Dot(localDir, Vector3.forward);
        //rightDot = Vector3.Dot(localDir, Vector3.right);

        //if (fwdDot > 0) Debug.Log("Front");
        //if (fwdDot < 0) Debug.Log("Back");
        //if (rightDot > 0) Debug.Log("Right");
        //if (rightDot < 0) Debug.Log("Left");

        //var angle = Vector3.Angle(transform.forward, collision.transform.forward);
        //Debug.Log($"{gameObject.name} direction = {transform.forward}; {collision.gameObject.name} direction = {collision.transform.forward} " +
        //    $"Dot = {Vector3.Dot(transform.forward, collision.transform.forward)}");
        //Debug.Log(collision.gameObject.name);
        //Debug.Log($"{gameObject.name} velocity = {_rb.velocity.z}; {collision.gameObject.name} velocity = {collision.transform.GetComponent<Rigidbody>().velocity.z}");

        //Debug.Log($"Obj = {gameObject.name}; Last vel = {lastVelocity}; New vel = {_rb.velocity.magnitude}");
        //var linVelMY = collision.relativeVelocity.magnitude;
        //Debug.Log($"Force = {_rb.mass * _rb.velocity.z}");
        //Debug.Log("Ram collision with " + collision.gameObject.name + "; velosity = " + collision.relativeVelocity.magnitude +
        //    "; impuls = " + (collision.impulse.magnitude / Time.fixedDeltaTime));
        //Debug.Log($"Force = {collision.relativeVelocity.magnitude * _rb.mass}");
        //Debug.Log(GetComponent<Rigidbody>().GetPointVelocity(transform.position) + " " + gameObject.name + " - point velocity");

        //var forceGO = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        //var forceCollision = collision.gameObject.GetComponent<Rigidbody>().velocity;
        //Debug.Log($"{gameObject.name} force = {forceGO}" +
        //    $" {collision.gameObject.name} force = {forceCollision}");


        //var valGO = collision.impulse / Time.fixedDeltaTime;
        //Debug.Log(valGO);
    }
}
