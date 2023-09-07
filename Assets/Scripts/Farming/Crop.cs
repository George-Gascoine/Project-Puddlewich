using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class Crop : MonoBehaviour
{
    public Player player;
    public Crop.CropData crop;
    public List<Sprite> growthStages;
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
        public int currentGrowthStage;
        public int growthStageDay;
        public bool cropIsWatered;
        public Vector3Int cropFarmPos;
    }

    public void Awake()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), GameObject.FindWithTag("Player").GetComponent<Player>().GetComponent<BoxCollider2D>());
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        cropRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Start()
    {
        InitializeSprite();
        cropRenderer.sprite = growthStages[crop.currentGrowthStage];
        cropRenderer.sortingOrder = 3;
        Vector2 spriteSize = cropRenderer.sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = spriteSize;
    }
    void OnMouseDown()
    {
        if (crop.currentGrowthStage == crop.maxGrowthStage)
        {
            player.GetComponent<Farming>().RemoveCrop(this.crop.cropFarmPos);
            player.inventory.Add(GameManager.instance.GetComponent<ItemManager>().itemList.item.Single(s => s.id == crop.cropIndex));
            Destroy(this.gameObject);
        }
    }

    Sprite InitializeSprite()
    {
            Sprite[] all = Resources.LoadAll<Sprite>("Sprites/Crops/");

            foreach (var s in all)
            {
                if (s.name.Contains(crop.sprite))
                {
                    growthStages.Add(s);
                }
            }
            return null;
    }
    public void CheckSprite()
    {
        
        if(cropRenderer.sprite != growthStages[crop.currentGrowthStage])
        {
            ChangeSprite(growthStages[crop.currentGrowthStage]);
        }
    }
    public void ChangeSprite(Sprite newSprite)
    {
        cropRenderer.sprite = newSprite; 
        Vector2 spriteSize = cropRenderer.sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = spriteSize;

    }
}

