﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestructor : MonoBehaviour
{
	public float destroyAfterTime = 1;
	private float timer = 0;
	private void LateUpdate()
	{
		timer += Time.deltaTime;
		if (timer > destroyAfterTime)
		{
			Destroy(gameObject);
		}
	}
}
