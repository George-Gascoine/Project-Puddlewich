using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirringStick : MonoBehaviour
{
    public PotionCraftManager manager;
    public float stirCounter;
    Vector3 mousePos;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cauldron")
        {
            mousePos = Input.mousePosition;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cauldron")
        {
            if (mousePos != Input.mousePosition)
            {
                Debug.Log("Mouse moved");
                mousePos = Input.mousePosition;
                stirCounter++;
                if (stirCounter > 50)
                {
                    manager.stirNo++;
                    stirCounter = 0;
                }
            }
        }
    }
}
