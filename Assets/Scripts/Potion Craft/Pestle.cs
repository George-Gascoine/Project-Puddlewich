using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ingredient")
        {
            if(collision.gameObject.GetComponent<IngredientDisplay>().inMortar == true)
            {
                if (collision.gameObject.GetComponent<IngredientDisplay>().crushCount < 2)
                {
                    collision.gameObject.GetComponent<IngredientDisplay>().crushCount++;
                }
                else
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite = collision.gameObject.GetComponent<IngredientDisplay>().crushedISprite;
                }
            }
        }
    }
}
