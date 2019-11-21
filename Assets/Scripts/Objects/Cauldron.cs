using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
public class Cauldron : MonoBehaviour
{
	[SerializeField] private int ingredientCount = 3;
	[SerializeField] private int recipeComplexity = 3;
	public List<Ingredient.IngredientType> recipe;
	[SerializeField] private List<Ingredient.IngredientType> submittedRecipe = null;
	[SerializeField] private GameObject failureParticle = null;
	[SerializeField] private GameObject successParticle = null;
	private AudioSource cauldronAS = null;
	[System.Serializable]
	public class RecipeEvent : UnityEvent<Cauldron>
	{ }
	[SerializeField] private RecipeEvent OnRecipeChange = null;
	public int completedPills = 0;
	public int nClues = 0;

	private void Start()
	{
		generateRecipe();
	}
	public void addIngredient(Ingredient.IngredientType ingredient)
	{
		submittedRecipe.Add(ingredient);
		if (submittedRecipe.Count == recipe.Count)
		{
			submitRecipe();
		}
	}

	private void generateRecipe()
	{
		for (int i = 0; i < recipeComplexity; i++)
		{
			recipe.Add((Ingredient.IngredientType)Random.Range(0, ingredientCount));
		}
		OnRecipeChange.Invoke(this);
	}

	private void submitRecipe()
	{
		if (ListExtensions.CompareLists<Ingredient.IngredientType>(submittedRecipe, recipe))
		{
			completedPills++;
			submittedRecipe.Clear();
			recipe.Clear();
			GameObject successEffect = Instantiate(successParticle);
			cauldronAS.Play();
			successEffect.transform.position = transform.position;
			generateRecipe();
		}
		else
		{
			submittedRecipe.Clear();
			GameObject failureEffect = Instantiate(failureParticle);
			failureEffect.transform.position = transform.position;
		}
	}

	public void refreshCauldron()
	{
		OnRecipeChange.Invoke(this);
	}
}

