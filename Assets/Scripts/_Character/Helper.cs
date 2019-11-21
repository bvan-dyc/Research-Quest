using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
	[SerializeField] private float health = 100;
	[SerializeField] private float regen = 5;
	[SerializeField] private float healAmount = 5;
	[SerializeField] private ParticleSystem heartSystem = null;
	[SerializeField] private bool isHealing = false;
	[SerializeField] private HintManager hintManager = null;
	[SerializeField] [TextArea] private string helperHintString = "helper default string";
	[SerializeField] private bool initialActivation = false;
	[SerializeField] private bool isDead = false;
	[SerializeField] private GameObject deadMesh = null;
	[SerializeField] private GameObject helperMesh = null;
	private Patient patient;

	private void Start()
	{
		patient = GameObject.FindGameObjectWithTag("Patient").GetComponent<Patient>();
	}
	private void Update()
	{
		if (isDead)
			return;
		if (!isHealing && health < 100)
		{
			health += regen * Time.deltaTime;
			if (health > 100)
				health = 100;
		}
		if (isHealing)
		{
			health -= Time.deltaTime * healAmount;
			if (health <= 0)
			{
				isDead = true;
				helperMesh.SetActive(false);
				deadMesh.SetActive(true);
				health = 0;
				patient.stopRegen();
				heartSystem.Stop();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Patient")
		{
			isHealing = true;
			patient.startRegen();
			heartSystem.Play();
			if (initialActivation == false)
			{
				initialActivation = true;
				hintManager.displayHint(helperHintString);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Patient")
		{
			isHealing = false;
			patient.stopRegen();
			heartSystem.Stop();
		}
	}
}
