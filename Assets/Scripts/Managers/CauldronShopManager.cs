using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CauldronShopManager : MonoBehaviour
{
    public NPCTrade npc;
    public NPCTrade instance;
    public bool enterShop = false;
    public bool leaveShop = false;
    Grid2D grid;
    private void Start()
    {
        //grid = FindObjectOfType<Grid2D>();
        //Tile checkoutTile = 
        //    grid.GetTileAtPosition(grid.TilePosition(new Vector2(GameObject.Find("Checkout").transform.position.x, GameObject.Find("Checkout").transform.position.y)));
        ////checkoutTile.isWalkable = false;
        //checkoutTile.highlight.SetActive(false);
    }
    private void Update()
    {

    }
}
