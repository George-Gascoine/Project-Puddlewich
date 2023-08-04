using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.IO;

public class PotionCraftManager : MonoBehaviour
{
    public TextAsset ingredientData;
    public TextAsset potionData;
    public TextAsset recipeData;
    public IngredientDisplay baseIngredient;
    public PotionDisplay basePotion;
    public StirringStick stick;
    public int stirNo;
    public float heatTimer;
    public float heatIntensity;

    public GameObject ingredientPanel;

    private void Start()
    {
        ReadJSON();
        PopulateIngredients();
    }
  
    [System.Serializable]
    public class IngredientList
    {
        public List<Ingredient> ingredient;
    }

    [System.Serializable]
    public class PotionList
    {
        public List<Potion> potion;
    }

    [System.Serializable]
    public class RecipeList
    {
        public List<Recipe> recipe;
    }
    public IngredientList ingredientList;
    public PotionList potionList;
    public RecipeList recipeList;
    void ReadJSON()
    {
        ingredientList = JsonUtility.FromJson<IngredientList>(ingredientData.text);
        potionList = JsonUtility.FromJson<PotionList>(potionData.text);
        recipeList = JsonUtility.FromJson<RecipeList>(recipeData.text);
        foreach (Recipe recipe in recipeList.recipe)
        {
            recipe.completedSteps = new List<bool>(new bool[recipe.steps.Count]);
        }
    }
    public void PopulateIngredients()
    {
        for (int i = 0; i < ingredientList.ingredient.Count; i ++)
        {
            IngredientDisplay ingredient = Instantiate(baseIngredient);
            ingredient.ingredient = ingredientList.ingredient[i];
            ingredient.gameObject.transform.position = new Vector3(5+(i*1.5f), 2.5f, 1);
            ingredient.transform.localScale = new Vector3(10, 10, 1);
        }
    }
    public void CreatePotion()
    {
            PotionDisplay finishedPotion = Instantiate(basePotion);
            finishedPotion.potion = potionList.potion[0];
            finishedPotion.transform.position = new Vector2(-6, -3);
            finishedPotion.transform.localScale = new Vector3(10, 10, 1);
            stirNo = 0;
            stick.stirCounter = 0;
    }
}
