using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Researcher : Character
{
	public GameObject heldIngredient;
	[SerializeField] private float interactionRadius = 3f;
	[System.Serializable]
	public class PickUpEvent : UnityEvent<Researcher>
	{ }
	[SerializeField] private PickUpEvent OnPickUp = null;

	public override void Action()
	{
		base.Action();
		Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius);
		Debug.Log(heldIngredient);
		foreach (Collider col in colliders)
		{
			if (col.tag == "Cauldron" && heldIngredient)
			{
				col.GetComponent<Cauldron>().addIngredient(heldIngredient.GetComponent<Ingredient>().type);
				heldIngredient = null;
				OnPickUp.Invoke(this);
				return;
			}
			else if (col.tag == "Ingredient")
			{
				if (heldIngredient)
				{
					heldIngredient.SetActive(true);
					heldIngredient.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				}
				heldIngredient = col.gameObject;
				col.gameObject.SetActive(false);
				OnPickUp.Invoke(this);
				return;
			}
		}
	}
}
