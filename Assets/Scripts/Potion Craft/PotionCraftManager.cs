using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCraftManager : MonoBehaviour
{
    public List<Potion> PotionList;
    public List<Ingredient> currentIngredientList;
    public PotionDisplay basePotion;
    public StirringStick stick;
    public int stirNo;
    public float heatTimer;
    public float heatIntensity;

    public void CreatePotion()
    {
        if (currentIngredientList.Count > 0 && stirNo != 0)
        {
            PotionDisplay finishedPotion = Instantiate(basePotion);
            finishedPotion.potion = PotionList[0];
            finishedPotion.transform.position = new Vector2(-6, -3);
            stirNo = 0;
            stick.stirCounter = 0;
            currentIngredientList.Clear();
        }
    }
}
