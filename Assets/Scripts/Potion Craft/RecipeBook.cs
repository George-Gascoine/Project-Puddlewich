using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemManager;

public class RecipeBook : MonoBehaviour
{
    public PotionCraftManager manager;
    public List<Item.Recipe> recipes = new();
    public int pageNo = 0;
    public TextMeshProUGUI recipeText;
    public TextMeshProUGUI potionText;
    public Image potionImage;

    private void Start()
    {
        FillRecipeBook();
        for (int i = 0; i < manager.itemList.item.Count; i++)
        {
            if (manager.itemList.item[i].type == "Potion")
            {
                Item.ItemData potion = manager.itemList.item.Find(x => x.name == recipes[pageNo].name);
                potionText.text = potion.name;
                potionImage.sprite = Resources.Load<Sprite>("Sprites/Potion Craft/" + potion.sprite);
            }
        }

        for (int i = 0; i < recipes[pageNo].steps.Count; i++)
        {
            recipeText.text += "Step " + (i + 1).ToString() + ": " + recipes[pageNo].steps[i] + "\n";
        }
    }

    void FillRecipeBook()
    {
        recipes = manager.recipeList.recipe;
    }
}
