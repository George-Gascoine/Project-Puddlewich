using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class George: MonoBehaviour 
{
    public Location currentLocation;
    public Tile currentTile;
    public TextAsset routineData;
    public List<Routine> winterRoutine;
    public PathFinding pathFinding;
    public int speed;
    Grid2D grid;
    private void Start()
    {
        speed = 1;
        grid = FindObjectOfType<Grid2D>();
        pathFinding = GetComponent<PathFinding>();
        winterRoutine = Routine.FromJson(routineData.text, "winter");
        CheckRoutine();
    }

    public void CheckRoutine()
    {
        Debug.Log(DayNightCycle.season);
        switch (DayNightCycle.season)
        {
            case "winter":
                foreach (var routine in winterRoutine)
                {
                    if (DayNightCycle.gameTimer == routine.startTime)
                    {
                        Debug.Log("Routine");
                        pathFinding.speed = speed;
                        //pathFinding.destroy = true;
                        pathFinding.FindPath(grid.tiles[new Vector2(5, 0)], grid.tiles[routine.target]);
                        pathFinding.StartCoroutine("FollowPath", routine.startTime * 2);
                    }
                }
                break;
        }
    }
}
