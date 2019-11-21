using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekerAI : MonoBehaviour
{
	[SerializeField] private float patrolSpeed = 3;
	[SerializeField] private float detectedSpeed = 5;
	[SerializeField] private float detectRadius = 10;
	[SerializeField] private float visionAngle = 30;
	[SerializeField] private float detectedDamage = 1;
	[SerializeField] private float heavyDamageRadius = 3;
	[SerializeField] private float heavyDamage = 5;
	[SerializeField] private GameObject target = null;
	private seekerState cState = seekerState.patrol;
	private NavMeshAgent agent;
	private Patient patient;
	private float epsilon = 0.1f;
	private float stunTimer = 0;
	private bool frozen = false;
	private AudioSource AS;
	public enum seekerState
	{
		patrol,
		chasing,
		stunned
	}

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		AS = GetComponent<AudioSource>();
		agent.destination = target.transform.position;
		transform.LookAt(target.transform);
		patient = GameObject.FindGameObjectWithTag("Patient").GetComponent<Patient>();
		agent.speed = patrolSpeed;
	}
	private void FixedUpdate()
	{
		if (frozen)
			return;
		if (cState == seekerState.stunned)
		{
			stunTimer -= Time.deltaTime;
			agent.isStopped = true;
			agent.ResetPath();
			if (stunTimer <= 0)
			{
				cState = seekerState.patrol;
				agent.SetDestination(target.transform.position);
			}
			return;
		}
		float distanceToPlayer = Vector3.Distance(patient.transform.position, transform.position);
		if (cState == seekerState.patrol)
		{
			if (distanceToPlayer <= detectRadius && patient.GetComponent<Patient>().isHidden == false)
			{
				seekPlayer();
			}
			float distance = Vector3.Distance(target.transform.position, transform.position);
			if (distance <= epsilon)
			{
				target = target.GetComponent<Node>().nextNode.gameObject;
				agent.SetDestination(target.transform.position);
				transform.LookAt(target.transform);
			}
			
		}
		if (cState == seekerState.chasing)
		{
			if (patient.GetComponent<Patient>().isHidden == true)
			{
				cState = seekerState.patrol;
				agent.SetDestination(target.transform.position);
				return;
			}
			agent.SetDestination(patient.transform.position);
			transform.LookAt(patient.transform);
			patient.GetComponent<Damageable>().TakeDamage((distanceToPlayer < heavyDamageRadius ? heavyDamage : detectedDamage) * Time.deltaTime);
		}
	}
	private void seekPlayer()
	{
		Vector3 directionToPlayer = patient.transform.position - transform.position;
		float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
		if (angleToPlayer <= visionAngle)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, detectRadius))
			{
				Debug.DrawLine(transform.position, hit.point, Color.red);
				if (hit.transform.gameObject.tag == "Patient")
				{
					Debug.Log("virus found Patient");
					cState = seekerState.chasing;
					agent.speed = detectedSpeed;
				}
			}
		}
	}
	public void Stun(float stunDelay)
	{
		cState = seekerState.stunned;
		stunTimer = stunDelay;
	}

	public void Freeze()
	{
		frozen = true;
		AS.Pause();
	}

	public void UnFreeze()
	{
		frozen = false;
		AS.Play();
	}
}
