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
    public ItemManager.ItemList itemList;
    public TextAsset recipeData;
    public Ingredient baseIngredient;
    public StirringStick stick;
    public int stirNo;
    public float heatTimer;
    public float heatIntensity;

    public GameObject ingredientPanel;

    private void Start()
    {
        ReadJSON();
        itemList = GameManager.instance.itemManager.itemList;
        PopulateIngredients();
    }

    [System.Serializable]
    public class RecipeList
    {
        public List<Item.Recipe> recipe;
    }

    public RecipeList recipeList;
    void ReadJSON()
    {
        recipeList = JsonUtility.FromJson<RecipeList>(recipeData.text);
        foreach (Item.Recipe recipe in recipeList.recipe)
        {
            recipe.completedSteps = new List<bool>(new bool[recipe.steps.Count]);
        }
    }
    public void PopulateIngredients()
    {
        for (int i = 0; i < itemList.item.Count; i ++)
        {
            if (itemList.item[i].type == "Ingredient")
            {
                Ingredient ingredient = Instantiate(baseIngredient);
                ingredient.ingredient = itemList.item[i];
                ingredient.gameObject.transform.position = new Vector3(5 + (i * 1.5f), 2.5f, 1);
                ingredient.transform.localScale = new Vector3(10, 10, 1);
            }
        }
    }
    public void CreatePotion()
    {
            //Potion finishedPotion = Instantiate(basePotion);
            //finishedPotion.potion = potionList.potion[0];
            //finishedPotion.transform.position = new Vector2(-6, -3);
            //finishedPotion.transform.localScale = new Vector3(10, 10, 1);
            //stirNo = 0;
            //stick.stirCounter = 0;
    }
}
