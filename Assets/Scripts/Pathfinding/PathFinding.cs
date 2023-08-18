using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Harry, Dennis, Steve, Rob, Dan, Jamie, Trevor
public class PathFinding : MonoBehaviour
{
    public Vector3[] finalPath;
    public float pathLength;
    public float pathTime;
    public float speed;
    int targetIndex;
    public bool destroy;
    Grid2D grid;

    public Transform seeker, target;

    public void Start()
    {
        grid = FindObjectOfType<Grid2D>();
    }
    private void Update()
    {
        if (targetIndex >= finalPath.Length)
        {
            if (destroy == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void FindPath(Tile startPos, Tile targetPos)
    {
        Tile startTile = startPos;
        Tile targetTile = targetPos;

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
            if (i == 0)
            {
                pathLength += Vector2.Distance(transform.position, points[0]);
            }
            else if (i != 0)
            {
                pathLength += Vector2.Distance(points[i - 1], points[i]);
            }
        }
        //nodeTime = pathLength / path.Count;
        //gameObject.GetComponent<NPCRoutine>().maxWaitTime = pathLength/2;
        return points.ToArray();
    }


    IEnumerator FollowPath(float delay)
    {
        //yield return new WaitForSeconds(delay);

        targetIndex = (int)((DayNightCycle.gameTimer * 2) - delay);
        Debug.Log(targetIndex + " " + finalPath.Length);
        if (targetIndex >= finalPath.Length)
        {
            if(destroy == true)
            {
                Destroy(this.gameObject);
            }
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
            //Debug.Log(currentPoint);
            pathTime = finalPath.Length / pathLength;
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
