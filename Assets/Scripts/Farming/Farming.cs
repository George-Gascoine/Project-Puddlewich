using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using Unity.Burst.Intrinsics;

public class Farming : MonoBehaviour
{
    public Player player;
    public TileBase grassTile;
    public TileBase soilTile;
    public TileBase wateredTile;
    public Tilemap farmMap;
    public List<Crop> crops = new();
    public List<Vector3Int> soilTiles = new();
    public Dictionary<Vector3Int, Crop> plantedCropDict = new();
    public Grid grid;

    public void Awake()
    {
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
            if (Input.GetMouseButtonDown(0) && player.equippedItem == GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.HOE))
            {
                if (farmMap.GetTile(currentCell) == grassTile)
                {
                    soilTiles.Add(currentCell);
                    farmMap.SetTile(currentCell, soilTile);
                }
            }
            if (Input.GetMouseButtonDown(0) && player.equippedItem == GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.WATERINGCAN))
            {
                if (farmMap.GetTile(currentCell) == soilTile)
                {
                    farmMap.SetTile(currentCell, wateredTile);
                    for (int i = 0; i < plantedCropDict.Count; i++)
                    {
                        if (currentCell == plantedCropDict.ElementAt(i).Key)
                        {
                            plantedCropDict[currentCell].cropIsWatered = true;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && player.equippedItem.crop != null)
            {
                if (farmMap.GetTile(currentCell) == soilTile)
                {
                    var plantThis = Instantiate(player.equippedItem.crop, currentCellCentre, Quaternion.identity);
                    plantedCropDict.Add(currentCell, plantThis);
                    plantThis.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        grid = FindObjectOfType<Grid>();
        farmMap = grid.transform.Find("Tilemap").GetComponent<Tilemap>();
        ResetFarm();
    }

    void ResetFarm()
    {
        foreach (Vector3Int soil in soilTiles)
        {
            Debug.Log(soil);
            farmMap.SetTile(soil, soilTile);
        }
        foreach (KeyValuePair<Vector3Int, Crop> crop in plantedCropDict)
        {
            //Create crops again here
            //Watered tile destroy when last tile is watered
        }
    }
}
