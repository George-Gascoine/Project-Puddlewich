using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    public Grid2D grid;
    public Tile checkoutTile;
    public float shopEarnings;
    public List<Collectable> itemsForSale = new();
    public List<NPCShopper> shopperList = new();
    public List<NPCShopper> checkoutQueue = new();
    public float checkoutIncX = 0.5f;
    public float checkoutIncY = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject checkout = GameObject.Find("ShopCheckout");
        checkoutTile = grid.GetTileAtPosition(grid.TilePosition(new Vector2(checkout.transform.position.x + 0.5f, checkout.transform.position.y + 0.25f)));
        InvokeRepeating("SelectBuyer", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && checkoutQueue.Count > 0)
        {
            checkoutQueue[0].PayPrice();
            checkoutQueue.RemoveAt(0);
            for(int i = 0; i < checkoutQueue.Count; i++)
            {
                checkoutQueue[i].UpdateQueue();
            }
        }
    }

    void SelectBuyer()
    {
        if (shopperList.Count > 0 && itemsForSale.Count > 0)
        {
            int shopperNo = Random.Range(0, shopperList.Count);
            int itemNo = Random.Range(0, itemsForSale.Count);
            NPCShopper selectedShopper = shopperList[shopperNo];
            Collectable item = itemsForSale[itemNo];
            selectedShopper.browsing = false;
            selectedShopper.CancelInvoke("Browse");
            selectedShopper.buyer = true;
            selectedShopper.buyItem = item;
            shopperList.Remove(selectedShopper);
            itemsForSale.Remove(item);
            Tile start = grid.GetTileAtPosition(grid.TilePosition(new Vector2(selectedShopper.transform.localPosition.x - 0.5f, selectedShopper.transform.localPosition.y)));
            Tile end = item.buyingTile;
            selectedShopper.SelectedShopper(start, end);
        }
    }
}
