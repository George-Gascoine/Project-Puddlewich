using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Tile
{
    public GameManager gameManager;
    [SerializeField] public GameObject highlight;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public float posX;
    public float posY;
    public bool isWalkable;
    public Tile parent;


    //Create Grid of invisible cells
    //Make cells not walkable etc
    //If clicked on work out how to react

    //private void OnMouseExit()
    //{
    //    highlight.SetActive(false); 
    //}
    public int FCost
    { 
        get { 
            return gCost + hCost;
        }
    }
}
