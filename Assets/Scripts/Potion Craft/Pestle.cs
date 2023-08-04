using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            if(collision.gameObject.GetComponent<Ingredient>().inMortar == true)
            {
                if (collision.gameObject.GetComponent<Ingredient>().crushCount < 2)
                {
                    collision.gameObject.GetComponent<Ingredient>().crushCount++;
                }
                else
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite = collision.gameObject.GetComponent<Ingredient>().crushedISprite;
                }
            }
        }
    }
}
