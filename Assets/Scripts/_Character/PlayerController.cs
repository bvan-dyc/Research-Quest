using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 8f;
	private Rigidbody rbody;
	private Character character;
	private AudioSource footstepsAS;
	public float mouseSensitivity = 1f;
	void Start()
	{
		rbody = GetComponent<Rigidbody>();
		footstepsAS = GetComponent<AudioSource>();
		character = GetComponent<Character>();
	}

	void Update()
	{
		if (rbody.velocity.x > 0.1 || rbody.velocity.z > 0.1)
		{
			footstepsAS.UnPause();
		}
		else
		{
			footstepsAS.Pause();
		}
	}

	public void Move(float vertical, float horizontal)
	{
		Vector3 newVelocity = transform.forward * vertical * speed + transform.right * horizontal * speed;
		newVelocity.y = rbody.velocity.y;
		rbody.velocity = newVelocity;
	}

	public void CharacterAction()
	{
		character.Action();
	}
}
