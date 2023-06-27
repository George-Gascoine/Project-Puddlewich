using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class NPCShopper : MonoBehaviour
{
    ShopManager shopManager;
    public Tile start;
    public Tile target;
    Grid2D grid;
    Vector3[] finalPath;
    int targetIndex;
    public Vector2 gridSize = new Vector2(1f, 0.5f);
    public bool buyer = false;
    public bool paying = false;
    public bool leaving = false;
    public Collectable buyItem;
    public float payPrice;

    private void Start()
    {
        //Get the tile by coordinate e.g. new Vector2(2,4)
        //Find the starting Tile
        grid = FindObjectOfType<Grid2D>();
        shopManager = FindObjectOfType<ShopManager>();
        FindPath(start, target);
        StartCoroutine("FollowPath", 0);
    }
    private void Update()
    {
        if (transform.position.x == target.transform.position.x + 0.5f && transform.position.y == target.transform.position.y)
        {
            if(leaving==true)
            {
                Destroy(this.gameObject);
            }
            if (paying == true)
            {
                StopCoroutine("FollowPath");
                paying = false;
                shopManager.shopEarnings += payPrice;
                start = target;
                target = grid.tiles[new Vector2(0, 0)];
                FindPath(start, target);
                StartCoroutine("FollowPath", 2);
                leaving = true; 
            }
            if (buyer == true)
            {
                StopCoroutine("FollowPath");
                payPrice = buyItem.itemCost;
                Destroy(buyItem.gameObject);
                start = grid.GetTileAtPosition(grid.TilePosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y))); ;
                target = shopManager.checkoutTile;
                buyer = false;
                paying = true;
                FindPath(start, target);
                StartCoroutine("FollowPath", 2);
            }
        }
    }

    public void FindPath(Tile startPos, Tile targetPos)
    {
        Tile startTile = startPos;
        Tile targetTile = targetPos;

        List<Tile> openSet = new List<Tile>(); //The set of Tiles to be evaluated
        HashSet<Tile> closedSet = new HashSet<Tile>(); //The set of Tiles already evaluated
        openSet.Add(startTile); //Add the startingTile to the openSet to be evaluated

        //Loop through set
        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentTile.FCost || openSet[i].FCost == currentTile.FCost && openSet[i].hCost < currentTile.hCost)
                {
                    currentTile = openSet[i]; //currentTile = lowest fCost Tile
                }
            }

            openSet.Remove(currentTile); //Remove the currentTile from openSet
            closedSet.Add(currentTile); //Add currentTile to closedSet

            if (currentTile == targetTile)
            {
                RetracePath(startTile, targetTile);
                return; //If the currentTile is targetTile, path has been found -> return the found path
            }
            grid = FindObjectOfType<Grid2D>();

            foreach (Tile neighbour in grid.GetNeighbours(currentTile)) //Foreach neighbourTile of currentTile 
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue; //If neighbourTile is in closedSet or is an obstacle skip it
                }

                int newMovementCostToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) //If new path to neighbourTile is shorter or neighbourTile is not in openSet
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetTile);//Set the fCost to neighbourTile
                    neighbour.parent = currentTile; //Set parent of neighbourTile to currentTile

                    if (!openSet.Contains(neighbour)) //If neighbourTile is not in openSet
                    {
                        openSet.Add(neighbour);//Add neighbourTile to openSet
                    }
                }
            }
        }
    }

    void RetracePath(Tile startTile, Tile targetTile)
    {
        List<Tile> path = new List<Tile>();

        Tile currentTile = targetTile;

        while (currentTile != startTile)
        {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        path.Add(startTile);
        path.Reverse();

        grid.path = path;
        finalPath = PathPoints(path);
    }

    Vector3[] PathPoints(List<Tile> path)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            points.Add(new Vector3(path[i].transform.position.x + 0.5f, path[i].transform.position.y, 1));
        }
        return points.ToArray();
    }


    IEnumerator FollowPath(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetIndex = 0;
        Vector3 currentPoint = finalPath[0];
        while (true)
        {
            if (transform.position == currentPoint)
            {
                targetIndex++;
                if (targetIndex >= finalPath.Length)
                {
                    yield break;
                }
                currentPoint = finalPath[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPoint, 1 * Time.deltaTime);
            yield return null;
        }
    }

    int GetDistance(Tile tileA, Tile tileB)
    {
        int dstX = Mathf.Abs(tileA.gridX - tileB.gridX);
        int dstY = Mathf.Abs(tileA.gridY - tileB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
    public void SelectedShopper(Tile begin, Tile end)
    {
        StopCoroutine("FollowPath");
        start = begin;
        target = end;
        FindPath(start, target);
        StartCoroutine("FollowPath", 0);
    }

}
