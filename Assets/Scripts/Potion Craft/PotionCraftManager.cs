using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PotionCraftManager : MonoBehaviour
{
    public TextAsset ingredientData;
    public TextAsset potionData;
    public TextAsset recipeData;
    public List<Potion> PotionList;
    public List<Ingredient> ingredientList;
    public List<Recipe> RecipeList;
    public IngredientDisplay baseIngredient;
    public PotionDisplay basePotion;
    public StirringStick stick;
    public int stirNo;
    public float heatTimer;
    public float heatIntensity;

    public GameObject ingredientPanel;

    private void Start()
    {
        ReadCSV();
        PopulateIngredients();
    }
    void ReadCSV()
    {
        string[] iData = ingredientData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int iDataSize = iData.Length / 7 - 1;
        for (int i = 0; i < iDataSize; i++)
        {

            Ingredient newIngredient = ScriptableObject.CreateInstance<Ingredient>();
            newIngredient.ingredientName = iData[7 * (i + 1)];
            newIngredient.ingredientType = iData[7 * (i + 1) + 1];
            newIngredient.ingredientDescription = iData[7 * (i + 1) + 2];
            newIngredient.ingredientSprite = Resources.Load<Sprite>("Sprites/Potion Craft/" + iData[7 * (i + 1) + 3]);
            newIngredient.crushedIngredientSprite = Resources.Load<Sprite>("Sprites/Potion Craft/" + iData[7 * (i + 1) + 4]);
            newIngredient.cost = int.Parse(iData[7 * (i + 1) + 5]);
            newIngredient.quality = int.Parse(iData[7 * (i + 1) + 6]);
            ingredientList.Add(newIngredient);
        }


        string[] pData = potionData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        
        int pDataSize = pData.Length / 9 - 1;
        for (int i = 0; i < pDataSize; i++)
        {
            Potion newPotion = ScriptableObject.CreateInstance<Potion>();
            newPotion.potionName = pData[9 * (i + 1)];
            newPotion.potionEffect = pData[9 * (i + 1) + 1];
            newPotion.potionDescription = pData[9 * (i + 1) + 2];
            newPotion.ingredientOne = ingredientList.Find(x => x.ingredientName == pData[9 * (i + 1) + 3]);
            newPotion.ingredientTwo = ingredientList.Find(x => x.ingredientName == pData[9 * (i + 1) + 4]);
            newPotion.ingredientThree = ingredientList.Find(x => x.ingredientName == pData[9 * (i + 1) + 5]);
            newPotion.potionSprite = Resources.Load<Sprite>("Sprites/Potion Craft/" + pData[9 * (i + 1) + 6]);
            newPotion.cost = int.Parse(pData[9 * (i + 1) + 7]);
            newPotion.quality = int.Parse(pData[9 * (i + 1) + 8]);
            PotionList.Add(newPotion);
        }

        string[] rData = recipeData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int rDataSize = rData.Length / 6 - 1;
        for (int i = 0; i < rDataSize; i++)
        {
            Recipe recipe = new()
            {
                recipeName = rData[6 * (i + 1)]
            };
            for (int j = 1; j < 6; j++)
            {
                if (rData[6 * (i + 1) + j] == "Null")
                {
                    break;
                }
                recipe.steps.Add(rData[6 * (i + 1) + j]);
            }
            recipe.completedSteps = new List<bool>(new bool[recipe.steps.Count]);
            RecipeList.Add(recipe);
            Debug.Log("ADDED");
        }
    }

    public void PopulateIngredients()
    {
        IngredientDisplay ingredient = Instantiate(baseIngredient);
        ingredient.ingredient = ingredientList[0];
        ingredient.gameObject.transform.position = new Vector3(6,2.5f,1);
        ingredient.transform.localScale = new Vector3(10,10,1);
    }
    public void CreatePotion()
    {
        PotionDisplay finishedPotion = Instantiate(basePotion);
        finishedPotion.potion = PotionList[0];
        finishedPotion.transform.position = new Vector2(-6, -3);
        finishedPotion.transform.localScale = new Vector3(10, 10, 1);
        stirNo = 0;
        stick.stirCounter = 0;
    }
}
