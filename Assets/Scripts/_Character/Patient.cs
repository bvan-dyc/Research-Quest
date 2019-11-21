using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patient : Character
{
	[SerializeField] private float regen = 5;
	[SerializeField] private float degen = 1;
	public bool isHidden = false;
	public int nPills = 5;
	public float shootDelay = 0.3f;
	[SerializeField] private Text pillNumText = null;
	public GameObject heldPill;
	private Damageable damageable;
	private bool isHealing = false;
	[SerializeField] private float throwSpeed = 10;
	private float cooldownTimer = 0f;

	private void Start()
	{
		damageable = GetComponent<Damageable>();
	}
	private void FixedUpdate()
	{
		if (cooldownTimer > 0)
			cooldownTimer -= Time.deltaTime;
	}
	public override void Action()
	{
		base.Action();
		if (nPills > 0)
		{
			throwPill();
		}
	}
	void Update()
    {
		if (frozen)
			return;
		pillNumText.text = "x " + "0" + nPills;
		if (isHealing)
		{
			damageable.GainHealth(regen * Time.deltaTime);
		}
		else
		{
			damageable.TakeDamage(degen * Time.deltaTime);
		}
    }

	public void startRegen()
	{
		isHealing = true;
	}

	public void stopRegen()
	{
		isHealing = false;
	}
	public void throwPill()
	{
		if (!heldPill || cooldownTimer > 0)
			return;
		cooldownTimer = shootDelay;
		GameObject newPill = Instantiate(heldPill);
		newPill.transform.position = transform.position;
		Vector3 direction = transform.GetComponentInChildren<Camera>().transform.forward;
		direction.y += 0.3f;
		newPill.GetComponent<Rigidbody>().velocity = direction * throwSpeed;
		nPills--;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Haven")
		{
			isHidden = true;
		}
	}
	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Haven")
		{
			isHidden = false;
		}
	}

}
