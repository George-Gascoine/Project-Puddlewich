using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "Ingredient", fileName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public string ingredientType;
    public string ingredientDescription;
    public Sprite ingredientSprite;
    public Sprite crushedIngredientSprite;
    public float cost;
    public float quality;


}
