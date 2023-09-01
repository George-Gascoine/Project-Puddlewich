using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        player.inventory.Remove(toolbar.selectedSlot.slotID);
        itemPrice = 0;
    }
    public void PlaceItem(Item.ItemData sale)
    {
        if (tableID.itemOnTable == null)
        {
            var itemX = tableID.transform.position.x;
            var itemY = tableID.transform.position.y - 0.08f;
            var itemZ = 1f;
            var item = Instantiate(new Item(), new Vector3(itemX, itemY, itemZ), Quaternion.identity);
            item.data = sale;
            item.itemCost = itemPrice;
            var choices = new float[] { -1.5f, .5f };
            item.buyingTile = grid.GetTileAtPosition(grid.TilePosition(new Vector2(itemX + choices[Random.Range(1, 1)], itemY)));
            tableID.itemOnTable = item.data;
            shopManager.itemsForSale.Add(item);
        }
    }
}
