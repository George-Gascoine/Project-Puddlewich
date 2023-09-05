using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Crop : MonoBehaviour
{
    public Player player;
    public Crop.CropData crop;
    public Sprite[] growthStages;
    public int cropCurrentGrowthStage;
    public int cropQuality;
    public bool cropIsWatered;
    public Vector3Int cropFarmPos;
    public SpriteRenderer cropRenderer;
    public GameManager manager;
    [System.Serializable]
    public class CropData
    {
        public string name;
        public int seedIndex;
        public int cropIndex;
        public string growthSeason;
        public int[] growthStageDays;
        public int maxGrowthStage;
        public int quality;
        public string sprite;
        public int cropCurrentGrowthStage;
        public bool cropIsWatered;
        public Vector3Int cropFarmPos;
    }


    public void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        cropIsWatered = false;
        cropRenderer = gameObject.GetComponent<SpriteRenderer>();
        growthStages = Resources.LoadAll<Sprite>("Sprites/Crops/" + crop.sprite);
        cropRenderer.sprite = growthStages[cropCurrentGrowthStage];
        cropRenderer.sortingOrder = 3;
    }
    void OnMouseDown()
    {
        //if(cropCurrentGrowthStage == cropMaxGrowthStage)
        //{
        //    List<CropData> data = manager.GetComponent<Farming>().cropData;
        //    foreach (CropData crop in data) 
        //    {
        //        if(crop.cropFarmPos == cropFarmPos)
        //        {
        //            manager.GetComponent<Farming>().plantedCrops.Remove(this);
        //            data.Remove(crop); break;
        //        }
        //    }
        //    //player.inventory.Add(crop);
        //    Destroy(this.gameObject);
        //}
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

