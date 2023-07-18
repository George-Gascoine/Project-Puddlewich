using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farming : MonoBehaviour
{
    public Player player;
    public TileBase grassTile;
    public TileBase soilTile;
    public TileBase wateredTile;
    public Tilemap farmMap;
    public List<Crop> crops = new();
    public List<Crop> plantedCrops = new();
    public Dictionary<Vector3Int, Crop> plantedCropDict = new();
    public Grid grid;

    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
        
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        // get current grid location
        farmMap.CompressBounds();
        Vector3Int currentCell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 8f)));
        Vector3 currentCellCentre = grid.GetCellCenterWorld(currentCell);

        // add one in a direction (you'll have to change this to match your directional control)
        if (Input.GetMouseButtonDown(0) && player.equippedItem == GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.HOE))
        {
            if (farmMap.GetTile(currentCell) == grassTile)
            {
                farmMap.SetTile(currentCell, soilTile);
            }
        }
        if (Input.GetMouseButtonDown(0) && player.equippedItem == GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.WATERINGCAN))
        {
            if (farmMap.GetTile(currentCell) == soilTile)
            {
                farmMap.SetTile(currentCell, wateredTile);
                for(int i = 0; i < plantedCropDict.Count; i++)
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
                plantedCrops.Add(plantThis);
                Debug.Log(plantedCrops[0]);
                plantThis.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
        }
    }
}
