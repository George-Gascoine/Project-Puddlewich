using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Player player;
    public ItemType type;
    public Sprite icon;
    public int itemCost;
    public Tile buyingTile;
    ShopManager shopManager;
    public void Awake()
    {
        player = FindAnyObjectByType<Player>();
        icon = GetComponent<SpriteRenderer>().sprite;
        shopManager = FindObjectOfType<ShopManager>();
    }

    private void Update()
    {

    }

    void Start()
    {

    }
    void OnMouseDown()
    {
        player.inventory.Add(this);
        player.slotChanged = true;
        Destroy(this.gameObject);
    }

    public enum ItemType
    {
        NONE, 
        ITEM,
        FOOD,
        DRINK,
        HOE,
        WATERINGCAN,
        SEED
    }
}

