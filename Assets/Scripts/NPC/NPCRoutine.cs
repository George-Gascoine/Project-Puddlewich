using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

[Serializable]
public class NPCRoutine : MonoBehaviour
{
    //Spawn
    //Calculate Path
    //Save Path and divide it based on time it would take
    //Calculate distance between each node
    //Add total distance and divide by speed, distance/speed = time
    //Load at those points based on time
    public Vector3[] finalPath;
    public float pathLength;
    public float pathTime;
    public float speed;
    public float maxWaitTime;
    int targetIndex;
    Grid2D grid;
    public Tile start;
    public Tile target;
    public GameObject CauldronShop;
    private PathFinding pathFinding;
    
    private void Start()
    {
        speed = 1;
        grid = FindObjectOfType<Grid2D>();
        CauldronShop = GameObject.Find("CauldronShop");
        pathFinding = GetComponent<PathFinding>();
        targetIndex = 0;
        FindRoutine();
    }
    private void Update()
    {
        //if (SceneManager.GetActiveScene().name == "World" && targetIndex >= finalPath.Length)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    void FindRoutine()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "World":
                //start = grid.GetTileAtPosition(grid.TilePosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.25f)));
                //target = grid.GetTileAtPosition(grid.TilePosition(CauldronShop.transform.position));
                //FindPath(start, target);
                //StartCoroutine("FollowPath", 0);
                //pathFinding.seeker = gameObject.transform;
                //pathFinding.target = CauldronShop.transform;
                if (DayNightCycle.gameTimer < 17)
                {
                    pathFinding.speed = speed;
                    pathFinding.destroy = true;
                    pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(CauldronShop.transform.position)));
                    pathFinding.StartCoroutine("FollowPath", 0);
                }
                else if (DayNightCycle.gameTimer > 37)
                {
                    pathFinding.speed = speed;
                    //pathFinding.destroy = true;
                    pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(CauldronShop.transform.position)), grid.GetTileAtPosition(new Vector2(3f,4f)));
                    pathFinding.StartCoroutine("FollowPath", 74);
                }
                //pathFinding.StartCoroutine("FollowPath", 0);
                break;
            case "Cauldron Shop":
                //start = grid.GetTileAtPosition(grid.TilePosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.25f)));
                //target = grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Checkout").transform.position));
                //FindPath(start, target);
                //Debug.Log(DayNightCycle.gameTimer * 2);
                //StartCoroutine("FollowPath", 34);
                //pathFinding.seeker = gameObject.transform;
                //pathFinding.target = GameObject.Find("Checkout").transform;
                if (DayNightCycle.gameTimer >= 17 && DayNightCycle.gameTimer <= 30)
                {
                    pathFinding.speed = speed;
                    pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Checkout").transform.position)));
                    pathFinding.StartCoroutine("FollowPath", 34);
                }
                else if (DayNightCycle.gameTimer > 30)
                {
                    pathFinding.destroy = true;
                    pathFinding.speed = speed;
                    pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Transition").transform.position)));
                    pathFinding.StartCoroutine("FollowPath", 60);
                }
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }

    }

    public void LeaveShop()
    {
        pathFinding.destroy = true;
        pathFinding.speed = speed;
        pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Transition").transform.position)));
        pathFinding.StartCoroutine("FollowPath", 60);
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
            if(i == 0)
            {
                pathLength += Vector2.Distance(transform.position, points[0]);
            }
            else if (i != 0)
            {
                pathLength += Vector2.Distance(points[i - 1], points[i]);
            }  
        }
        pathTime = pathLength / 1;
        return points.ToArray();
    }


    IEnumerator FollowPath(float delay)
    {
        //yield return new WaitForSeconds(delay);
        
        targetIndex = (int)((DayNightCycle.gameTimer * 2) - delay);
        Debug.Log(targetIndex + " " + finalPath.Length);
        if (targetIndex >= finalPath.Length)
        {
            Debug.Log("Move");
            gameObject.transform.position = (Vector2)finalPath[finalPath.Length - 1];
            yield break;
        }
        gameObject.transform.position = (Vector2)finalPath[targetIndex];
        Vector2 currentPoint = finalPath[targetIndex];
        while (true)
        {
            if ((Vector2)transform.position == currentPoint)
            {
                targetIndex++;
                if (targetIndex >= finalPath.Length)
                {
                    yield break;
                }
                currentPoint = (Vector2)finalPath[targetIndex];
            }
            transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)currentPoint, speed * Time.deltaTime);
            pathTime = finalPath.Length/pathLength;
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
