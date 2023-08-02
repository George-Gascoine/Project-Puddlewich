using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDisplay : MonoBehaviour
{
    public Ingredient ingredient;

    public PotionCraftManager manager;
    public string iName;
    public string iType;
    public string iDescription;
    public Sprite iSprite;
    public Sprite crushedISprite;
    public float iCost;
    public float iQuality;
    public Vector2 origPos;
    public bool overMortar = false;
    public bool inMortar = false;
    public bool overCauldron = false;
    public bool inCauldron = false;
    public int crushCount;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PotionCraftManager>();
        origPos = transform.position;
        iName = ingredient.ingredientName;
        iType = ingredient.ingredientType;
        iDescription = ingredient.ingredientDescription;
        iSprite = ingredient.ingredientSprite;
        crushedISprite = ingredient.crushedIngredientSprite;
        iCost = ingredient.cost;
        iQuality = ingredient.quality;
        crushCount = 0;

        GetComponent<SpriteRenderer>().sprite = iSprite;
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Mortar")
        {
            overMortar = true;
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Cauldron")
        {
            overCauldron = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.collider);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Mortar")
        {
            overMortar = false;
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Cauldron")
        {
            overCauldron = false;
        }
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }

    void OnMouseUp()
    {
        if (overMortar == true)
        {
            transform.position = GameObject.Find("Mortar").transform.position;
            inMortar = true;
        }
        else if (overCauldron == true)
        {
            manager.currentIngredientList.Add(this.ingredient);
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = origPos;
        }
    }
}
