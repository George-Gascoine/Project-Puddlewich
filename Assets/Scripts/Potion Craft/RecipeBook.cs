using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    public PotionCraftManager manager;
    public List<Recipe> recipes = new();
    public int pageNo = 0;
    public TextMeshProUGUI recipeText;
    public TextMeshProUGUI potionText;
    public Image potionImage;

    private void Start()
    {
        FillRecipeBook();
        Potion potion = manager.potionList.potion.Find(x => x.name == recipes[pageNo].name);
        potionText.text = potion.name;
        potionImage.sprite = Resources.Load<Sprite>("Sprites/Potion Craft/" + potion.sprite);
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
