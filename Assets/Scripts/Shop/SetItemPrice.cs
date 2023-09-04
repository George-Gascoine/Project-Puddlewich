using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemManager;
using static UnityEngine.Rendering.DebugUI;

public class SetItemPrice : MonoBehaviour
{
    public GameObject pricePanel;
    public int itemPrice;
    public TextMeshProUGUI uiPrice;
    public Item.ItemData itemForSale;
    public Player player;
    public ToolbarUI toolbar;
    public SnapToGrid tableID;
    public ShopManager shopManager;
    Grid2D grid;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        itemPrice = 0;
    }
    void Update()
    {
        grid = FindAnyObjectByType<Grid2D>();
        uiPrice.text = itemPrice.ToString();
    }

    public void IncreasePrice()
    {
        itemPrice++;
    }

    public void DecreasePrice()
    {
        if (itemPrice > 0)
        {
            itemPrice--;
        }
    }

    public void ConfirmPrice()
    {
        pricePanel.SetActive(false);
        PlaceItem(player.equippedItem);
        itemPrice = 0;
    }
    public void PlaceItem(Item.ItemData sale)
    {
        if (tableID.itemOnTable == null)
        {
            var itemX = tableID.transform.position.x;
            var itemY = tableID.transform.position.y - 0.08f;
            var itemZ = 1f;
            var item = Instantiate(GameManager.instance.baseItem, new Vector3(itemX, itemY, itemZ), Quaternion.identity);
            item.GetComponent<Item>().data = sale;
            item.GetComponent<Item>().itemCost = itemPrice;
            var choices = new float[] { -1.5f, .5f };
            item.GetComponent<Item>().buyingTile = grid.GetTileAtPosition(grid.TilePosition(new Vector2(itemX + choices[Random.Range(1, 1)], itemY)));
            tableID.itemOnTable = item.GetComponent<Item>();
            shopManager.itemsForSale.Add(item.GetComponent<Item>());
        }
    }
}
