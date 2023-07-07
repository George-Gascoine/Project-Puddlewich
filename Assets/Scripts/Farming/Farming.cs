using System.Collections;
using System.Collections.Generic;
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

    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
        //Debug.Log(transform.position);
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        // get current grid location
        Vector3Int currentCell = farmMap.WorldToCell(Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 8f)));
        Vector3 currentCellCentre = farmMap.GetCellCenterWorld(currentCell);
        // add one in a direction (you'll have to change this to match your directional control)
        if (Input.GetMouseButtonDown(0) && player.equippedItem == GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.HOE))
        {
            if(farmMap.GetTile(currentCell) == grassTile)
            {
                Debug.Log(currentCell);
                farmMap.SetTile(currentCell, soilTile);
            }
        }
        if (Input.GetMouseButtonDown(0) && player.equippedItem == GameManager.instance.itemManager.GetItemByType(Collectable.ItemType.SEED))
        {
            if (farmMap.GetTile(currentCell) == soilTile)
            { 
                Crop plantThis = GameManager.instance.farmManager.GetCropByType(Crop.CropType.CHILLI);
                Debug.Log(currentCell);
                Instantiate(plantThis, currentCellCentre, Quaternion.identity);
                plantThis.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
        }
    }
}
