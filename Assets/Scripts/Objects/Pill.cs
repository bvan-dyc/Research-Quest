using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
	[SerializeField] private float stunPower = 3f;
	[SerializeField] private GameObject impactEffect = null;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			other.GetComponent<SeekerAI>().Stun(stunPower);
			if (impactEffect)
			{
				GameObject impact = Instantiate(impactEffect);
				impact.transform.position = transform.position;
			}
			Destroy(gameObject);
		}
	}
}
