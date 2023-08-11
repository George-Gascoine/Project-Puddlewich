using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    [SerializeField] Tile[] tileArray;
    [SerializeField] int gridWidth = 40;
    [SerializeField] int gridHeight = 40;
    [SerializeField] float tileSize = 1f;
    Vector2 gridSize = new(1f, 0.5f);


    public Dictionary<Vector2, Tile> tiles;
    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GenerateGrid();
    }

    private void Update()
    {
        if (path != null)
        {
            foreach (Tile n in path)
            {
                //n.highlight.SetActive(true);
            }
        }
    }
    private void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                
                //var newTile = Instantiate(groundTile, transform);
                Tile newTile = new();
                float posX = (x * tileSize - y * tileSize) / 2f;
                float posY = (x * tileSize + y * tileSize) / 4f;

                //newTile.transform.position = new Vector2(posX, posY);
                //newTile.name = x + " , " + y;
                newTile.posX = posX;
                newTile.posY = posY;
                newTile.gridX = x;
                newTile.gridY = y;
                newTile.isWalkable = true;

                tiles[new Vector2(x, y)] = newTile;
            }
        }
    }

    public List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();

        for(int x = -1;  x <= 1; x++) 
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = tile.gridX + x;
                int checkY = tile.gridY + y;

                if(checkX >= 0 && checkX < gridWidth && checkY >= 0  && checkY < gridHeight)
                {
                    neighbours.Add(tiles[new Vector2(checkX,checkY)]);
                }
            }
        }
        return neighbours;
    }

    public List<Tile> path;

    public Vector2 TilePosition(Vector2 localPosition)
    {
        // Calculate ratios for simple grid snap
        float xx = Mathf.Round(localPosition.y / gridSize.y - localPosition.x / gridSize.x);// Y grid coordinate
        float yy = Mathf.Round(localPosition.y / gridSize.y + localPosition.x / gridSize.x);// X grid coordinate

        // Calculate grid aligned position from current position
        Vector2 position = transform.localPosition;
        float x = (yy - xx) * 0.5f * gridSize.x;
        float y = (yy + xx) * 0.5f * gridSize.y;

        return new Vector2(x, y);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        Vector2 dictionaryKey = new Vector2(2 * pos.y + pos.x, 2 * pos.y - pos.x);
        //Debug.Log(tiles[dictionaryKey]);
        return tiles[dictionaryKey];
    }

}
