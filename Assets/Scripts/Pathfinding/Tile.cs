using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    public GameManager GameManager;
    [SerializeField] public GameObject highlight;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public bool isWalkable;
    public Tile parent;

    Grid2D grid;

    //Create Grid of invisible cells
    //Make cells not walkable etc
    //If clicked on work out how to react

    //private void OnMouseExit()
    //{
    //    highlight.SetActive(false); 
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("NPC"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<PolygonCollider2D>());
        }
    }

    public int FCost
    { 
        get { 
            return gCost + hCost;
        }
    }
}
