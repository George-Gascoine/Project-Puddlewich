using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public GameObject highlight;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public bool isWalkable;
    public Tile parent;

    Grid2D grid;

    //private void OnMouseEnter()
    //{
    //    highlight.SetActive(true);
    //}
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
