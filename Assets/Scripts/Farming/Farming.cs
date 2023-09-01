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
    public TileBase grassTile;
    public TileBase soilTile;
    public TileBase wateredTile;
    public Tilemap farmMap;
    public List<Vector3Int> wateredTiles = new();
    public List<Vector3Int> soilTiles = new();
    public List<Crop> plantedCrops = new();
    public List<CropData> cropData = new();
    public Grid grid;

    public void Awake()
    {
        cropData.Clear();
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
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
                    for (int i = 0; i < cropData.Count; i++)
                    {
                        if (currentCell == cropData[i].cropFarmPos)
                        {
                            cropData[i].cropIsWatered = true;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && player.equippedItem.type == "Seed")
            {
                if (farmMap.GetTile(currentCell) == soilTile || farmMap.GetTile(currentCell) == wateredTile)
                {
                    bool cellOccupied = cropData.Any(CropData => CropData.cropFarmPos == currentCell);
                    if(!cellOccupied)
                    {
                        //CROP JSON
                        //PlantCrop(player.equippedItem.effect, currentCell, currentCellCentre);
                    }
                }
            }
        }
    }
    void PlantCrop(Crop crop, Vector3Int cell, Vector3 cellCentre)
    {
        Debug.Log(farmMap.GetTile(cell) == wateredTile);
        Crop planted = Instantiate(crop, cellCentre, Quaternion.identity);
        plantedCrops.Add(planted);
        CropData savePlantData = new()
        {
            cropType = crop,
            cropFarmPos = cell,
            cropCellCentre = cellCentre,
            cropIsWatered = farmMap.GetTile(cell) == wateredTile,
            cropCurrentGrowthStage = 0
        };
        cropData.Add(savePlantData);
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
        foreach(CropData crop in cropData)
        {
            Crop plantThis = Instantiate(crop.cropType, crop.cropCellCentre, Quaternion.identity);
            plantThis.cropCurrentGrowthStage = crop.cropCurrentGrowthStage;
            plantThis.cropIsWatered = crop.cropIsWatered;
            plantThis.CheckSprite();
            plantedCrops.Add(plantThis);
        }
    }
}
