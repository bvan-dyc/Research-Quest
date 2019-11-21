using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldItemHUD : MonoBehaviour
{
	private Researcher researcher;
	[SerializeField] private Image heldImage = null;
	[SerializeField] private Sprite[] sprites = null;
	private void Start()
	{
		researcher = GameObject.FindGameObjectWithTag("Researcher").GetComponent<Researcher>();
	}

	public void RefreshIngredient()
	{
		if (!researcher.heldIngredient)
		{
			heldImage.sprite = sprites[0];
			return;
		}
		switch (researcher.heldIngredient.GetComponent<Ingredient>().type)
		{
			case Ingredient.IngredientType.shroom:
				{
					heldImage.sprite = sprites[1];
					break;
				}
			case Ingredient.IngredientType.plant:
				{
					heldImage.sprite = sprites[2];
					break;
				}
			case Ingredient.IngredientType.molecule:
				{
					heldImage.sprite = sprites[3];
					break;
				}
		}
	}
}
