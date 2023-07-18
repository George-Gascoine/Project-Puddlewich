using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;

//[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{
    Player player;
    public GameManager gameManager;
    Grid2D grid;
    ShopManager shopManager;
    ShopperSpawner shopperSpawner;
    public Collectable itemOnTable;
    public Tile browseTile1;
    public Tile browseTile2;
    public Tile browseTile3;
    public Tile browseTile4;
    public GameObject pricePanel;
    public SetItemPrice setItemPrice;
    void Start()
    {
        grid = FindObjectOfType<Grid2D>();
        Tile tableTile = grid.GetTileAtPosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.25f)); // Returns the current tile
        Debug.Log(transform.localPosition.x - 0.5f);
        tableTile.isWalkable = false;
        shopManager = FindObjectOfType<ShopManager>();
        shopperSpawner = FindObjectOfType<ShopperSpawner>();
        FindTiles();
    }

    void Update()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnMouseDown()
    {
        pricePanel.SetActive(true);
        setItemPrice.itemForSale = player.equippedItem;
        setItemPrice.tableID = this;
    }

    public void FindTiles()
    {
        // xChange is 0.5 and yChange is 0.25
        browseTile1 = grid.GetTileAtPosition(new Vector2(transform.localPosition.x - 1f, transform.localPosition.y - 0.5f));
        browseTile2 = grid.GetTileAtPosition(new Vector2(transform.localPosition.x - 1f, transform.localPosition.y));
        browseTile3 = grid.GetTileAtPosition(new Vector2(transform.localPosition.x, transform.localPosition.y - 0.5f));
        browseTile4 = grid.GetTileAtPosition(new Vector2(transform.localPosition.x, transform.localPosition.y));
        browseTile1.highlight.SetActive(true);
        browseTile2.highlight.SetActive(true);
        browseTile3.highlight.SetActive(true);
        browseTile4.highlight.SetActive(true);

        shopperSpawner.browsePoints.Add(browseTile1);
        shopperSpawner.browsePoints.Add(browseTile2);
        shopperSpawner.browsePoints.Add(browseTile3);
        shopperSpawner.browsePoints.Add(browseTile4);
    }
}