using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop : MonoBehaviour
{
    public Player player;
    public CropType cropType;
    public Collectable crop;
    public Sprite[] growthStages;
    public int cropCurrentGrowthStage;
    public int cropMaxGrowthStage;
    public int cropQuality;
    public bool cropIsWatered;
    public Vector3Int cropFarmPos;
    public SpriteRenderer cropRenderer;
    public GameManager manager;

    public void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        cropIsWatered = false;
        cropRenderer = gameObject.GetComponent<SpriteRenderer>();
        cropRenderer.sortingOrder = 3;
    }

    public enum CropType
    {
        NONE,
        CHILLI,
        TOMATO,
        CUCUMBER,
        ONION
    }
    void OnMouseDown()
    {
        if(cropCurrentGrowthStage == cropMaxGrowthStage)
        {
            List<CropData> data = manager.GetComponent<Farming>().cropData;
            foreach (CropData crop in data) 
            {
                if(crop.cropFarmPos == cropFarmPos)
                {
                    manager.GetComponent<Farming>().plantedCrops.Remove(this);
                    data.Remove(crop); break;
                }
            }
            player.inventory.Add(crop);
            Destroy(this.gameObject);
        }
    }
    public void CheckSprite()
    {
        if(cropRenderer.sprite != growthStages[cropCurrentGrowthStage])
        {
            ChangeSprite(growthStages[cropCurrentGrowthStage]);
        }
    }
    public void ChangeSprite(Sprite newSprite)
    {
        cropRenderer.sprite = newSprite;
    }
}

