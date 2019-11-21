using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
	public IngredientType type;
	public enum IngredientType
	{
		shroom,
		plant,
		molecule,
		unknown
	}
}
