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

public class Farming : MonoBehaviour
{
    public Player player;
    public GameObject baseCrop;
    public TileBase grassTile;
    public TileBase soilTile;
    public TileBase wateredTile;
    public Tilemap farmMap;
    public List<Vector3Int> wateredTiles = new();
    public List<Vector3Int> soilTiles = new();
    public List<Crop.CropData> plantedCrops = new();
    public Grid grid;

    public void Awake()
    {
 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        player = GameManager.instance.player;
        grid = FindObjectOfType<Grid>();
        farmMap = grid.transform.Find("Tilemap").GetComponent<Tilemap>();
    }
    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "World" && player.equippedItem != null)
        {
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
                    //    //CROP JSON
                    int seedIndex = player.equippedItem.id;
                    Crop.CropData cropData = GameManager.instance.farmManager.cropList.crop.Single(s => s.seedIndex == seedIndex);
                        PlantCrop(cropData, currentCell, currentCellCentre);
                    }
                }
            }
        }
    }
    void PlantCrop(Crop.CropData crop, Vector3Int cell, Vector3 cellCentre)
    {
        crop.cropFarmPos = cell;
        crop.cropCurrentGrowthStage = 0;
        crop.cropIsWatered = false;
        GameObject planted = Instantiate(baseCrop, cellCentre, Quaternion.identity);
        planted.GetComponent<Crop>().crop = crop;
        plantedCrops.Add(crop);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "World")
        {
            grid = FindObjectOfType<Grid>();
            farmMap = grid.transform.Find("Tilemap").GetComponent<Tilemap>();
            plantedCrops.Clear();
            ResetFarm();
        }
    }

    void ResetFarm()
    {
        foreach (Vector3Int soil in soilTiles)
        {
            farmMap.SetTile(soil, soilTile);
        }
        foreach (Vector3Int water in wateredTiles)
        {
            farmMap.SetTile(water, wateredTile);
        }
    }
}
