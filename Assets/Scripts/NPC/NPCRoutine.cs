using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class NPCRoutine : MonoBehaviour
{
    ////Spawn
    ////Calculate Path
    ////Save Path and divide it based on time it would take
    ////Calculate distance between each node
    ////Add total distance and divide by speed, distance/speed = time
    ////Load at those points based on time
    //public Vector3[] finalPath;
    //public float pathLength;
    //public float pathTime;
    //public float speed;
    //public float maxWaitTime;
    //public int targetIndex;
    //Grid2D grid;
    //public Tile start;
    //public Tile target;
    //public GameObject CauldronShop;
    //private PathFinding pathFinding;
    
    //private void Start()
    //{
    //    speed = 1;
    //    grid = FindObjectOfType<Grid2D>();
    //    CauldronShop = GameObject.Find("CauldronShop");
    //    pathFinding = GetComponent<PathFinding>();
    //    targetIndex = 0;
    //    FindRoutine();
    //}
    //private void Update()
    //{
    //    //if (SceneManager.GetActiveScene().name == "World" && targetIndex >= finalPath.Length)
    //    //{
    //    //    Destroy(this.gameObject);
    //    //}
    //}

    //void FindRoutine()
    //{
    //    switch (SceneManager.GetActiveScene().name)
    //    {
    //        case "World":
    //            //start = grid.GetTileAtPosition(grid.TilePosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.25f)));
    //            //target = grid.GetTileAtPosition(grid.TilePosition(CauldronShop.transform.position));
    //            //FindPath(start, target);
    //            //StartCoroutine("FollowPath", 0);
    //            //pathFinding.seeker = gameObject.transform;
    //            //pathFinding.target = CauldronShop.transform;
    //            if (DayNightCycle.gameTimer < 17)
    //            {
    //                pathFinding.speed = speed;
    //                pathFinding.destroy = true;
    //                pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.tiles[new Vector2(25, 43)]);
    //                    //grid.GetTileAtPosition(grid.TilePosition(CauldronShop.transform.position)));
    //                pathFinding.StartCoroutine("FollowPath", 0);
    //            }
    //            else if (DayNightCycle.gameTimer > 37)
    //            {
    //                pathFinding.speed = speed;
    //                //pathFinding.destroy = true;
    //                pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(CauldronShop.transform.position)), grid.GetTileAtPosition(new Vector2(3f,4f)));
    //                pathFinding.StartCoroutine("FollowPath", 74);
    //            }
    //            //pathFinding.StartCoroutine("FollowPath", 0);
    //            break;
    //        case "Cauldron Shop":
    //            //start = grid.GetTileAtPosition(grid.TilePosition(new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.25f)));
    //            //target = grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Checkout").transform.position));
    //            //FindPath(start, target);
    //            //Debug.Log(DayNightCycle.gameTimer * 2);
    //            //StartCoroutine("FollowPath", 34);
    //            //pathFinding.seeker = gameObject.transform;
    //            //pathFinding.target = GameObject.Find("Checkout").transform;
    //            if (DayNightCycle.gameTimer >= 17 && DayNightCycle.gameTimer <= 30)
    //            {
    //                pathFinding.speed = speed;
    //                pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Checkout").transform.position)));
    //                pathFinding.StartCoroutine("FollowPath", 34);
    //            }
    //            else if (DayNightCycle.gameTimer > 30)
    //            {
    //                pathFinding.destroy = true;
    //                pathFinding.speed = speed;
    //                pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Transition").transform.position)));
    //                pathFinding.StartCoroutine("FollowPath", 60);
    //            }
    //            break;
    //        default:
    //            print("Incorrect intelligence level.");
    //            break;
    //    }

    //}

    //public void LeaveShop()
    //{
    //    //pathFinding.destroy = true;
    //    pathFinding.speed = speed;
    //    pathFinding.FindPath(grid.GetTileAtPosition(grid.TilePosition(this.transform.position)), grid.GetTileAtPosition(grid.TilePosition(GameObject.Find("Transition").transform.position)));
    //    pathFinding.StartCoroutine("FollowPath", 60);
    //}
}
