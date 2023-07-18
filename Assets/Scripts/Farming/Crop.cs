using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//create a new type
public class Crop : MonoBehaviour
{
    public CropType cropType;
    public int cropCurrentGrowthStage;
    public int cropMaxGrowthStage;
    public int cropQuality;
    public bool cropIsWatered;
    public TileBase cropFarmPos;

    public void Awake()
    {
        cropIsWatered = false;
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

