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
    public ShopManager shopManager;
    public FarmManager farmManager;
    public Crop crop;
    public void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>();
        farmManager = FindObjectOfType<FarmManager>();
    }

    private void Update()
    {

    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        icon = GetComponent<SpriteRenderer>().sprite;
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
        SEED,
        CHILLISEED,
        TOMATOSEED,
        CUCUMBERSEED,
        ONIONSEED
    }
}

