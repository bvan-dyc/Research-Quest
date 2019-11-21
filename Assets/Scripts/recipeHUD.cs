using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class recipeHUD : MonoBehaviour
{
	[SerializeField] private Image[] imageSlots = null;
	[SerializeField] private Sprite[] sprites = null;
	private Cauldron cauldron;
	private void Awake()
	{
		cauldron = GameObject.FindGameObjectWithTag("Cauldron").GetComponent<Cauldron>();
	}

	public void refreshRecipe()
	{
		for (int i = 0; i < cauldron.recipe.Count; i++)
		{
			if (i < cauldron.nClues)
			{
				switch (cauldron.recipe[i])
				{
					case Ingredient.IngredientType.shroom:
						{
							imageSlots[i].sprite = sprites[1];
							break;
						}
					case Ingredient.IngredientType.plant:
						{
							imageSlots[i].sprite = sprites[2];
							break;
						}
					case Ingredient.IngredientType.molecule:
						{
							imageSlots[i].sprite = sprites[3];
							break;
						}
				}
			}
			else
				imageSlots[i].sprite = sprites[0];
		}
	}
}
