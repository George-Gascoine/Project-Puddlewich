using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class Potion : MonoBehaviour
{
    public Item.Potion potion;

    public string pName;
    public string pEffect;
    public string pDescription;
    public Sprite pSprite;
    public float cost;
    public float quality;
    public Vector2 origPos;
    void Start()
    {
        origPos = transform.position;
        pName = potion.name;
        pEffect = potion.effect;
        pDescription = potion.description;
        pSprite = Resources.Load<Sprite>("Sprites/Potion Craft/" + potion.sprite);
        cost = potion.cost;
        quality = potion.quality;

        GetComponent<SpriteRenderer>().sprite = pSprite;
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private Vector3 screenPoint;
    private Vector3 offset;

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
}
