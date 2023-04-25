using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsor : MonoBehaviour
{
	[SerializeField] private float reflectForce = 10f;

	void OnCollisionEnter(Collision collision)
	{
		ContactPoint[] tmpContact = collision.contacts;
		ContactPoint contact = tmpContact[0];

		Vector3 contactNormal = contact.normal;

		Rigidbody _rb = collision.gameObject.GetComponent<Rigidbody>();

		if(_rb != null) _rb.AddForce(reflectForce * -contactNormal.normalized, ForceMode.VelocityChange);
	}
}
