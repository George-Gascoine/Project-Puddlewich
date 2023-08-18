using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

//Check if npc has arrived at destination tile
//If they have stop coroutine
//Activate NPC and apply Vector2 currentPos to transform
//Apply pathfinding to transform if in same room
//Pause if blocked/delayed and start wait timer
//Read destination and start time fro JSON, start pos is current tile no matter where NPC is

public class UpdatedPathFinding : MonoBehaviour
{
    public GameManager gameManager;
    public NPC npc;
    public Vector2 currentPos;
    public Vector3[] finalPath;
    public float pathLength, pathTime, speed;
    public Location currentLocation;
    public Location destination;
    public Tile finalDestination;
    public Tile startTile;
    public Tile targetTile;
    public Transition.Warp targetWarp;
    int targetIndex;
    public bool arrived;
    public UpdatedGrid2D grid;

    //public Transform seeker, target;

    public void Start()
    {
        speed = 1.0f;
        targetIndex = 0;
        //Debug.Log(grid);
    }
    private void Update()
    {
    }
    public void FindPath(Tile startPos, Tile targetPos)
    {
        if (npc.location == npc.destination)
        {
            startTile = startPos;
            targetTile = targetPos;
            //arrived = true;
        }
        else if (npc.location != npc.destination)
        {
            foreach (Location loc in currentLocation.connectedLocations)
            {
                if (loc == npc.destination)
                {
                    startTile = startPos;
                    foreach (Transition.Warp warp in currentLocation.warps)
                    {
                        if (warp.destination == npc.destination)
                        {
                            targetWarp = warp;
                            destination = warp.destination;
                            targetTile = gameManager.sceneGrids[npc.location.locationName].GetTileAtPosition(gameManager.sceneGrids[npc.location.locationName].TilePosition(warp.locationTile));
                            targetTile.isWalkable = true;
                            //npc.location.locationGrid.tiles[tran.locationTile];
                            break;
                        }
                    }
                }
                else if (loc.locationName == "World")
                {
                    startTile = startPos;
                    foreach (Transition.Warp warp in currentLocation.warps)
                    {
                        if (warp.destination.locationName == "World")
                        {
                            targetWarp = warp;
                            destination = warp.destination;
                            targetTile = gameManager.sceneGrids[npc.location.locationName].tiles[warp.locationTile];
                            break;
                        }
                    }
                }
            }
        }
        List<Tile> openSet = new(); //The set of Tiles to be evaluated
        HashSet<Tile> closedSet = new(); //The set of Tiles already evaluated
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
            grid = gameManager.sceneGrids[currentLocation.locationName];

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
        List<Tile> path = new();

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
        List<Vector3> points = new();
        for (int i = 0; i < path.Count; i++)
        {
            points.Add(new Vector3(path[i].posX + 0.5f, path[i].posY, 1));
            //if (i == 0)
            //{
            //    pathLength += Vector2.Distance(transform.position, points[0]);
            //}
            //else if (i != 0)
            //{
            //    pathLength += Vector2.Distance(points[i - 1], points[i]);
            //}
        }
        //nodeTime = pathLength / path.Count;
        //gameObject.GetComponent<NPCRoutine>().maxWaitTime = pathLength/2;
        return points.ToArray();
    }


    IEnumerator FollowPath(float delay)
    {
        //yield return new WaitForSeconds(delay);

        //targetIndex = 0;
        //if (targetIndex >= finalPath.Length)
        //{
        //    gameObject.transform.position = (Vector2)finalPath[finalPath.Length - 1];
        //    yield break;
        //}
        Debug.Log(finalPath.Length);
        currentPos = (Vector2)finalPath[targetIndex];

        Vector2 currentPoint = (Vector2)finalPath[targetIndex];
        while (true)
        {
            if ((Vector2)currentPos == currentPoint)
            {
                targetIndex++;

                if (targetIndex >= finalPath.Length)
                {
                    //if (currentLocation.locationGrid.TilePosition(currentPos) == )

                    targetIndex = 0;
                    npc.location = destination;
                    currentLocation = destination;
                    grid = gameManager.sceneGrids[destination.locationName];
                    //destination.locationGrid;
                    currentPos = targetWarp.destinationSpawn;
                    FindPath(grid.tiles[currentPos], finalDestination);
                    StartCoroutine("FollowPath", 0);
                    yield break;
                }
                currentPoint = (Vector2)finalPath[targetIndex];
            }
            currentPos = Vector2.MoveTowards((Vector2)currentPos, (Vector2)currentPoint, speed * Time.deltaTime);
            npc.currentPosition = currentPos;
            if (gameManager.sceneGrids[npc.location.locationName].GetTileAtPosition(gameManager.sceneGrids[npc.location.locationName].TilePosition(new Vector2(npc.currentPosition.x - 0.5f, npc.currentPosition.y)))
                //npc.location.locationGrid.GetTileAtPosition(npc.location.locationGrid.TilePosition(new Vector2(npc.currentPosition.x - 0.5f, npc.currentPosition.y))) 
                == finalDestination)
            {
                StopAllCoroutines();
                npc.onWay = false;
            }
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
}

