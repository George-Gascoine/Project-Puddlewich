using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//create a new type
public class Crop : MonoBehaviour
{

    public Player player;
    public CropType cropType;
    public int cropCurrentGrowthStage;
    public int cropMaxGrowthStage;
    public int cropQuality;
    public bool cropIsWatered;
    public TileBase cropFarmPos;
    public Sprite icon;
    FarmManager farmManager;

    public void Awake()
    {
        player = FindAnyObjectByType<Player>();
        icon = GetComponent<SpriteRenderer>().sprite;
        farmManager = FindObjectOfType<FarmManager>();
    }
    public enum CropType
    {
        NONE,
        CHILLI,
        TOMATO,
        CUCUMBER,
        ONION
    }
}

