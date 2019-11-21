using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	protected bool frozen = false;
	public virtual void Action()
	{

	}

	public void Freeze()
	{
		frozen = true;
	}
	public void UnFreeze()
	{
		frozen = false;
	}
}