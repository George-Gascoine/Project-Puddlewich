using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine.Rendering.Universal;
using static Crop;
using System.Runtime.CompilerServices;

public class Farming : MonoBehaviour
{
    public static Farming instance { get; private set; }
    public Player player;
    public GameObject baseCrop;
    public TileBase grassTile;
    public TileBase soilTile;
    public TileBase wateredTile;
    public Tilemap farmMap;
    public List<Vector3Int> wateredTiles = new();
    public List<Vector3Int> soilTiles = new();
    public List<Crop.CropData> plantedCrops = new();
    public List<GameObject> plantedObjects = new();
    public Grid grid;

    public void Awake()
    {
        wateredTiles.Clear();
        soilTiles.Clear();
        plantedObjects.Clear();
        plantedObjects.Clear();
    }
    private void Start()
    {
        wateredTiles.Clear();
        soilTiles.Clear();
        plantedObjects.Clear();
        plantedObjects.Clear();
        grid = FindObjectOfType<Grid>();
    }
    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {  
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "World" && player.equippedItem != null)
        {
            grid = FindObjectOfType<Grid>();
            farmMap = grid.transform.Find("Tilemap").GetComponent<Tilemap>();
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            // get current grid location
            farmMap.CompressBounds();
            Vector3Int currentCell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 10f)));
            Vector3 currentCellCentre = grid.GetCellCenterWorld(currentCell);

            // add one in a direction (you'll have to change this to match your directional control)
            if (Input.GetMouseButtonDown(0) && player.equippedItem.name == "Hoe")
            {
                if (farmMap.GetTile(currentCell) == grassTile)
                {
                    soilTiles.Add(currentCell);
                    farmMap.SetTile(currentCell, soilTile);
                }
            }
            if (Input.GetMouseButtonDown(0) && player.equippedItem.name == "Watering Can")
            {
                if (farmMap.GetTile(currentCell) == soilTile)
                {
                    wateredTiles.Add(currentCell);
                    farmMap.SetTile(currentCell, wateredTile);
                    for (int i = 0; i < plantedCrops.Count; i++)
                    {
                        if (currentCell == plantedCrops[i].cropFarmPos)
                        {
                            plantedCrops[i].cropIsWatered = true;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && player.equippedItem.type == "Seed")
            {
                if (farmMap.GetTile(currentCell) == soilTile || farmMap.GetTile(currentCell) == wateredTile)
                {
                    bool cellOccupied = plantedCrops.Any(CropData => CropData.cropFarmPos == currentCell);
                    if(!cellOccupied)
                    {
                        //CROP JSON
                        int seedIndex = player.equippedItem.id;
                        Crop.CropData cropData = GameManager.instance.farmManager.cropList.crop.Single(s => s.seedIndex == seedIndex);
                        Crop.CropData newCropData = new Crop.CropData
                        {
                            name = cropData.name,
                            seedIndex = cropData.seedIndex,
                            cropIndex = cropData.cropIndex,
                            growthSeason = cropData.growthSeason,
                            growthStageDays = cropData.growthStageDays,
                            maxGrowthStage = cropData.maxGrowthStage,
                            quality = cropData.quality,
                            sprite  = cropData.sprite,
                            currentGrowthStage = cropData.currentGrowthStage,
                            cropIsWatered = cropData.cropIsWatered,
                            cropFarmPos = currentCell,
                        };
                        PlantCrop(newCropData, currentCell, currentCellCentre);
                    }
                }
            }
        }
    }
    void PlantCrop(Crop.CropData crop, Vector3Int cell, Vector3 cellCentre)
    {
        crop.cropFarmPos = cell;
        GameObject planted = Instantiate(baseCrop, cellCentre, Quaternion.identity);
        planted.GetComponent<Crop>().crop = crop;
        plantedObjects.Add(planted);
        plantedCrops.Add(crop);
    }

    public void ResetFarm()
    {
        grid = FindObjectOfType<Grid>();
        farmMap = grid.transform.Find("Tilemap").GetComponent<Tilemap>();
        Debug.Log(plantedCrops.Count);
        foreach (Vector3Int soil in soilTiles)
        {
            farmMap.SetTile(soil, soilTile);
        }
        foreach (Vector3Int water in wateredTiles)
        {
            farmMap.SetTile(water, wateredTile);
        }
        foreach (GameObject plant in plantedObjects)
        { 
            Destroy(plant);
        }
        List<Crop.CropData> tempCrops = new List<Crop.CropData>(plantedCrops);
        plantedCrops.Clear();
        plantedObjects.Clear();
        foreach (Crop.CropData plant in tempCrops)
        {
            PlantCrop(plant, plant.cropFarmPos, grid.GetCellCenterWorld(plant.cropFarmPos));
        }
        tempCrops.Clear();
    }

    public void RemoveCrop(Vector3Int cropFarmPos)
    {
        plantedCrops.RemoveAll(s => s.cropFarmPos == cropFarmPos);
        plantedObjects.RemoveAll(s => s.GetComponent<Crop>().crop.cropFarmPos == cropFarmPos);
    }
    public void DrySoil()
    {
        foreach (Vector3Int soil in wateredTiles)
        {
            farmMap.SetTile(soil, soilTile);
        }
        wateredTiles.Clear();
    }
}
